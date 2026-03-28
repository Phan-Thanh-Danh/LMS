// src/services/api.js
import { db } from '../mocks/db'

// Helper: Simulate Network Latency
const delay = (ms = 800) => new Promise(resolve => setTimeout(resolve, ms))

export const apiService = {
    // STU-01: Dashboard Data
    async getDashboardData() {
        await delay()
        return {
            user: db.currentUser,
            recommendations: db.myCourses.slice(0, 3)
        }
    },

    // STU-02: My Courses with filtering
    async getMyCourses(filter = 'all') {
        await delay()
        if (filter === 'all') return db.myCourses
        return db.myCourses.filter(c => c.status === filter)
    },

    // STU-03 & STU-04: Lesson Details & Progress
    async getLessonDetail(courseId, lessonId) {
        await delay(500)
        const course = db.courseDetails[courseId]
        if (!course) throw new Error("Course not found")
        
        let lesson = null
        course.chapters.forEach(ch => {
            const found = ch.lessons.find(l => l.id === parseInt(lessonId))
            if (found) lesson = found
        })
        
        return {
            courseTitle: course.title,
            chapters: course.chapters,
            currentLesson: lesson
        }
    },

    // STU-05: Quiz Data
    async getQuizData(quizId) {
        await delay(1000)
        const quiz = db.quizzes[quizId]
        if (!quiz) throw new Error("Quiz not found")
        
        // Return quiz WITHOUT answers for security (simulation)
        const clientQuiz = {
            ...quiz,
            questions: quiz.questions.map(q => ({
                id: q.id,
                text: q.text,
                options: q.options
            }))
        }
        return clientQuiz
    },

    // STU-05: Submit Quiz
    async submitQuiz(quizId, answers) {
        await delay(1200)
        const quiz = db.quizzes[quizId]
        if (!quiz) throw new Error("Quiz not found")

        let score = 0
        quiz.questions.forEach((q, index) => {
            if (answers[index] === q.correctAnswer) {
                score += (100 / quiz.questions.length)
            }
        })

        const passed = score >= quiz.passingScore
        return {
            score: Math.round(score),
            passed,
            message: passed ? "Chúc mừng! Bạn đã vượt qua bài thi." : "Rất tiếc, bạn cần ôn tập thêm."
        }
    },

    // STU-06: Certificate
    async getCertificate(courseId) {
        await delay()
        // Find cert by courseId
        const certId = Object.keys(db.certificates).find(id => db.certificates[id].courseId === parseInt(courseId))
        return db.certificates[certId] || null
    }
}
