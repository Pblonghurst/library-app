<template>
  <div
    class="bg-white p-4 rounded-xl flex justify-between items-center border border-gray-200 w-full"
  >
    <!-- Book info -->
    <div class="flex items-center gap-4">
      <div class="flex items-center justify-center bg-blue-500 rounded-md p-2 h-20 w-15">
        <i class="pi pi-book text-white text-center" aria-hidden="true"></i>
      </div>
      <div class="flex flex-col gap-1">
        <div class="flex flex-col gap-0">
          <h3>{{ book.title }}</h3>
          <p class="text-gray-500">{{ book.author }}</p>
        </div>
        <div class="flex gap-2 text-sm">
          <p v-if="book.isbn" class="text-gray-500">ISBN: {{ book.isbn }}</p>
          <div v-if="book.comments" class="flex items-center gap-1">
            <i class="pi pi-comment text-green-700" aria-hidden="true"></i>
            <p class="text-green-700">Has Notes</p>
          </div>
        </div>
      </div>
    </div>

    <!-- right -->
    <div class="flex items-center gap-4">
      <!-- rating -->
      <div class="flex items-center gap-2">
        <Rating :model-value="book.rating" readonly class="flex items-center" />
        <p class="text-gray-500">{{ book.rating }}/5</p>
      </div>

      <!-- actions -->
      <div class="flex items-center gap-2">
        <ButtonIcon
          icon="pi pi-pencil"
          ariaLabel="Edit"
          :disabled="bookStore.loading"
          @click="bookStore.openEditDialog(book.id)"
        />
        <ButtonIcon
          icon="pi pi-eye"
          ariaLabel="View"
          :disabled="bookStore.loading"
          @click="bookStore.openViewDialog(book.id)"
        />
        <ButtonIcon
          icon="pi pi-trash"
          ariaLabel="Delete"
          :disabled="bookStore.loading"
          @click="bookStore.openDeleteDialog(book.id)"
        />
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import type { Book } from '@/types/book'
import Rating from 'primevue/rating'
import ButtonIcon from '@/components/ui/ButtonIcon.vue'
import { useBookStore } from '@/stores/storeBooks'

const bookStore = useBookStore()

defineProps<{ book: Book }>()
</script>
