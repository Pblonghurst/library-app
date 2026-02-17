<template>
  <div class="flex items-center gap-4">
    <Search class="flex-1" :disabled="bookStore.loading" />
    <Select
      v-model="sortBy"
      class="bg-white text-black p-2 rounded-md"
      :options="sortOptions"
      option-label="label"
      option-value="value"
      placeholder="Sort by..."
      :disabled="bookStore.loading"
      :style="{
        width: '200px',
        height: '42px',
        display: 'flex',
        alignItems: 'center',
        fontWeight: '700',
      }"
    />
    <!-- view toggle -->
    <ButtonToggle />
  </div>
</template>
<script setup lang="ts">
import { computed } from 'vue'
import Search from '../../books/Search.vue'
import Select from 'primevue/select'
import { useBookStore } from '@/stores/storeBooks'
import { ref } from 'vue'
import ButtonToggle from '@/components/ui/ButtonToggle.vue'

const bookStore = useBookStore()

const view = ref(false)
function toggleView() {
  view.value = !view.value
}

const sortOptions = [
  { value: '', label: 'All' },
  { value: 'title', label: 'Sort by Title' },
  { value: 'author', label: 'Sort by Author' },
  { value: 'isbn', label: 'Sort by ISBN' },
  { value: 'rating', label: 'Sort by Rating' },
]

const sortBy = computed({
  get: () => bookStore.sortBy,
  set: (value: string) => bookStore.setSortBy(value),
})
</script>
