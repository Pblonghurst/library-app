<template>
  <button
    type="button"
    class="p-1.5 rounded-md w-10 h-10 flex items-center justify-center hover:bg-gray-200 transition-all duration-300 disabled:opacity-50 disabled:pointer-events-none"
    :class="{
      'bg-gray-100': color === 'gray',
      'bg-white': color === 'white',
    }"
    :aria-label="ariaLabel"
    :disabled="disabled"
  >
    <i :class="icon" aria-hidden="true"></i>
  </button>
</template>
<script setup lang="ts">
withDefaults(
  defineProps<{
    icon: string
    ariaLabel?: string
    color?: 'gray' | 'white'
    disabled?: boolean
  }>(),
  { color: 'gray', disabled: false }
)
</script>
