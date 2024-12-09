<script setup>
import { Dialog, DialogPanel, DialogTitle, TransitionChild, TransitionRoot } from '@headlessui/vue'
import { XMarkIcon } from '@heroicons/vue/24/outline'
import { useCartStore } from '@/stores/cartStore'
import { TrashIcon } from '@heroicons/vue/20/solid'
import { useRouter } from 'vue-router'

const router = useRouter()
const cartStore = useCartStore()

const removeFromCart = (product) => {
  cartStore.removeItem(product)
}

const props = defineProps({
  open: { type: Boolean, required: true, default: false }
})

const onCheckout = () => {
  emit('toogle', false)
  router.push('/checkout')
}
const emit = defineEmits(['toogle'])
</script>

<template>
  <TransitionRoot as="template" :show="props.open">
    <Dialog class="relative z-10" @close="emit('toogle', false)">
      <TransitionChild
        as="template"
        enter="ease-in-out duration-500"
        enter-from="opacity-0"
        enter-to="opacity-100"
        leave="ease-in-out duration-500"
        leave-from="opacity-100"
        leave-to="opacity-0"
      >
        <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
      </TransitionChild>

      <div class="fixed inset-0 overflow-hidden">
        <div class="absolute inset-0 overflow-hidden">
          <div class="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
            <TransitionChild
              as="template"
              enter="transform transition ease-in-out duration-500 sm:duration-700"
              enter-from="translate-x-full"
              enter-to="translate-x-0"
              leave="transform transition ease-in-out duration-500 sm:duration-700"
              leave-from="translate-x-0"
              leave-to="translate-x-full"
            >
              <DialogPanel class="pointer-events-auto w-screen max-w-xs">
                <div class="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                  <div class="flex-1 overflow-y-auto px-4 py-6 sm:px-6">
                    <div class="flex items-start justify-between">
                      <DialogTitle class="text-lg font-medium text-gray-900"
                        >Votre panier</DialogTitle
                      >
                      <div class="ml-3 flex h-7 items-center">
                        <button
                          type="button"
                          class="relative -m-2 p-2 text-gray-400 hover:text-gray-500"
                          @click="emit('toogle', false)"
                        >
                          <span class="absolute -inset-0.5" />
                          <span class="sr-only">Close panel</span>
                          <XMarkIcon class="h-6 w-6" aria-hidden="true" />
                        </button>
                      </div>
                    </div>

                    <div class="mt-8">
                      <div class="flow-root">
                        <ul role="list" class="-my-6 divide-y divide-gray-200">
                          <li
                            v-for="product in cartStore.items"
                            :key="product.id"
                            class="flex py-3"
                          >
                            <div
                              class="h-24 w-20 flex-shrink-0 overflow-hidden rounded-md border border-gray-200"
                            >
                              <img
                                :src="product.image"
                                :alt="`${product.name}_${product.color}_${product.size}`"
                                class="h-full w-full object-cover object-center"
                              />
                            </div>

                            <div class="ml-6 flex flex-1 flex-col">
                              <div class="flex">
                                <div class="min-w-0 flex-1">
                                  <h4 class="text-sm">
                                    <a
                                      :href="product.href"
                                      class="font-medium text-gray-700 hover:text-gray-800"
                                      >{{ product.name }}</a
                                    >
                                  </h4>
                                  <p class="mt-1 text-sm text-gray-500">{{ product.color }}</p>
                                  <p class="mt-1 text-sm text-gray-500">{{ product.size }}</p>
                                </div>

                                <div class="ml-4 flow-root flex-shrink-0">
                                  <button
                                    type="button"
                                    class="-m-2.5 flex items-center justify-center bg-white p-2.5 text-gray-400 hover:text-gray-500"
                                  >
                                    <span class="sr-only">Remove</span>
                                    <TrashIcon
                                      @click="removeFromCart(product)"
                                      class="h-5 w-5"
                                      aria-hidden="true"
                                    />
                                  </button>
                                </div>
                              </div>

                              <div class="flex flex-1 items-end justify-between pt-2">
                                <p>
                                  <span
                                    :class="
                                      product.discountPrice &&
                                      product.discountPrice < product.price * product.quantity
                                        ? 'line-through text-red-600'
                                        : ''
                                    "
                                  >
                                    {{ product.price * product.quantity }} €</span
                                  >

                                  <span
                                    class="mt-1 text-sm font-medium text-gray-900 ml-4"
                                    v-if="
                                      product.discountPrice &&
                                      product.discountPrice < product.price * product.quantity
                                    "
                                    >{{ product.discountPrice }}
                                    €
                                  </span>
                                </p>

                                <div class="ml-4">
                                  <label for="quantity" class="sr-only">Quantity</label>
                                  <select
                                    v-model="product.quantity"
                                    @change="cartStore.editItem(product, product.quantity)"
                                    id="quantity"
                                    name="quantity"
                                    class="rounded-md border border-gray-300 text-left text-base font-medium text-gray-700 shadow-sm focus:border-gray-500 focus:outline-none focus:ring-1 focus:ring-gray-500 sm:text-sm"
                                  >
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                  </select>
                                </div>
                              </div>
                            </div>
                          </li>
                        </ul>
                      </div>
                    </div>
                  </div>

                  <div class="border-t border-gray-200 px-4 py-6 sm:px-6">
                    <div class="flex justify-between text-base font-medium text-gray-900">
                      <p>Total</p>
                      <p>
                        <span
                          :class="cartStore.totalDiscountPrice ? 'line-through text-red-600' : ''"
                          >{{ cartStore.totalPrice }} € </span
                        >
                        
                        <span class="ml-3">{{ cartStore.totalDiscountPrice }} €</span>
                      </p>
                    </div>
                    <p class="mt-0.5 text-sm text-gray-500">
                      Frais de port et taxes calculés à la caisse.
                    </p>
                    <div class="mt-6">
                      <span
                        @click="onCheckout"
                        class="flex items-center justify-center rounded-md border border-transparent bg-gray-600 px-6 py-3 text-base font-medium text-white shadow-sm hover:bg-gray-700 cursor-pointer"
                        >Checkout</span
                      >
                    </div>
                    <div class="mt-6 flex justify-center text-center text-sm text-gray-500">
                      <p>
                        ou {{ ' ' }}
                        <button
                          type="button"
                          class="font-medium text-gray-600 hover:text-gray-500"
                          @click="emit('toogle', false)"
                        >
                          Continue Shopping
                          <span aria-hidden="true"> &rarr;</span>
                        </button>
                      </p>
                    </div>
                  </div>
                </div>
              </DialogPanel>
            </TransitionChild>
          </div>
        </div>
      </div>
    </Dialog>
  </TransitionRoot>
</template>