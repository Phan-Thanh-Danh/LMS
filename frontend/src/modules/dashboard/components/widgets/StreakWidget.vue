<script setup>
defineProps({
    days: {
        type: Number,
        default: 0
    }
})

const weekDays = ['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']
const activeDays = [true, true, true, true, false, false, false] // Mock
</script>

<template>
  <div class="bg-linear-to-br from-slate-900 to-indigo-950 rounded-2xl p-6 text-white shadow-xl shadow-indigo-950/20 relative overflow-hidden group border border-white/5">
    <div class="absolute -right-4 -top-4 w-24 h-24 bg-primary/10 rounded-full blur-3xl group-hover:scale-150 transition-transform duration-1000"></div>
    
    <div class="flex items-center justify-between mb-6 relative z-10">
      <div class="p-3 bg-white/10 rounded-xl backdrop-blur-md border border-white/10">
        <span class="material-symbols-outlined text-orange-400 text-3xl font-bold animate-pulse-slow">local_fire_department</span>
      </div>
      <div class="text-right">
        <p class="text-[32px] font-black leading-none tracking-tighter">{{ days }}</p>
        <p class="text-[10px] font-black text-primary uppercase tracking-[0.2em] mt-1">Chuỗi ngày học</p>
      </div>
    </div>

    <div class="space-y-4 relative z-10">
      <p class="text-xs font-bold text-slate-300 uppercase tracking-widest">Hoạt động tuần này</p>
      <div class="flex justify-between gap-2">
        <div v-for="(day, idx) in weekDays" :key="idx" class="flex flex-col items-center gap-2">
          <div :class="['w-8 h-8 rounded-lg flex items-center justify-center transition-all duration-500', 
                       activeDays[idx] ? 'bg-primary text-white shadow-lg shadow-primary/30 scale-110 border border-white/20' : 'bg-white/5 text-slate-500 border border-white/5']">
            <span v-if="activeDays[idx]" class="material-symbols-outlined text-sm font-black">check</span>
            <span v-else class="text-[10px] font-bold">{{ day }}</span>
          </div>
          <span class="text-[8px] font-black uppercase text-slate-500 tracking-tighter">{{ day }}</span>
        </div>
      </div>
    </div>

    <div class="mt-6 pt-6 border-t border-white/5 relative z-10">
      <p class="text-[10px] font-bold text-slate-400 leading-relaxed uppercase tracking-tighter">
        Bạn chỉ còn <span class="text-white">2 ngày</span> nữa để nhận badge <span class="text-primary tracking-widest">"Chăm chỉ"</span>!
      </p>
    </div>
  </div>
</template>

<style scoped>
@keyframes pulse-slow {
  0%, 100% { transform: scale(1); opacity: 1; }
  50% { transform: scale(1.1); opacity: 0.8; }
}
.animate-pulse-slow {
  animation: pulse-slow 3s infinite ease-in-out;
}
</style>
