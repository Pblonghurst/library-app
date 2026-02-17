<template>
  <InputText
    v-model="searchInput"
    type="text"
    placeholder="Search by title or author..."
    class="bg-white text-black p-2 rounded-xl border border-gray-200 w-full"
  />
</template>
<script setup lang="ts">
import { ref, watch } from 'vue'
import InputText from 'primevue/inputtext'
import { useBookStore } from '@/stores/storeBooks'

const bookStore = useBookStore()
const searchInput = ref(bookStore.searchQuery)

let debounceTimer: ReturnType<typeof setTimeout> | null = null
watch(searchInput, (value) => {
  if (debounceTimer) clearTimeout(debounceTimer)
  debounceTimer = setTimeout(() => {
    bookStore.setSearch(value)
    debounceTimer = null
  }, 300)
})
</script>
