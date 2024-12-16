<template>
    <section class="flat-spacing-6 pt_0">
        <div class="container">
            <div class="flat-title wow fadeInUp" data-wow-delay="0s">
                <span class="title">Editor's Picks</span>
                <div class="d-flex gap-16 align-items-center box-pagi-arr">
                    <div class="nav-sw-arrow nav-next-slider nav-next-product"><span
                            class="icon icon-arrow1-left"></span></div>
                    <a href="product-style-05.html" class="tf-btn btn-line fs-12 fw-6">VIEW ALL</a>
                    <div class="nav-sw-arrow nav-prev-slider nav-prev-product"><span
                            class="icon icon-arrow1-right"></span></div>
                </div>
            </div>
            <div class="hover-sw-nav hover-sw-2">
                <Swiper class="swiper tf-sw-product-sell wrap-sw-over" :modules="[Navigation, Pagination]"
                    :slides-per-view="slidesPerView.mobile" :space-between="spacing.mobile" :speed="speed"
                    :pagination="{ el: '.sw-pagination-product', clickable: true }"
                    :navigation="{ prevEl: '.nav-prev-product', nextEl: '.nav-next-product' }"
                    :slides-per-group="slidesPerGroup.mobile" :breakpoints="breakpoints">
                    <SwiperSlide v-for="(product, index) in slides" :key="index">
                        <div class="card-product style-5">
                            <div class="card-product-wrapper">
                                <a :href="product.detailLink" class="product-img">
                                    <img class="lazyload img-product" :data-src="product.imgSrc" :src="product.imgSrc"
                                        :alt="product.alt" />
                                    <img class="lazyload img-hover" :data-src="product.imgHoverSrc"
                                        :src="product.imgHoverSrc" :alt="product.alt" />
                                </a>
                                <div class="list-product-btn gap-0">
                                    <a href="#quick_add" data-bs-toggle="modal"
                                        class="box-icon bg_white quick-add tf-btn-loading shadow-none">
                                        <span class="icon icon-bag"></span>
                                        <span class="tooltip">Quick Add</span>
                                    </a>
                                    <a href="javascript:void(0);"
                                        class="box-icon bg_white wishlist btn-icon-action shadow-none">
                                        <span class="icon icon-heart"></span>
                                        <span class="tooltip">Add to Wishlist</span>
                                        <span class="icon icon-delete"></span>
                                    </a>
                                    <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                        class="box-icon bg_white compare btn-icon-action shadow-none">
                                        <span class="icon icon-compare"></span>
                                        <span class="tooltip">Add to Compare</span>
                                        <span class="icon icon-check"></span>
                                    </a>
                                    <a href="#quick_view" data-bs-toggle="modal"
                                        class="box-icon bg_white quickview tf-btn-loading shadow-none">
                                        <span class="icon icon-view"></span>
                                        <span class="tooltip">Quick View</span>
                                    </a>
                                </div>
                                <div class="size-list style-3">
                                    <span>S</span>
                                    <span>M</span>
                                    <span>L</span>
                                    <span>XL</span>
                                </div>
                            </div>
                            <div class="card-product-info">
                                <a href="product-detail.html" class="title link">Ribbed Tank Top</a>
                                <span class="price">$16.95</span>
                                <ul class="list-color-product">
                                    <li class="list-color-item color-swatch active">
                                        <span class="tooltip">Orange</span>
                                        <span class="swatch-value bg_orange-3"></span>
                                        <img class="lazyload" data-src="/images/products/orange-1.jpg"
                                            src="/images/products/orange-1.jpg" alt="image-product">
                                    </li>
                                    <li class="list-color-item color-swatch">
                                        <span class="tooltip">Black</span>
                                        <span class="swatch-value bg_dark"></span>
                                        <img class="lazyload" data-src="/images/products/black-1.jpg"
                                            src="/images/products/black-1.jpg" alt="image-product">
                                    </li>
                                    <li class="list-color-item color-swatch">
                                        <span class="tooltip">White</span>
                                        <span class="swatch-value bg_white"></span>
                                        <img class="lazyload" data-src="/images/products/white-1.jpg"
                                            src="/images/products/white-1.jpg" alt="image-product">
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </SwiperSlide>
                </Swiper>
                <div class="nav-sw nav-next-slider nav-next-product box-icon w_46 round"><span
                        class="icon icon-arrow-left"></span></div>
                <div class="nav-sw nav-prev-slider nav-prev-product box-icon w_46 round"><span
                        class="icon icon-arrow-right"></span></div>
            </div>
        </div>
    </section>
</template>
<script setup>
import { ref } from 'vue';
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Navigation, Pagination } from 'swiper/modules';


const slides = ref([
    {
        imgSrc: "/images/products/orange-1.jpg",
        imgHoverSrc: "/images/products/white-1.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Ribbed Tank Top",
        price: "$16.95",
    },
    {
        imgSrc: "/images/products/brown.jpg",
        imgHoverSrc: "/images/products/purple.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Ribbed modal T-shirt",
        price: "From $18.95",
    },
    {
        imgSrc: "/images/products/white-3.jpg",
        imgHoverSrc: "/images/products/white-4.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Oversized Printed T-shirt",
        price: "$10.00",
    },
    {
        imgSrc: "/images/products/white-2.jpg",
        imgHoverSrc: "/images/products/pink-1.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Oversized Printed T-shirt",
        price: "$16.95",
    },
    {
        imgSrc: "/images/products/brown.jpg",
        imgHoverSrc: "/images/products/purple.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Ribbed modal T-shirt",
        price: "From $18.95",
    },
    {
        imgSrc: "/images/products/white-3.jpg",
        imgHoverSrc: "/images/products/white-4.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Oversized Printed T-shirt",
        price: "$10.00",
    },
    {
        imgSrc: "/images/products/white-2.jpg",
        imgHoverSrc: "/images/products/pink-1.jpg",
        alt: "image-product",
        detailLink: "product-detail.html",
        title: "Oversized Printed T-shirt",
        price: "$16.95",
    },
]);

const slidesPerView = ref({
    mobile: 2,
    tablet: 3,
    desktop: 4,
});
const spacing = ref({
    mobile: 15,
    tablet: 15,
    desktop: 30,
});
const slidesPerGroup = ref({
    mobile: 2,
    tablet: 3,
    desktop: 3,
});

const breakpoints = ref({
    768: {
        slidesPerView: slidesPerView.value.tablet,
        spaceBetween: spacing.value.tablet,
        slidesPerGroup: slidesPerGroup.value.tablet,
    },
    1150: {
        slidesPerView: slidesPerView.value.desktop,
        spaceBetween: spacing.value.desktop,
        slidesPerGroup: slidesPerGroup.value.desktop,
    },
});
const speed = ref(1000);

</script>
