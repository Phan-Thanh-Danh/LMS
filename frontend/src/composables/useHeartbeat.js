import { onMounted, onUnmounted } from 'vue'
import { useEnrolledCoursesStore } from '@/stores/enrolledCourses'

/**
 * useHeartbeat Composable
 * Handles periodic pings to the "server" (mock for now) 
 * to update student streak and persist video progress.
 */
export function useHeartbeat(options = {}) {
    const { 
        courseId = null, 
        lessonId = null, 
        interval = 10000, // 10s as per workflow
        type = 'general' // 'general' (dashboard) or 'learning' (video player)
    } = options

    const store = useEnrolledCoursesStore()
    let heartbeatId = null

    const sendPing = () => {
        console.log(`[Heartbeat] Ping sent for ${type}...`)
        
        if (type === 'learning' && courseId) {
            // Persist progress to store/mock
            const course = store.getCourseById(courseId)
            if (course) {
                // Update last accessed time
                course.lastAccessed = new Date()
                console.log(`[Heartbeat] Persisted progress for course ${courseId}, lesson ${lessonId}`)
                // In a real app: await api.patch(`/api/student/progress/${courseId}`, { last_lesson: lessonId })
            }
        } else {
            // General streak heartbeat
            console.log(`[Heartbeat] Streak updated.`)
            // In a real app: await api.post('/api/student/heartbeat')
        }
    }

    onMounted(() => {
        // Initial ping
        sendPing()
        // Periodic pings
        heartbeatId = setInterval(sendPing, interval)
    })

    onUnmounted(() => {
        if (heartbeatId) {
            clearInterval(heartbeatId)
            console.log(`[Heartbeat] Stopped heartbeat for ${type}`)
        }
    })

    return {
        sendPing
    }
}
