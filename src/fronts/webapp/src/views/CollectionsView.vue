<template>
    <div id="wrapper">
        <!-- page-title -->
        <div class="tf-page-title">
            <div class="container-full">
                <div class="heading text-center">New Arrival</div>
                <p class="text-center text-2 text_black-2 mt_5">Shop through our latest selection of Fashion</p>
            </div>
        </div>
        <!-- /page-title -->
         
        <!-- Collection -->
        <section class="flat-spacing-3 pb_0">
            <div class="container">
                <div class="hover-sw-nav">
                    <CollectionsSlider />
                    <div class="nav-sw nav-next-slider nav-next-collection box-icon w_46 round"><span
                            class="icon icon-arrow-left"></span></div>
                    <div class="nav-sw nav-prev-slider nav-prev-collection box-icon w_46 round"><span
                            class="icon icon-arrow-right"></span></div>
                    <div class="sw-dots style-2 sw-pagination-collection justify-content-center"></div>
                </div>
            </div>
        </section>
        <!-- /Collection -->

        <!-- Section Product -->
        <ProductList />
        <!-- /Section Product -->

        <!-- Filter -->
        <FilterCanvas />
        <!-- End Filter -->

        <!-- modal compare -->
        <CompareModal />
        <!-- /modal compare -->

        <!-- modal quick_add -->
        <QuickAddModal />
        <!-- /modal quick_add -->

        <!-- modal quick_view -->
        <QuickViewModal />
        <!-- /modal quick_view -->
    </div>
</template>
<script setup>
import CollectionsSlider from '@/components/Collections.vue'
import QuickViewModal from '@/components/QuickViewModal2.vue'
import QuickAddModal from '@/components/QuickAddModal.vue'
import CompareModal from '@/components/CompareModal.vue'
import ProductList from '@/components/ProductList.vue'
import FilterCanvas from '@/components/FilterCanvas.vue'
import { ref } from 'vue';

</script>
<style scoped>
.tf-page-title {
    margin-top: 94px;
}
</style>