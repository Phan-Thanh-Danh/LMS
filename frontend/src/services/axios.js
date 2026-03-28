import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5029/api/v1',
  headers: {
    'Content-Type': 'application/json'
  }
})

// Request interceptor to attach JWT token if available
api.interceptors.request.use(
  (config) => {
    const userString = localStorage.getItem('user')
    if (userString) {
      try {
        const user = JSON.parse(userString)
        if (user && user.token) {
          config.headers.Authorization = `Bearer ${user.token}`
        }
      } catch (e) {
        console.error('Error parsing user from localStorage', e)
      }
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

export default api
