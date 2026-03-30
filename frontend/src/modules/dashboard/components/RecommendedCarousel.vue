<script setup>
defineProps({
    courses: {
        type: Array,
        default: () => []
    }
})
</script>

<template>
  <div class="space-y-6">
    <div class="flex items-center justify-between">
      <h2 class="text-2xl font-black text-gray-900 tracking-tighter uppercase">Gợi ý riêng cho bạn</h2>
      <a href="#" class="text-primary font-bold text-xs uppercase tracking-widest hover:underline">Khám phá tất cả</a>
    </div>

    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
      <router-link v-for="course in courses" :key="course.id" :to="`/course/${course.id || 'vcore-ai'}`"
        class="group bg-white rounded-2xl overflow-hidden shadow-sm border border-gray-100 hover:shadow-2xl hover:-translate-y-1 transition-all duration-500 cursor-pointer flex flex-col">
        
        <div class="aspect-video relative overflow-hidden">
          <img :src="course.thumbnail" :alt="course.title" class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-700">
          <div class="absolute inset-x-0 bottom-0 h-1/2 bg-linear-to-t from-black/60 to-transparent"></div>
          <span class="absolute top-3 left-3 bg-amber-400 text-white text-[10px] font-black px-2 py-1 rounded-md shadow-lg shadow-amber-400/20 uppercase tracking-widest">Best Seller</span>
          
          <div class="absolute bottom-3 left-3 flex items-center gap-1.5 text-white">
            <div class="flex text-amber-400">
              <span v-for="i in 5" :key="i" class="material-symbols-outlined text-[14px]" style="font-variation-settings: 'FILL' 1;">star</span>
            </div>
            <span class="text-[10px] font-bold opacity-90">(4.9)</span>
          </div>
        </div>

        <div class="p-5 flex-1 flex flex-col space-y-3">
          <h3 class="font-black text-gray-900 leading-tight tracking-tight h-10 line-clamp-2 group-hover:text-primary transition-colors text-sm uppercase">
            {{ course.title }}
          </h3>
          <p class="text-[10px] font-bold text-gray-400 uppercase tracking-widest leading-none">Bởi {{ course.instructor }}</p>
          
          <div class="pt-4 mt-auto border-t border-gray-50 flex items-center justify-between">
            <div class="flex items-center gap-2">
              <span class="text-lg font-black text-gray-900 tracking-tighter">{{ course.price || '399.000đ' }}</span>
              <span class="text-[10px] text-gray-400 line-through font-bold">1.200.000đ</span>
            </div>
            <div class="w-8 h-8 rounded-full border-2 border-primary/20 flex items-center justify-center group-hover:bg-primary group-hover:border-primary transition-all">
              <span class="material-symbols-outlined text-sm font-bold text-primary group-hover:text-white">arrow_forward</span>
            </div>
          </div>
        </div>
      </router-link>
    </div>
  </div>
</template>
