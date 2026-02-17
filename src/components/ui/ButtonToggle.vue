<template>
  <div
    class="flex items-center border border-gray-300 rounded-md w-25 h-[42px] bg-white rounded-md overflow-hidden"
  >
    <button
      @click="toggleView"
      :class="{ 'bg-[var(--app-primary)] text-white': view }"
      class="w-full h-full flex-1 transition-colors"
    >
      <i class="pi pi-th-large" aria-hidden="true"></i>
    </button>
    <button
      @click="toggleView"
      :class="{ 'bg-[var(--app-primary)] text-white': !view }"
      class="w-full h-full flex-1 transition-colors"
    >
      <i class="pi pi-list" aria-hidden="true"></i>
    </button>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue'

const view = ref(false)
function toggleView() {
  view.value = !view.value
}
</script>
