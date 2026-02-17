import { createRouter, createWebHistory } from 'vue-router'
import DefaultLayout from '../layouts/default.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: DefaultLayout,
      children: [
        {
          path: '',
          name: 'home',
          component: () => import('../views/ViewHome.vue'),
        },
        {
          path: 'books',
          name: 'books',
          component: () => import('../views/ViewBooks.vue'),
        },
        {
          path: 'analytics',
          name: 'analytics',
          component: () => import('../views/ViewAnalytics.vue'),
        },
        {
          path: 'settings',
          name: 'settings',
          component: () => import('../views/ViewSettings.vue'),
        },
      ],
    },
  ],
})

export default router
