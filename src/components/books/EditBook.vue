<template>
  <DialogBasic v-model:visible="bookStore.editBookDialog" title="Edit Book">
    <template v-if="bookStore.selectedBook">
      <span class="text-surface-500 dark:text-surface-400 block mb-4"
        >Edit rating and comments for {{ bookStore.selectedBook.title }}.</span
      >
      <!-- rating -->
      <div class="flex gap-2 mb-4 w-full">
        <label for="edit-rating" class="font-semibold">Rating:</label>
        <Rating id="edit-rating" v-model="rating" :cancel="false" class="flex items-center" />
      </div>
      <!-- comments -->
      <div class="flex gap-4 mb-4 flex-col w-full">
        <label for="edit-comments" class="font-semibold w-24">Comments:</label>
        <InputText
          id="edit-comments"
          v-model="comments"
          class="flex-auto"
          autocomplete="off"
          placeholder="Add or edit notes..."
        />
      </div>

      <div class="flex justify-end gap-2">
        <Button
          type="button"
          label="Cancel"
          severity="secondary"
          :disabled="bookStore.loading"
          @click="bookStore.editBookDialog = false"
        />
        <Button
          type="button"
          label="Save"
          :loading="bookStore.loading"
          :disabled="bookStore.loading"
          @click="handleSave"
        />
      </div>
    </template>
  </DialogBasic>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import InputText from 'primevue/inputtext'
import Rating from 'primevue/rating'
import Button from 'primevue/button'
import DialogBasic from '../ui/DialogBasic.vue'
import { useBookStore } from '../../stores/storeBooks'

const bookStore = useBookStore()

const rating = ref(0)
const comments = ref('')

watch(
  () => bookStore.selectedBook,
  (book) => {
    if (book) {
      rating.value = book.rating
      comments.value = book.comments ?? ''
    }
  },
  { immediate: true },
)

async function handleSave() {
  if (bookStore.selectedBook == null) return
  await bookStore.updateBook({
    id: bookStore.selectedBook.id,
    rating: rating.value,
    comments: comments.value || null,
  })
}
</script>
