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
                            <a :href="slide.link"
                                class="fade-item fade-item-3 tf-btn btn-light-icon animate-hover-btn btn-xl radius-3">
                                <span>Shop collection</span><i class="icon icon-arrow-right"></i>
                            </a>
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


const slides = ref([
    {
        imgSrc: "/images/slider/fashion-slideshow-10.jpg",
        alt: "fashion-slideshow-01",
        title: "Effortless Elegance",
        description: "From casual to formal, we've got you covered",
        link: "shop-collection-list.html",
    },
    {
        imgSrc: "/images/slider/fashion-slideshow-11.jpg",
        alt: "fashion-slideshow-02",
        title: "The mystery behind",
        description: "From casual to formal, we've got you covered",
        link: "shop-collection-list.html",
    },
    {
        imgSrc: "/images/slider/fashion-slideshow-12.jpg",
        alt: "fashion-slideshow-03",
        title: "Multi-faceted Beauty",
        description: "From casual to formal, we've got you covered",
        link: "shop-collection-list.html",
    },
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
