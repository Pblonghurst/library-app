<template>
  <nav class="bg-white w-full flex justify-between items-center h-[60px] sm:h-[70px] border-b border-gray-200 px-4 sm:px-8">
    <div class="flex items-center gap-2 sm:gap-4">
      <ButtonIcon
        icon="pi pi-bars"
        ariaLabel="Menu"
        color="white"
        @click="dashboardStore.toggleSideBar"
      />
      <h2 class="text-base sm:text-xl">Dashboard</h2>
    </div>
    <div class="flex gap-1 sm:gap-2 items-center">
      <ButtonIcon icon="pi pi-bell" ariaLabel="Notifications" color="white" />
      <ButtonIcon icon="pi pi-search" ariaLabel="Search" color="white" />
    </div>
  </nav>
</template>
<script setup lang="ts">
import ButtonIcon from './ui/ButtonIcon.vue'
import { useDashboardStore } from '@/stores/storeDashboard'

const dashboardStore = useDashboardStore()
</script>
