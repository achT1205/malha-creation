<template>
    <div id="wrapper">
        <!-- Announcement Bar -->
        <AnnouncementBaner />
        <!-- /Announcement Bar -->
        <!-- Header -->
        <AppHeader />
        <!-- /Header -->
        <!-- Slider -->
        <Hero />
        <!-- /Slider -->
        <!-- Collection -->
        <CollectionSlider />
        <!-- /Collection -->
        <!-- Product -->
        <ProductSlider />
        <!-- /Product -->
        <!-- Banner Countdown -->
        <PromotionCTA />
        <!-- /Banner Countdown -->
        <!-- FeaturesSlider box -->
        <FeaturesSlider />
        <!-- /FeaturesSlider box -->
    </div>
</template>
<script setup>
import '@/assets/css/swiper-bundle.min.css'

import AnnouncementBaner from '@/components/AnnouncementBaner.vue'
import AppHeader from '@/components/AppHeader.vue'
import Hero from '@/components/Hero.vue'
import CollectionSlider from '@/components/CollectionSlider.vue'
import ProductSlider from '@/components/ProductSlider.vue'
import PromotionCTA from '@/components/PromotionCTA.vue'
import FeaturesSlider from '@/components/FeaturesSlider.vue'
</script>