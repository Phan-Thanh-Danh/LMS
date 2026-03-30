<script setup>
const categories = [
  { name: 'Lập trình', icon: 'code' },
  { name: 'Thiết kế', icon: 'brush' },
  { name: 'Marketing', icon: 'trending_up' },
  { name: 'Kinh doanh', icon: 'payments' },
  { name: 'Nhiếp ảnh', icon: 'camera_alt' },
  { name: 'Ngoại ngữ', icon: 'language' },
]

const courses = [
  {
    title: 'Mastering React & Next.js: Từ Zero đến Pro',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDFfuV6DKABPU7qHvGhU_i94GasnZ83ZyWKqiUDArnqA2C45m2F200AFljw9mIb_VwxGiLxgoY1dIPTMF7KQ3oXL-bxXMOdolMTJM3ww1aTJDUZkuI0lCzsT7nmWdd3_B42JGlm89kRgll92vVkpatt7q74zA_xpQ-_VvPn0p--4rDZDvXzl1XYAxDfJr-zQjZuzgJltnWYEMoecQ9WTrveZL9l-5VmvKuq-KRmmDwR8HIGttkeLYmAnjuyZxcMajuGkXPW-UUu0QQ',
    rating: 4.9,
    reviews: 1240,
    instructor: 'Lê Minh Tuấn',
    instructorImage: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAJwCTXM3mVFQ7Lg1M09aIwyBGTjf5kRUf2viM73ubRuH1FdBIvSlOENCVSSpbC-gtDDbXUJKEiOjhopfExPpv7y4JylFFkv2UZF57AX54PugAYILF7Io0iRzIN8O_TpsrgcJNVV8AJxAbAyylQymHPfw8YPoG-uyU58Stm8wwj6iTjGJWeBE0fnD6sTPs9-5TH8YB-5P2KCh2LHj6QdCOfSP8-BBnETf0ftC347K0IKN_g3zhHycAe0srVII-dtYvchu_xFJL_XdI',
    price: '899.000',
    oldPrice: '1.200.000',
    tag: 'Best Seller'
  },
  {
    title: 'Thiết kế UI/UX Chuyên nghiệp với Figma 2024',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCCFO-pqpvxXKjPj2IVCTmESby-d8e_HLNpgXqE2_vmwSe342VSg_eHBieXxT5-c_qMJzoGuKBCdM8ow4tHR_tK-kzOK8NztlruBAQ7PDbI0_MMgcDaBvdPBGin5_MSvOYCw0lgqbz2R0aqceihPd5R43jRa2-A5E18mTDrnB5RVLKnp3CWHlh9CXYIkrIoAu-8KCOeZskb3WwA5aA8-xXc55KU0qQiYi5Z-wRjtdc8GO4_U2MH2iVWuaftsn_ECs9aihBRnA3N8qM',
    rating: 4.8,
    reviews: 850,
    instructor: 'Nguyễn Thảo Vy',
    instructorImage: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAxbL3oySaC8FZLsaCks37dPjfvG02K_y2eXrnmVnMnC3_-E0fsqMF7p99ILoBRXwqq4ggdpvxAN1QAMK4yjQLXpp7ng8WMgG1RhTRRm3tKCO_ZU7q50PXAL64_bCybUKeIIViVoAv9HzUmJXO8Jl80OzHOytgS3pJ-qFDc0q-I_lyhD1vWNZzUTYwbJwFyPzrNTeTgA0M_TUxHomlZ4xuPBKFGz5d2dawKNMhzS7z6sT7s0j4H-cvMuuBu3p1qitEiPBK7Sxi9xek',
    price: '599.000',
    oldPrice: '950.000'
  },
  {
    title: 'Chiến lược Digital Marketing Thực chiến cho SMEs',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDwv1XUsgDxDBXTzfLGqw2t2us2dKd7LknpPMNiRWvxYZGEmA733VrrllMfM7G_wUyBX7x0LwmTsQRo2gMUYWGQxmX7g7aaUg8WdJU9xVZuYn1Ie2AQXJ3H_Jdu1iwoxMNnTf-mZ1pDsuxZtowqaT2zsxpEKf3B00c7Ip7LjOWVyQRNe1WG2USh8PD4erL3lEZ_lMOxMXUDC2OxazNaY4zhRoUXif1tnSoiUN5QGSB3Alz6zNVT0tm9TuNZkHnAp0KqrZ3-aFKXlfU',
    rating: 5.0,
    reviews: 420,
    instructor: 'Trần Hoàng Nam',
    instructorImage: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAewmgAj8x69UeCJsSM5LvWZQYMncTGCIaPtxMXX5Cys5fFa8ReoZW6zVMkA5zhW6rQ2_FI0_2aKtB2bzqmCCoDfhwItFL7tGd90snJ_2eKoBALiJFdZVvDNTWD_QwGczDjUijV9c5xtPNOuTHsdcQV3tbPYKC4IWzu6uSIo0BcG7XmpOBw1nLfS2WgCLPC9pfODia-PtH4niJfdP_nkMl7Jyu0glc2XEBGeASoUz44jWz3qha6G5_mrv-IrYXfYFdMAmiWSWaF4SU',
    price: '1.299.000',
    oldPrice: '1.800.000'
  },
  {
    title: 'Python & AI: Ứng dụng thực tế trong công việc',
    image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDcvHPkjcyMNVJ9jEsW3vHIuHZw58zbwqwk4HOsnZ2rh1cL7YTLnxPrg_WiVv_AJlIzfGqymU2Ubq1n9K1A9kIuzlfJZtJoXjEAhJypHAAtjuXkZVDixfq3Rr107VQGiAoeKZpII4xirHnwPeACWgxF4d8XZoGAptBiaLIeFl-nWz1hHV5l_CqctsshYQb2aDQJ6OASEzjL6S5gYJhUIvUcQfBBfrDrsfgkj1vu3QVqG_MvN0oV8OOhqxUJj0AOQkJ6Pc-IxBUVg_Q',
    rating: 4.7,
    reviews: 115,
    instructor: 'Phạm Anh Tú',
    instructorImage: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCjaFrdGet5P4uIZI_LBBlJ5HYYOsN1McSEJ5nGy757vzLdgB2uNJNcTXI87rNbGbkvfinu7Rf1pGWsLJYzlW0d3J5yH3skGkwY_mhdSjF_xFbA_-kswflB0XC0AOaMEV3--Tbt3s-pdGpJxAFWMYBHhHrC0GaZg76T4aeQjdpWiCjzon2uo3H3sNjWLp2TnBDfO_tfgTFlBO-Yvn1urhIyYM6pJA7W6M-sT-Fkb11waHOajgVJcKMfNQPuT7rZymAbY3M1w4Rqebs',
    price: '749.000',
    oldPrice: '1.100.000',
    tag: 'New'
  }
]
</script>

<template>
  <div class="landing-page">
    <!-- Section 1: Sticky Topbar -->
    <nav class="fixed top-0 w-full z-50 bg-white shadow-sm border-none">
      <div class="flex items-center justify-between px-6 py-3 w-full max-w-8xl mx-auto">
        <!-- Logo -->
        <div class="text-2xl font-bold tracking-tighter text-primary shrink-0">
          AET LMS
        </div>
        
        <!-- Search Bar (40% width) -->
        <div class="hidden md:flex w-[40%] relative items-center group">
          <span class="material-symbols-outlined absolute left-4 text-outline">search</span>
          <input 
            aria-label="Tìm kiếm khóa học" 
            class="w-full bg-surface-container-high border-none rounded-full py-2.5 pl-12 pr-4 text-sm focus:ring-2 focus:ring-primary-container transition-all outline-none" 
            placeholder="Tìm kiếm khóa học, giảng viên..." 
            type="text"
          >
        </div>

        <!-- Navigation Links & Actions -->
        <div class="flex items-center gap-6">
          <div class="hidden lg:flex items-center gap-6 text-sm font-medium tracking-tight">
            <a class="text-primary font-semibold border-b-2 border-primary" href="#">Courses</a>
            <a class="text-slate-600 dark:text-slate-400 hover:text-slate-900 dark:hover:text-slate-100 transition-all" href="#">Instructors</a>
            <a class="text-slate-600 dark:text-slate-400 hover:text-slate-900 dark:hover:text-slate-100 transition-all" href="#">Resources</a>
          </div>
          <div class="flex items-center gap-3 border-l border-surface-container-highest pl-6">
            <button aria-label="Giỏ hàng" class="p-2 rounded-full hover:bg-slate-100/50 transition-all active:scale-95">
              <span class="material-symbols-outlined text-on-surface-variant">shopping_cart</span>
            </button>
            <button aria-label="Thông báo" class="p-2 rounded-full hover:bg-slate-100/50 transition-all active:scale-95">
              <span class="material-symbols-outlined text-on-surface-variant">notifications</span>
            </button>
            <router-link to="/login" class="px-5 py-2 text-sm font-semibold text-primary hover:text-blue-700 transition-colors">Đăng Nhập</router-link>
            <router-link to="/register" class="px-6 py-2 bg-primary text-white text-sm font-semibold rounded-full hover:shadow-lg active:scale-95 transition-all">Đăng Ký</router-link>
          </div>
        </div>
      </div>
    </nav>

    <!-- Section 2: Hero Section -->
    <header class="relative min-h-screen flex items-center pt-16 hero-pattern overflow-hidden">
      <div class="max-w-7xl mx-auto px-6 grid grid-cols-1 lg:grid-cols-12 gap-12 items-center relative z-10">
        <div class="lg:col-span-7">
          <div class="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-primary-container/10 text-primary text-xs font-bold uppercase tracking-widest mb-6">
            <span class="w-2 h-2 rounded-full bg-primary animate-pulse"></span>
            Nền tảng học tập thế hệ mới
          </div>
          <h1 class="text-5xl lg:text-7xl font-extrabold tracking-tighter text-on-surface leading-[1.1] mb-6">
            Nâng tầm kiến thức cùng <span class="text-primary italic">Chuyên gia.</span>
          </h1>
          <p class="text-lg text-on-surface-variant max-w-xl leading-relaxed mb-10">
            Trải nghiệm lộ trình học tập được cá nhân hóa, tối ưu hóa bởi dữ liệu và sự dẫn dắt từ những giảng viên hàng đầu thế giới.
          </p>
          <div class="flex flex-col sm:flex-row gap-4 items-center">
            <button class="w-full sm:w-auto px-10 py-4 bg-primary text-on-primary font-bold rounded-xl shadow-xl hover:shadow-primary/20 hover:scale-[1.02] active:scale-95 transition-all">
              Khám phá khóa học
            </button>
            <div class="flex items-center gap-4 px-6 py-4 rounded-xl bg-white/50 backdrop-blur shadow-sm ghost-border cursor-pointer hover:bg-white transition-colors">
              <span class="material-symbols-outlined text-primary">play_circle</span>
              <span class="text-sm font-bold text-on-surface">Xem Trailer</span>
            </div>
          </div>
          <div class="mt-12 flex items-center gap-8 border-t border-surface-container-high pt-8">
            <div class="flex -space-x-3">
              <img class="w-10 h-10 rounded-full border-2 border-white object-cover shadow-sm" src="https://lh3.googleusercontent.com/aida-public/AB6AXuB0R9_igU4iDcaUgb2SQgc8D9vO63Tz9MtVEbUTwZOmvWoT5gtMZDX6nsHISb4q9knxNuD5LPer87AhXEv37KL7ekWimYdNt07-xU7JGXlWbUJH0L9QrDUuXOWx70J6Pw-ZvwVXqlJC0WBKD8wHHO4jwYKP44569L7u6Jqa40ptWfFcEgFkZL9I6vqhholGpNtmOalUh0e-V7dyzdVAJ2u006PZJV06v3cBz08PqWdTCwQ2Z8d6AslOCSYq1LDG_LEnxAxIy8MXJtU" alt="st">
              <img class="w-10 h-10 rounded-full border-2 border-white object-cover shadow-sm" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAqi2GzkPdJPF35OAwfXx1VdCt35FhYoft-QRsGRwpPH3B8lq25QnxmiNorNkAR9FLrJ8_aSwvmGr4mVxyZF5mNjBju7RrPYZmf84Xc9FP1MMi6jmByONnZ12O7V4aFTp7qDeix5ZGmPiAL4wxGGLyZKGaIsMOwtwOy3DBRP_naabk6Fs3cef1HuQSZRkH6V1hG_aHpvADNfWWxjKStirMj3ydR40F3YrH1rwGisMS1TyKi91NdSwYtp-va5NuSjMq_5YUhnwE-wNg" alt="st">
              <img class="w-10 h-10 rounded-full border-2 border-white object-cover shadow-sm" src="https://lh3.googleusercontent.com/aida-public/AB6AXuA0hl7HW0qI8szPNfEXsePPPlY-rI4lmWqZwHtPvyjvGpAJXDDoTRsru5-VNIMeGmFH_akKrSynU3jRfNnLEu1mqTU0Ow_clDy2ERt84bIW-OVYUgV6KjeCxHqZQeAbz60zGqmvKCptR2f-Brv0eM3mj345KMxVaOGxGF8MNhjWRt11WHkqHJBOLN2ZWY7hYcIAiySozSU_8OeVLuuYkjqr09j-WeGG2M8taf_RJ7UW8eHG0ZSktXRfslk60TeWvuas15u-UREu9gE" alt="st">
              <div class="w-10 h-10 rounded-full bg-surface-container-highest border-2 border-white flex items-center justify-center text-[10px] font-bold text-on-surface-variant">+15k</div>
            </div>
            <p class="text-sm font-medium text-on-surface-variant">
              <span class="text-on-surface font-bold">4.9/5</span> dựa trên 2,400+ đánh giá
            </p>
          </div>
        </div>

        <!-- Decorative Element (Asymmetric Grid) -->
        <div class="hidden lg:block lg:col-span-5 relative">
          <div class="absolute -top-20 -right-20 w-64 h-64 bg-primary-container/10 rounded-full blur-3xl"></div>
          <div class="grid grid-cols-2 gap-4">
            <div class="space-y-4 pt-12">
              <div class="h-64 rounded-2xl overflow-hidden shadow-2xl rotate-3 transition-transform hover:rotate-0">
                <img class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuBkm4fAVv1aBGcr6rRxaDqI1EGnLm4mJYAsfXnOLtXuhTbl1h2nqeAjFLaCxzp_b8XT30lm58_Opnmchoi8NDQy0-iAMHqXrs7c6YmGetmHk5ItgdH2mrdYKzo7ZFXcjhE3LyLdKOYy-XzI2CIx9I153GSZXHwN2_DyvKL-is6p4OQGfu228zp8U_pf6X4Wg_BA-Fej-JminuuLin0YLHfRmTriRVUnGDSTfOfoNQ37-4TJzGQYiPggR7BGyddgwIlRRH9dXe6ghNo" alt="st">
              </div>
              <div class="bg-primary p-6 rounded-2xl text-white shadow-xl -rotate-2">
                <span class="material-symbols-outlined text-4xl mb-4">auto_awesome</span>
                <h3 class="text-xl font-bold leading-tight">Học tập thông minh hơn</h3>
              </div>
            </div>
            <div class="space-y-4">
              <div class="bg-white p-6 rounded-2xl shadow-xl rotate-1">
                <div class="flex items-center gap-3 mb-4">
                  <div class="w-10 h-10 rounded-full bg-secondary-container/30 flex items-center justify-center">
                    <span class="material-symbols-outlined text-primary">verified_user</span>
                  </div>
                  <div class="h-2 w-20 bg-surface-container-high rounded"></div>
                </div>
                <div class="space-y-2">
                  <div class="h-3 w-full bg-surface-container-low rounded"></div>
                  <div class="h-3 w-2/3 bg-surface-container-low rounded"></div>
                </div>
              </div>
              <div class="h-80 rounded-2xl overflow-hidden shadow-2xl -rotate-2 transition-transform hover:rotate-0">
                <img class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuBPfuWDEhdm90lQOzRfHiqMbwV0AZXTswxFu-5p2Mz9nhdSgV9375rouPQka0LPi23Gl5dzH6BZp_dL32u1GprjV_4-C98ITUsYy5-j4YMy1HH4-7wgv-xKyc-ELEieiKbDa_RhQeVcI1xZEKdjnesZ9m4tNxYQys_HYgIlZNp5ApFNiBNnjY8hK3kz-TfIAfhx3PRpraD1WpVFuR-Efk8Skn7BplJZhaQ5SbXCtcuOqc9YB2CWiO9Xs89JpqLP5z-pJUhlsBbhiLA" alt="st">
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>

    <!-- Section 3: Stats Bar -->
    <section class="relative z-20 py-12 px-6">
      <div class="max-w-7xl mx-auto bg-white rounded-3xl shadow-xl flex flex-wrap items-center justify-around py-10 px-6 gap-8">
        <div class="text-center px-8">
          <div class="text-4xl font-black text-on-surface mb-1">25,000+</div>
          <div class="text-xs uppercase tracking-widest font-bold text-on-surface-variant">Học viên tích cực</div>
        </div>
        <div class="hidden md:block w-px h-16 bg-surface-container-highest"></div>
        <div class="text-center px-8">
          <div class="text-4xl font-black text-on-surface mb-1">1,200+</div>
          <div class="text-xs uppercase tracking-widest font-bold text-on-surface-variant">Khóa học chuyên sâu</div>
        </div>
        <div class="hidden md:block w-px h-16 bg-surface-container-highest"></div>
        <div class="text-center px-8">
          <div class="text-4xl font-black text-on-surface mb-1">450+</div>
          <div class="text-xs uppercase tracking-widest font-bold text-on-surface-variant">Giảng viên uy tín</div>
        </div>
      </div>
    </section>

    <!-- Section 4: Featured Categories -->
    <section class="py-20 px-6 bg-surface">
      <div class="max-w-7xl mx-auto">
        <div class="flex justify-between items-end mb-12">
          <div>
            <h2 class="text-3xl font-extrabold tracking-tight text-on-surface mb-2">Danh mục nổi bật</h2>
            <p class="text-on-surface-variant">Khám phá các lĩnh vực đang dẫn đầu xu hướng thị trường.</p>
          </div>
          <button class="text-primary font-bold text-sm flex items-center gap-2 hover:underline">
            Xem tất cả danh mục <span class="material-symbols-outlined text-sm">arrow_forward</span>
          </button>
        </div>
        <div class="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-4">
          <!-- Category Item -->
          <a v-for="cat in categories" :key="cat.name" class="group p-6 bg-white rounded-2xl ghost-border transition-all hover:bg-primary-container hover:shadow-2xl active:scale-95 flex flex-col items-center text-center" href="#">
            <div class="w-12 h-12 rounded-xl bg-surface-container-low flex items-center justify-center mb-4 group-hover:bg-white/20 transition-colors">
              <span class="material-symbols-outlined text-primary group-hover:text-white">{{ cat.icon }}</span>
            </div>
            <span class="font-bold text-sm text-on-surface group-hover:text-white">{{ cat.name }}</span>
          </a>
        </div>
      </div>
    </section>

    <!-- Section 5: Featured Courses -->
    <section class="py-24 px-6 bg-surface-container-low">
      <div class="max-w-7xl mx-auto">
        <div class="flex flex-col md:flex-row justify-between items-start md:items-end mb-16">
          <div class="max-w-2xl">
            <h2 class="text-4xl font-extrabold tracking-tight text-on-surface mb-4">Khóa học thịnh hành</h2>
            <p class="text-lg text-on-surface-variant">Bắt đầu hành trình chinh phục kỹ năng mới ngay hôm nay với những khóa học được đánh giá cao nhất.</p>
          </div>
          <div class="mt-6 md:mt-0 flex gap-2">
            <button class="p-3 rounded-xl bg-white ghost-border hover:bg-primary-container hover:text-white transition-all">
              <span class="material-symbols-outlined">chevron_left</span>
            </button>
            <button class="p-3 rounded-xl bg-white ghost-border hover:bg-primary-container hover:text-white transition-all">
              <span class="material-symbols-outlined">chevron_right</span>
            </button>
          </div>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          <!-- Course Card 1 -->
          <router-link to="/course/vcore-ai" v-for="course in courses" :key="course.title" class="bg-white rounded-2xl overflow-hidden editorial-shadow hover:-translate-y-2 transition-transform duration-300 block text-left">
            <div class="aspect-video relative overflow-hidden">
              <img class="w-full h-full object-cover" :src="course.image" :alt="course.title">
              <div v-if="course.tag" :class="['absolute px-2 py-1 rounded-md text-[10px] font-black uppercase tracking-widest', course.tag === 'Best Seller' ? 'top-3 left-3 bg-white/90 backdrop-blur text-primary' : 'top-3 right-3 bg-tertiary text-white']">
                {{ course.tag }}
              </div>
            </div>
            <div class="p-6">
              <div class="flex items-center gap-1 mb-2">
                <span class="material-symbols-outlined text-sm text-yellow-400">star</span>
                <span class="text-xs font-bold text-on-surface">{{ course.rating }}</span>
                <span class="text-[10px] text-on-surface-variant font-medium">({{ course.reviews }} đánh giá)</span>
              </div>
              <h3 class="font-bold text-on-surface leading-snug mb-3 line-clamp-2 hover:text-primary cursor-pointer transition-colors">
                {{ course.title }}
              </h3>
              <div class="flex items-center gap-2 mb-4">
                <img class="w-6 h-6 rounded-full object-cover" :src="course.instructorImage" :alt="course.instructor">
                <span class="text-xs font-medium text-on-surface-variant">{{ course.instructor }}</span>
              </div>
              <div class="flex items-center justify-between border-t border-surface-container pt-4">
                <div class="text-lg font-black text-primary">{{ course.price }}đ</div>
                <div class="text-xs text-on-surface-variant line-through">{{ course.oldPrice }}đ</div>
              </div>
            </div>
          </router-link>
        </div>
        <div class="mt-16 text-center">
          <a class="inline-flex items-center gap-2 text-primary font-bold text-lg group" href="#">
            Xem thêm khóa học 
            <span class="material-symbols-outlined transition-transform group-hover:translate-x-1">trending_flat</span>
          </a>
        </div>
      </div>
    </section>

    <!-- Section 6: Social Proof -->
    <section class="py-24 px-6 bg-white overflow-hidden">
      <div class="max-w-7xl mx-auto grid grid-cols-1 lg:grid-cols-12 gap-16 items-center">
        <div class="lg:col-span-4">
          <h2 class="text-4xl font-extrabold tracking-tight text-on-surface mb-6 leading-tight">Mọi người nói gì về <span class="text-primary">Curator?</span></h2>
          <p class="text-on-surface-variant mb-10 leading-relaxed">Tham gia cộng đồng học tập của chúng tôi và bắt đầu viết nên câu chuyện thành công của chính bạn.</p>
          <div class="flex items-center gap-4">
            <div class="flex -space-x-3">
              <div class="w-12 h-12 rounded-full border-4 border-white bg-surface-container flex items-center justify-center font-bold text-on-surface-variant">SC</div>
              <div class="w-12 h-12 rounded-full border-4 border-white bg-primary text-white flex items-center justify-center font-bold">TC</div>
            </div>
            <div>
              <div class="font-bold text-on-surface">2,400+</div>
              <div class="text-xs font-medium text-on-surface-variant">Học viên hài lòng</div>
            </div>
          </div>
        </div>
        <div class="lg:col-span-8 relative">
          <div class="absolute -top-10 -left-10 text-9xl text-surface-container font-black opacity-20 pointer-events-none">“</div>
          <div class="relative bg-surface p-10 lg:p-14 rounded-[2rem] editorial-shadow">
            <div class="flex items-center gap-1 mb-6">
              <span v-for="i in 5" :key="i" class="material-symbols-outlined text-yellow-500">star</span>
            </div>
            <blockquote class="text-2xl font-medium text-on-surface mb-10 leading-relaxed italic">
              "Khóa học tại Digital Curator thực sự thay đổi tư duy làm nghề của tôi. Nội dung được trình bày cực kỳ chuyên nghiệp và dễ hiểu, đúng chất editorial mà tôi tìm kiếm."
            </blockquote>
            <div class="flex items-center gap-4">
              <img class="w-14 h-14 rounded-full object-cover grayscale" src="https://lh3.googleusercontent.com/aida-public/AB6AXuC7i-EtP53c0pf8h_qButSVveHMQmwBKAlcdBOmrXrvK0mo_ftXf5YZkqT3r480ZfqMxwWSQpyrX7b1ZLK4AKSQv2oHAEJ2EreNFMAlSGl2lIocVK-LwzPaePi87ndjBbejKk6C0o-rlI7aEouS6kgwcOZkDO329sOUh6S0NGk2_IyL7XP8ub0SMRVwQJuxXxkiXfxDBydAPPPQGmu3qZOVIpUxSAaw8VatksYXpoxFDyzLVXD9F_UpYqpmRQvWNx2JgaVQ9-Bvi7g" alt="alt">
              <div>
                <div class="font-bold text-on-surface text-lg leading-none mb-1">Đặng Thu Trang</div>
                <div class="text-sm font-medium text-on-surface-variant">Creative Director tại VinGroup</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Section 7: Instructor CTA -->
    <section class="py-24 px-6">
      <div class="max-w-7xl mx-auto bg-primary rounded-[3rem] overflow-hidden relative shadow-2xl">
        <div class="absolute inset-0 opacity-10 pointer-events-none">
          <div class="absolute -top-24 -left-24 w-96 h-96 bg-white rounded-full blur-3xl"></div>
          <div class="absolute bottom-0 right-0 w-[50%] h-full bg-surface-container-low opacity-20 transform skew-x-12"></div>
        </div>
        <div class="relative z-10 grid grid-cols-1 lg:grid-cols-2 gap-12 p-12 lg:p-20 items-center">
          <div class="text-white">
            <h2 class="text-4xl lg:text-5xl font-black mb-6 leading-tight">Trở thành giảng viên của chúng tôi</h2>
            <p class="text-primary-container text-lg mb-10">Chia sẻ kiến thức của bạn với hàng triệu học viên và nhận được mức thu nhập xứng đáng cùng tầm ảnh hưởng quốc tế.</p>
            <ul class="space-y-4 mb-10">
              <li class="flex items-center gap-3">
                <span class="material-symbols-outlined text-primary-container">check_circle</span>
                <span class="font-medium">Nền tảng quản lý khóa học thông minh AI-Powered</span>
              </li>
              <li class="flex items-center gap-3">
                <span class="material-symbols-outlined text-primary-container">check_circle</span>
                <span class="font-medium">Đội ngũ hỗ trợ sản xuất nội dung chuyên nghiệp</span>
              </li>
              <li class="flex items-center gap-3">
                <span class="material-symbols-outlined text-primary-container">check_circle</span>
                <span class="font-medium">Mạng lưới học viên rộng khắp 20+ quốc gia</span>
              </li>
            </ul>
            <button class="px-10 py-4 bg-white text-primary font-black rounded-xl hover:bg-primary-container hover:text-white transition-all shadow-xl active:scale-95">
              Đăng ký giảng dạy ngay
            </button>
          </div>
          <div class="relative hidden lg:block">
            <div class="rounded-3xl overflow-hidden shadow-2xl transform rotate-2">
              <img class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCD6dg_h4OqThwvihLcaja5QRnHrn4o8de3AF6JZO5pmithnTlVKyAqHGhnwMYLIezcqCp9C2P-5G9HeY3VHETIGTP7daoYZWC_RNiULk3MGXz1x4tRjJR1IbX-WA6eYgCNk_gI79hA64OWFo2bg_vfruDv1U6czTregz4kqfnzwqSOZnrupwOf0ZlsXt7Or9f9yw1U_KU-WNVCVmmd7FEq831ngCWIXXBcfLnqi6_hcT0q6xQfJJnNDSW6NK3FJzP8f7M2S5UeMJw" alt="alt">
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Section 8: Footer -->
    <footer class="bg-slate-50 dark:bg-slate-950 pt-20 border-t-0">
      <div class="max-w-7xl mx-auto px-8 grid grid-cols-1 md:grid-cols-4 gap-12 pb-16">
        <div class="space-y-6">
          <div class="text-xl font-black text-slate-900 dark:text-white">AET LMS</div>
          <p class="text-slate-500 text-sm leading-relaxed">Nền tảng giáo dục số hàng đầu tập trung vào trải nghiệm người dùng và chất lượng chuyên môn cao nhất.</p>
          <div class="flex gap-4">
            <a class="w-10 h-10 rounded-full bg-slate-200 flex items-center justify-center hover:bg-primary hover:text-white transition-colors" href="#">
              <span class="material-symbols-outlined text-sm">share</span>
            </a>
            <a class="w-10 h-10 rounded-full bg-slate-200 flex items-center justify-center hover:bg-primary hover:text-white transition-colors" href="#">
              <span class="material-symbols-outlined text-sm">groups</span>
            </a>
          </div>
        </div>
        <div>
          <h4 class="font-bold text-slate-900 mb-6 text-sm uppercase tracking-widest">Khám phá</h4>
          <ul class="space-y-4">
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Về chúng tôi</a></li>
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Thư viện khóa học</a></li>
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Cộng đồng</a></li>
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Tin tức & Sự kiện</a></li>
          </ul>
        </div>
        <div>
          <h4 class="font-bold text-slate-900 mb-6 text-sm uppercase tracking-widest">Hỗ trợ</h4>
          <ul class="space-y-4">
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Trung tâm trợ giúp</a></li>
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Liên hệ trực tiếp</a></li>
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Chính sách bảo mật</a></li>
            <li><a class="text-slate-500 hover:text-primary text-sm font-medium transition-colors" href="#">Điều khoản dịch vụ</a></li>
          </ul>
        </div>
        <div>
          <h4 class="font-bold text-slate-900 mb-6 text-sm uppercase tracking-widest">Bản tin</h4>
          <p class="text-slate-500 text-xs mb-4 font-medium uppercase tracking-widest leading-loose">Nhận cập nhật về khóa học mới nhất và khuyến mãi hàng tuần.</p>
          <form class="space-y-2">
            <input class="w-full bg-white border-none rounded-lg p-3 text-sm focus:ring-2 focus:ring-primary outline-none" placeholder="Email của bạn" type="email">
            <button class="w-full bg-primary text-white py-3 rounded-lg font-bold text-sm hover:opacity-90 transition-opacity">Đăng ký</button>
          </form>
        </div>
      </div>
      <div class="border-t border-slate-200 py-8 px-8">
        <div class="max-w-7xl mx-auto flex flex-col md:flex-row justify-between items-center gap-6">
          <div class="font-headline text-xs uppercase tracking-widest font-semibold text-slate-500">
            © 2026 AET LMS. All educational rights reserved.
          </div>
          <div class="flex gap-8">
            <a class="font-headline text-xs uppercase tracking-widest font-semibold text-slate-500 hover:text-blue-700 transition-colors" href="#">Privacy Policy</a>
            <a class="font-headline text-xs uppercase tracking-widest font-semibold text-slate-500 hover:text-blue-700 transition-colors" href="#">Terms of Service</a>
          </div>
        </div>
      </div>
    </footer>
  </div>
</template>

<style scoped>
/* Scoped styles can be minimal since most are in global CSS or utility classes */
</style>
