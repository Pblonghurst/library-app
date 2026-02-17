<template>
  <!-- Backdrop for mobile -->
  <div
    v-if="dashboardStore.sideBarOpen"
    class="fixed inset-0 bg-black/50 z-10 md:hidden"
    @click="dashboardStore.toggleSideBar"
  ></div>
  <div
    class="bg-[#2563eb] text-white flex flex-col justify-between h-screen fixed overflow-hidden transition-all duration-300 ease-in-out z-20"
    :class="[
      dashboardStore.sideBarOpen
        ? 'w-72 translate-x-0'
        : 'w-72 -translate-x-full md:w-0 md:translate-x-0',
    ]"
  >
    <div class="flex flex-col">
      <!-- logo -->
      <div class="flex items-center gap-4 py-8 px-6 border-b border-white/20">
        <!-- Open book icon (two pages spread) -->
        <Logo />
        <h1>BookHub</h1>
      </div>
      <!-- links -->
      <div class="flex flex-col py-8 dashboard-links">
        <RouterLink to="/">
          <i class="pi pi-home text-white" aria-hidden="true"></i>
          Dashboard
        </RouterLink>
        <RouterLink to="/books">
          <i class="pi pi-book text-white" aria-hidden="true"></i>
          Books
        </RouterLink>
        <RouterLink to="/analytics">
          <i class="pi pi-chart-line text-white" aria-hidden="true"></i>
          Analytics
        </RouterLink>
        <RouterLink to="/settings">
          <i class="pi pi-cog text-white" aria-hidden="true"></i>
          Settings
        </RouterLink>
      </div>
    </div>

    <!-- profile -->
    <div class="flex items-center gap-4 py-8 px-4 border-t border-white/20">
      <Avatar label="BL" shape="circle" size="large" width="40" height="40" class="shrink-0" />
      <div class="flex flex-col gap-0 whitespace-nowrap">
        <h3>Book Lover</h3>
        <p>Reader</p>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import Avatar from 'primevue/avatar'
import { useDashboardStore } from '@/stores/storeDashboard'
import Logo from './ui/Logo.vue'

const dashboardStore = useDashboardStore()
</script>
