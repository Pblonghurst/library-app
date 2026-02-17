# UX/UI improvement ideas

Short list of changes that would improve the app’s look and feel.

---

**Whitespace and layout**

- There’s a lot of empty space, especially on large screens. Use a **max-width container** (e.g. 900–1200px) for the main content and center it so the books list and analytics don’t stretch across the whole width.
- Consider a **dedicated “Books” page** with a tighter, card-based layout so the list feels like a real library view instead of a single wide column.

---

**Books list**

- Add an **empty state** when there are no books: illustration or icon, short message, and a primary “Add your first book” button.
- Give **book cards** a light hover state (e.g. shadow or border) so it’s clear they’re interactive.

---

**Sidebar and navigation**

- Make the **active route** more obvious (e.g. stronger background or label weight) in addition to the current accent bar.

---

**Polish**

- Add **skeleton loaders** on Books and Analytics while data loads instead of (or with) the spinner for a calmer loading experience.
- Keep **spacing and type scale** consistent (e.g. one spacing scale and one set of heading sizes) across Books, Analytics, and Home.
