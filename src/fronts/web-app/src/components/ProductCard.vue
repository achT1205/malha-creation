<script setup>
import { ref } from 'vue'

import { useRouter } from 'vue-router'
import { RadioGroup, RadioGroupOption } from '@headlessui/vue'
import { useCartStore } from '@/stores/cartStore'

const cartStore = useCartStore()
const router = useRouter()
const selectedColor = ref()
const props = defineProps({
  product: { type: Object, required: true }
})

const onSelectSize = (event) => {
  event.stopPropagation()

  const product = {
    id: props.product.id,
    name: props.product.name,
    price: selectedColor.value.price,
    color: selectedColor.value.name.toLowerCase(),
    size: selectedColor.value.selectedSize.value,
    reviews: selectedColor.value.reviews,
    image: selectedColor.value.image,
    quantity: 1
  }
  cartStore.addItem(product)
}

const onProductSelect = () => {
  router.push(`/products/${selectedColor.value.slug}`)
}

selectedColor.value = props.product.colorVariants.find(
  (c) => c.name.toLowerCase() === props.product.defaultColor.toLowerCase()
)
</script>


<template>
  <div class="group relative cursor-pointer" v-if="selectedColor">
    <div class="aspect-h-1 aspect-w-1 w-full overflow-hidden bg-gray-200">
      <img
        :src="selectedColor.image"
        :alt="`${props.product.name}_${selectedColor.name}`"
        class="h-full w-full object-cover object-center group-hover:opacity-75"
      />
    </div>
    <div
      class="absolute inset-x-0 top-10 flex h-3/5 items-end justify-start overflow-hidden rounded-lg p-4"
      @click="onProductSelect()"
    >
      <span
        :class="[
          'inline-flex items-center rounded-full  px-1.5 py-0.5 text-xs font-medium  group-hover:hidden transition-all w-full',
          selectedColor.isNewArrival === true
            ? 'text-gray-600 bg-gray-100'
            : selectedColor.discount
            ? 'text-red-700 bg-red-100'
            : ''
        ]"
      >
        <span v-if="selectedColor.isNewArrival === true">NOUVEAU</span>
        <span v-else-if="selectedColor.discount">{{ selectedColor.discount.label }}</span>
      </span>

      <div
        class="bg-gray-100 shadow sm:rounded-lg w-96 opacity-0 group-hover:opacity-100 transition-opacity duration-300"
      >
        <div class="px-4 py-2">
          <div class="mt-1">
            <h2 class="text-l font-bold tracking-tight text-gray-900">QUICK ADD</h2>
            <RadioGroup v-model="selectedColor.selectedSize" class="mt-4 grid grid-cols-4 gap-2">
              <RadioGroupOption
                as="template"
                v-for="size in selectedColor.sizes"
                :key="size.name"
                :value="size"
                :disabled="!size.quantity || size.quantity == 0"
                v-slot="{ active, checked }"
                @click="onSelectSize($event, size)"
              >
                <div
                  :class="[
                    size.quantity > 0
                      ? 'cursor-pointer bg-white text-gray-900 shadow-sm'
                      : 'cursor-not-allowed bg-gray-50 text-gray-200',
                    active ? 'ring-2 ring-black' : '',
                    'group relative flex items-center justify-center rounded-md border h-8 px-4 py-3 text-sm font-medium uppercase hover:bg-black hover:text-white focus:outline-none sm:flex-1',
                    size.name.length > 3 ? 'w-12' : 'w-8'
                  ]"
                >
                  <span>{{ size.name }}</span>
                  <span
                    v-if="size.quantity > 0"
                    :class="[
                      active ? 'border' : 'border-2',
                      checked ? 'border-black' : 'border-transparent',
                      'pointer-events-none absolute -inset-px rounded-md'
                    ]"
                    aria-hidden="true"
                  />
                  <span
                    v-else
                    aria-hidden="true"
                    class="pointer-events-none absolute -inset-px rounded-md border-2 border-gray-200"
                  >
                    <svg
                      class="absolute inset-0 h-full w-full stroke-2 text-gray-200"
                      viewBox="0 0 100 100"
                      preserveAspectRatio="none"
                      stroke="currentColor"
                    >
                      <line x1="0" y1="100" x2="100" y2="0" vector-effect="non-scaling-stroke" />
                    </svg>
                  </span>
                </div>
              </RadioGroupOption>
            </RadioGroup>
          </div>
        </div>
      </div>
    </div>

    <div class="mt-6">
      <h3 class="mt-1 font-semibold text-gray-900 cursor-pointer" @click="onProductSelect()">
        {{ props.product.name }}
      </h3>
      <h4 class="sr-only">Available colors</h4>
      <RadioGroup
        v-model="selectedColor"
        class="mt-auto flex items-center justify-center space-x-3 pt-6"
      >
        <RadioGroupOption
          as="template"
          v-for="color in props.product.cocolorVariantslors"
          :key="color.color"
          :value="color"
          :aria-label="color.name"
          v-slot="{ active, checked }"
        >
          <div
            :class="[
              color.class,
              (active && checked) || selectedColor.name === color.name ? 'ring ring-offset-1' : '',
              !active && checked ? 'ring-2' : '',
              'relative -m-0.5 flex cursor-pointer items-center justify-center rounded-full p-0.5 ring-current focus:outline-none'
            ]"
          >
            <span
              aria-hidden="true"
              class="h-4 w-4 rounded-full border border-black border-opacity-10 bg-current"
            />
          </div>
        </RadioGroupOption>
      </RadioGroup>

      <p class="mt-5 text-gray-900">€{{ selectedColor.price }} EUR</p>
    </div>
  </div>
</template>