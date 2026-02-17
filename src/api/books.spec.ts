import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { addBook } from './books'
import type { BookCreate } from '@/types/book'

describe('books API', () => {
  const originalFetch = globalThis.fetch

  beforeEach(() => {
    vi.stubGlobal(
      'fetch',
      vi.fn((url: string, options?: RequestInit) => Promise.resolve(new Response('', { status: 200 }))),
    )
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('addBook throws with backend message when response is 400', async () => {
    vi.mocked(fetch).mockResolvedValueOnce(
      new Response(JSON.stringify({ message: 'Validation failed.', errors: ['Rating must be between 1 and 5.'] }), {
        status: 400,
      }),
    )
    const book: BookCreate = {
      title: 'Test',
      author: 'Author',
      rating: 10,
      isbn: '9780123456789',
    }
    await expect(addBook(book)).rejects.toThrow('Rating must be between 1 and 5')
  })
})
