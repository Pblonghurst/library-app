import { defineStore } from 'pinia'
import type { Book, BookCreate, BookUpdate } from '@/types/book'
import {
  fetchBooks as apiFetchBooks,
  addBook as apiAddBook,
  updateBook as apiUpdateBook,
  deleteBook as apiDeleteBook,
} from '@/api/books'
import { getToast } from '@/services/toast'

export const useBookStore = defineStore('book', {
  state: () => ({
    books: [] as Book[],
    totalCount: 0,
    currentPage: 1,
    pageSize: 10,
    searchQuery: '',
    sortBy: '', // '' | 'title' | 'author' | 'isbn' | 'rating'
    addBookDialog: false,
    deleteBookDialog: false,
    editBookDialog: false,
    viewBookDialog: false,
    selectedBookId: null as number | null,
    loading: false,
    error: null as string | null,
  }),

  getters: {
    selectedBook(state): Book | null {
      if (state.selectedBookId == null) return null
      return state.books.find((b) => b.id === state.selectedBookId) ?? null
    },
  },

  actions: {
    // open add dialog
    openAddDialog() {
      this.addBookDialog = true
    },

    // open edit dialog
    openEditDialog(id: number) {
      this.selectedBookId = id
      this.editBookDialog = true
    },

    // open delete dialog
    openDeleteDialog(id: number) {
      this.selectedBookId = id
      this.deleteBookDialog = true
    },

    // open view dialog
    openViewDialog(id: number) {
      this.selectedBookId = id
      this.viewBookDialog = true
    },

    // load books (optional page, defaults to current; uses searchQuery for search)
    async loadBooks(page?: number) {
      const p = page ?? this.currentPage
      this.currentPage = p
      this.loading = true
      this.error = null
      try {
        const { items, total } = await apiFetchBooks(
          p,
          this.pageSize,
          this.searchQuery || undefined,
          this.sortBy || undefined,
        )
        this.books = items
        this.totalCount = total
      } catch (e) {
        this.error = e instanceof Error ? e.message : 'Failed to load books'
        getToast()?.add({
          severity: 'error',
          summary: 'Error',
          detail: this.error,
          life: 5000,
        })
      } finally {
        this.loading = false
      }
    },

    // set search and reload first page
    setSearch(query: string) {
      this.searchQuery = query
      this.loadBooks(1)
    },

    // set sort and reload first page
    setSortBy(sortBy: string) {
      this.sortBy = sortBy
      this.loadBooks(1)
    },

    // add book (navigate to last page where new book appears)
    async addBook(book: BookCreate) {
      this.loading = true
      this.error = null
      try {
        const created = await apiAddBook(book)
        this.totalCount += 1
        const lastPage = Math.max(1, Math.ceil(this.totalCount / this.pageSize))
        await this.loadBooks(lastPage)
        getToast()?.add({
          severity: 'success',
          summary: 'Book added',
          detail: 'The book has been added to your collection.',
          life: 4000,
        })
        return created
      } catch (e) {
        this.error = e instanceof Error ? e.message : 'Failed to add book'
        getToast()?.add({
          severity: 'error',
          summary: 'Add failed',
          detail: this.error,
          life: 5000,
        })
        throw e
      } finally {
        this.loading = false
      }
    },

    // update book
    async updateBook(book: BookUpdate) {
      this.loading = true
      this.error = null
      try {
        const updated = await apiUpdateBook(book)
        const idx = this.books.findIndex((b) => b.id === updated.id)
        if (idx !== -1) this.books[idx] = updated
        this.editBookDialog = false
        this.selectedBookId = null
        getToast()?.add({
          severity: 'success',
          summary: 'Book updated',
          detail: 'Rating and notes have been saved.',
          life: 4000,
        })
        return updated
      } catch (e) {
        this.error = e instanceof Error ? e.message : 'Failed to update book'
        getToast()?.add({
          severity: 'error',
          summary: 'Update failed',
          detail: this.error,
          life: 5000,
        })
        throw e
      } finally {
        this.loading = false
      }
    },

    // delete book (refresh current page in case we're left with empty page)
    async deleteBook(id: number) {
      this.loading = true
      this.error = null
      try {
        await apiDeleteBook(id)
        this.totalCount = Math.max(0, this.totalCount - 1)
        this.books = this.books.filter((b) => b.id !== id)
        this.deleteBookDialog = false
        this.selectedBookId = null
        getToast()?.add({
          severity: 'success',
          summary: 'Book deleted',
          detail: 'The book has been removed from your collection.',
          life: 4000,
        })
        if (this.books.length === 0 && this.currentPage > 1) {
          await this.loadBooks(this.currentPage - 1)
        }
      } catch (e) {
        this.error = e instanceof Error ? e.message : 'Failed to delete book'
        getToast()?.add({
          severity: 'error',
          summary: 'Delete failed',
          detail: this.error,
          life: 5000,
        })
        throw e
      } finally {
        this.loading = false
      }
    },
  },
})
