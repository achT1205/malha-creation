<template>
    <div class="announcement-bar bg_orange-2">
        <div class="px_15 lg-px_40">
            <div class="row justify-content-center">
                <div class="col-xl-3 col-md-4 col-12 text-center wrap-announcement-bar-2">
                    <Swiper class="swiper tf-sw-top_bar" :modules="[Navigation, Autoplay]"
                        :slides-per-view="slidesPerView" :loop="loop" :speed="speed" :breakpoints="breakpoints"
                        :space-between="spaceBetween" :autoplay="autoplayConfig"
                        :navigation="{ prevEl: '.nav-prev-topbar', nextEl: '.nav-next-topbar' }">
                        <SwiperSlide v-for="(slide, index) in slides" :key="index">
                            <p class="noti-bar-text text-white fw-5">{{ slide }}</p>
                        </SwiperSlide>
                    </Swiper>

                    <div class="navigation-topbar nav-next-topbar"><span class="icon icon-arrow-left"></span>
                    </div>
                    <div class="navigation-topbar nav-prev-topbar"><span class="icon icon-arrow-right"></span>
                    </div>
                </div>
            </div>
            <span class="icon-close close-announcement-bar"></span>
        </div>
    </div>
</template>
<script setup>
import { ref } from 'vue';
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Navigation, Autoplay } from 'swiper/modules';


const slides = ref([
    'Welcome to our store',
    'The best HTML theme',
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
