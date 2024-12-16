<template>
    <section class="flat-spacing-15">
        <div class="container-full">
            <div class="flat-title flex-row justify-content-between px-0">
                <span class="title wow fadeInUp" data-wow-delay="0s">Featured Collections</span>
                <div class="box-sw-navigation">
                    <div class="nav-sw nav-next-slider nav-next-collection"><span class="icon icon-arrow-left"></span>
                    </div>
                    <div class="nav-sw nav-prev-slider nav-prev-collection"><span class="icon icon-arrow-right"></span>
                    </div>
                </div>
            </div>
            <Swiper class="swiper tf-sw-collection sw-wrapper-right" :modules="[Navigation, Pagination, Autoplay]"
                :autoplay="autoplayConfig" :slides-per-view="slidesPerView.mobile" :loop="loop"
                :space-between="spacing.mobile" :speed="speed"
                :pagination="{ el: '.sw-pagination-collection', clickable: true }"
                :navigation="{ prevEl: '.nav-prev-collection', nextEl: '.nav-next-collection' }"
                :breakpoints="breakpoints" :slides-per-group="slidesPerGroup">
                <!-- Slides -->
                <SwiperSlide v-for="(item, index) in collections" :key="index">
                    <div class="collection-item-v3 hover-img">
                        <a :href="item.link" class="collection-image img-style">
                            <img class="lazyload" :data-src="item.imgSrc" :src="item.imgSrc" :alt="item.alt" />
                            <span class="box-icon">
                                <i class="icon-icon icon-arrow1-top-left"></i>
                            </span>
                        </a>
                        <div class="collection-content">
                            <a :href="item.link" class="link title fw-5">{{ item.title }}</a>
                            <div class="count">{{ item.count }}</div>
                        </div>
                    </div>
                </SwiperSlide>
            </Swiper>
        </div>
    </section>
</template>
<script setup>
import { ref } from 'vue';
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Navigation, Autoplay, Pagination } from 'swiper/modules';


const collections = ref([
    {
        imgSrc: "/images/collections/collection-32.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Blazers",
        count: "14 items",
    },
    {
        imgSrc: "/images/collections/collection-33.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Sweaters",
        count: "23 items",
    },
    {
        imgSrc: "/images/collections/collection-34.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Sneakers",
        count: "21 items",
    },
    {
        imgSrc: "/images/collections/collection-35.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Clothing",
        count: "31 items",
    },
    {
        imgSrc: "/images/collections/collection-36.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Accessories",
        count: "14 items",
    },
    {
        imgSrc: "/images/collections/collection-37.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Men",
        count: "9 items",
    },
    {
        imgSrc: "/images/collections/collection-38.jpg",
        alt: "collection-img",
        link: "shop-collection-sub.html",
        title: "Shoes",
        count: "21 items",
    },
]);

// Swiper settings
const loop = ref(false); // Matches `data-loop`
const speed = ref(1000); // Matches `data-speed`
const slidesPerGroup = ref(1); // Matches `slidesPerGroup`
const spacing = ref({
    mobile: 15, // Matches `data-space`
    tablet: 30, // Matches `data-space-md`
    desktop: 30, // Matches `data-space-lg`
});
const slidesPerView = ref({
    mobile: 2.4, // Matches `data-mobile`
    tablet: 2.4, // Matches `data-tablet`
    desktop: 4.5, // Matches `data-preview`
});
const autoplayConfig = ref(false); // Matches `data-auto-play` (can be set to `false`)
const breakpoints = ref({
    768: {
        slidesPerView: slidesPerView.value.tablet, // Matches `data-tablet`
        spaceBetween: spacing.value.tablet, // Matches `data-space-md`
        slidesPerGroup: 2,
    },
    1150: {
        slidesPerView: slidesPerView.value.desktop, // Matches `data-preview`
        spaceBetween: spacing.value.desktop, // Matches `data-space-lg`
        slidesPerGroup: 2,
    },
});

</script>
