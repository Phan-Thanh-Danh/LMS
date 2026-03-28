import { defineStore } from 'pinia'
import { ref } from 'vue'
import api from '@/services/axios'

export const useAuthStore = defineStore('auth', () => {
  const user = ref(JSON.parse(localStorage.getItem('user')) || null)
  const isAuthenticated = ref(!!user.value)
  const pendingRegistrationEmail = ref('')
  const pendingAction = ref('register') // 'register' or 'forgot-password'
  const pendingOtpCode = ref('')

  const login = async (email, password) => {
    try {
      const response = await api.post('/Auth/login', { email, password })
      // LoginResponse from backend contains: { token, refreshToken, user: { id, email, hoTen, roles } }
      const userData = {
        token: response.data.token,
        refreshToken: response.data.refreshToken,
        id: response.data.user.id,
        email: response.data.user.email,
        name: response.data.user.hoTen,
        roles: response.data.user.roles,
        // Fake avatar fallback if not provided
        avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDEidDb1VqdaeEskLwwEHnq8KXXg2Q5DCyVyAvmJ8UKDM9WPNHy3LVwV7ixaySFtdHLAeYLEkawECHzLkR37GSe4wYv00suwzkFMQ5rjb61H2nipFZ5pTrvlO3Xk-jiJszR1Bp9daAEYO3fbefIUa1AawZN-0aISC4Nop3F9NZXuK-6GVvdxDetV_SZa1XuLHQFSPCbaTu7mEthClMyRjvoIeqP8Ast-sWbIxckQJEYYePYJ_2l-VE98SVdLNgqVo5jpPw7xpVHaQ4'
      }
      user.value = userData
      isAuthenticated.value = true
      localStorage.setItem('user', JSON.stringify(userData))
      return { success: true }
    } catch (error) {
      console.error('Login error:', error)
      return { 
        success: false, 
        message: error.response?.data?.message || 'Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin.' 
      }
    }
  }

  const register = async (userData) => {
    try {
      // payload expects: email, password, hoTen, role
      const response = await api.post('/Auth/register', userData)
      // Save email to state so VerifyOTP can use it
      pendingRegistrationEmail.value = response.data.email || userData.email
      pendingAction.value = 'register'
      return { success: true, data: response.data }
    } catch (error) {
      console.error('Registration error:', error)
      return { 
        success: false, 
        message: error.response?.data?.message || 'Đăng ký thất bại. Vui lòng thử lại.' 
      }
    }
  }

  const logout = () => {
    user.value = null
    isAuthenticated.value = false
    localStorage.removeItem('user')
  }

  const forgotPassword = async (email) => {
    try {
      const response = await api.post('/Auth/forgot-password', { email })
      pendingRegistrationEmail.value = email
      pendingAction.value = 'forgot-password'
      return { success: true, message: response.data?.message }
    } catch (error) {
      return { success: false, message: error.response?.data?.message || 'Có lỗi xảy ra khi gửi yêu cầu' }
    }
  }

  const verifyEmail = async (code) => {
    try {
      await api.post('/Auth/verify-email', { 
        email: pendingRegistrationEmail.value, 
        code 
      })
      return { success: true }
    } catch (error) {
      return { success: false, message: error.response?.data?.message || 'Mã xác thực không chính xác' }
    }
  }

  const resetPassword = async (newPassword) => {
    try {
      const response = await api.post('/Auth/reset-password', { 
        email: pendingRegistrationEmail.value, 
        code: pendingOtpCode.value, 
        newPassword 
      })
      return { success: true, message: response.data?.message }
    } catch (error) {
      return { success: false, message: error.response?.data?.message || 'Không thể đặt lại mật khẩu' }
    }
  }

  const resendOtp = async () => {
    try {
      const type = pendingAction.value === 'register' ? 'EmailVerify' : 'PasswordReset'
      await api.post(`/Auth/resend-otp?email=${pendingRegistrationEmail.value}&type=${type}`)
      return { success: true }
    } catch (error) {
      return { success: false, message: 'Lỗi khi gửi lại OTP' }
    }
  }

  return { 
    user, isAuthenticated, pendingRegistrationEmail, pendingAction, pendingOtpCode, 
    login, register, logout, forgotPassword, verifyEmail, resetPassword, resendOtp 
  }
})
