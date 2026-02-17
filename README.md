# Library App

A Vue 3 app for managing your book collection—browse, add, edit, and track what you read.

## Prerequisites

- **Node.js** `^20.19.0` or `>=22.12.0`
- **pnpm** `10.11.0` or compatible (recommended; the project uses pnpm)

## Setup

1. **Clone the repo** (if you haven’t already):

   ```bash
   git clone <repository-url>
   cd library-app
   ```

2. **Install dependencies**:

   ```bash
   pnpm install
   ```

3. **Start the dev server**:
   ```bash
   pnpm dev
   ```
   The app will be available at the URL shown in the terminal (typically `http://localhost:5173`).

## Scripts

| Command          | Description                     |
| ---------------- | ------------------------------- |
| `pnpm dev`       | Start the Vite dev server       |
| `pnpm build`     | Type-check and production build |
| `pnpm preview`   | Preview the production build    |
| `pnpm test:unit` | Run unit tests with Vitest      |
| `pnpm lint`      | Lint and fix with ESLint/Oxlint |
| `pnpm format`    | Format source with Prettier     |

## Tech stack

- **Vue 3** (Composition API) + **Vue Router** + **Pinia**
- **Vite 7** + **TypeScript**
- **Tailwind CSS 4** + **PrimeVue**
- **Vitest** for unit tests
- **Backend**: .NET LibraryApi (ASP.NET Core minimal API) at `http://localhost:5201`

## Backend API

The backend enforces: max 25 books, rating 1–5, the word "horrible" disallowed in comments, and string length limits. It returns proper HTTP status codes (400 for validation, 404 for not found) and JSON error messages.

Run the API from `LibraryApi`:

```bash
cd LibraryApi
dotnet run
```

## Testing

run commands from root

### Frontend tests

```bash
pnpm test:unit
```

### Backend tests (xUnit)

The `LibraryApi.Tests` project contains unit tests for the `BookService`:

```bash
dotnet test LibraryApi.Tests
```

Tests cover:

- Getting all books and by ID
- Adding, updating, and deleting books
- Pagination and search filtering
