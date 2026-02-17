<template>
  <nav class="bg-white w-full flex justify-between items-center h-[70px] border-b border-gray-200">
    <div class="flex items-center gap-4 ml-8">
      <ButtonIcon
        icon="pi pi-bars"
        ariaLabel="Menu"
        color="white"
        @click="dashboardStore.toggleSideBar"
      />
      <h2>Dashboard</h2>
    </div>
    <div class="flex gap-2 items-center mr-2">
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
