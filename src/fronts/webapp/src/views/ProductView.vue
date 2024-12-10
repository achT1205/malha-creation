<script setup>
import { ref, onMounted, watchEffect, watch } from 'vue'
import { StarIcon, PlusIcon, MinusIcon } from '@heroicons/vue/20/solid'
import {
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
  RadioGroup,
  RadioGroupOption
} from '@headlessui/vue'
import { useCartStore } from '@/stores/cartStore'
import { useRouter, useRoute } from 'vue-router'
import { useProductStore } from '@/stores/productStore'
import ProcuctList from '@/components/ProcuctList.vue'
import ProductReviews from '@/components/ProductReviews.vue'
import { useScreenSize } from '@/composables/useScreenSize'

const { isMobile, isTablet, isDesktop } = useScreenSize()
const productStore = useProductStore()
const router = useRouter()
const route = useRoute()

const props = defineProps({
  slug: { type: String, required: true }
})

const zoomableImage = ref(null)
const outfits = ref([])
const selectedColor = ref()
const selectImageIndex = ref(0)
const slugArr = ref()

const cartStore = useCartStore()
const product = ref()
const onOverImage = (index) => {
  selectImageIndex.value = index
}

const reviewsSection = ref(null)

const scrollToSection = () => {
  reviewsSection.value.scrollIntoView({ behavior: 'smooth' })
}

const addToCart = () => {
  const p = {
    id: product.id,
    name: product.name,
    price: selectedColor.value.price,
    color: selectedColor.value.name.toLowerCase(),
    size: selectedColor.value.selectedSize.value,
    reviews: selectedColor.value.reviews,
    image: selectedColor.value.images[0].src,
    quantity: 1
  }
  cartStore.addItem(p)
}

const onSelectSize = () => {
  const query = {}
  query.size = selectedColor.value.selectedSize.value.toLocaleLowerCase()
  selectImageIndex.value = 1
  router.push({ query: query })
}

const onSelectOutfit = (outfit) => {
  if (!outfit.slug) return
  const params = {}
  params.slug = outfit.slug
  router.replace({ params: params })
}

onMounted(() => {
  handleRouteChange()
})

const fetchOutfits = () => {
  outfits.value = []
  if (selectedColor.value && selectedColor.value.outfits && selectedColor.value.outfits.length) {
    outfits.value.push({
      src: selectedColor.value.images[0].src
    })
    selectedColor.value.outfits.forEach((ot) => {
      outfits.value.push({
        isSeparator: true
      })
      outfits.value.push({
        slug: ot.slug,
        src: ot.image.src
      })
    })
  }
}
const handleRouteChange = () => {
  slugArr.value = props.slug.split('-in-')

  product.value = {}
  productStore.fetchProducts()
  selectImageIndex.value = 0
  productStore.getProductBySlug(props.slug)
  product.value = productStore.product

  selectedColor.value = product.value.colors.find(
    (c) => c.name.toLocaleLowerCase() === slugArr.value[slugArr.value.length - 1]
  )
  fetchOutfits()
  if (route.query.size && selectedColor.value) {
    selectedColor.value.selectedSize = selectedColor.value.sizes.find(
      (s) => s.value.toLocaleLowerCase() === route.query.size
    )
  }
}

const onMouseMove = (e) => {
  const rect = zoomableImage.value.getBoundingClientRect()
  const x = e.clientX - rect.left
  const y = e.clientY - rect.top

  const xPercent = (x / rect.width) * 100
  const yPercent = (y / rect.height) * 100

  zoomableImage.value.style.transformOrigin = `${xPercent}% ${yPercent}%`
  zoomableImage.value.style.transform = 'scale(2)' // Adjust the scale value to control zoom level
}

const onMouseLeave = () => {
  zoomableImage.value.style.transformOrigin = 'center center'
  zoomableImage.value.style.transform = 'scale(1)'
}

watch(() => route.fullPath, handleRouteChange)
watchEffect(() => fetchOutfits())
</script>
<template>
  <div class="bg-white" v-if="selectedColor">
    <div class="mx-auto max-w-2xl px-4 py-4 sm:px-6 sm:py-6 lg:max-w-7xl lg:px-8">
      <div class="lg:grid lg:grid-cols-2 lg:items-start lg:gap-x-8">
        <!-- Image gallery -->
        <div class="flex flex-row">
          <!-- Image selector -->
          <div class="flex flex-col w-2/12 overflow-y-scroll h-96">
            <div
              v-for="(colorImage, imageIndex) in selectedColor.images"
              :key="imageIndex"
              class="h-20 w-18 mt-2 mr-3"
              @mouseover="onOverImage(imageIndex)"
              @click="onOverImage(imageIndex)"
            >
              <img
                :src="colorImage.src"
                class="h-full w-full object-cover object-cente rounded-md"
                :class="[
                  selectImageIndex === imageIndex
                    ? 'border-black border-spacing-2   border-solid border-2 '
                    : 'ring-transparent'
                ]"
              />
            </div>
          </div>

          <div class="aspect-h-1 aspect-w-1 w-10/12">
            <div
              class="relative overflow-hidden h-full w-full hover:zoom hover:cursor-zoom-in"
              @mousemove="onMouseMove"
              @mouseleave="onMouseLeave"
            >
              <img
                :src="selectedColor.images[selectImageIndex].src"
                class="object-cover w-full h-full transform transition-transform duration-300 ease-in-out"
                ref="zoomableImage"
              />
            </div>
          </div>
        </div>

        <!-- Product info -->
        <div class="mt-10 px-4 sm:mt-16 sm:px-0 lg:mt-0">
          <div>
            <div class="flex justify-between">
              <h1 class="text-xl font-medium text-gray-900">{{ product.name }} {{ slug }}</h1>
              <p class="text-xl font-medium text-gray-900">€ {{ selectedColor.price }} EUR</p>
            </div>
            <!-- Reviews -->
            <div class="mt-4">
              <h2 class="sr-only">Reviews</h2>
              <div class="flex items-center">
                <p class="text-sm text-gray-700">
                  {{ selectedColor.reviews.average }}
                  <span class="sr-only"> out of 5 stars</span>
                </p>
                <div class="ml-1 flex items-center">
                  <StarIcon
                    v-for="rating in [0, 1, 2, 3, 4]"
                    :key="rating"
                    :class="[
                      selectedColor.reviews.average > rating ? 'text-black-400' : 'text-gray-200',
                      'h-5 w-5 flex-shrink-0'
                    ]"
                    aria-hidden="true"
                  />
                </div>
                <div aria-hidden="true" class="ml-4 text-sm text-gray-300">·</div>
                <div class="ml-4 flex">
                  <span
                    @click="scrollToSection"
                    class="text-sm font-medium text-gray-600 hover:text-gray-500 cursor-pointer"
                    >Voir toutes les {{ selectedColor.reviews.totalCount }} reviews</span
                  >
                </div>
              </div>
            </div>
          </div>
          <!-- Color picker -->
          <div class="mt-6">
            <h2 class="text-sm font-medium text-gray-900">Color</h2>

            <fieldset aria-label="Choose a color" class="mt-3">
              <RadioGroup v-model="selectedColor" class="flex items-center space-x-3">
                <RadioGroupOption
                  as="template"
                  v-for="color in product.colorVariants"
                  :key="color.color"
                  :value="color"
                  :aria-label="color.name"
                  v-slot="{ active, checked }"
                >
                  <router-link :to="`/products/${color.slug}`">
                    <div
                      :class="[
                        color.class,
                        active && checked ? 'ring ring-offset-1' : '',
                        !active && checked ? 'ring-2' : '',
                        'relative -m-0.5 flex cursor-pointer items-center justify-center rounded-full p-0.5 ring-current focus:outline-none'
                      ]"
                    >
                      <span
                        aria-hidden="true"
                        class="h-4 w-4 rounded-full border border-black border-opacity-10 bg-current"
                      /></div
                  >
                  </router-link>
                  
                </RadioGroupOption>
                
              </RadioGroup>
            </fieldset>
          </div>

          <!-- Size picker -->
          <div class="mt-8">
            <div class="flex items-center justify-between">
              <h2 class="text-sm font-medium text-gray-900">Size</h2>
            </div>

            <fieldset aria-label="Choose a size" class="mt-2">
              <RadioGroup
                v-model="selectedColor.selectedSize"
                class="grid grid-cols-6 gap-3 sm:grid-cols-12"
              >
                <RadioGroupOption
                  as="template"
                  v-for="size in selectedColor.sizes"
                  :key="size.name"
                  :value="size"
                  :disabled="!size.quantity || size.quantity === 0"
                  v-slot="{ active, checked }"
                  @click="onSelectSize"
                >
                  <div
                    :class="[
                      size.quantity > 0
                        ? 'cursor-pointer focus:outline-none'
                        : 'cursor-not-allowed opacity-25',
                      active ? 'ring-2 ring-gray-500 ' : '',
                      checked
                        ? 'border-transparent bg-gray-600 text-white hover:bg-gray-700'
                        : 'border-gray-200 bg-white text-gray-900 hover:bg-gray-50',
                      'flex items-center justify-center rounded-md border px-3 py-3 text-sm font-medium uppercase sm:flex-1 h-9',
                      size.name.length > 3 ? 'w-12' : 'w-10'
                    ]"
                  >
                    {{ size.name }}
                  </div>
                </RadioGroupOption>
              </RadioGroup>
            </fieldset>
          </div>

          <button
            :disabled="!selectedColor.selectedSize"
            @click="addToCart"
            class="mt-8 flex w-full items-center justify-center rounded-md border border-transparent bg-gray-600 px-8 py-3 text-base font-medium text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2"
          >
            Add to cart
          </button>

          <!-- Product details -->
          <div class="mt-10">
            <h2 class="text-sm font-medium text-gray-900">Description</h2>

            <div class="prose prose-sm mt-4 text-gray-500" v-html="product.description" />
          </div>

          <div class="mt-8 border-t border-gray-200 pt-8">
            <h2 class="text-sm font-medium text-gray-900">Fabric &amp; Care</h2>

            <div class="prose prose-sm mt-4 text-gray-500">
              <div class="divide-y divide-gray-200 border-t">
                <Disclosure
                  as="div"
                  v-for="detail in product.details"
                  :key="detail.name"
                  v-slot="{ open }"
                >
                  <h3>
                    <DisclosureButton
                      class="group relative flex w-full items-center justify-between py-6 text-left"
                    >
                      <span
                        :class="[open ? 'text-gray-600' : 'text-gray-900', 'text-sm font-medium']"
                        >{{ detail.title }}</span
                      >
                      <span class="ml-6 flex items-center">
                        <PlusIcon
                          v-if="!open"
                          class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
                          aria-hidden="true"
                        />
                        <MinusIcon
                          v-else
                          class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
                          aria-hidden="true"
                        />
                      </span>
                    </DisclosureButton>
                  </h3>
                  <DisclosurePanel as="div" class="prose prose-sm pb-6">
                    {{ detail.details }}
                  </DisclosurePanel>
                </Disclosure>

                <div v-if="outfits.length">
                  <h3>
                    <div
                      class="group relative flex w-full items-center justify-between py-6 text-left"
                    >
                      <span :class="['text-gray-900', 'text-sm font-medium']">SHOP THE LOOK</span>
                      <MinusIcon
                        class="block h-6 w-6 text-gray-400 group-hover:text-gray-500"
                        aria-hidden="true"
                      />
                    </div>
                  </h3>

                  <div as="div" class="prose prose-sm pb-6">
                    <div class="flex flex-row">
                      <div v-for="outfit in outfits" :key="outfit.slug">
                        <div class="h-20 w-18 mr-3 text-center mt-20" v-if="outfit.isSeparator">
                          <PlusIcon class="block h-6 w-6 text-gray-400 group-hover:text-gray-500" />
                        </div>

                        <div
                          class="h-40 w-32 mt-2 mr-3"
                          :class="[outfit.slug ? 'cursor-pointer' : '']"
                          v-else
                          @click="onSelectOutfit(outfit)"
                        >
                          <img
                            :src="outfit.src"
                            class="h-full w-full object-cover object-cente rounded-md"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- YOU MAY ALSO LIKE -->
    <section aria-labelledby="trending-heading" class="bg-white" v-if="productStore.products">
      <div class="py-16 sm:py-24 lg:mx-auto lg:max-w-12xl lg:px-4 lg:py-16">
        <div class="flex items-center justify-center px-4 sm:px-6 lg:px-0">
          <h2 id="trending-heading" class="text-xl font-bold tracking-tight text-gray-900">
            YOU MAY ALSO LIKE
          </h2>
        </div>
        <ProcuctList
          :products="productStore.products"
          :width="isDesktop ? 'w-1/6' : isTablet ? 'w-1/3' : isMobile ? 'w-1/2' : '1/6'"
        />
      </div>
    </section>
    <section aria-labelledby="trending-heading" class="bg-white" ref="reviewsSection">
      <ProductReviews :reviews="product.reviews" @scrollToSection="scrollToSection" />
    </section>
  </div>
</template>
<style>
.hover\:zoom:hover img {
  transform: scale(2); /* Adjust the scale value to control zoom level */
}
</style>


