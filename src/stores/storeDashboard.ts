import { defineStore } from 'pinia'

export const useDashboardStore = defineStore('dashboard', {
  state: () => ({
    sideBarOpen: true as boolean,
  }),
  actions: {
    toggleSideBar() {
      this.sideBarOpen = !this.sideBarOpen
    },
  },
})
