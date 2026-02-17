// book interface
export interface Book {
  id: number
  title: string
  author: string
  rating: number
  comments?: string | null
  isbn: string
}

// book create interface
export interface BookCreate {
  title: string
  author: string
  rating: number
  comments?: string | null
  isbn: string
}

// book update interface
export interface BookUpdate {
  id: number
  rating: number
  comments?: string | null
}
