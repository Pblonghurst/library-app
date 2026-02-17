<template>
  <div
    class="bg-white p-4 rounded-xl flex flex-col min-[1200px]:flex-row justify-between items-start min-[1200px]:items-center gap-4 border border-gray-200 w-full"
  >
    <!-- Book info -->
    <div class="flex items-center gap-3 min-[1200px]:gap-4 w-full min-[1200px]:w-auto">
      <div
        class="flex items-center justify-center rounded-md p-2 h-16 w-12 min-[1200px]:h-20 min-[1200px]:w-15 shrink-0"
        style="background: var(--app-gradient)"
      >
        <i class="pi pi-book text-white" style="font-size: 24px" aria-hidden="true"></i>
      </div>
      <div class="flex flex-col gap-1 min-w-0">
        <div class="flex flex-col gap-0">
          <h3 class="truncate">{{ book.title }}</h3>
          <p class="text-gray-500 text-sm min-[1200px]:text-base truncate">{{ book.author }}</p>
        </div>
        <div class="flex flex-wrap gap-2 text-xs min-[1200px]:text-sm">
          <p v-if="book.isbn" class="text-gray-500">ISBN: {{ book.isbn }}</p>
          <div v-if="book.comments" class="flex items-center gap-1">
            <i class="pi pi-comment text-green-600" aria-hidden="true"></i>
            <p class="text-green-600">Has Notes</p>
          </div>
        </div>
      </div>
    </div>

    <!-- right -->
    <div class="flex items-center justify-between min-[1200px]:justify-end gap-4 w-full min-[1200px]:w-auto">
      <!-- rating -->
      <div class="flex items-center gap-2">
        <Rating :model-value="book.rating" readonly class="flex items-center" />
        <p class="text-gray-500 text-sm">{{ book.rating }}/5</p>
      </div>

      <!-- actions -->
      <div class="flex items-center gap-1 min-[1200px]:gap-2">
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
