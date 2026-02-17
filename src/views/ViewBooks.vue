<template>
  <main class="p-8 flex flex-col gap-10">
    <!-- title -->
    <div class="flex justify-between items-start">
      <div class="flex flex-col gap-3">
        <h1>My Books</h1>
        <p class="text-gray-500">Manage your book collections and discover new reads.</p>
      </div>
      <!-- add book button -->
      <Button
        label="Add Book"
        icon="pi pi-plus"
        severity="primary"
        :disabled="bookStore.loading"
        @click="bookStore.openAddDialog"
        class="py-2! px-6!"
      />
      <!-- dialogs -->
      <AddBook v-if="bookStore.addBookDialog" />
      <EditBook v-if="bookStore.editBookDialog" />
      <ViewBook v-if="bookStore.viewBookDialog" />
      <DeleteBook v-if="bookStore.deleteBookDialog" />
    </div>

    <!-- Filters and views -->
    <BookOptions />

    <!-- x of y books -->
    <div class="text-sm text-gray-500">
      {{ bookStore.books.length }} of {{ bookStore.totalCount }} books
    </div>
    <!-- books list -->
    <div class="flex flex-col gap-4 items-center justify-center">
      <Spinner v-if="bookStore.loading && !bookStore.books.length" class="mt-24" />
      <p v-else-if="bookStore.error" class="text-red-600">{{ bookStore.error }}</p>
      <template v-else>
        <ItemBook v-for="book in bookStore.books" :key="book.id" :book="book" />
      </template>
      <!-- paginator -->
      <Paginator
        v-if="bookStore.totalCount > 0 && !bookStore.loading"
        :rows="bookStore.pageSize"
        :total-records="bookStore.totalCount"
        :first="(bookStore.currentPage - 1) * bookStore.pageSize"
        template="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport"
        @page="onPage"
        class="mt-6"
      />
    </div>
  </main>
</template>
<script setup lang="ts">
import { onMounted } from 'vue'
import BookOptions from '../components/views/books/BookOptions.vue'
import ItemBook from '../components/views/books/ItemBook.vue'
import Paginator from 'primevue/paginator'
import AddBook from '../components/books/AddBook.vue'
import EditBook from '../components/books/EditBook.vue'
import ViewBook from '../components/books/ViewBook.vue'
import DeleteBook from '../components/books/DeleteBook.vue'
import Button from 'primevue/button'
import Spinner from '../components/ui/Spinner.vue'
import { useBookStore } from '../stores/storeBooks'

const bookStore = useBookStore()

onMounted(() => {
  bookStore.loadBooks()
})

function onPage(event: { page: number }) {
  bookStore.loadBooks(event.page + 1)
}
</script>
