<template>
    <section class="tf-slideshow slider-effect-fade slider-home-5 position-relative">
        <Swiper class="swiper tf-sw-slideshow" :modules="[Autoplay, Pagination, Navigation]" :autoplay="autoplayConfig"
            :slides-per-view="slidesPerView.mobile" :loop="loop" :space-between="spaceBetween" :speed="speed"
            :pagination="{ el: '.sw-pagination-slider', clickable: true }"
            :navigation="{ nextEl: '.navigation-next-slider', prevEl: '.navigation-prev-slider' }"
            :centered-slides="centeredSlides" :breakpoints="breakpoints">
            <SwiperSlide v-for="(slide, index) in slides" :key="index">
                <div class="wrap-slider">
                    <img class="lazyload" :data-src="slide.imgSrc" :src="slide.imgSrc" :alt="slide.alt" />
                    <div class="box-content text-center">
                        <div class="container">
                            <h1 class="fade-item fade-item-1 text-white heading">{{ slide.title }}</h1>
                            <p class="fade-item fade-item-2 text-white">{{ slide.description }}</p>
                            <router-link :to="`collections/${slide.slug}`"
                                class="fade-item fade-item-3 tf-btn btn-light-icon animate-hover-btn btn-xl radius-3">
                                <span>Shop collection</span><i class="icon icon-arrow-right"></i>
                            </router-link>
                        </div>
                    </div>
                </div>
            </SwiperSlide>
        </Swiper>

        <!-- Pagination -->
        <div class="wrap-pagination">
            <div class="sw-dots style-2 dots-white sw-pagination-slider justify-content-center"></div>
        </div>
    </section>
</template>
<script setup>
import { ref } from 'vue';
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Navigation, Autoplay, Pagination } from 'swiper/modules';
import { RouterLink } from 'vue-router'


const slides = ref([
    {
        imgSrc: "https://placehold.co/2000x1125",
        alt: "Elegant Kabil Dresses for Women",
        title: "Graceful Kabil Dresses",
        description: "Discover our exclusive collection of Kabil dresses for women, perfect for every occasion.",
        slug: "kabil-dresses-women",
    },
    {
        imgSrc: "https://placehold.co/2000x1125",
        alt: "Traditional Burno for Men",
        title: "Classic Burno for Men",
        description: "Explore our timeless Burno attire for men, crafted with premium fabrics and elegant designs.",
        slug: "burno-men",
    },
    {
        imgSrc: "https://placehold.co/2000x1125",
        alt: "Kabil Dresses & Burno for Kids",
        title: "Stylish Attire for Children",
        description: "Shop our adorable Kabil dresses and Burno for children, combining comfort and style.",
        slug: "kabil-burno-children",
    }
]);


const speed = ref(1000);
const slidesPerView = ref(1);
const loop = ref(true);
const spaceBetween = ref(0);
const autoplayConfig = ref({
    delay: 2000,
    tablet: 1,
    mobile: 1,
    disableOnInteraction: false,
    pauseOnMouseEnter: true,
});
const centeredSlides = ref(false);

const breakpoints = ref({
    768: {
        slidesPerView: 1,
        spaceBetween: spaceBetween.value,
        centeredSlides: false,
    },
    1150: {
        slidesPerView: 1,
        spaceBetween: spaceBetween.value,
        centeredSlides: centeredSlides.value,
    },
});

</script>
