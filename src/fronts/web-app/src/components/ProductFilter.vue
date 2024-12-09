<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import {
  Dialog,
  DialogPanel,
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
  Menu,
  MenuButton,
  MenuItem,
  MenuItems,
  Popover,
  PopoverButton,
  PopoverGroup,
  PopoverPanel,
  TransitionChild,
  TransitionRoot
} from '@headlessui/vue'
import { XMarkIcon } from '@heroicons/vue/24/outline'
import { ChevronDownIcon } from '@heroicons/vue/20/solid'
import MultiSlider from '@/components/MultiSlider.vue'
import { useRouter, useRoute } from 'vue-router'

const sortOptions = ref([
  { name: 'Most Popular', current: true },
  { name: 'Best Rating', current: false },
  { name: 'Newest', current: false }
])

const router = useRouter()
const route = useRoute()
const sort = ref(null)
const filters = ref()

const minPrice = ref(20)
const maxPrice = ref(150)
const canApplyFilter = ref(false)
const priceRange = ref([10, 200])

filters.value = [
  {
    id: 'price',
    name: 'Prix'
  },
  {
    id: 'category',
    name: 'Category',
    options: [
      { value: 'new-arrivals', label: 'All New Arrivals', checked: false },
      { value: 'dresses', label: 'Robe', checked: false }
    ]
  },
  {
    id: 'color',
    name: 'Color',
    options: [
      { value: 'white', label: 'Blanc', checked: false },
      { value: 'pink', label: 'Rose', checked: false },
      { value: 'blue', label: 'Bleu', checked: false },
      { value: 'green', label: 'Vert', checked: false },
      { value: 'red', label: 'Rouge', checked: false }
    ]
  },
  {
    id: 'material',
    name: 'Matière',
    options: [
      { value: 'coton', label: 'Cotonhite', checked: false },
      { value: 'wool', label: 'Laine', checked: false }
    ]
  },
  {
    id: 'model',
    name: 'Model',
    options: [{ value: 'classical', label: 'Classique', checked: false }]
  },
  {
    id: 'occasion',
    name: 'Occasion',
    options: [{ value: 'traditionals', label: 'Célébrations traditionnelles', checked: false }]
  },
  {
    id: 'sizes',
    name: 'Sizes',
    options: [
      { value: 'small', label: 'Small', checked: false },
      { value: 'medium', label: 'Medium', checked: false },
      { value: 'large', label: 'Large', checked: false }
    ]
  }
]
const activeFilters = [
  {
    value: 'new-in',
    label: 'Nouvels arrivés',
    description: 'Thoughtfully designed Collection 1 for'
  },
  {
    value: 'women-dresses',
    label: 'Robes femme',
    description: 'Thoughtfully designed Collection 1 for'
  },
  {
    value: 'gilr-dresses',
    label: 'Robes fille',
    description: 'Thoughtfully designed Collection 1 for'
  },
  {
    value: 'men-urnous',
    label: 'Burnous homme',
    description: 'Thoughtfully designed Collection 1 for'
  },
  {
    value: 'boy-urnous',
    label: 'Burnous garçon',
    description: 'Thoughtfully designed Collection 1 for'
  },
  {
    value: 'jewelry',
    label: 'Bijoux',
    description: 'Thoughtfully designed Collection 1 for'
  },
  { value: 'bags', label: 'Sacs', description: 'Thoughtfully designed Collection 1 for' }
]
const selectedFilter = ref()

const open = ref(false)

const onSelectFilter = (filter) => {
  selectedFilter.value = filter

  filters.value.forEach((filter) => {
    if (filter.option) filter.options.forEach((_) => (_.checked = false))
  })
}

const handleSortOptionChange = (selectedOption) => {
  sortOptions.value.forEach((option) => {
    option.current = option === selectedOption
    sort.value = selectedOption
  })
  canApplyFilter.value = true
}

const handleRouteChange = () => {
  selectedFilter.value = activeFilters.find(
    (f) => f.value.toLocaleLowerCase() === route.params.slug
  )
  if (route.query) {
    const query = route.query
    for (const key in query) {
      if (query.hasOwnProperty(key)) {
        const index = filters.value.findIndex((_) => _.id === key)
        if (index === -1) {
          if (key === 'min-price') {
            const val = parseInt(query['min-price'])
            minPrice.value = val < priceRange.value[0] ? priceRange.value[0] : val
          }
          if (key === 'max-price') {
            const val = parseInt(query['max-price'])
            maxPrice.value = val > priceRange.value[1] ? priceRange.value[1] : val
          }
          if (key === 'sort') {
            sort.value = sortOptions.value.find((_) => _.name === query['sort'])
            handleSortOptionChange(sort.value)
          }
        } else {
          filters.value[index].options.forEach((option) => {
            if (query[key].indexOf(option.value) > -1) {
              option.checked = true
            } else {
              option.checked = false
            }
          })
        }
      }
    }
  }

  canApplyFilter.value = false
}

const handleRangeChange = (range) => {
  minPrice.value = range[0]
  maxPrice.value = range[1]
  canApplyFilter.value = true
}

const handleFiltershange = () => {
  const query = {}
  const newfilters = filters.value
  newfilters.forEach((filter) => {
    if (filter.id !== 'price') {
      const checkeds = filter.options.filter((_) => _.checked)
      if (checkeds.length > 0) query[filter.id] = checkeds.map((_) => _.value)
    }
  })
  query['min-price'] = minPrice.value
  query['max-price'] = maxPrice.value
  query['sort'] = sort.value?.name
  canApplyFilter.value = false

  router.push({ query: query })
}

onMounted(() => route.fullPath, handleRouteChange())

watch(
  filters.value,
  () => {
    canApplyFilter.value = true
  },
  { deep: true }
)
</script>
<template>
  <div class="bg-white">
    <!-- Mobile filter dialog -->
    <TransitionRoot as="template" :show="open">
      <Dialog class="relative z-40 sm:hidden" @close="open = false">
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
            enter-from="translate-x-full"
            enter-to="translate-x-0"
            leave="transition ease-in-out duration-300 transform"
            leave-from="translate-x-0"
            leave-to="translate-x-full"
          >
            <DialogPanel
              class="relative ml-auto flex h-full w-full max-w-xs flex-col overflow-y-auto bg-white py-4 pb-12 shadow-xl"
            >
              <div class="flex items-center justify-between px-4">
                <h2 class="text-lg font-medium text-gray-900">Filters</h2>
                <button
                  type="button"
                  class="-mr-2 flex h-10 w-10 items-center justify-center rounded-md bg-white p-2 text-gray-400"
                  @click="open = false"
                >
                  <span class="sr-only">Close menu</span>
                  <XMarkIcon class="h-6 w-6" aria-hidden="true" />
                </button>
              </div>

              <!-- Filters -->
              <form class="mt-4">
                <Disclosure
                  as="div"
                  v-for="section in filters"
                  :key="section.name"
                  class="border-t border-gray-200 px-4 py-6"
                  v-slot="{ open }"
                >
                  <h3 class="-mx-2 -my-3 flow-root">
                    <DisclosureButton
                      class="flex w-full items-center justify-between bg-white px-2 py-3 text-sm text-gray-400"
                    >
                      <span class="font-medium text-gray-900">{{ section.name }}</span>
                      <span class="ml-6 flex items-center">
                        <ChevronDownIcon
                          :class="[open ? '-rotate-180' : 'rotate-0', 'h-5 w-5 transform']"
                          aria-hidden="true"
                        />
                      </span>
                    </DisclosureButton>
                  </h3>
                  <DisclosurePanel class="pt-6">
                    <div class="space-y-6">
                      <div
                        v-for="(option, optionIdx) in section.options"
                        :key="option.value"
                        class="flex items-center"
                      >
                        <input
                          :id="`filter-mobile-${section.id}-${optionIdx}`"
                          :name="`${section.id}[]`"
                          :value="option.value"
                          type="checkbox"
                          :checked="option.checked"
                          class="h-4 w-4 rounded border-gray-300 text-gray-600 focus:ring-gray-500"
                        />
                        <label
                          :for="`filter-mobile-${section.id}-${optionIdx}`"
                          class="ml-3 text-sm text-gray-500"
                          >{{ option.label }}</label
                        >
                      </div>
                    </div>
                  </DisclosurePanel>
                </Disclosure>
              </form>
            </DialogPanel>
          </TransitionChild>
        </div>
      </Dialog>
    </TransitionRoot>

    <div
      class="mx-auto max-w-3xl px-4 text-center sm:px-6 lg:max-w-7xl lg:px-8"
      v-if="selectedFilter"
    >
      <div class="py-20">
        <h1 class="text-4xl font-bold tracking-tight text-gray-900">
          {{ selectedFilter.label }}
        </h1>
        <p class="mx-auto mt-4 max-w-3xl text-base text-gray-500">
          {{ selectedFilter.description }}
        </p>
      </div>
    </div>

    <!-- Filters -->
    <section aria-labelledby="filter-heading">
      <h2 id="filter-heading" class="sr-only">Filters</h2>

      <div class="border-b border-gray-200 bg-white pb-4">
        <div class="mx-auto flex max-w-7xl items-center justify-between px-4 sm:px-6 lg:px-8">
          <Menu as="div" class="relative inline-block text-left">
            <div>
              <MenuButton
                class="group inline-flex justify-center text-sm font-medium text-gray-700 hover:text-gray-900"
              >
                Sort <span v-if="sort">({{ sort.name }})</span>
                <ChevronDownIcon
                  class="-mr-1 ml-1 h-5 w-5 flex-shrink-0 text-gray-400 group-hover:text-gray-500"
                  aria-hidden="true"
                />
              </MenuButton>
            </div>

            <transition
              enter-active-class="transition ease-out duration-100"
              enter-from-class="transform opacity-0 scale-95"
              enter-to-class="transform opacity-100 scale-100"
              leave-active-class="transition ease-in duration-75"
              leave-from-class="transform opacity-100 scale-100"
              leave-to-class="transform opacity-0 scale-95"
            >
              <MenuItems
                class="absolute left-0 z-10 mt-2 w-40 origin-top-left rounded-md bg-white shadow-2xl ring-1 ring-black ring-opacity-5 focus:outline-none"
              >
                <div class="py-1">
                  <MenuItem v-for="option in sortOptions" :key="option.name" v-slot="{ active }">
                    <span
                      @click="handleSortOptionChange(option)"
                      :class="[
                        option.current ? 'font-medium text-gray-900' : 'text-gray-500',
                        active ? 'bg-gray-100' : '',
                        'block px-4 py-2 text-sm cursor-pointer'
                      ]"
                      >{{ option.name }}</span
                    >
                  </MenuItem>
                </div>
              </MenuItems>
            </transition>
          </Menu>

          <button
            type="button"
            class="inline-block text-sm font-medium text-gray-700 hover:text-gray-900 sm:hidden"
            @click="open = true"
          >
            Filters
          </button>

          <div class="hidden sm:block">
            <div class="flow-root">
              <PopoverGroup class="-mx-4 flex items-center divide-x divide-gray-200">
                <Popover
                  v-for="section in filters"
                  :key="section.name"
                  class="relative inline-block px-4 text-left"
                >
                  <PopoverButton
                    class="group inline-flex justify-center text-sm font-medium text-gray-700 hover:text-gray-900"
                  >
                    <span>{{ section.name }}</span>
                    <span
                      v-if="
                        section.id != 'price' && section.options.filter((o) => o.checked).length
                      "
                      class="ml-1.5 rounded bg-gray-200 px-1.5 py-0.5 text-xs font-semibold tabular-nums text-gray-700"
                      >{{ section.options.filter((o) => o.checked).length }}</span
                    >
                    <span v-if="section.id === 'price' && minPrice && maxPrice"
                      >({{ minPrice }}-{{ maxPrice }})€</span
                    >
                    <ChevronDownIcon
                      class="-mr-1 ml-1 h-5 w-5 flex-shrink-0 text-gray-400 group-hover:text-gray-500"
                      aria-hidden="true"
                    />
                  </PopoverButton>

                  <transition
                    enter-active-class="transition ease-out duration-100"
                    enter-from-class="transform opacity-0 scale-95"
                    enter-to-class="transform opacity-100 scale-100"
                    leave-active-class="transition ease-in duration-75"
                    leave-from-class="transform opacity-100 scale-100"
                    leave-to-class="transform opacity-0 scale-95"
                  >
                    <PopoverPanel
                      class="absolute left-0 z-10 mt-2 origin-top-right rounded-md bg-white p-4 shadow-2xl ring-1 ring-black ring-opacity-5 focus:outline-none flex w-screen max-w-max"
                    >
                      <div class="space-y-4" v-if="section.id != 'price'">
                        <div
                          v-for="(option, optionIdx) in section.options"
                          :key="option.value"
                          class="flex items-center"
                        >
                          <input
                            :id="`filter-${section.id}-${optionIdx}`"
                            :name="`${section.id}[]`"
                            :value="option.value"
                            type="checkbox"
                            v-model="option.checked"
                            :checked="option.checked"
                            class="h-4 w-4 rounded border-gray-300 text-gray-600 focus:ring-gray-500"
                          />
                          <label
                            :for="`filter-${section.id}-${optionIdx}`"
                            class="ml-3 whitespace-nowrap pr-6 text-sm font-medium text-gray-900"
                            >{{ option.label }}</label
                          >
                        </div>
                      </div>
                      <div class="space-y-4" v-else>
                        <MultiSlider
                          :min="priceRange[0]"
                          :max="priceRange[1]"
                          :minVal="minPrice"
                          :maxVal="maxPrice"
                          @handleRangeChange="handleRangeChange"
                        />
                      </div>
                    </PopoverPanel>
                  </transition>
                </Popover>
                <button
                  class="inline-block border px-2 py-1 text-base text-gray-900 hover:bg-gray-100"
                  :class="canApplyFilter ? 'bg-gray-300' : 'bg-white'"
                  @click="handleFiltershange"
                >
                  APPLIQUER
                </button>
              </PopoverGroup>
            </div>
          </div>
        </div>
      </div>

      <!-- Active filters -->
      <div class="bg-gray-100">
        <div class="mx-auto max-w-7xl px-4 py-3 sm:flex sm:items-center sm:px-6 lg:px-8">
          <h3 class="text-sm font-medium text-gray-500">
            Collections
            <span class="sr-only">, active</span>
          </h3>

          <div aria-hidden="true" class="hidden h-5 w-px bg-gray-300 sm:ml-4 sm:block" />

          <div class="mt-2 sm:ml-4 sm:mt-0">
            <div class="-m-1 flex flex-wrap items-center">
              <router-link
                :to="`/collections/${activeFilter.value}`"
                v-for="activeFilter in activeFilters"
                :key="activeFilter.value"
                class="m-1 inline-flex items-center rounded-full border py-1.5 pl-3 pr-2 text-sm font-medium text-gray-900 cursor-pointer"
                :class="
                  selectedFilter && selectedFilter.value === activeFilter.value
                    ? 'border-gray-500 bg-gray-200'
                    : 'border-gray-200 bg-white'
                "
                @click="onSelectFilter(activeFilter)"
              >
                <span>{{ activeFilter.label }}</span>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>
<style scoped>
input[type='range'] {
  -webkit-appearance: none;
  width: 100%;
  background: transparent;
  pointer-events: none;
}

input[type='range']::-webkit-slider-thumb {
  -webkit-appearance: none;
  appearance: none;
  width: 20px;
  height: 20px;
  background: #1f2937;
  cursor: pointer;
  pointer-events: auto;
  border-radius: 50%;
  border: 2px solid #ffffff;
}

input[type='range']::-moz-range-thumb {
  width: 20px;
  height: 20px;
  background: #1f2937;
  cursor: pointer;
  pointer-events: auto;
  border-radius: 50%;
  border: 2px solid #ffffff;
}
</style>
