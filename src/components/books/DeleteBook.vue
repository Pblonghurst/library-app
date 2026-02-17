<template>
  <DialogBasic v-model:visible="bookStore.deleteBookDialog" title="Delete Book">
    <span class="text-surface-500 dark:text-surface-400 block mb-8"
      >Are you sure you want to delete this book?</span
    >
    <div class="flex justify-end gap-2">
      <Button
        type="button"
        label="Cancel"
        severity="secondary"
        :disabled="bookStore.loading"
        @click="bookStore.deleteBookDialog = false"
      />
      <Button
        type="button"
        label="Yes, Delete"
        severity="danger"
        :loading="bookStore.loading"
        :disabled="bookStore.selectedBookId == null || bookStore.loading"
        @click="handleDelete"
      />
    </div>
  </DialogBasic>
</template>

<script setup lang="ts">
import Button from 'primevue/button'
import DialogBasic from '../ui/DialogBasic.vue'
import { useBookStore } from '../../stores/storeBooks'

const bookStore = useBookStore()

async function handleDelete() {
  if (bookStore.selectedBookId == null) return
  await bookStore.deleteBook(bookStore.selectedBookId)
}
</script>
