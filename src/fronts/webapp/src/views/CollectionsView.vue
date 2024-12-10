<script setup>
import ProductCard from '@/components/ProductCard.vue'
import ProductFilter from '@/components/ProductFilter.vue'
import ProductPagination from '@/components/ProductPagination.vue'
import { useProductStore } from '@/stores/productStore'
import { onMounted } from 'vue'

const productStore = useProductStore()

onMounted(() => {
  productStore.fetchProducts()
})
</script>

<template>
  <div class="bg-white">
    <div class="py-16 sm:py-4 lg:mx-auto lg:max-w-7xl lg:px-8">
      <ProductFilter />
      <div class="relative mt-8">
        <div class="relative -mb-6 w-full overflow-x-auto pb-6">
          <ul
            role="list"
            class="mx-4 inline-flex space-x-3 sm:mx-6 lg:mx-0 lg:grid lg:grid-cols-5 lg:gap-x-3 lg:space-x-0"
          >
            <li
              v-for="product in productStore.products"
              :key="product.id"
              class="inline-flex w-64 flex-col text-center lg:w-auto"
            >
              <ProductCard :product="product" />
            </li>
          </ul>
        </div>
      </div>
     <div class="mt-10">
      <ProductPagination/>
      </div>
    </div>
  </div>
</template>