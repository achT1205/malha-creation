import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/HomeView.vue'),
    },
    {
      path: '/collections/:slug',
      name: 'collections',
      component: () => import('../views/CollectionsView.vue'),
    },
    {
      path: '/tests',
      name: 'tests',
      component: () => import('../views/Tests.vue'),
    }
  ],
})

export default router
