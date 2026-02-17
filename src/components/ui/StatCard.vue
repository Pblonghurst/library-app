<template>
  <div class="bg-white p-4 rounded-md border border-gray-200 flex-1">
    <div class="flex flex-col gap-3">
      <p class="text-sm">{{ title }}</p>
      <p class="font-bold mt-1">{{ value }}</p>
    </div>
  </div>
</template>
<script setup lang="ts">
withDefaults(
  defineProps<{
    title: string
    value: string | number
  }>(),
  { title: '', value: '' },
)
</script>
