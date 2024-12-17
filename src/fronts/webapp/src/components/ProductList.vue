<template>
    <section class="flat-spacing-2">
        <div class="container">
            <div class="tf-shop-control grid-3 align-items-center">
                <div class="tf-control-filter">
                    <a href="#filterShop" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                        class="tf-btn-filter"><span class="icon icon-filter"></span><span class="text">Filter</span></a>
                </div>
                <ul class="tf-control-layout d-flex justify-content-center">
                    <li @click="onSelectLayout('grid-2')"
                        :class="['tf-view-layout-switch sw-layout-2', selectedLayout === 'grid-2' ? 'active' : '']"
                        data-value-grid="grid-2">
                        <div class="item"><span class="icon icon-grid-2"></span></div>
                    </li>
                    <li :class="['tf-view-layout-switch sw-layout-3', selectedLayout === 'grid-3' ? 'active' : '']"
                        @click="onSelectLayout('grid-3')" data-value-grid="grid-3">
                        <div class="item"><span class="icon icon-grid-3"></span></div>
                    </li>
                    <li @click="onSelectLayout('grid-4')"
                        :class="['tf-view-layout-switch sw-layout-4', selectedLayout === 'grid-4' ? 'active' : '']"
                        data-value-grid="grid-4">
                        <div class="item"><span class="icon icon-grid-4"></span></div>
                    </li>
                    <li @click="onSelectLayout('grid-5')"
                        :class="['tf-view-layout-switch sw-layout-5', selectedLayout === 'grid-5' ? 'active' : '']"
                        data-value-grid="grid-5">
                        <div class="item"><span class="icon icon-grid-5"></span></div>
                    </li>
                    <li @click="onSelectLayout('grid-6')"
                        :class="['tf-view-layout-switch sw-layout-6', selectedLayout === 'grid-6' ? 'active' : '']"
                        data-value-grid="grid-6">
                        <div class="item"><span class="icon icon-grid-6"></span></div>
                    </li>
                </ul>
                <div class="tf-control-sorting d-flex justify-content-end">
                    <div class="tf-dropdown-sort" data-bs-toggle="dropdown">
                        <div class="btn-select">
                            <span class="text-sort-value">Featured</span>
                            <span class="icon icon-arrow-down"></span>
                        </div>
                        <div class="dropdown-menu">
                            <div class="select-item active">
                                <span class="text-value-item">Featured</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Best selling</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Alphabetically, A-Z</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Alphabetically, Z-A</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Price, low to high</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Price, high to low</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Date, old to new</span>
                            </div>
                            <div class="select-item">
                                <span class="text-value-item">Date, new to old</span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="wrapper-control-shop">
                <div class="meta-filter-shop"></div>
                <div class="grid-layout wrapper-shop" :data-grid="selectedLayout">
                    <!-- card product 1 -->
                    <div class="card-product" data-price="16.95" data-color="orange black white">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/orange-1.jpg"
                                    src="/images/products/orange-1.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/white-1.jpg"
                                    src="/images/products/white-1.jpg" alt="image-product">
                            </a>
                            <div class="list-product-btn absolute-2">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
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
                    <!-- card product 2 -->
                    <div class="card-product" data-price="18.95" data-size="m l xl"
                        data-color="brown light-purple light-green">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/brown.jpg"
                                    src="/images/products/brown.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/purple.jpg"
                                    src="/images/products/purple.jpg" alt="image-product">
                            </a>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                            <div class="size-list">
                                <span>M</span>
                                <span>L</span>
                                <span>XL</span>
                            </div>
                            <div class="countdown-box">
                                <div class="js-countdown" data-timer="1007500" data-labels="d :,h :,m :,s"></div>
                            </div>
                            <div class="on-sale-wrap text-end">
                                <div class="on-sale-item">-33%</div>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Ribbed modal T-shirt</a>
                            <span class="price">From $18.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Brown</span>
                                    <span class="swatch-value bg_brown"></span>
                                    <img class="lazyload" data-src="/images/products/brown.jpg"
                                        src="/images/products/brown.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Light Purple</span>
                                    <span class="swatch-value bg_purple"></span>
                                    <img class="lazyload" data-src="/images/products/purple.jpg"
                                        src="/images/products/purple.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Light Green</span>
                                    <span class="swatch-value bg_light-green"></span>
                                    <img class="lazyload" data-src="/images/products/green.jpg"
                                        src="/images/products/green.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 3 -->
                    <div class="card-product" data-price="10">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/white-3.jpg"
                                    src="/images/products/white-3.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/white-4.jpg"
                                    src="/images/products/white-4.jpg" alt="image-product">
                            </a>
                            <div class="list-product-btn absolute-2">
                                <a href="#shoppingCart" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Add to cart</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Oversized Printed T-shirt</a>
                            <span class="price">$10.00</span>
                        </div>
                    </div>
                    <!-- card product 4 -->
                    <div class="card-product" data-price="16.95" data-size="s m l xl" data-color="white purple black">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/white-2.jpg"
                                    src="/images/products/white-2.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/pink-1.jpg"
                                    src="/images/products/pink-1.jpg" alt="image-product">
                            </a>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                                <span>XL</span>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title">Oversized Printed T-shirt</a>
                            <span class="price">$16.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">White</span>
                                    <span class="swatch-value bg_white"></span>
                                    <img class="lazyload" data-src="/images/products/white-2.jpg"
                                        src="/images/products/white-2.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Pink</span>
                                    <span class="swatch-value bg_purple"></span>
                                    <img class="lazyload" data-src="/images/products/pink-1.jpg"
                                        src="/images/products/pink-1.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Black</span>
                                    <span class="swatch-value bg_dark"></span>
                                    <img class="lazyload" data-src="/images/products/black-2.jpg"
                                        src="/images/products/black-2.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 5 -->
                    <div class="card-product" data-price="114.95" data-size="s m l xl" data-color="brown white">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/brown-2.jpg"
                                    src="/images/products/brown-2.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/brown-3.jpg"
                                    src="/images/products/brown-3.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                                <span>XL</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">V-neck linen T-shirt</a>
                            <span class="price">$114.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Brown</span>
                                    <span class="swatch-value bg_brown"></span>
                                    <img class="lazyload" data-src="/images/products/brown-2.jpg"
                                        src="/images/products/brown-2.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">White</span>
                                    <span class="swatch-value bg_white"></span>
                                    <img class="lazyload" data-src="/images/products/white-5.jpg"
                                        src="/images/products/white-5.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 6 -->
                    <div class="card-product" data-price="10.00"
                        data-color="light-green black blue dark-blue white light-grey">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/light-green-1.jpg"
                                    src="/images/products/light-green-1.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/light-green-2.jpg"
                                    src="/images/products/light-green-2.jpg" alt="image-product">
                            </a>
                            <div class="list-product-btn absolute-2">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Loose Fit Sweatshirt</a>
                            <span class="price">$10.00</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Light Green</span>
                                    <span class="swatch-value bg_light-green"></span>
                                    <img class="lazyload" data-src="/images/products/light-green-1.jpg"
                                        src="/images/products/light-green-1.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Black</span>
                                    <span class="swatch-value bg_dark"></span>
                                    <img class="lazyload" data-src="/images/products/black-3.jpg"
                                        src="/images/products/black-3.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Blue</span>
                                    <span class="swatch-value bg_blue-2"></span>
                                    <img class="lazyload" data-src="/images/products/blue.jpg"
                                        src="/images/products/blue.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Dark Blue</span>
                                    <span class="swatch-value bg_dark-blue"></span>
                                    <img class="lazyload" data-src="/images/products/dark-blue.jpg"
                                        src="/images/products/dark-blue.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">White</span>
                                    <span class="swatch-value bg_white"></span>
                                    <img class="lazyload" data-src="/images/products/white-6.jpg"
                                        src="/images/products/white-6.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Light Grey</span>
                                    <span class="swatch-value bg_light-grey"></span>
                                    <img class="lazyload" data-src="/images/products/light-grey.jpg"
                                        src="/images/products/light-grey.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 7 -->
                    <div class="card-product" data-price="10.00" data-size="s m l"
                        data-color="black dark-blue beige light-blue white">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/black-4.jpg"
                                    src="/images/products/black-4.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/black-5.jpg"
                                    src="/images/products/black-5.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Regular Fit Oxford Shirt</a>
                            <span class="price">$10.00</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Black</span>
                                    <span class="swatch-value bg_dark"></span>
                                    <img class="lazyload" data-src="/images/products/black-4.jpg"
                                        src="/images/products/black-4.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Dark Blue</span>
                                    <span class="swatch-value bg_dark-blue"></span>
                                    <img class="lazyload" data-src="/images/products/dark-blue-2.jpg"
                                        src="/images/products/dark-blue-2.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Beige</span>
                                    <span class="swatch-value bg_beige"></span>
                                    <img class="lazyload" data-src="/images/products/beige.jpg"
                                        src="/images/products/beige.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Light Blue</span>
                                    <span class="swatch-value bg_light-blue"></span>
                                    <img class="lazyload" data-src="/images/products/light-blue.jpg"
                                        src="/images/products/light-blue.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">White</span>
                                    <span class="swatch-value bg_white"></span>
                                    <img class="lazyload" data-src="/images/products/white-7.jpg"
                                        src="/images/products/white-7.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 8 -->
                    <div class="card-product" data-price="9.95" data-size="s m l xl" data-color="white black blue">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/white-8.jpg"
                                    src="/images/products/white-8.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/black-6.jpg"
                                    src="/images/products/black-6.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                                <span>XL</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Loose Fit Hoodie</a>
                            <span class="price">$9.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">White</span>
                                    <span class="swatch-value bg_white"></span>
                                    <img class="lazyload" data-src="/images/products/white-8.jpg"
                                        src="/images/products/white-8.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Black</span>
                                    <span class="swatch-value bg_dark"></span>
                                    <img class="lazyload" data-src="/images/products/black-7.jpg"
                                        src="/images/products/black-7.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Blue</span>
                                    <span class="swatch-value bg_blue-2"></span>
                                    <img class="lazyload" data-src="/images/products/blue-2.jpg"
                                        src="/images/products/blue-2.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 9 -->
                    <div class="card-product" data-price="14.95" data-size="m l xl" data-color="brown black">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/brown-4.jpg"
                                    src="/images/products/brown-4.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/black-8.jpg"
                                    src="/images/products/black-8.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>M</span>
                                <span>L</span>
                                <span>XL</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Patterned scarf</a>
                            <span class="price">$14.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Brown</span>
                                    <span class="swatch-value bg_brown"></span>
                                    <img class="lazyload" data-src="/images/products/brown-4.jpg"
                                        src="/images/products/brown-4.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Black</span>
                                    <span class="swatch-value bg_dark"></span>
                                    <img class="lazyload" data-src="/images/products/black-8.jpg"
                                        src="/images/products/black-8.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 10 -->
                    <div class="card-product" data-price="18.95" data-size="s m l" data-color="black white">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/black-9.jpg"
                                    src="/images/products/black-9.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/black-10.jpg"
                                    src="/images/products/black-10.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Slim Fit Fine-knit Turtleneck
                                Sweater</a>
                            <span class="price">$18.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Black</span>
                                    <span class="swatch-value bg_dark"></span>
                                    <img class="lazyload" data-src="/images/products/black-9.jpg"
                                        src="/images/products/black-9.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">White</span>
                                    <span class="swatch-value bg_white"></span>
                                    <img class="lazyload" data-src="/images/products/white-9.jpg"
                                        src="/images/products/white-9.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 11 -->
                    <div class="card-product" data-price="18.95" data-size="s m l" data-color="grey pink light-pink">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/grey-2.jpg"
                                    src="/images/products/grey-2.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/grey.jpg"
                                    src="/images/products/grey.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Slim Fit Fine-knit Turtleneck
                                Sweater</a>
                            <span class="price">$18.95</span>
                            <ul class="list-color-product">
                                <li class="list-color-item color-swatch active">
                                    <span class="tooltip">Grey</span>
                                    <span class="swatch-value bg_grey"></span>
                                    <img class="lazyload" data-src="/images/products/grey-2.jpg"
                                        src="/images/products/grey-2.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Pink</span>
                                    <span class="swatch-value bg_pink"></span>
                                    <img class="lazyload" data-src="/images/products/pink-2.jpg"
                                        src="/images/products/pink-2.jpg" alt="image-product">
                                </li>
                                <li class="list-color-item color-swatch">
                                    <span class="tooltip">Light Pink</span>
                                    <span class="swatch-value bg_light-pink"></span>
                                    <img class="lazyload" data-src="/images/products/light-pink.jpg"
                                        src="/images/products/light-pink.jpg" alt="image-product">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- card product 12 -->
                    <div class="card-product" data-price="18.95" data-size="s m l">
                        <div class="card-product-wrapper">
                            <a href="product-detail.html" class="product-img">
                                <img class="lazyload img-product" data-src="/images/products/black-11.jpg"
                                    src="/images/products/black-11.jpg" alt="image-product">
                                <img class="lazyload img-hover" data-src="/images/products/black-12.jpg"
                                    src="/images/products/black-12.jpg" alt="image-product">
                            </a>
                            <div class="size-list">
                                <span>S</span>
                                <span>M</span>
                                <span>L</span>
                            </div>
                            <div class="list-product-btn">
                                <a href="#quick_add" data-bs-toggle="modal"
                                    class="box-icon bg_white quick-add tf-btn-loading">
                                    <span class="icon icon-bag"></span>
                                    <span class="tooltip">Quick Add</span>
                                </a>
                                <a href="javascript:void(0);" class="box-icon bg_white wishlist btn-icon-action">
                                    <span class="icon icon-heart"></span>
                                    <span class="tooltip">Add to Wishlist</span>
                                    <span class="icon icon-delete"></span>
                                </a>
                                <a href="#compare" data-bs-toggle="offcanvas" aria-controls="offcanvasLeft"
                                    class="box-icon bg_white compare btn-icon-action">
                                    <span class="icon icon-compare"></span>
                                    <span class="tooltip">Add to Compare</span>
                                    <span class="icon icon-check"></span>
                                </a>
                                <a href="#quick_view" data-bs-toggle="modal"
                                    class="box-icon bg_white quickview tf-btn-loading">
                                    <span class="icon icon-view"></span>
                                    <span class="tooltip">Quick View</span>
                                </a>
                            </div>
                        </div>
                        <div class="card-product-info">
                            <a href="product-detail.html" class="title link">Slim Fit Fine-knit Turtleneck
                                Sweater</a>
                            <span class="price">$18.95</span>

                        </div>
                    </div>
                </div>
                <!-- pagination -->
                <ul class="tf-pagination-wrap tf-pagination-list tf-pagination-btn">
                    <li class="active">
                        <a href="#" class="pagination-link">1</a>
                    </li>
                    <li>
                        <a href="#" class="pagination-link animate-hover-btn">2</a>
                    </li>
                    <li>
                        <a href="#" class="pagination-link animate-hover-btn">3</a>
                    </li>
                    <li>
                        <a href="#" class="pagination-link animate-hover-btn">4</a>
                    </li>
                    <li>
                        <a href="#" class="pagination-link animate-hover-btn">
                            <span class="icon icon-arrow-right"></span>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </section>
</template>
<script setup>
import { ref } from 'vue';

const selectedLayout = ref('grid-4')

const onSelectLayout = (layout) => {
    selectedLayout.value = layout
}

</script>