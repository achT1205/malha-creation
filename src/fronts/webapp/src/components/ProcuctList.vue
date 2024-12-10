<script setup>
import AccessoryProductCard from '@/components/AccessoryProductCard.vue'
import ClothingProductCard from '@/components/ClothingProductCard.vue'
import { ref, onMounted, onUnmounted } from 'vue'
import { ArrowLeftIcon, ArrowRightIcon } from '@heroicons/vue/20/solid'

const scrollContainer = ref(null)
const showNavigators = ref(false)
const isScrolling = ref(false)
let scrollTimeout = null
const atStart = ref(false)
const atEnd = ref(false)

const props = defineProps({
  products: { type: Array, required: true },
  width: { type: String, required: true, default: 'w-1/6' }
})

const handleScroll = () => {
  if (!isScrolling.value) {
    onScrollStart()
  }
  clearTimeout(scrollTimeout)
  scrollTimeout = setTimeout(onScrollEnd, 100)
}

const scrollToStart = () => {
  if (scrollContainer.value) {
    scrollContainer.value.scrollTo({ left: 0, behavior: 'smooth' })
    atStart.value = true
    atEnd.value = false
    showNavigators.value = false
  }
}

const scrollToEnd = () => {
  if (scrollContainer.value) {
    scrollContainer.value.scrollTo({ left: scrollContainer.value.scrollWidth, behavior: 'smooth' })
    atStart.value = false
    atEnd.value = true
    showNavigators.value = false
  }
}
const onScrollStart = () => {
  showNavigators.value = false
  atStart.value = false
  atEnd.value = false
  isScrolling.value = true
}

const onScrollEnd = () => {
  showNavigators.value = true
  isScrolling.value = false
}

onMounted(() => {
  if (scrollContainer.value) {
    scrollContainer.value.addEventListener('scroll', handleScroll)
  }

  if (props.products && props.products.length > 5) {
    showNavigators.value = true
  }
})
onUnmounted(() => {
  if (scrollContainer.value) {
    scrollContainer.value.removeEventListener('scroll', handleScroll)
  }
  clearTimeout(scrollTimeout)
})
</script>



<template>
  <div class="relative mt-8">
    <div class="relative w-full overflow-x-auto mb-10">
      <ul
        role="list"
        class="overflow-x-auto whitespace-nowrap hide-scrollbar w-full mx-4 space-x-2 sm:mx-6 lg:mx-0"
        ref="scrollContainer"
        @scroll="handleScroll"
      >
        <li v-for="product in props.products.filter(_ => _.productType === 0)" :key="product.id" class="inline-block" :class="props.width">
          <ClothingProductCard :product="product" />
        </li>
      </ul>
      <button
        @click="scrollToStart"
        v-if="showNavigators || (atStart === false && atEnd === true)"
        class="absolute left-0 top-1/2 transform -translate-y-1/2 p-2 bg-white"
      >
        <ArrowLeftIcon
          class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
          aria-hidden="true"
        />
      </button>
      <button
        @click="scrollToEnd"
        v-if="showNavigators || (atStart === true && atEnd === false)"
        class="absolute right-0 top-1/2 transform -translate-y-1/2 p-2 bg-white"
      >
        <ArrowRightIcon
          class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
          aria-hidden="true"
        />
      </button>
    </div>
    <div class="relative w-full overflow-x-auto">
      <ul
        role="list"
        class="overflow-x-auto whitespace-nowrap hide-scrollbar w-full mx-4 space-x-2 sm:mx-6 lg:mx-0"
        ref="scrollContainer"
        @scroll="handleScroll"
      >
        <li v-for="product in props.products.filter(_ => _.productType === 1)" :key="product.id" class="inline-block" :class="props.width">
          <AccessoryProductCard :product="product" />
        </li>
      </ul>
      <button
        @click="scrollToStart"
        v-if="showNavigators || (atStart === false && atEnd === true)"
        class="absolute left-0 top-1/2 transform -translate-y-1/2 p-2 bg-white"
      >
        <ArrowLeftIcon
          class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
          aria-hidden="true"
        />
      </button>
      <button
        @click="scrollToEnd"
        v-if="showNavigators || (atStart === true && atEnd === false)"
        class="absolute right-0 top-1/2 transform -translate-y-1/2 p-2 bg-white"
      >
        <ArrowRightIcon
          class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
          aria-hidden="true"
        />
      </button>
    </div>
  </div>
</template>