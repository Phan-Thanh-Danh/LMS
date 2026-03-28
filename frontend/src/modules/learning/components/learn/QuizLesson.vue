<script setup>
import { ref, onMounted, onUnmounted, computed } from 'vue'
import { apiService } from '@/services/api'
import { useRouter } from 'vue-router'

const props = defineProps(['courseId', 'lessonId'])
const emit = defineEmits(['next-lesson'])
const router = useRouter()

// 1. Quản lý Trạng thái (quizState)
const quizState = ref('intro')
const isCheatModalVisible = ref(false)
const cheatWarnings = ref(0)
const cheatMessage = ref('')
const antiCheatTimer = ref(10)
let timerInterval = null

// Quiz Data & Results
const quizInfo = ref(null)
const isLoading = ref(true)
const results = ref(null)

const fetchQuiz = async () => {
    try {
        isLoading.value = true
        quizInfo.value = await apiService.getQuizData(props.lessonId)
        timeLeft.value = quizInfo.value.timeLimit
    } catch (err) {
        console.error("Failed to fetch quiz", err)
    } finally {
        isLoading.value = false
    }
}

const timeLeft = ref(0)
const currentQuestionIndex = ref(0)
const answers = ref({})

const answeredCount = computed(() => Object.keys(answers.value).length)
const formattedTime = computed(() => {
    const minutes = Math.floor(timeLeft.value / 60)
    const seconds = timeLeft.value % 60
    return `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`
})

// 2. Logic Fullscreen & Anti-cheat
const startQuiz = async () => {
    try {
        const elem = document.documentElement
        if (elem.requestFullscreen) await elem.requestFullscreen()
        
        quizState.value = 'playing'
        startTimer()
        setupAntiCheat()
    } catch (err) {
        quizState.value = 'playing'
        startTimer()
        setupAntiCheat()
    }
}

const startTimer = () => {
    timerInterval = setInterval(() => {
        if (timeLeft.value > 0) timeLeft.value--
        else submitQuiz()
    }, 1000)
}

const submitQuiz = async () => {
    clearInterval(timerInterval)
    removeAntiCheat()
    
    try {
        isLoading.value = true
        results.value = await apiService.submitQuiz(props.lessonId, answers.value)
        quizState.value = 'result'
        if (document.exitFullscreen) document.exitFullscreen()
    } catch (err) {
        console.error("Submission failed", err)
    } finally {
        isLoading.value = false
    }
}

const handleFullscreenChange = () => {
    if (quizState.value === 'playing' && !document.fullscreenElement && !document.webkitFullscreenElement) {
        handleCheatAction('fullscreen')
    }
}

const handleMouseLeave = () => handleCheatAction('mouseleave')
const handleBlur = () => handleCheatAction('blur')
const handleVisibilityChange = () => {
    if (document.hidden) handleCheatAction('visibility')
}

const setupAntiCheat = () => {
    document.documentElement.addEventListener('mouseleave', handleMouseLeave)
    window.addEventListener('blur', handleBlur)
    document.addEventListener('visibilitychange', handleVisibilityChange)
}

const removeAntiCheat = () => {
    document.documentElement.removeEventListener('mouseleave', handleMouseLeave)
    window.removeEventListener('blur', handleBlur)
    document.removeEventListener('visibilitychange', handleVisibilityChange)
}

const handleCheatAction = (reason) => {
    if (quizState.value !== 'playing') return
    if (isCheatModalVisible.value && cheatWarnings.value >= 3) return

    cheatWarnings.value++
    isCheatModalVisible.value = true
    
    switch (reason) {
        case 'fullscreen':
            cheatMessage.value = "Bạn vừa thoát khỏi chế độ Toàn màn hình."
            break
        case 'mouseleave':
            cheatMessage.value = "Bạn vừa di chuyển chuột ra khỏi khu vực làm bài."
            break
        case 'blur':
            cheatMessage.value = "Hệ thống phát hiện bạn vừa chuyển sang ứng dụng khác hoặc màn hình khác."
            break
        case 'visibility':
            cheatMessage.value = "Hệ thống phát hiện hành vi Chuyển Tab trình duyệt."
            break
    }

    if (cheatWarnings.value >= 3) {
        cheatMessage.value = "ĐÌNH CHỈ THI. Bạn đã vi phạm quá 3 lần. Bài thi sẽ tự động nộp sau 3 giây."
        setTimeout(() => {
            submitQuiz()
        }, 3000)
    }
}

const reEnterFullscreen = async () => {
    try {
        const elem = document.documentElement
        if (elem.requestFullscreen) {
            await elem.requestFullscreen()
        } else if (elem.webkitRequestFullscreen) {
            await elem.webkitRequestFullscreen()
        }
        isCheatModalVisible.value = false
    } catch (err) {
        console.error(err)
        isCheatModalVisible.value = false
    }
}

onMounted(() => {
    fetchQuiz()
    document.addEventListener('fullscreenchange', handleFullscreenChange)
    document.addEventListener('webkitfullscreenchange', handleFullscreenChange)
})

onUnmounted(() => {
    document.removeEventListener('fullscreenchange', handleFullscreenChange)
    document.removeEventListener('webkitfullscreenchange', handleFullscreenChange)
    removeAntiCheat()
    clearInterval(timerInterval)
})
</script>

<template>
  <div class="h-screen w-full bg-surface text-gray-900 flex flex-col overflow-hidden font-plus-jakarta relative">
    
    <!-- Loading State -->
    <div v-if="isLoading" class="flex-1 flex items-center justify-center bg-gray-50/50">
        <span class="material-symbols-outlined text-4xl animate-spin text-primary">progress_activity</span>
    </div>

    <!-- Màn hình 1: INTRO (Chuẩn bị làm bài) -->
    <div v-else-if="quizState === 'intro' && quizInfo" class="flex-1 flex items-center justify-center p-6 bg-gray-50/50">
        <div class="bg-white rounded-2xl shadow-xl border border-surface-container-highest max-w-xl w-full p-8 md:p-10 text-center animate-in fade-in zoom-in duration-300">
            <div class="w-16 h-16 bg-primary/10 rounded-full flex items-center justify-center mx-auto mb-6">
                <span class="material-symbols-outlined text-3xl text-primary font-bold">assignment</span>
            </div>
            <h2 class="text-2xl md:text-3xl font-extrabold text-gray-900 mb-2">{{ quizInfo.title }}</h2>
            <p class="text-gray-500 mb-8">Vui lòng đọc kỹ hướng dẫn trước khi bắt đầu bài kiểm tra.</p>

            <div class="grid grid-cols-3 gap-4 mb-8">
                <div class="p-4 bg-surface rounded-xl border border-surface-container">
                    <p class="text-[10px] uppercase font-bold text-gray-500 mb-1">Thời gian</p>
                    <p class="text-lg font-mono font-bold text-gray-900">{{ quizInfo.timeLimit / 60 }} min</p>
                </div>
                <div class="p-4 bg-surface rounded-xl border border-surface-container">
                    <p class="text-[10px] uppercase font-bold text-gray-500 mb-1">Số câu hỏi</p>
                    <p class="text-lg font-mono font-bold text-gray-900">{{ quizInfo.questions.length }} câu</p>
                </div>
                <div class="p-4 bg-surface rounded-xl border border-surface-container">
                    <p class="text-[10px] uppercase font-bold text-gray-500 mb-1">Điểm đạt</p>
                    <p class="text-lg font-mono font-bold text-gray-900">{{ quizInfo.passingScore }}%</p>
                </div>
            </div>

            <!-- Warning Box -->
            <div class="bg-warning/10 border-l-4 border-warning p-5 mb-8 text-left rounded-r-xl">
                <div class="flex gap-3">
                    <span class="material-symbols-outlined text-warning">warning</span>
                    <div>
                        <h4 class="font-bold text-yellow-900 text-sm">Lưu ý quan trọng: Toàn màn hình</h4>
                        <p class="text-sm text-yellow-800 leading-relaxed mt-1">
                            Bài kiểm tra yêu cầu thực hiện trong chế độ <strong>Toàn màn hình</strong>. Việc thoát khỏi chế độ này hoặc chuyển tab có thể khiến kết quả bài thi bị hủy bỏ hoặc tự động nộp.
                        </p>
                    </div>
                </div>
            </div>

            <button @click="startQuiz" class="w-full bg-primary hover:bg-blue-700 text-white font-extrabold py-4 rounded-xl shadow-lg shadow-primary/20 transition-all hover:scale-[1.02] active:scale-95 text-xl">
                Bắt đầu làm bài
            </button>
        </div>
    </div>

    <!-- Màn hình 2: PLAYING (Giao diện thi) -->
    <template v-else-if="quizState === 'playing' && quizInfo">
        <header class="h-16 shrink-0 bg-white border-b border-surface-container-highest flex items-center justify-between px-6 z-20">
            <div class="flex items-center gap-4">
                <div class="flex items-center gap-2 text-gray-400 font-medium text-sm select-none">
                    <span class="material-symbols-outlined text-lg">lock</span>
                    <span>Chế độ thi tập trung</span>
                </div>
                <div class="w-px h-6 bg-surface-container-highest hidden md:block"></div>
                <h1 class="font-bold text-gray-900 truncate max-w-[200px] md:max-w-none">{{ quizInfo.title }}</h1>
            </div>
            <div class="flex items-center gap-3">
                <div class="lg:hidden flex items-center gap-2 bg-surface-container px-3 py-1.5 rounded-full">
                    <span class="material-symbols-outlined text-sm text-gray-500">timer</span>
                    <span class="font-mono font-bold text-gray-900 text-sm leading-none">{{ formattedTime }}</span>
                </div>
            </div>
        </header>

        <main class="flex-1 flex flex-col lg:flex-row overflow-hidden relative">
            <!-- Questions Column -->
            <div class="flex-1 overflow-y-auto p-6 md:p-12 relative bg-surface">
                <div v-if="quizInfo.questions[currentQuestionIndex]" class="max-w-3xl mx-auto pb-24">
                    <div class="mb-6 flex items-center gap-3">
                        <span class="inline-flex items-center justify-center bg-gray-900 text-white font-bold text-sm px-4 py-1.5 rounded-full">
                            Câu hỏi {{ currentQuestionIndex + 1 }}
                        </span>
                        <span class="text-sm font-medium text-gray-500">Chủ đề: {{ quizInfo.title }}</span>
                    </div>
                    <h2 class="text-2xl md:text-[28px] font-extrabold leading-snug mb-8">
                        {{ quizInfo.questions[currentQuestionIndex].text }}
                    </h2>
                    
                    <div class="flex flex-col gap-4">
                        <label v-for="(option, idx) in quizInfo.questions[currentQuestionIndex].options" :key="idx" class="relative cursor-pointer group">
                            <input type="radio" :name="'quiz-ans-' + currentQuestionIndex" class="answer-radio peer sr-only" 
                                :value="idx" 
                                v-model="answers[currentQuestionIndex]">
                            <div class="w-full p-5 rounded-xl border-2 border-surface-container-highest bg-white transition-all flex items-start gap-4 hover:border-primary/40 group-hover:shadow-sm">
                                <div class="radio-indicator w-6 h-6 rounded-full border-2 border-gray-300 mt-0.5 shrink-0 transition-all"></div>
                                <span class="text-lg font-medium text-gray-900 leading-relaxed">
                                    {{ option }}
                                </span>
                            </div>
                        </label>
                    </div>
                </div>

                <!-- Floating Pagination -->
                <div class="absolute bottom-0 left-0 right-0 p-6 bg-gradient-to-t from-surface via-surface to-transparent flex justify-center gap-4">
                    <button @click="currentQuestionIndex--" :disabled="currentQuestionIndex === 0" class="disabled:opacity-50 flex items-center gap-2 px-6 py-3 bg-white border border-surface-container-highest text-gray-900 font-bold rounded-lg shadow-sm hover:bg-gray-50 transition-colors">
                        <span class="material-symbols-outlined">arrow_back</span> Câu trước
                    </button>
                    <button @click="currentQuestionIndex++" :disabled="currentQuestionIndex === quizInfo.questions.length - 1" class="disabled:opacity-50 flex items-center gap-2 px-6 py-3 bg-white border border-surface-container-highest text-gray-900 font-bold rounded-lg shadow-sm hover:bg-gray-50 transition-colors">
                        Câu tiếp theo <span class="material-symbols-outlined">arrow_forward</span>
                    </button>
                </div>
            </div>

            <!-- Info Sidebar (Timer & Question List) -->
            <aside class="w-full lg:w-[360px] shrink-0 bg-white border-l border-surface-container-highest flex flex-col h-[40vh] lg:h-auto z-10 shadow-[-4px_0_15px_rgba(0,0,0,0.02)]">
                <div class="p-6 border-b border-surface-container-highest text-center bg-surface-container-low hidden lg:block">
                    <p class="text-sm text-gray-500 font-medium mb-1">Thời gian còn lại</p>
                    <div :class="['text-[42px] font-mono font-extrabold leading-none tracking-tight mb-4 transition-colors', timeLeft < 60 ? 'text-red-500 animate-pulse' : 'text-gray-900']">
                        {{ formattedTime }}
                    </div>
                    <div class="w-full h-2 bg-surface-container-highest rounded-full overflow-hidden">
                        <div class="h-full bg-primary transition-all duration-1000" :style="{ width: (timeLeft / quizInfo.timeLimit * 100) + '%' }"></div>
                    </div>
                    <p class="text-xs text-gray-500 mt-2 font-medium">Đã trả lời {{ answeredCount }}/{{ quizInfo.questions.length }} câu</p>
                </div>

                <div class="flex-1 overflow-y-auto p-6 text-center">
                    <h3 class="font-bold text-gray-900 mb-4 text-left">Danh sách câu hỏi</h3>
                    <div class="grid grid-cols-5 gap-3">
                        <button v-for="(q, n) in quizInfo.questions" :key="n"
                            @click="currentQuestionIndex = n"
                            :class="['aspect-square rounded-lg flex items-center justify-center font-bold text-sm transition-all', 
                                    n === currentQuestionIndex ? 'bg-primary text-white shadow-md ring-2 ring-primary ring-offset-2' : 
                                    answers[n] !== undefined ? 'bg-primary-container text-primary border border-primary/20 hover:bg-primary hover:text-white' : 
                                    'bg-white text-gray-500 border-2 border-surface-container-highest hover:border-primary']">
                            {{ n + 1 }}
                        </button>
                    </div>
                </div>

                <div class="p-6 border-t border-surface-container-highest bg-white">
                    <button @click="submitQuiz" class="w-full py-4 rounded-xl bg-primary hover:bg-blue-700 text-white font-bold text-lg shadow-lg shadow-primary/20 transition-all flex items-center justify-center gap-2">
                        <span class="material-symbols-outlined">send</span> Nộp bài kiểm tra
                    </button>
                </div>
            </aside>
        </main>
    </template>

    <!-- Màn hình 3: RESULT (Kết quả) -->
    <div v-else-if="quizState === 'result' && results" class="flex-1 flex items-center justify-center p-6 bg-surface">
        <div class="bg-white rounded-2xl shadow-xl border border-surface-container-highest max-w-lg w-full p-10 text-center">

            <!-- PASS State -->
            <template v-if="results.passed">
                <div class="w-20 h-20 bg-success/10 rounded-full flex items-center justify-center mx-auto mb-6">
                    <span class="material-symbols-outlined text-4xl text-success" style="font-variation-settings: 'FILL' 1;">check_circle</span>
                </div>
                <h2 class="text-3xl font-extrabold text-gray-900 mb-2">Xuất sắc! 🎉</h2>
                <p class="text-gray-500 mb-8">{{ results.message }}</p>
            </template>

            <!-- FAIL State -->
            <template v-else>
                <div class="w-20 h-20 bg-error/10 rounded-full flex items-center justify-center mx-auto mb-6">
                    <span class="material-symbols-outlined text-4xl text-error" style="font-variation-settings: 'FILL' 1;">cancel</span>
                </div>
                <h2 class="text-3xl font-extrabold text-gray-900 mb-2">Chưa đạt 😔</h2>
                <p class="text-gray-500 mb-8">{{ results.message }}</p>
            </template>

            <!-- Score Card -->
            <div :class="['p-6 rounded-2xl border mb-8', results.passed ? 'bg-emerald-50 border-emerald-200' : 'bg-error/5 border-error/20']">
                <p class="text-sm font-bold text-gray-500 uppercase tracking-widest mb-2">Điểm số đạt được</p>
                <div :class="['text-[52px] font-mono font-extrabold leading-tight', results.passed ? 'text-success' : 'text-error']">
                    {{ results.score }}<span class="text-2xl text-on-surface-variant">%</span>
                </div>
                <div class="text-sm font-bold mt-1" :class="results.passed ? 'text-success' : 'text-error'">
                    {{ results.passed ? 'Vượt qua ✓' : 'Chưa đạt ✗' }}
                </div>
            </div>

            <!-- Actions -->
            <div class="flex flex-col gap-3">
                <router-link v-if="results.passed" :to="`/learning/${courseId}/certificate`" class="w-full bg-emerald-600 hover:bg-emerald-700 text-white font-bold py-4 rounded-xl shadow-lg shadow-emerald-200 transition-all flex items-center justify-center gap-2">
                    <span class="material-symbols-outlined">workspace_premium</span>
                    Nhận chứng chỉ ngay
                </router-link>
                <button v-else @click="quizState = 'intro'; fetchQuiz(); answers = {}" class="w-full bg-primary hover:bg-blue-700 text-white font-bold py-4 rounded-xl shadow-lg shadow-primary/20 transition-all flex items-center justify-center gap-2">
                    <span class="material-symbols-outlined">replay</span>
                    Thi lại
                </button>
                <button @click="router.push('/dashboard')" class="w-full font-bold py-4 rounded-xl transition-all bg-surface hover:bg-surface-container text-on-surface border border-surface-container-highest">
                    Quay lại Dashboard
                </button>
            </div>
        </div>
    </div>

    <!-- CheatWarningModal (STU-05-EX) -->
    <div v-if="isCheatModalVisible" class="fixed inset-0 z-[100] flex items-center justify-center p-6">
        <div class="absolute inset-0 bg-gray-900/95 backdrop-blur-xl"></div>
        <div class="relative bg-white rounded-2xl shadow-2xl w-full max-w-lg overflow-hidden transform animate-in fade-in zoom-in duration-300 border-t-8 border-error">
            <div class="p-8 md:p-10 text-center">
                <div class="w-24 h-24 bg-error/10 rounded-full flex items-center justify-center mx-auto mb-8 animate-pulse">
                    <span class="material-symbols-outlined text-6xl text-error font-black">gavel</span>
                </div>
                
                <h3 class="text-3xl font-black text-gray-900 mb-4 tracking-tight uppercase">Cảnh báo vi phạm quy chế!</h3>
                
                <div class="bg-error/5 border border-error/20 p-6 rounded-2xl mb-8">
                    <p class="text-lg text-error font-bold mb-2">{{ cheatMessage }}</p>
                    <p class="text-sm text-gray-600 leading-relaxed font-medium">
                        Hành vi này bị nghiêm cấm trong quá trình thi. <br/>Vui lòng tuân thủ quy định để không bị hủy kết quả.
                    </p>
                </div>

                <div class="flex items-center justify-center gap-2 mb-10 text-gray-500 font-bold uppercase tracking-widest text-xs">
                    Số lần vi phạm: 
                    <span :class="['text-2xl px-3 py-1 rounded-lg', cheatWarnings >= 3 ? 'bg-error text-white' : 'bg-gray-100 text-gray-900']">
                        {{ cheatWarnings }}/3
                    </span>
                </div>

                <button v-if="cheatWarnings < 3" @click="reEnterFullscreen" 
                    class="w-full py-5 bg-primary hover:bg-blue-700 text-white font-black rounded-xl shadow-xl shadow-primary/20 transition-all hover:scale-[1.02] active:scale-95 text-xl flex items-center justify-center gap-3">
                    <span class="material-symbols-outlined font-bold">check_circle</span>
                    Tôi đã hiểu và quay lại làm bài
                </button>
                
                <div v-else class="text-error font-black text-xl flex items-center justify-center gap-2 py-4">
                    <div class="w-4 h-4 bg-error rounded-full animate-ping"></div>
                    Đang xử lý nộp bài...
                </div>
            </div>
        </div>
    </div>

  </div>
</template>

<style scoped>
/* Enterprise styles scoped to component */
.answer-radio:checked + div {
    border-color: #1A56DB;
    background-color: #EBF0FF;
    box-shadow: 0 4px 6px -1px rgba(26, 86, 219, 0.1);
}
.answer-radio:checked + div .radio-indicator {
    border-color: #1A56DB;
    border-width: 6px;
}

/* Scrollbar custom */
::-webkit-scrollbar { width: 6px; }
::-webkit-scrollbar-track { background: transparent; }
::-webkit-scrollbar-thumb { background: #d1d5db; border-radius: 10px; }
::-webkit-scrollbar-thumb:hover { background: #9ca3af; }

.font-plus-jakarta {
    font-family: 'Plus Jakarta Sans', sans-serif;
}
</style>
