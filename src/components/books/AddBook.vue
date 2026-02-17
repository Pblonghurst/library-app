<template>
  <DialogBasic v-model:visible="bookStore.addBookDialog" title="Add Book">
    <span class="text-surface-500 dark:text-surface-400 block mb-8"
      >Add a new book to your collection.</span
    >
    <!-- title -->
    <div class="flex gap-2 mb-4 flex-col w-full">
      <label for="book-title" class="font-semibold w-24">title</label>
      <InputText id="book-title" v-model="title" class="flex-auto" autocomplete="off" />
    </div>
    <!-- author -->
    <div class="flex gap-2 mb-4 flex-col w-full">
      <label for="book-author" class="font-semibold w-24">author</label>
      <InputText id="book-author" v-model="author" class="flex-auto" autocomplete="off" />
    </div>
    <!-- rating -->
    <div class="flex gap-2 mb-4 flex-col w-full">
      <label for="book-rating" class="font-semibold w-24">rating</label>
      <InputNumber id="book-rating" v-model="rating" :min="1" :max="5" class="flex-auto" />
    </div>
    <!-- comments -->
    <div class="flex gap-2 mb-4 flex-col w-full">
      <label for="book-comments" class="font-semibold w-24">comments</label>
      <InputText id="book-comments" v-model="comments" class="flex-auto" autocomplete="off" />
    </div>
    <!-- isbn -->
    <div class="flex gap-2 mb-8 flex-col w-full">
      <label for="book-isbn" class="font-semibold w-24">ISBN</label>
      <InputText id="book-isbn" v-model="isbn" class="flex-auto" autocomplete="off" />
    </div>

    <div class="flex justify-end gap-2">
      <Button
        type="button"
        label="Cancel"
        severity="secondary"
        :disabled="bookStore.loading"
        @click="bookStore.addBookDialog = false"
      />
      <Button
        type="button"
        label="Save"
        :loading="bookStore.loading"
        :disabled="bookStore.loading"
        @click="onSave"
      />
    </div>
  </DialogBasic>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import Button from 'primevue/button'
import DialogBasic from '../ui/DialogBasic.vue'
import { useBookStore } from '../../stores/storeBooks'

const bookStore = useBookStore()

const title = ref('')
const author = ref('')
const rating = ref<number>(5)
const comments = ref('')
const isbn = ref('')

function resetForm() {
  title.value = ''
  author.value = ''
  rating.value = 5
  comments.value = ''
  isbn.value = ''
}

async function onSave() {
  if (!title.value.trim() || !author.value.trim()) return
  await bookStore.addBook({
    title: title.value.trim(),
    author: author.value.trim(),
    rating: rating.value,
    comments: comments.value.trim() || undefined,
    isbn: isbn.value.trim(),
  })
  bookStore.addBookDialog = false
  resetForm()
}
</script>
