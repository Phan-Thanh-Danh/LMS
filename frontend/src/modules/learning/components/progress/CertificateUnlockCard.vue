<script setup>
import { computed } from 'vue'
import { useRouter } from 'vue-router'

const props = defineProps({
    progressNeeded: { type: Number, default: 35 },
    courseId: { type: [String, Number], default: 1 }
})

const router = useRouter()
const isCompleted = computed(() => props.progressNeeded === 0)
</script>

<template>
    <div class="sticky top-24 bg-white rounded-xl border border-surface-container-highest shadow-sm p-6 flex flex-col items-center text-center">
        
        <!-- Completed State: SuccessBanner -->
        <template v-if="isCompleted">
            <div class="w-32 h-24 bg-emerald-50 rounded border-2 border-emerald-200 flex flex-col items-center justify-center mb-6 gap-1">
                <span class="material-symbols-outlined text-4xl text-emerald-500" style="font-variation-settings: 'FILL' 1;">workspace_premium</span>
                <span class="text-[10px] font-bold text-emerald-700 uppercase tracking-wider">Đã mở khóa</span>
            </div>
            <h3 class="text-lg font-extrabold text-gray-900 mb-2">Chúc mừng!</h3>
            <p class="text-sm text-on-surface-variant mb-6 leading-relaxed">
                Bạn đã hoàn thành <strong class="text-gray-900">100%</strong> khóa học. Chứng chỉ của bạn đã sẵn sàng!
            </p>
            <router-link :to="`/learning/${courseId}/certificate`" class="w-full bg-emerald-600 hover:bg-emerald-700 text-white font-bold py-3 px-4 rounded-md transition-all shadow-sm shadow-emerald-200 flex items-center justify-center gap-2 mb-3">
                <span class="material-symbols-outlined text-lg">workspace_premium</span>
                Xem chứng chỉ của bạn
            </router-link>
            <router-link :to="`/learn/${courseId}`" class="w-full bg-surface-container text-on-surface hover:bg-surface-container-highest font-bold py-3 px-4 rounded-md transition-all flex items-center justify-center gap-2 text-sm">
                <span class="material-symbols-outlined text-base">replay</span>
                Ôn lại khóa học
            </router-link>
        </template>

        <!-- Locked State: Progress needed -->
        <template v-else>
            <div class="w-32 h-24 bg-surface-container rounded border-2 border-dashed border-surface-container-highest flex items-center justify-center mb-6 relative overflow-hidden opacity-50 grayscale">
                <span class="material-symbols-outlined text-4xl text-on-surface-variant">workspace_premium</span>
                <div class="absolute inset-0 bg-surface/50 backdrop-blur-[1px] flex items-center justify-center">
                    <span class="material-symbols-outlined text-3xl text-gray-900">lock</span>
                </div>
            </div>
            <h3 class="text-lg font-extrabold text-gray-900 mb-2">Chứng chỉ hoàn thành</h3>
            <p class="text-sm text-on-surface-variant mb-6 leading-relaxed">
                Bạn cần hoàn thành thêm <strong class="text-gray-900">{{ progressNeeded }}%</strong> để mở khóa chứng chỉ này.
            </p>

            <router-link :to="`/learn/${courseId}`" class="w-full bg-primary hover:bg-blue-700 text-white font-bold py-3 px-4 rounded-md transition-all shadow-sm shadow-primary/20 flex items-center justify-center gap-2">
                <span class="material-symbols-outlined text-lg">play_arrow</span>
                Tiếp tục bài đang học
            </router-link>
            
            <div class="mt-6 pt-4 border-t border-surface-container-highest w-full text-left">
                <p class="text-[11px] text-on-surface-variant italic">*Khi đạt 100%, bạn sẽ có thể tải PDF chứng chỉ từ đây.</p>
            </div>
        </template>
    </div>
</template>
