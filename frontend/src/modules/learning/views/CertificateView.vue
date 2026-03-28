<script setup>
import { ref, onMounted, computed } from 'vue'
import { useRoute } from 'vue-router'
import { apiService } from '@/services/api'

const route = useRoute()
const courseId = computed(() => Number(route.params.id))
const certificate = ref(null)
const isLoading = ref(true)

onMounted(async () => {
    try {
        isLoading.value = true
        certificate.value = await apiService.getCertificate(courseId.value)
    } finally {
        isLoading.value = false
    }
})

const downloadPDF = () => {
    window.print()
}
</script>

<template>
  <div class="min-h-screen bg-gray-200 flex flex-col items-center justify-center p-4 md:p-8 font-sans overflow-x-hidden print:p-0 print:bg-white pt-24 md:pt-24 lg:pt-8">
    
    <!-- Professional Header for screen only -->
    <div class="fixed top-0 left-0 right-0 h-16 bg-white/80 backdrop-blur-md border-b border-gray-200 z-100 flex items-center justify-between px-8 print:hidden">
        <div class="flex items-center gap-2">
            <span class="material-symbols-outlined text-primary font-bold">verified_user</span>
            <h1 class="font-bold text-gray-900">Chứng chỉ AET LMS</h1>
        </div>
        <div class="flex gap-4">
            <button @click="$router.back()" class="px-4 py-2 text-sm font-bold text-gray-500 hover:text-gray-900 transition-colors">Quay lại</button>
            <button @click="downloadPDF" class="flex items-center gap-2 px-6 py-2 bg-primary text-white rounded-lg font-bold shadow-lg shadow-primary/20 hover:bg-blue-700 transition-all">
                <span class="material-symbols-outlined text-sm">download</span>
                Tải xuống PDF
            </button>
        </div>
    </div>

    <!-- Certificate Container -->
    <div v-if="isLoading" class="w-full max-w-[1100px] aspect-[1.414/1] bg-white rounded-xl flex items-center justify-center border-4 border-dashed border-gray-300">
        <span class="material-symbols-outlined text-6xl animate-spin text-primary">progress_activity</span>
    </div>

    <div v-else-if="certificate" id="certificate-content" class="w-full max-w-[1100px] aspect-[1.414/1] border-ornate bg-certificate relative p-12 flex flex-col items-center justify-center overflow-hidden shadow-2xl scale-[0.8] md:scale-90 lg:scale-100 origin-center bg-white">
        
        <!-- Corner Accents -->
        <div class="corner-accent corner-tl"></div>
        <div class="corner-accent corner-tr"></div>
        <div class="corner-accent corner-bl"></div>
        <div class="corner-accent corner-br"></div>
        
        <!-- Watermark Background -->
        <div class="absolute inset-0 flex items-center justify-center opacity-[0.03] pointer-events-none">
            <svg class="w-[600px] h-[600px] text-primary" fill="currentColor" viewBox="0 0 24 24">
                <path d="M12 3L1 9l4 2.18v6L12 21l7-2.82v-6l2.08-1.13L21 14v5h2v-6.9l1-2.1L12 3zm6.82 6L12 12.72 5.18 9 12 5.28 18.82 9zM17 15.99l-5 2.01-5-2.01v-4.62l5 2.72 5-2.72v4.62z"/>
            </svg>
        </div>

        <div class="relative z-10 flex flex-col items-center text-center w-full h-full pt-8 pb-4">
            
            <h1 class="font-serif text-4xl md:text-5xl lg:text-6xl text-brand-gradient tracking-widest font-bold mb-3 uppercase">
                Certificate
            </h1>
            <h2 class="font-serif text-base md:text-lg lg:text-xl text-gray-500 tracking-[0.25em] uppercase mb-10">
                Of Achievement
            </h2>
            
            <p class="font-sans text-sm md:text-base text-gray-500 font-medium mb-2">
                This prestigious award is proudly presented to
            </p>
            
            <h3 class="font-serif text-5xl md:text-6xl lg:text-7xl text-navy-900 font-bold mb-6 tracking-wide drop-shadow-sm">
                {{ certificate.studentName }}
            </h3>
            
            <p class="font-sans text-sm md:text-base text-gray-700 font-medium mb-4">
                Has successfully completed the professional course:
            </p>
            
            <h4 class="font-serif text-2xl md:text-3xl lg:text-4xl text-brand-gradient font-bold mb-3 uppercase tracking-wider">
                {{ certificate.courseTitle }}
            </h4>
            
            <h5 class="font-serif text-lg md:text-xl text-primary uppercase tracking-[0.15em] mb-auto font-semibold">
                AET Academy & LMS
            </h5>

            <!-- Signatures & Seal Section -->
            <div class="w-full flex justify-between items-end px-4 md:px-16 mt-8 relative">
                
                <!-- Signature 1 -->
                <div class="flex flex-col items-center text-center w-64">
                    <div class="font-signature text-4xl md:text-5xl text-gray-800 -mb-2">John Wick</div>
                    <div class="w-full h-px bg-gray-400 mb-2"></div>
                    <div class="font-sans font-bold text-gray-900 text-sm">Phan Thanh Danh</div>
                    <div class="font-sans text-gray-500 text-xs uppercase tracking-wider">AET Director</div>
                </div>

                <!-- Central Seal -->
                <div class="absolute left-1/2 bottom-[-20px] transform -translate-x-1/2 flex items-center justify-center">
                    <!-- Ribbon Tails (Blue/Navy instead of Gold) -->
                    <div class="absolute w-20 h-32 bg-linear-to-b from-blue-900 to-navy-950 ribbon-tail -ml-24 mt-12 -rotate-45 shadow-lg border-2 border-primary/30"></div>
                    <div class="absolute w-20 h-32 bg-linear-to-b from-blue-900 to-navy-950 ribbon-tail ml-24 mt-12 rotate-45 shadow-lg border-2 border-primary/30"></div>
                    
                    <!-- Main Seal Circle -->
                    <div class="relative z-10 w-36 h-36 md:w-40 md:h-40 rounded-full bg-linear-to-br from-blue-400 via-primary to-blue-900 flex items-center justify-center seal-shadow border-4 border-white/80">
                        <div class="w-[88%] h-[88%] rounded-full bg-navy-950 flex flex-col items-center justify-center p-2 shadow-inner border border-blue-400 relative">
                            <div class="absolute inset-1 border border-dashed border-blue-300 rounded-full opacity-50"></div>
                            
                            <span class="font-serif text-[10px] text-blue-300 tracking-widest uppercase mt-1 italic">Verified</span>
                            <h2 class="font-serif text-2xl font-bold text-white leading-tight my-1">AET<br>Academy</h2>
                            
                            <div class="w-10 h-10 bg-white rounded-full flex items-center justify-center mt-1 shadow-md">
                                <span class="material-symbols-outlined text-primary text-2xl font-black">check_circle</span>
                            </div>
                            
                            <span class="font-sans text-[7px] text-blue-300 tracking-[0.2em] uppercase absolute bottom-2 font-bold">LMS Certified</span>
                        </div>
                    </div>
                </div>

                <!-- Signature 2 & Date -->
                <div class="flex flex-col items-center text-center w-64">
                    <div class="font-signature text-3xl md:text-4xl text-gray-800 -mb-2 italic">AET Education Team</div>
                    <div class="w-full h-px bg-gray-400 mb-2"></div>
                    <div class="font-sans font-bold text-gray-900 text-xs md:text-sm">Certification Board,</div>
                    <div class="font-sans text-gray-500 text-[10px] uppercase tracking-wider mb-4">Academic Division</div>
                    
                    <div class="font-sans font-bold text-gray-900 text-sm mt-2 mb-1">{{ certificate.issueDate }}</div>
                    <div class="w-32 h-px bg-gray-300 mb-1"></div>
                    <div class="font-sans text-gray-400 text-[10px] uppercase tracking-widest font-bold">Issue Date</div>
                    <div class="font-sans text-primary text-[9px] font-bold mt-2">ID: {{ certificate.verificationCode }}</div>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Playfair+Display:ital,wght@0,400;0,700;0,900;1,400&family=Great+Vibes&family=Montserrat:wght@400;500;700;900&display=swap');

.font-serif { font-family: 'Playfair Display', serif; }
.font-sans { font-family: 'Montserrat', sans-serif; }
.font-signature { font-family: 'Great Vibes', cursive; }

.bg-certificate {
    background: 
        radial-gradient(circle at 30% 30%, rgba(26, 86, 219, 0.05) 0%, transparent 60%),
        radial-gradient(circle at 70% 70%, rgba(30, 64, 175, 0.05) 0%, transparent 50%),
        linear-gradient(135deg, #ffffff 0%, #f9fafb 50%, #ffffff 100%);
}

.border-ornate {
    position: relative;
    box-shadow: 
        inset 0 0 0 10px #ffffff,
        inset 0 0 0 13px #1A56DB, /* AET Primary Blue */
        inset 0 0 0 18px #ffffff,
        inset 0 0 0 20px #1E40AF, /* AET Dark Blue */
        0 40px 100px -20px rgba(0, 0, 0, 0.15);
}

.corner-accent {
    position: absolute;
    width: 35px;
    height: 35px;
    border: 3px solid #1A56DB;
    background: white;
    z-index: 10;
}
.corner-tl { top: 13px; left: 13px; border-right: none; border-bottom: none; }
.corner-tr { top: 13px; right: 13px; border-left: none; border-bottom: none; }
.corner-bl { bottom: 13px; left: 13px; border-right: none; border-top: none; }
.corner-br { bottom: 13px; right: 13px; border-left: none; border-top: none; }

.ribbon-tail {
    clip-path: polygon(0 0, 100% 0, 100% 100%, 50% 85%, 0 100%);
}

.text-brand-gradient {
    background: linear-gradient(to bottom, #1A56DB 0%, #1E3A8A 100%);
    background-clip: text;
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    color: #1A56DB;
}

.seal-shadow {
    box-shadow: 0 15px 35px rgba(26, 86, 219, 0.3), inset 0 4px 6px rgba(255,255,255,0.4);
}

.navy-900 { color: #0F172A; }
.navy-950 { background-color: #020617; }

@media print {
    body { background: white; }
    .bg-gray-200 { background: white; padding: 0 !important; }
    .border-ornate { box-shadow: none; border: 1px solid #eee; }
    .scale-90, .scale-100 { scale: 1 !important; transform: none !important; }
    #certificate-content { 
        width: 297mm; 
        height: 210mm; 
        margin: 0; 
        padding: 40px;
        box-shadow: none;
    }
}
</style>
