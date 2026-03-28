import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

// Mock data — sau này thay bằng API call
const MOCK_COURSES = [
    {
        id: 1,
        title: 'Mastering Vue 3: The Complete Composition API Guide',
        instructor: 'Maximillian Schwarzmüller',
        thumbnail: 'https://images.unsplash.com/photo-1555066931-4365d14bab8c?q=80&w=600&auto=format&fit=crop',
        progress: 65,
        totalLessons: 24,
        completedLessons: 12,
        nextLesson: '2.2 Reactivity chuyên sâu',
        status: 'enrolled',
        lastAccessed: new Date(Date.now() - 2 * 60 * 60 * 1000), // 2h ago
        tab: 'learning', // learning | completed | wishlist
    },
    {
        id: 2,
        title: 'Data Science Fundamentals: Python & SQL',
        instructor: 'Dr. Angela Yu',
        progress: 0,
        totalLessons: 45,
        completedLessons: 0,
        nextLesson: '1.1 Giới thiệu SQL',
        status: 'enrolled',
        lastAccessed: null,
        tab: 'wishlist',
    }
]

export const useEnrolledCoursesStore = defineStore('enrolledCourses', () => {
    const allCourses = ref(MOCK_COURSES)
    const activeTab = ref('learning') // 'learning' | 'completed' | 'wishlist'
    const searchQuery = ref('')
    const sortBy = ref('recent') // 'recent' | 'progress' | 'name'

    // Filtered + Sorted courses based on active tab, search, and sort
    const filteredCourses = computed(() => {
        let result = allCourses.value.filter(c => c.tab === activeTab.value)

        // Search filter
        if (searchQuery.value.trim()) {
            const q = searchQuery.value.toLowerCase()
            result = result.filter(c =>
                c.title.toLowerCase().includes(q) ||
                c.instructor.toLowerCase().includes(q)
            )
        }

        // Sort
        result = [...result].sort((a, b) => {
            if (sortBy.value === 'progress') return b.progress - a.progress
            if (sortBy.value === 'name') return a.title.localeCompare(b.title)
            // Default: recent
            if (!a.lastAccessed) return 1
            if (!b.lastAccessed) return -1
            return b.lastAccessed - a.lastAccessed
        })

        return result
    })

    // Counts for badge tabs
    const tabCounts = computed(() => ({
        learning: allCourses.value.filter(c => c.tab === 'learning').length,
        completed: allCourses.value.filter(c => c.tab === 'completed').length,
        wishlist: allCourses.value.filter(c => c.tab === 'wishlist').length,
    }))

    // Get a single course by ID
    const getCourseById = (id) => allCourses.value.find(c => c.id === Number(id))

    // Remove from wishlist
    const removeFromWishlist = (id) => {
        allCourses.value = allCourses.value.filter(c => c.id !== id)
    }

    return {
        allCourses,
        activeTab,
        searchQuery,
        sortBy,
        filteredCourses,
        tabCounts,
        getCourseById,
        removeFromWishlist,
    }
})
