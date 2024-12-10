<script setup>
import { useRouter } from 'vue-router'
import ProcuctList from '@/components/ProcuctList.vue'
import { useProductStore } from '@/stores/productStore'
import { useScreenSize } from '@/composables/useScreenSize'
import { onMounted, computed } from 'vue'

const productStore = useProductStore()

const router = useRouter()
const { isMobile, isTablet, isDesktop } = useScreenSize()


const response = computed(() => productStore.products);
const isLoading = computed(() => productStore.isLoading);
const error = computed(() => productStore.error);
const successed = computed(() => productStore.successed);


const offers = [
  { name: 'Livraison gratuite', description: 'pour des achats de +150€' },
  { name: '35 jours', description: 'retour facile' },
  { name: 'Acheter maintenant', description: 'Payer plutard' }
]

const collections = [
  {
    id: 1,
    name: 'FEMMES',
    value: 'women-dresses'
  },
  {
    id: 2,
    name: 'HOMMES',
    value: 'men-burnous'
  },
  {
    id: 3,
    name: 'FILLES',
    value: 'girl-dresses'
  },
  {
    id: 4,
    name: 'GARCONS',
    value: 'boys-burnous'
  }
]
const accessories = [
  {
    id: 1,
    name: 'SACS BERBERES',
    value: 'bags'
  },
  {
    id: 2,
    name: 'BIJOUX KAYLES',
    value: 'jewelry'
  },
  {
    id: 3,
    name: 'BROCHES',
    value: 'broches'
  },
  {
    id: 4,
    name: 'FOULARDS',
    value: 'scarves'
  }
]

const onCollectionSelect = (collection) => {
  router.push(`collections/${collection.value}`)
}

onMounted(() => {
  productStore.fetchProducts()
})
</script>

<template>
  <div>
    <div v-if="productStore.loading">Loading...</div>
    <div v-if="productStore.error">{{ error }}</div>
    <div v-if="!productStore.loading && !productStore.error">
      <!-- Hero section -->
      <div class="relative">
        <!-- Decorative image and overlay -->
        <div aria-hidden="true" class="absolute inset-0 overflow-hidden">
          <img src="https://placehold.co/1792x1024" alt="" class="h-full w-full object-cover object-center" />
        </div>

        <div class="relative px-6 py-32 sm:py-40 lg:px-8 lg:py-56 lg:pr-0">
          <div class="mx-auto max-w-2xl lg:mx-0 lg:max-w-xl">
            <h1 class="text-4xl font-bold tracking-tight text-white sm:text-4xl">
              L'élégance et la tradition kabyle, par Malha Création.
            </h1>
            <div class="mt-10 flex items-center gap-x-6">
              <router-link
                class="mt-8 inline-block border bg-white px-8 py-3 text-base font-medium text-gray-900 hover:bg-gray-100"
                to="/collections/new-in">
                SHOP NEW
              </router-link>
            </div>
          </div>
        </div>
      </div>
      <main>
        <nav aria-label="Offers" class="order-last lg:order-first">
          <div class="mx-auto max-w-7xl lg:px-8">
            <ul role="list" class="grid grid-cols-1 divide-y divide-gray-200 lg:grid-cols-3 lg:divide-x lg:divide-y-0">
              <li v-for="offer in offers" :key="offer.name" class="flex flex-col">
                <a class="relative flex flex-1 flex-col justify-center bg-white px-4 py-6 text-center focus:z-10">
                  <p class="text-sm text-gray-500">
                    {{ offer.name }}
                  </p>
                  <p class="font-semibold text-gray-900">{{ offer.description }}</p>
                </a>
              </li>
            </ul>
          </div>
        </nav>

        <!-- new arrivals -->
        <section aria-labelledby="trending-heading" class="bg-white" v-if="response.data">
          <div class="py-16 sm:py-24 lg:mx-auto lg:max-w-12xl lg:px-4 lg:py-16">
            <div class="flex items-center justify-center px-4 sm:px-6 lg:px-0">
              <h2 id="trending-heading" class="text-xl font-bold tracking-tight text-gray-900">
                NOUVELS ARRIVES
              </h2>
            </div>
            <ProcuctList :products="response.data"
              :width="isDesktop ? 'w-1/5' : isTablet ? 'w-1/3' : isMobile ? 'w-1/2' : '1/6'" />
            <div class="relative mx-auto flex max-w-3xl flex-col items-center text-center mt-8">
              <router-link
                class="mt-8 inline-block border bg-white px-8 py-3 text-base font-medium text-gray-900 hover:bg-gray-100"
                to="/collections/new-in">
                VOIR TOUT
              </router-link>
            </div>
          </div>
        </section>

        <!-- categories -->
        <section aria-labelledby="trending-heading" class="bg-white">
          <div class="lg:mx-auto lg:max-w-12xl lg:px-4 lg:py-4">
            <div class="relative">
              <div class="relative w-full overflow-x-auto">
                <ul role="list"
                  class="mx-4 inline-flex space-x-8 sm:mx-6 lg:mx-0 lg:grid lg:grid-cols-4 lg:gap-x-2 lg:space-x-0">
                  <li v-for="collection in collections" :key="collection.id" @click="onCollectionSelect(product)"
                    class="inline-flex w-64 flex-col text-center lg:w-auto">
                    <div class="group relative">
                      <div class="aspect-h-1 aspect-w-1 w-full overflow-hidden bg-gray-200">
                        <img :src="`https://placehold.co/1024x1792`" :alt="collection.name"
                          class="h-full w-full object-cover object-center group-hover:opacity-75" />
                      </div>
                      <div
                        class="absolute inset-x-0 top-0 flex h-full items-end justify-center overflow-hidden rounded-lg p-4">
                        <div class="relative mx-auto flex max-w-3xl flex-col items-center text-center">
                          <h2 class="text-3xl font-bold tracking-tight text-white sm:text-4xl">
                            {{ collection.name }}
                          </h2>
                          <router-link
                            class="mt-8 inline-block border bg-white px-8 py-3 text-base font-medium text-gray-900 hover:bg-gray-100"
                            :to="`https://placehold.co/1024x1792`">
                            DECOUVRIR
                          </router-link>
                        </div>
                      </div>
                    </div>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </section>

        <div class="relative">
          <!-- Decorative image and overlay -->
          <div aria-hidden="true" class="absolute inset-0 overflow-hidden">
            <img src="https://placehold.co/1792x1024" alt="" class="h-full w-full object-cover object-center" />
          </div>

          <div class="relative px-6 py-32 sm:py-40 lg:px-8 lg:py-56 lg:pr-0">
            <div class="mx-auto max-w-2xl lg:mx-0 lg:max-w-xl">
              <h1 class="text-4xl font-bold tracking-tight text-white sm:text-4xl">
                Intensifiez votre jeu de mode
              </h1>
              <div class="mt-10 flex items-center gap-x-6">
                <router-link
                  class="mt-8 inline-block border bg-white px-8 py-3 text-base font-medium text-gray-900 hover:bg-gray-100"
                  to="/collections/men">
                  DECOUVRIR
                </router-link>
              </div>
            </div>
          </div>
        </div>

        <!-- accessories -->
        <section aria-labelledby="trending-heading" class="bg-white">
          <div class="lg:mx-auto lg:max-w-12xl lg:px-4 lg:py-4">
            <div class="relative mt-2">
              <div class="relative w-full overflow-x-auto">
                <ul role="list"
                  class="mx-4 inline-flex space-x-8 sm:mx-6 lg:mx-0 lg:grid lg:grid-cols-4 lg:gap-x-2 lg:space-x-0">
                  <li v-for="accessory in accessories" :key="accessory.id"
                    class="inline-flex w-64 flex-col text-center lg:w-auto">
                    <div class="group relative">
                      <div class="aspect-h-1 aspect-w-1 w-full overflow-hidden bg-gray-200">
                        <img :src="`https://placehold.co/1024x1792`" :alt="accessory.name"
                          class="h-full w-full object-cover object-center group-hover:opacity-75" />
                      </div>
                      <div
                        class="absolute inset-x-0 top-0 flex h-full items-end justify-center overflow-hidden rounded-lg p-4">
                        <div class="relative mx-auto flex max-w-3xl flex-col items-center text-center">
                          <h2 class="text-3xl font-bold tracking-tight text-white sm:text-4xl">
                            {{ accessory.name }}
                          </h2>
                          <router-link
                            class="mt-8 inline-block border bg-white px-8 py-3 text-base font-medium text-gray-900 hover:bg-gray-100"
                            :to="`/collections/${accessory.value}`">
                            DECOUVRIR
                          </router-link>
                        </div>
                      </div>
                    </div>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </section>
      </main>
    </div>
  </div>
</template>