// src/mocks/db.js

export const db = {
    currentUser: {
        id: 1,
        name: "Trần Tấn Lộc",
        email: "loc.tran@aet.edu.vn",
        avatar: "https://i.pravatar.cc/150?u=loc.tran",
        stats: {
            totalHours: 45,
            completedLessons: 12,
            currentStreak: 7,
            totalPoints: 1250
        }
    },

    myCourses: [
        {
            id: 101,
            title: "Lập trình Vue 3 Composition API từ cơ bản đến nâng cao",
            instructor: "Phan Thanh Danh",
            thumbnail: "https://img-c.udemycdn.com/course/240x135/4461344_ce4a_6.jpg",
            progress: 85,
            status: "in_progress",
            lastAccessedLessonId: 12,
            category: "Phát triển Web"
        },
        {
            id: 102,
            title: "Thiết kế UI/UX chuyên nghiệp với Figma",
            instructor: "Nguyễn Văn A",
            thumbnail: "https://img-c.udemycdn.com/course/240x135/1255072_97ef_9.jpg",
            progress: 100,
            status: "completed",
            lastAccessedLessonId: 45,
            category: "Thiết kế"
        },
        {
            id: 103,
            title: "Mastering Python for Data Science",
            instructor: "Dr. Smith",
            thumbnail: "https://img-c.udemycdn.com/course/240x135/567828_adb1_20.jpg",
            progress: 0,
            status: "not_started",
            lastAccessedLessonId: null,
            category: "Khoa học Dữ liệu"
        }
    ],

    courseDetails: {
        101: {
            id: 101,
            title: "Lập trình Vue 3 Composition API",
            description: "Khóa học chuyên sâu về Vue 3, Vite, Pinia và thiết kế Component chuẩn Enterprise.",
            chapters: [
                {
                    id: 1,
                    title: "Chương 1: Giới thiệu & Setup môi trường",
                    lessons: [
                        { id: 1, title: "1. Tại sao chọn Vue 3?", type: "video", duration: "10:00", status: "completed" },
                        { id: 2, title: "2. Cài đặt Node.js & Vite", type: "video", duration: "15:00", status: "completed" }
                    ]
                },
                {
                    id: 2,
                    title: "Chương 2: Kiến thức cốt lõi",
                    lessons: [
                        { id: 11, title: "11. Reactivity API (ref vs reactive)", type: "video", duration: "25:00", status: "completed" },
                        { 
                            id: 12, 
                            title: "12. Xây dựng Custom Composables", 
                            type: "video", 
                            duration: "30:00", 
                            status: "current",
                            videoUrl: "https://www.w3schools.com/html/mov_bbb.mp4" 
                        },
                        { 
                            id: 13, 
                            title: "13. Bài kiểm tra cuối chương: Logic & State", 
                            type: "quiz", 
                            duration: "15:00", 
                            status: "locked",
                            quizId: 501 
                        }
                    ]
                }
            ]
        }
    },

    quizzes: {
        501: {
            id: 501,
            title: "Kiểm tra kiến thức Vue 3 Reactivity",
            timeLimit: 900, // 15 mins
            passingScore: 80,
            questions: [
                {
                    id: 1,
                    text: "Sự khác biệt chính giữa ref và reactive là gì?",
                    options: [
                        "Ref dùng cho primitive, Reactive dùng cho object",
                        "Reactive chạy nhanh hơn ref",
                        "Ref chỉ dùng được trong template",
                        "Không có sự khác biệt"
                    ],
                    correctAnswer: 0
                },
                {
                    id: 2,
                    text: "Để theo dõi sự thay đổi của một ref, ta sử dụng hàm nào?",
                    options: ["onMounted", "watch", "computed", "useEffect"],
                    correctAnswer: 1
                },
                {
                    id: 3,
                    text: "Linh hoạt nhất trong việc tái sử dụng logic trong Vue 3 là gì?",
                    options: ["Mixins", "Scoped Slots", "Composables", "Options API"],
                    correctAnswer: 2
                }
            ]
        }
    },

    certificates: {
        2001: {
            id: 2001,
            courseId: 102,
            courseTitle: "Thiết kế UI/UX chuyên nghiệp với Figma",
            studentName: "Trần Tấn Lộc",
            issueDate: "March 20, 2026",
            verificationCode: "AET-Figma-8899",
            instructor: "Nguyễn Văn A"
        }
    }
}
