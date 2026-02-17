<template>
  <div class="bg-[#f8fafc] w-full min-h-screen flex">
    <Toast />
    <SideBar />
    <div
      class="flex-1 min-w-0 transition-[margin] duration-300 ease-in-out"
      :class="dashboardStore.sideBarOpen ? 'ml-72' : 'ml-0'"
    >
      <NavBar />
      <div class="max-w-7xl mx-auto p-12">
        <RouterView />
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { RouterView } from 'vue-router'
import Toast from 'primevue/toast'
import { useToast } from 'primevue/usetoast'
import NavBar from '../components/NavBar.vue'
import SideBar from '../components/SideBar.vue'
import { setToast } from '@/services/toast'
import { useDashboardStore } from '@/stores/storeDashboard'

const dashboardStore = useDashboardStore()
setToast(useToast())
</script>
