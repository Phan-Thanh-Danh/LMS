import { createRouter, createWebHistory } from 'vue-router'
import LandingPage from '../modules/marketing/views/LandingPage.vue'
import { useAuthStore } from '../stores/auth'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: LandingPage
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../modules/auth/views/LoginView.vue')
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('../modules/auth/views/RegisterView.vue')
  },
  {
    path: '/forgot-password',
    name: 'ForgotPassword',
    component: () => import('../modules/auth/views/ForgotPasswordView.vue')
  },
  {
    path: '/verify-otp',
    name: 'VerifyOTP',
    component: () => import('../modules/auth/views/VerifyOTPView.vue')
  },
  {
    path: '/reset-password',
    name: 'ResetPassword',
    component: () => import('../modules/auth/views/ResetPasswordView.vue')
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: () => import('../modules/dashboard/views/DashboardView.vue'),
    meta: { requiresAuth: true }
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next('/login')
  } else {
    next()
  }
})

export default router
