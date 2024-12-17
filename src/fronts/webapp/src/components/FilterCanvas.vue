<template>
    <div class="offcanvas offcanvas-start canvas-filter" id="filterShop">
        <div class="canvas-wrapper">
            <header class="canvas-header">
                <div class="filter-icon">
                    <span class="icon icon-filter"></span>
                    <span>Filter</span>
                </div>
                <span class="icon-close icon-close-popup" data-bs-dismiss="offcanvas" aria-label="Close"></span>
            </header>
            <div class="canvas-body">
                <div class="widget-facet wd-categories">
                    <div class="facet-title" data-bs-target="#categories" data-bs-toggle="collapse" aria-expanded="true"
                        aria-controls="categories">
                        <span>Product categories</span>
                        <span class="icon icon-arrow-up"></span>
                    </div>
                    <div id="categories" class="collapse show">
                        <ul class="list-categoris current-scrollbar mb_36">
                            <li class="cate-item current"><a href="shop-default.html"><span>Fashion</span></a></li>
                            <li class="cate-item"><a href="shop-default.html"><span>Men</span></a></li>
                            <li class="cate-item"><a href="shop-default.html"><span>Women</span></a></li>
                            <li class="cate-item"><a href="shop-default.html"><span>Denim</span></a></li>
                            <li class="cate-item"><a href="shop-default.html"><span>Dress</span></a></li>
                        </ul>
                    </div>
                </div>
                <form action="#" id="facet-filter-form" class="facet-filter-form">
                    <div class="widget-facet">
                        <div class="facet-title" data-bs-target="#availability" data-bs-toggle="collapse"
                            aria-expanded="true" aria-controls="availability">
                            <span>Availability</span>
                            <span class="icon icon-arrow-up"></span>
                        </div>
                        <div id="availability" class="collapse show">
                            <ul class="tf-filter-group current-scrollbar mb_36">
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="availability" class="tf-check" id="availability-1">
                                    <label for="availability-1" class="label"><span>In
                                            stock</span>&nbsp;<span>(14)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="availability" class="tf-check" id="availability-2">
                                    <label for="availability-2" class="label"><span>Out of
                                            stock</span>&nbsp;<span>(2)</span></label>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="widget-facet">
                        <div class="facet-title" data-bs-target="#price" data-bs-toggle="collapse" aria-expanded="true"
                            aria-controls="price">
                            <span>Price</span>
                            <span class="icon icon-arrow-up"></span>
                        </div>
                        <div id="price" class="collapse show">
                            <div class="widget-price filter-price">
                                <div class="tow-bar-block">
                                    <div class="progress-price"></div>
                                </div>
                                <div class="range-input">
                                    <input class="range-min" type="range" min="0" max="300" value="0" />
                                    <input class="range-max" type="range" min="0" max="300" value="300" />
                                </div>
                                <div class="box-title-price">
                                    <span class="title-price">Price :</span>
                                    <div class="caption-price">
                                        <div>
                                            <span>$</span>
                                            <span class="min-price">0</span>
                                        </div>
                                        <span>-</span>
                                        <div>
                                            <span>$</span>
                                            <span class="max-price">300</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="widget-facet">
                        <div class="facet-title" data-bs-target="#brand" data-bs-toggle="collapse" aria-expanded="true"
                            aria-controls="brand">
                            <span>Brand</span>
                            <span class="icon icon-arrow-up"></span>
                        </div>
                        <div id="brand" class="collapse show">
                            <ul class="tf-filter-group current-scrollbar mb_36">
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="brand" class="tf-check" id="brand-1">
                                    <label for="brand-1" class="label"><span>Ecomus</span>&nbsp;<span>(8)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="brand" class="tf-check" id="brand-2">
                                    <label for="brand-2" class="label"><span>M&H</span>&nbsp;<span>(8)</span></label>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="widget-facet">
                        <div class="facet-title" data-bs-target="#color" data-bs-toggle="collapse" aria-expanded="true"
                            aria-controls="color">
                            <span>Color</span>
                            <span class="icon icon-arrow-up"></span>
                        </div>
                        <div id="color" class="collapse show">
                            <ul class="tf-filter-group filter-color current-scrollbar mb_36">
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_beige" id="beige"
                                        value="beige">
                                    <label for="beige" class="label"><span>Beige</span>&nbsp;<span>(3)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_dark" id="black"
                                        value="black">
                                    <label for="black" class="label"><span>Black</span>&nbsp;<span>(18)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_blue-2" id="blue"
                                        value="blue">
                                    <label for="blue" class="label"><span>Blue</span>&nbsp;<span>(3)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_brown" id="brown"
                                        value="brown">
                                    <label for="brown" class="label"><span>Brown</span>&nbsp;<span>(3)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_cream" id="cream"
                                        value="cream">
                                    <label for="cream" class="label"><span>Cream</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_dark-beige"
                                        id="dark-beige" value="dark-beige">
                                    <label for="dark-beige" class="label"><span>Dark
                                            Beige</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_dark-blue"
                                        id="dark-blue" value="dark-blue">
                                    <label for="dark-blue" class="label"><span>Dark
                                            Blue</span>&nbsp;<span>(3)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_dark-green"
                                        id="dark-green" value="dark-green">
                                    <label for="dark-green" class="label"><span>Dark
                                            Green</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_dark-grey"
                                        id="dark-grey" value="dark-grey">
                                    <label for="dark-grey" class="label"><span>Dark
                                            Grey</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_grey" id="grey"
                                        value="grey">
                                    <label for="grey" class="label"><span>Grey</span>&nbsp;<span>(2)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_light-blue"
                                        id="light-blue" value="light-blue">
                                    <label for="light-blue" class="label"><span>Light
                                            Blue</span>&nbsp;<span>(5)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_light-green"
                                        id="light-green" value="light-green">
                                    <label for="light-green" class="label"><span>Light
                                            Green</span>&nbsp;<span>(3)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_light-grey"
                                        id="light-grey" value="light-grey">
                                    <label for="light-grey" class="label"><span>Light
                                            Grey</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_light-pink"
                                        id="light-pink" value="light-pink">
                                    <label for="light-pink" class="label"><span>Light
                                            Pink</span>&nbsp;<span>(2)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_purple"
                                        id="light-purple" value="light-purple">
                                    <label for="light-purple" class="label"><span>Light
                                            Purple</span>&nbsp;<span>(2)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_light-yellow"
                                        id="light-yellow" value="light-yellow">
                                    <label for="light-yellow" class="label"><span>Light
                                            Yellow</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_orange" id="orange"
                                        value="orange">
                                    <label for="orange" class="label"><span>Orange</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_pink" id="pink"
                                        value="pink">
                                    <label for="pink" class="label"><span>Pink</span>&nbsp;<span>(2)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_taupe" id="taupe"
                                        value="taupe">
                                    <label for="taupe" class="label"><span>Taupe</span>&nbsp;<span>(1)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_white" id="white"
                                        value="white">
                                    <label for="white" class="label"><span>White</span>&nbsp;<span>(14)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="checkbox" name="color" class="tf-check-color bg_yellow" id="yellow"
                                        value="yellow">
                                    <label for="yellow" class="label"><span>Yellow</span>&nbsp;<span>(1)</span></label>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="widget-facet">
                        <div class="facet-title" data-bs-target="#size" data-bs-toggle="collapse" aria-expanded="true"
                            aria-controls="size">
                            <span>Size</span>
                            <span class="icon icon-arrow-up"></span>
                        </div>
                        <div id="size" class="collapse show">
                            <ul class="tf-filter-group current-scrollbar">
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="size" class="tf-check tf-check-size" value="s" id="s">
                                    <label for="s" class="label"><span>S</span>&nbsp;<span>(7)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="size" class="tf-check tf-check-size" value="m" id="m">
                                    <label for="m" class="label"><span>M</span>&nbsp;<span>(8)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="size" class="tf-check tf-check-size" value="l" id="l">
                                    <label for="l" class="label"><span>L</span>&nbsp;<span>(8)</span></label>
                                </li>
                                <li class="list-item d-flex gap-12 align-items-center">
                                    <input type="radio" name="size" class="tf-check tf-check-size" value="xl" id="xl">
                                    <label for="xl" class="label"><span>XL</span>&nbsp;<span>(6)</span></label>
                                </li>
                            </ul>
                        </div>
                    </div>
                </form>
            </div>

        </div>
    </div>
</template>
<script setup></script>