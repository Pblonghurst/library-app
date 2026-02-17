<template>
  <DialogBasic v-model:visible="bookStore.viewBookDialog" title="Book details">
    <template v-if="bookStore.selectedBook">
      <div class="flex flex-col gap-6">
        <!-- header: title + icon -->
        <div class="flex items-start gap-4">
          <div
            class="flex items-center justify-center bg-blue-500 rounded-lg p-3 h-16 w-16 shrink-0"
          >
            <i class="pi pi-book text-white text-2xl" aria-hidden="true"></i>
          </div>
          <div class="flex flex-col gap-1 min-w-0">
            <h2>{{ bookStore.selectedBook.title }}</h2>
            <p>{{ bookStore.selectedBook.author }}</p>
          </div>
        </div>

        <!-- details -->
        <div class="flex flex-col gap-3 border-t border-surface-200 pt-4">
          <!-- rating -->
          <div class="flex items-center gap-2">
            <span class="shrink-0 font-semibold">Rating:</span>
            <Rating :model-value="bookStore.selectedBook.rating" readonly class="flex items-center" />
          </div>
          <!-- isbn -->
          <div v-if="bookStore.selectedBook.isbn" class="flex gap-2">
            <span class="shrink-0 font-semibold">ISBN:</span>
            <span>{{ bookStore.selectedBook.isbn }}</span>
          </div>
          <!-- comments -->
          <div v-if="bookStore.selectedBook.comments" class="flex flex-col gap-1">
            <span class="font-semibold">Comments:</span>
            <p class="whitespace-pre-wrap rounded-md bg-surface-100 py-2 text-sm">
              {{ bookStore.selectedBook.comments }}
            </p>
          </div>
          <p v-else class="text-sm italic">No notes for this book.</p>
        </div>

        <div class="flex justify-end gap-2 border-t border-surface-200 pt-4">
          <Button
            type="button"
            label="Close"
            severity="secondary"
            :disabled="bookStore.loading"
            @click="bookStore.viewBookDialog = false"
          />
        </div>
      </div>
    </template>
  </DialogBasic>
</template>

<script setup lang="ts">
import Rating from 'primevue/rating'
import Button from 'primevue/button'
import DialogBasic from '../ui/DialogBasic.vue'
import { useBookStore } from '../../stores/storeBooks'

const bookStore = useBookStore()
</script>
