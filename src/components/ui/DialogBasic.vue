<template>
  <Dialog
    v-model:visible="visibleModel"
    modal
    :header="title"
    :draggable="false"
    :style="{
      width: '100%',
      maxWidth: '500px',
      backgroundColor: 'white',
      borderRadius: '16px',
      boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.1)',
      border: '1px solid #e0e0e0',
      margin: '32px',
    }"
  >
    <slot></slot>
  </Dialog>
</template>
<script setup lang="ts">
import Dialog from 'primevue/dialog'
import { computed } from 'vue'

// props
const props = defineProps<{
  visible: boolean
  title: string
}>()

// emits
const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
}>()

// computed
const visibleModel = computed({
  get: () => props.visible,
  set: (value: boolean) => emit('update:visible', value),
})
</script>
