<template>
    <Swiper class="swiper tf-sw-collection" :modules="[Navigation, Pagination, Autoplay]"
        :slides-per-view="slidesPerView.mobile" :space-between="spacing.mobile" :loop="loop" :autoplay="autoplayConfig"
        :speed="speed" :slides-per-group="1" :pagination="{ el: '.sw-pagination-collection', clickable: true }"
        :navigation="{ prevEl: '.nav-prev-collection', nextEl: '.nav-next-collection' }" :breakpoints="breakpoints">
        <!-- Slides -->
        <SwiperSlide v-for="(item, index) in collections" :key="index">
            <div class="collection-item style-2 hover-img">
                <div class="collection-inner">
                    <a :href="item.link" class="collection-image img-style">
                        <img class="lazyload" :data-src="item.image" :src="item.image" :alt="item.alt" />
                    </a>
                    <div class="collection-content">
                        <a :href="item.link" class="tf-btn collection-title hover-icon fs-15">
                            <span>{{ item.title }}</span>
                            <i class="icon icon-arrow1-top-left"></i>
                        </a>
                    </div>
                </div>
            </div>
        </SwiperSlide>
    </Swiper>
</template>
<script setup>
import { ref } from "vue";
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation, Pagination, Autoplay } from "swiper/modules";

const collections = ref([
    {
        image: "/images/collections/collection-14.jpg",
        title: "Accessories",
        alt: "collection-img",
        link: "shop-default.html",
    },
    {
        image: "/images/collections/collection-15.jpg",
        title: "Men",
        alt: "collection-img",
        link: "shop-default.html",
    },
    {
        image: "/images/collections/collection-16.jpg",
        title: "Shoes",
        alt: "collection-demo-1",
        link: "shop-default.html",
    },
    {
        image: "/images/collections/collection-17.jpg",
        title: "New Arrival",
        alt: "collection-demo-1",
        link: "shop-default.html",
    },
    {
        image: "/images/collections/collection-18.jpg",
        title: "Handbag",
        alt: "collection-demo-1",
        link: "shop-default.html",
    },
    {
        image: "/images/collections/collection-19.jpg",
        title: "Women",
        alt: "collection-demo-1",
        link: "shop-default.html",
    },
]);

// Swiper Configuration
const slidesPerView = ref({
    mobile: 2,
    tablet: 3,
    desktop: 5,
});

const spacing = ref({
    mobile: 15,
    tablet: 30,
    desktop: 30,
});

const loop = ref(false); // Matches `data-loop`
const autoplayConfig = ref(false); // Matches `data-auto-play` as false
const speed = ref(1000);

// Breakpoints Configuration
const breakpoints = ref({
    768: {
        slidesPerView: slidesPerView.value.tablet,
        spaceBetween: spacing.value.tablet,
        slidesPerGroup: 2,
    },
    1150: {
        slidesPerView: slidesPerView.value.desktop,
        spaceBetween: spacing.value.desktop,
        slidesPerGroup: 2,
    },
});
</script>