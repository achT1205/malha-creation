<template>
    <div id="wrapper">
        <!-- Announcement Bar -->
        <!--<AnnouncementBaner />-->
        <!-- /Announcement Bar -->
         
        <!-- Hero -->
        <Hero />
        <!-- /Hero -->

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

        <!-- gotop -->
        <div class="progress-wrap">
            <svg class="progress-circle svg-content" width="100%" height="100%" viewBox="-1 -1 102 102">
                <path d="M50,1 a49,49 0 0,1 0,98 a49,49 0 0,1 0,-98"
                    style="transition: stroke-dashoffset 10ms linear 0s; stroke-dasharray: 307.919, 307.919; stroke-dashoffset: 286.138;">
                </path>
            </svg>
        </div>
        <!-- /gotop -->

        <!-- toolbar-bottom -->
        <BottomToolbar />
        <!-- /toolbar-bottom -->

        <!-- mobile menu -->
        <MobileMenu />
        <!-- /mobile menu -->

        <!-- canvasSearch -->
        <SearchCanvas />
        <!-- /canvasSearch -->

        <!-- toolbarShopmb -->
        <ShopmbToolbar />
        <!-- /toolbarShopmb -->

        <!-- modal login -->
        <LoginModal />
        <!-- /modal login -->

        <!-- shoppingCart -->
        <ShoppingCartModal />
        <!-- /shoppingCart -->

        <!-- modal compare -->
        <CompareModal />
        <!-- /modal compare -->

        <!-- modal quick_add -->
        <QuickAddModal />
        <!-- /modal quick_add -->

        <!-- modal quick_view -->
        <QuickViewModal />
        <!-- /modal quick_view -->

        <!-- modal find_size -->
        <div class="modal fade modalDemo tf-product-modal" id="find_size">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="header">
                        <div class="demo-title">Size chart</div>
                        <span class="icon-close icon-close-popup" data-bs-dismiss="modal"></span>
                    </div>
                    <div class="tf-rte">
                        <div class="tf-table-res-df">
                            <h6>Size guide</h6>
                            <table class="tf-sizeguide-table">
                                <thead>
                                    <tr>
                                        <th>Size</th>
                                        <th>US</th>
                                        <th>Bust</th>
                                        <th>Waist</th>
                                        <th>Low Hip</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>XS</td>
                                        <td>2</td>
                                        <td>32</td>
                                        <td>24 - 25</td>
                                        <td>33 - 34</td>
                                    </tr>
                                    <tr>
                                        <td>S</td>
                                        <td>4</td>
                                        <td>34 - 35</td>
                                        <td>26 - 27</td>
                                        <td>35 - 26</td>
                                    </tr>
                                    <tr>
                                        <td>M</td>
                                        <td>6</td>
                                        <td>36 - 37</td>
                                        <td>28 - 29</td>
                                        <td>38 - 40</td>
                                    </tr>
                                    <tr>
                                        <td>L</td>
                                        <td>8</td>
                                        <td>38 - 29</td>
                                        <td>30 - 31</td>
                                        <td>42 - 44</td>
                                    </tr>
                                    <tr>
                                        <td>XL</td>
                                        <td>10</td>
                                        <td>40 - 41</td>
                                        <td>32 - 33</td>
                                        <td>45 - 47</td>
                                    </tr>
                                    <tr>
                                        <td>XXL</td>
                                        <td>12</td>
                                        <td>42 - 43</td>
                                        <td>34 - 35</td>
                                        <td>48 - 50</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tf-page-size-chart-content">
                            <div>
                                <h6>Measuring Tips</h6>
                                <div class="title">Bust</div>
                                <p>Measure around the fullest part of your bust.</p>
                                <div class="title">Waist</div>
                                <p>Measure around the narrowest part of your torso.</p>
                                <div class="title">Low Hip</div>
                                <p class="mb-0">With your feet together measure around the fullest part of your
                                    hips/rear.
                                </p>
                            </div>
                            <div>
                                <img class="sizechart lazyload" data-src="/images/shop/products/size_chart2.jpg"
                                    src="/images/shop/products/size_chart2.jpg" alt="">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /modal find_size -->

    </div>
</template>
<script setup>

import AnnouncementBaner from '@/components/AnnouncementBaner.vue'
import Hero from '@/components/Hero.vue'
import CollectionSlider from '@/components/CollectionSlider.vue'
import ProductSlider from '@/components/ProductSlider.vue'
import PromotionCTA from '@/components/PromotionCTA.vue'
import FeaturesSlider from '@/components/FeaturesSlider.vue'
import QuickViewModal from '@/components/QuickViewModal.vue'
import QuickAddModal from '@/components/QuickAddModal.vue'
import CompareModal from '@/components/CompareModal.vue'
import ShoppingCartModal from '@/components/ShoppingCartModal.vue'
import LoginModal from '@/components/LoginModal.vue'
import ShopmbToolbar from '@/components/ShopmbToolbar.vue'
import SearchCanvas from '@/components/SearchCanvas.vue'
import MobileMenu from '@/components/MobileMenu.vue'
import BottomToolbar from '@/components/BottomToolbar.vue'
</script>