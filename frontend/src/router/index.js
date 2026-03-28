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
    path: '/student/dashboard',
    name: 'Dashboard',
    component: () => import('../modules/dashboard/views/DashboardView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/dashboard',
    redirect: '/student/dashboard'
  },
  {
    path: '/learning',
    name: 'MyLearning',
    component: () => import('../modules/learning/views/MyLearningView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/learning/:id/progress',
    name: 'CourseProgress',
    component: () => import('../modules/learning/views/CourseProgressView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/learn/:id',
    name: 'Learn',
    component: () => import('../modules/learning/views/LearnView.vue'),
    meta: { requiresAuth: true },
    children: [
      {
        path: ':lessonId',
        name: 'Lesson',
        component: () => import('../modules/learning/views/LearnView.vue')
      },
        {
          path: 'quiz/:quizId',
          name: 'quiz-lesson',
          component: () => import('../modules/learning/components/learn/QuizLesson.vue')
        },
      ]
    },
    {
      path: '/learning/:id/certificate',
      name: 'certificate',
      component: () => import('../modules/learning/views/CertificateView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/cart',
      name: 'Cart',
      component: () => import('../modules/cart/views/cart.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/cart-details',
      name: 'CartDetails',
      component: () => import('../modules/cart/views/cartdetails.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/profile',
      name: 'Profile',
      component: () => import('../modules/users/components/info.vue'),
      meta: { requiresAuth: true }
    },
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
