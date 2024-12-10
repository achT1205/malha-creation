
<script setup>
import { ref } from 'vue'
import {
  Dialog,
  DialogPanel,
  Popover,
  PopoverButton,
  PopoverGroup,
  PopoverPanel,
  Tab,
  TabGroup,
  TabList,
  TabPanel,
  TabPanels,
  TransitionChild,
  TransitionRoot
} from '@headlessui/vue'
import {
  Bars3Icon,
  MagnifyingGlassIcon,
  ShoppingCartIcon,
  UserIcon,
  XMarkIcon
} from '@heroicons/vue/24/outline'
import { ChevronDownIcon } from '@heroicons/vue/20/solid'
import Cart from './ProductCart.vue'
import LogoView from './LogoView.vue'
import SearchComponent from './SearchComponent.vue'
import { useCartStore } from '@/stores/cartStore'
import { useCurrentUser, useFirebaseAuth } from 'vuefire'
import { signOut } from '@firebase/auth'
import { useRouter } from 'vue-router'

const auth = useFirebaseAuth()
const user = useCurrentUser()
const cartStore = useCartStore()
const router = useRouter()

const navigation = {
  categories: [
    {
      name: 'Femmes',
      featured: [
        {
          name: 'LES PLUS POPULAIRES',

          imageSrc: '/images/categories/women-dresses.png'
        },
        {
          name: 'NOUVELLES ARRIVESS',

          imageSrc: '/images/categories/women-dresses.png'
        }
      ],
      sections: [
        {
          id: 'category',
          name: 'Par catégories',
          items: [
            { name: 'Robes kabyles pour femmes', href: '/collections/women-dresses' },
            { name: 'Sacs', href: '/collections/wemen-bags' },
            { name: 'Accessoires', href: '/collections/wemen-accessoires' }
          ]
        },
        {
          id: 'joularies',
          name: 'En vedette',
          items: [
            { name: 'Nouvelles arrivées ', href: '/collections/new-in' },
            { name: 'Promo en cours (2 pour 20%) ', href: '/collections/sale' },
            { name: 'Les plus populairs ', href: '/collections/most-popular' }
          ]
        },
        {
          id: 'occasions',
          name: 'Par Occasion',
          items: [
            { name: 'Pour le bureau', href: '/collections/for-the-office' },
            { name: 'Fêtes religieuses (ex. : Aïd)', href: '/collections/for-celebrations' },
            { name: 'Simple', href: '/collections/casual-weekends' },
            { name: 'Mariages', href: '/collections/for-wedding' },
            {
              name: 'Célébrations traditionnelles',
              href: '/collections/for-traditional-celebrations'
            }
          ]
        }
      ]
    },
    {
      name: 'Hommes',
      featured: [
        {
          name: 'LES PLUS POPULAIRES',

          imageSrc: '/images/categories/men-burnous.png'
        },
        {
          name: 'NOUVELLES ARRIVESS',

          imageSrc: '/images/categories/men-burnous.png'
        }
      ],
      sections: [
        {
          id: 'category',
          name: 'Par catégories',
          items: [
            { name: 'bournu kabyles', href: '/collections/men-bournu' },
            { name: 'Accessoires', href: '/collections/men-accessoires' }
          ]
        },
        {
          id: 'occasions',
          name: 'Par Occasion',
          items: [
            { name: 'Pour le bureau', href: '/collections/for-the-office' },
            { name: 'Fêtes religieuses (ex. : Aïd)', href: '/collections/for-celebrations' },
            { name: 'Simple', href: '/collections/casual-weekends' },
            { name: 'Mariages', href: '/collections/for-wedding' },
            {
              name: 'Célébrations traditionnelles',
              href: '/collections/for-traditional-celebrations'
            }
          ]
        }
      ]
    },
    {
      name: 'Enfants',
      featured: [
        {
          name: 'LES PLUS POPULAIRES',

          imageSrc: '/images/categories/girl-dresses.png'
        },
        {
          name: 'NOUVELLES ARRIVESS',

          imageSrc: '/images/categories/boys-burnous.png'
        }
      ],
      sections: [
        {
          id: 'category',
          name: 'Par catégories',
          items: [
            { name: 'Robes kabyles pour filles', href: '/collections/women-dresses' },
            { name: 'Bournu kabyles pour garçons', href: '/collections/girls-dresses' },
            { name: 'Accessoires', href: '/collections/wemen-accessoires' }
          ]
        },
        {
          id: 'joularies',
          name: 'En vedette',
          items: [
            { name: 'Nouvelles arrivées ', href: '/collections/new-in' },
            { name: 'Promo en cours (2 pour 20%) ', href: '/collections/sale' }
          ]
        },
        {
          id: 'occasions',
          name: 'Par Occasion',
          items: [
            { name: 'Fêtes religieuses (ex. : Aïd)', href: '/collections/for-celebrations' },
            { name: 'Simple', href: '/collections/casual-weekends' },
            { name: 'Mariages', href: '/collections/for-wedding' },
            {
              name: 'Célébrations traditionnelles',
              href: '/collections/for-traditional-celebrations'
            }
          ]
        }
      ]
    },
    {
      name: 'Collections',
      featured: [
        {
          name: 'Nouvels arrivés',

          imageSrc: '/images/collections/1.png'
        },
        {
          name: 'Robes de fete',

          imageSrc: '/images/collections/2.png'
        },
        {
          name: 'Robes de mariages',

          imageSrc: '/images/collections/3.png'
        },
        {
          name: 'Accessoires',

          imageSrc: '/images/collections/4.png'
        }
      ]
    }
  ],
  pages: [{ name: 'Nous contacter', path: 'contact' }]
}

const profileMenus = [
  { name: 'Mes commandes', to: '/commandes' },
  { name: 'Profile', to: '/profile' }
]

const mobileMenuOpen = ref(false)
const open = ref(false)
const openSearch = ref(false)

const toogleCart = (toogle) => {
  if (toogle === true && open.value === false && cartStore.itemCount === 0) return
  open.value = toogle
}

const toogleSeach = (toogle) => {
  openSearch.value = toogle
}

const logout = async () => {
  await signOut(auth)
    .then(() => {
      console.log('logged out')
      router.push('/')
    })
    .catch((error) => {
      console.log(error)
    })
}

const handleMouseOver = (event) => {
  event.target.click()
}
</script>

<template>
  <div>
    <!-- Mobile menu -->
    <TransitionRoot as="template" :show="mobileMenuOpen">
      <Dialog class="relative z-40 lg:hidden" @close="mobileMenuOpen = false">
        <TransitionChild
          as="template"
          enter="transition-opacity ease-linear duration-300"
          enter-from="opacity-0"
          enter-to="opacity-100"
          leave="transition-opacity ease-linear duration-300"
          leave-from="opacity-100"
          leave-to="opacity-0"
        >
          <div class="fixed inset-0 bg-black bg-opacity-25" />
        </TransitionChild>

        <div class="fixed inset-0 z-40 flex">
          <TransitionChild
            as="template"
            enter="transition ease-in-out duration-300 transform"
            enter-from="-translate-x-full"
            enter-to="translate-x-0"
            leave="transition ease-in-out duration-300 transform"
            leave-from="translate-x-0"
            leave-to="-translate-x-full"
          >
            <DialogPanel
              class="relative flex w-full max-w-xs flex-col overflow-y-auto bg-white pb-12 shadow-xl"
            >
              <div class="flex px-4 pb-2 pt-5">
                <button
                  type="button"
                  class="-m-2 inline-flex items-center justify-center rounded-md p-2 text-gray-400"
                  @click="mobileMenuOpen = false"
                >
                  <span class="sr-only">Close menu</span>
                  <XMarkIcon class="h-6 w-6" aria-hidden="true" />
                </button>
              </div>
              <!-- Links -->
              <TabGroup as="div" class="mt-2">
                <div class="border-b border-gray-200">
                  <TabList class="-mb-px flex space-x-8 px-4">
                    <Tab
                      as="template"
                      v-for="category in navigation.categories"
                      :key="category.name"
                      v-slot="{ selected }"
                    >
                      <button
                        :class="[
                          selected ? 'border-black text-black' : 'border-transparent text-gray-900',
                          'flex-1 whitespace-nowrap border-b-2 px-1 py-4 text-base font-medium'
                        ]"
                      >
                        {{ category.name }}
                      </button>
                    </Tab>
                  </TabList>
                </div>
                <TabPanels as="template">
                  <TabPanel
                    v-for="category in navigation.categories"
                    :key="category.name"
                    class="space-y-12 px-4 py-6"
                  >
                    <div class="grid grid-cols-2 gap-x-4 gap-y-10">
                      <div
                        v-for="item in category.featured"
                        :key="item.name"
                        class="group relative"
                      >
                        <div
                          class="aspect-h-1 aspect-w-1 overflow-hidden rounded-md bg-gray-100 group-hover:opacity-75"
                        >
                          <img
                            :src="item.imageSrc"
                            :alt="item.imageAlt"
                            class="object-cover object-center"
                          />
                        </div>
                        <a :href="item.href" class="mt-6 block text-sm font-medium text-gray-900">
                          <span class="absolute inset-0 z-10" aria-hidden="true" />
                          {{ item.name }}
                        </a>
                      </div>
                    </div>

                    <div v-for="section in category.sections" :key="section.name">
                      <p
                        :id="`${category.id}-${section.id}-heading-mobile`"
                        class="font-medium text-gray-900"
                      >
                        {{ section.name }}
                      </p>
                      <ul
                        role="list"
                        :aria-labelledby="`${category.id}-${section.id}-heading-mobile`"
                        class="mt-6 flex flex-col space-y-6"
                      >
                        <li v-for="item in section.items" :key="item.name" class="flow-root">
                          <a :href="item.href" class="-m-2 block p-2 text-gray-500">{{
                            item.name
                          }}</a>
                        </li>
                      </ul>
                    </div>
                  </TabPanel>
                </TabPanels>
              </TabGroup>
              <div class="space-y-6 border-t border-gray-200 px-4 py-6">
                <div v-for="page in navigation.pages" :key="page.name" class="flow-root">
                  <router-link
                    :to="`/collections/${page.path}`"
                    class="-m-2 block p-2 font-medium text-gray-900"
                    >{{ page.name }}</router-link
                  >
                </div>
              </div>
            </DialogPanel>
          </TransitionChild>
        </div>
      </Dialog>
    </TransitionRoot>

    <header class="relative z-10">
      <nav aria-label="Top">
        <!-- Secondary navigation -->
        <div class="bg-white">
          <div class="border-b border-gray-200">
            <div class="mx-auto max-w-12xl px-4 sm:px-6 lg:px-8">
              <div class="flex h-16 items-center justify-between">
                <!-- Logo (lg+) -->
                <div class="hidden lg:flex lg:items-center">
                  <a href="/">
                    <span class="sr-only">Your Company</span>
                    <LogoView />
                  </a>
                </div>

                <div class="hidden h-full lg:flex">
                  <!-- Mega menus -->
                  <PopoverGroup class="inset-x-0 bottom-0 px-4">
                    <div class="flex h-full justify-center space-x-8">
                      <Popover
                        v-for="category in navigation.categories"
                        :key="category.name"
                        class="flex"
                        v-slot="{ open }"
                      >
                        <div class="relative flex">
                          <PopoverButton
                            class="relative z-10 flex items-center justify-center text-sm font-mediumtransition-colors duration-200 ease-out decoration-white cursor-pointer"
                          >
                            {{ category.name }}
                            <ChevronDownIcon :class="{ 'rotate-180 transform': open }" />
                            <span
                              :class="[
                                open ? 'bg-black' : '',
                                'absolute inset-x-0 -bottom-px h-0.5 transition duration-200 ease-out'
                              ]"
                              aria-hidden="true"
                            />
                          </PopoverButton>
                        </div>

                        <transition
                          enter-active-class="transition ease-out duration-200"
                          enter-from-class="opacity-0"
                          enter-to-class="opacity-100"
                          leave-active-class="transition ease-in duration-150"
                          leave-from-class="opacity-100"
                          leave-to-class="opacity-0"
                        >
                          <PopoverPanel class="absolute inset-x-0 top-full text-sm text-gray-500">
                            <!-- Presentational element used to render the bottom shadow, if we put the shadow on the actual panel it pokes out the top, so we use this shorter element to hide the top of the shadow -->
                            <div
                              class="absolute inset-0 top-1/2 bg-white shadow"
                              aria-hidden="true"
                            />

                            <div v-if="!category.sections" class="relative bg-white">
                              <div class="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                                <div class="grid grid-cols-4 gap-x-8 gap-y-10 py-16">
                                  <div
                                    v-for="item in category.featured"
                                    :key="item.name"
                                    class="group relative"
                                  >
                                    <div
                                      class="aspect-h-1 aspect-w-1 overflow-hidden rounded-md bg-gray-100 group-hover:opacity-75"
                                    >
                                      <img
                                        :src="item.imageSrc"
                                        :alt="item.name"
                                        class="object-cover object-center"
                                      />
                                    </div>
                                    <a
                                      :href="item.href"
                                      class="mt-4 block font-medium text-gray-900"
                                    >
                                      <span class="absolute inset-0 z-10" aria-hidden="true" />
                                      {{ item.name }}
                                    </a>
                                    <p aria-hidden="true" class="mt-1">DECOUVRIR</p>
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div class="relative bg-white" v-else>
                              <div class="mx-auto max-w-7xl px-8">
                                <div class="grid grid-cols-2 gap-x-8 gap-y-10 py-16">
                                  <div class="col-start-2 grid grid-cols-2 gap-x-8">
                                    <div
                                      v-for="item in category.featured"
                                      :key="item.name"
                                      class="group relative text-base sm:text-sm"
                                    >
                                      <div
                                        class="aspect-h-1 aspect-w-1 overflow-hidden rounded-lg bg-gray-100 group-hover:opacity-75 h-60"
                                      >
                                        <img
                                          :src="item.imageSrc"
                                          :alt="item.name"
                                          class="object-cover object-center"
                                        />
                                      </div>

                                      <router-link
                                        :to="'/'"
                                        class="mt-6 block font-medium text-gray-900 no-underline"
                                      >
                                        <span class="absolute inset-0 z-10" aria-hidden="true" />
                                        {{ item.name }}</router-link
                                      >
                                    </div>
                                  </div>
                                  <div
                                    class="row-start-1 grid grid-cols-3 gap-x-8 gap-y-10 text-sm"
                                  >
                                    <div v-for="section in category.sections" :key="section.name">
                                      <p
                                        :id="`${section.name}-heading`"
                                        class="font-medium text-gray-900"
                                      >
                                        {{ section.name }}
                                      </p>
                                      <ul
                                        role="list"
                                        :aria-labelledby="`${section.name}-heading`"
                                        class="mt-6 space-y-6 sm:mt-4 sm:space-y-4"
                                      >
                                        <li
                                          v-for="item in section.items"
                                          :key="item.name"
                                          class="flex"
                                        >
                                          <router-link
                                            :to="item.href"
                                            class="hover:text-gray-800 no-underline"
                                            >{{ item.name }}</router-link
                                          >
                                        </li>
                                      </ul>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </PopoverPanel>
                        </transition>
                      </Popover>
                      <router-link
                        v-for="page in navigation.pages"
                        :key="page.name"
                        :to="`/collections/${page.path}`"
                        class="flex items-center text-sm font-medium"
                        >{{ page.name }}</router-link
                      >
                    </div>
                  </PopoverGroup>
                </div>

                <!-- Mobile menu and search (lg-) -->
                <div class="flex flex-1 items-center lg:hidden">
                  <button
                    type="button"
                    class="-ml-2 rounded-md bg-white p-2 text-gray-400"
                    @click="mobileMenuOpen = true"
                  >
                    <span class="sr-only">Open menu</span>
                    <Bars3Icon class="h-6 w-6" aria-hidden="true" />
                  </button>

                  <!-- Search -->
                  <span
                    @click="toogleSeach(true)"
                    class="ml-2 p-2 text-gray-400 hover:text-gray-500"
                  >
                    <span class="sr-only">Search</span>
                    <MagnifyingGlassIcon class="h-6 w-6" aria-hidden="true" />
                  </span>
                </div>

                <!-- Logo (lg-) -->
                <a href="/" class="lg:hidden">
                  <span class="sr-only">Your Company</span>
                  <svg
                    width="64"
                    height="64"
                    viewBox="0 0 64 64"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <text
                      x="8"
                      y="40"
                      font-family="Roboto, Arial, sans-serif"
                      font-size="32"
                      font-style="italic"
                      fill="black"
                    >
                      M
                    </text>
                    <text
                      x="25"
                      y="40"
                      font-family="Roboto, Arial, sans-serif"
                      font-size="32"
                      font-style="italic"
                      fill="black"
                    >
                      C
                    </text>
                  </svg>
                </a>

                <div class="flex flex-1 items-center justify-end">
                  <div class="flex items-center lg:ml-8">
                    <div class="flex space-x-8">
                      <div class="hidden lg:flex">
                        <span
                          @click="toogleSeach(true)"
                          class="-m-2 p-2 text-gray-400 hover:text-gray-500 cursor-pointer"
                        >
                          <span class="sr-only">Search</span>
                          <MagnifyingGlassIcon class="h-6 w-6" aria-hidden="true" />
                        </span>
                      </div>

                      <div class="flex">
                        <router-link
                          v-if="!user"
                          to="/login"
                          class="-m-2 p-2 text-gray-400 hover:text-gray-500"
                        >
                          <span class="sr-only">Account</span>
                          <UserIcon class="h-8 w-8" aria-hidden="true" />
                        </router-link>
                        <span v-else>
                          <Popover class="relative">
                            <PopoverButton
                              class="inline-flex items-center gap-x-1 text-sm font-semibold leading-6 text-gray-900"
                            >
                              <img
                                class="inline-block h-6 w-6 rounded-full"
                                :src="user.providerData[0].photoURL"
                                alt=""
                              />
                              <span class="ml-3">{{ user.providerData[0].displayName }}</span>
                              <ChevronDownIcon class="h-5 w-5" aria-hidden="true" />
                            </PopoverButton>

                            <transition
                              enter-active-class="transition ease-out duration-200"
                              enter-from-class="opacity-0 translate-y-1"
                              enter-to-class="opacity-100 translate-y-0"
                              leave-active-class="transition ease-in duration-150"
                              leave-from-class="opacity-100 translate-y-0"
                              leave-to-class="opacity-0 translate-y-1"
                            >
                              <PopoverPanel
                                class="absolute left-1/2 z-10 mt-5 flex w-screen max-w-min -translate-x-1/2 px-4"
                              >
                                <div
                                  class="w-56 shrink rounded-xl bg-white p-4 text-sm font-semibold leading-6 text-gray-900 shadow-lg ring-1 ring-gray-900/5"
                                >
                                  <router-link
                                    v-for="item in profileMenus"
                                    :key="item.name"
                                    :to="item.to"
                                    class="block p-2 hover:text-gray-600"
                                    >{{ item.name }}</router-link
                                  >
                                  <a @click="logout" class="block p-2 hover:text-gray-600 cursor-pointer"
                                    >Deconexion</a
                                  >
                                </div>
                              </PopoverPanel>
                            </transition>
                          </Popover>
                        </span>
                      </div>
                    </div>

                    <span class="mx-4 h-6 w-px bg-gray-200 lg:mx-6" aria-hidden="true" />

                    <div class="flow-root cursor-pointer" @click="toogleCart(true)">
                      <a class="group -m-2 flex items-center p-2">
                        <ShoppingCartIcon
                          class="h-6 w-6 flex-shrink-0 text-gray-400 group-hover:text-gray-500"
                          aria-hidden="true"
                        />
                        <span
                          class="ml-2 text-sm font-medium text-gray-700 group-hover:text-gray-800"
                          >{{ cartStore.itemCount }}</span
                        >
                        <span class="sr-only">items in cart, view bag</span>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </nav>
    </header>
    <Cart :open="open" @toogle="toogleCart" />
    <SearchComponent :open="openSearch" @toogle="toogleSeach" />
  </div>
</template>