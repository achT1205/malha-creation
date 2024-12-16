<template>
    <section class="flat-spacing-9 flat-iconbox-v2">
        <div class="container">
            <div class="wrap-carousel wrap-mobile wow fadeInUp" data-wow-delay="0s">
                <Swiper class="swiper tf-sw-mobile" v-if="isMobile" :modules="[Pagination, Navigation]"
                    :slides-per-view="slidesPerView" :space-between="spaceBetween" :speed="speed"
                    :pagination="{ el: '.sw-pagination-mb', clickable: true }"
                    :navigation="{ prevEl: '.nav-prev-mb', nextEl: '.nav-next-mb' }">
                    <SwiperSlide v-for="(item, index) in slidesIcons" :key="index">
                        <div class="tf-icon-box text-center">
                            <div class="icon">
                                <i :class="item.icon"></i>
                            </div>
                            <div class="content">
                                <div class="title">{{ item.title }}</div>
                                <p>{{ item.description }}</p>
                            </div>
                        </div>
                    </SwiperSlide>
                </Swiper>
                <div v-else dir="ltr" class="swiper tf-sw-mobile" data-preview="1" data-space="15">
                    <div class="swiper-wrapper wrap-iconbox">
                        <div class="swiper-slide">
                            <div class="tf-icon-box text-center">
                                <div class="icon">
                                    <i class="icon-shipping-1"></i>
                                </div>
                                <div class="content">
                                    <div class="title">Free Shipping</div>
                                    <p>Free shipping over order $120</p>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-slide">
                            <div class="tf-icon-box text-center">
                                <div class="icon">
                                    <i class="icon-payment-1"></i>
                                </div>
                                <div class="content">
                                    <div class="title">Flexible Payment</div>
                                    <p>Pay with Multiple Credit Cards</p>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-slide">
                            <div class="tf-icon-box text-center">
                                <div class="icon">
                                    <i class="icon-return-1"></i>
                                </div>
                                <div class="content">
                                    <div class="title">14 Day Returns</div>
                                    <p>Within 30 days for an exchange</p>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="sw-dots style-2 sw-pagination-mb justify-content-center"></div>
            </div>
        </div>
    </section>
</template>
<script setup>
import { ref, watch, onMounted, onUnmounted } from 'vue';
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Navigation, Pagination } from 'swiper/modules';

const speed = ref(1000);

const slidesIcons = ref([
    {
        icon: "icon-shipping-1",
        title: "Free Shipping",
        description: "Free shipping over order $120",
    },
    {
        icon: "icon-payment-1",
        title: "Flexible Payment",
        description: "Pay with Multiple Credit Cards",
    },
    {
        icon: "icon-return-1",
        title: "14 Day Returns",
        description: "Within 30 days for an exchange",
    },
]);

const slidesPerView = ref(1);
const spaceBetween = ref(15);
const isMobile = ref(window.matchMedia("only screen and (max-width: 767px)").matches);

// Function to handle resize and update mobile state
const handleResize = () => {
    isMobile.value = window.matchMedia("only screen and (max-width: 767px)").matches;
};

// Watch for `isMobile` state change to handle initialization/destruction of Swiper
watch(isMobile, (mobile) => {
    if (!mobile) {
        // Reset Swiper-specific styles when destroyed
        const swiperWrapper = document.querySelector(".tf-sw-mobile .swiper-wrapper");
        const swiperSlides = document.querySelectorAll(".tf-sw-mobile .swiper-slide");

        if (swiperWrapper) swiperWrapper.removeAttribute("style");
        if (swiperSlides) swiperSlides.forEach((slide) => slide.removeAttribute("style"));
    }
});

onMounted(() => {
    window.addEventListener("resize", handleResize);
});

onUnmounted(() => {
    window.removeEventListener("resize", handleResize);
});

</script>
