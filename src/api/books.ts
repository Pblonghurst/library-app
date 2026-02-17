import type { Book, BookCreate, BookUpdate } from '@/types/book'

const API_BASE =
  (import.meta as unknown as { env: { VITE_API_URL?: string } }).env.VITE_API_URL ??
  'http://localhost:5201'

// paged result
export interface BooksPage {
  items: Book[]
  total: number
}

// fetch books (paginated, 1-based page, optional search and sortBy: title | author | isbn | rating)
export async function fetchBooks(
  page = 1,
  pageSize = 10,
  search?: string,
  sortBy?: string
): Promise<BooksPage> {
  const params = new URLSearchParams({
    page: String(page),
    pageSize: String(pageSize),
  })
  if (search?.trim()) params.set('search', search.trim())
  if (sortBy?.trim()) params.set('sortBy', sortBy.trim())
  const res = await fetch(`${API_BASE}/api/books?${params}`)
  if (!res.ok) await errorFromResponse(res, 'Failed to fetch books')
  return res.json()
}

async function errorFromResponse(res: Response, fallback: string): Promise<never> {
  let message = fallback
  try {
    const body = (await res.json()) as { message?: string; errors?: string[] }
    if (body.message) message = body.errors?.length ? `${body.message} ${body.errors.join(' ')}` : body.message
  } catch {
    /* use fallback */
  }
  throw new Error(message)
}

// add book
export async function addBook(book: BookCreate): Promise<Book> {
  const res = await fetch(`${API_BASE}/api/books`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(book),
  })
  if (!res.ok) await errorFromResponse(res, 'Failed to add book')
  return res.json()
}

// update book
export async function updateBook(book: BookUpdate): Promise<Book> {
  const res = await fetch(`${API_BASE}/api/books/${book.id}`, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ rating: book.rating, comments: book.comments }),
  })
  if (!res.ok) await errorFromResponse(res, 'Failed to update book')
  return res.json()
}

// delete book
export async function deleteBook(id: number): Promise<void> {
  const res = await fetch(`${API_BASE}/api/books/${id}`, { method: 'DELETE' })
  if (!res.ok) await errorFromResponse(res, 'Failed to delete book')
}
