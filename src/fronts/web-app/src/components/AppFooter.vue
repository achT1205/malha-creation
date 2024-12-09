<template>
  <footer aria-labelledby="footer-heading" class="bg-white">
    <h2 id="footer-heading" class="sr-only">Footer</h2>
    <div class="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
      <div class="border-t border-gray-200">
        <div class="pb-20 pt-16">
          <div class="mx-auto mt-16 max-w-5xl xl:grid xl:grid-cols-2 xl:gap-8">
            <div class="grid grid-cols-2 gap-8 xl:col-span-2">
              <div class="space-y-12 md:grid md:grid-cols-2 md:gap-8 md:space-y-0">
                <div>
                  <h3 class="text-sm font-medium text-gray-900">Robes & Burnous Kabyles</h3>
                  <ul role="list" class="mt-6 space-y-6">
                    <li v-for="item in footerNavigation.clothing" :key="item.name" class="text-sm">
                      <a :href="item.href" class="text-gray-500 hover:text-gray-600">{{
                        item.name
                      }}</a>
                    </li>
                  </ul>
                </div>
                <div>
                  <h3 class="text-sm font-medium text-gray-900">Bijoux Kabyles</h3>
                  <ul role="list" class="mt-6 space-y-6">
                    <li v-for="item in footerNavigation.jewelry" :key="item.name" class="text-sm">
                      <a :href="item.href" class="text-gray-500 hover:text-gray-600">{{
                        item.name
                      }}</a>
                    </li>
                  </ul>
                </div>
              </div>
              <div class="space-y-12 md:grid md:grid-cols-2 md:gap-8 md:space-y-0">
                <div>
                  <h3 class="text-sm font-medium text-gray-900">Sacs Accessoires</h3>
                  <ul role="list" class="mt-6 space-y-6">
                    <li
                      v-for="item in footerNavigation.accessories"
                      :key="item.name"
                      class="text-sm"
                    >
                      <a :href="item.href" class="text-gray-500 hover:text-gray-600">{{
                        item.name
                      }}</a>
                    </li>
                  </ul>
                </div>
                <div>
                  <h3 class="text-sm font-medium text-gray-900">Service client</h3>
                  <ul role="list" class="mt-6 space-y-6">
                    <li v-for="item in footerNavigation.services" :key="item.name" class="text-sm">
                      <a :href="item.href" class="text-gray-500 hover:text-gray-600">{{
                        item.name
                      }}</a>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="lg:grid lg:grid-cols-1 lg:gap-x-6 xl:gap-x-8">
          <div class="flex items-center rounded-lg bg-gray-100 p-6 sm:p-10">
            <div class="mx-auto max-w-sm">
              <h3 class="font-semibold text-gray-900">Inscrivez-vous à notre newsletter</h3>
              <p class="mt-2 text-sm text-gray-500">
                Les dernières nouvelles, articles et ressources, envoyés chaque semaine dans votre
                boîte de réception.
              </p>
              <form class="mt-4 sm:mt-6 sm:flex">
                <label for="email-address" class="sr-only"> Addresse Email</label>
                <input
                  id="email-address"
                  type="text"
                  autocomplete="email"
                  required=""
                  class="w-full min-w-0 appearance-none rounded-md border border-gray-300 bg-white px-4 py-2 text-base text-gray-900 placeholder-gray-500 shadow-sm focus:border-white-500 focus:outline-none focus:ring-1 focus:ring-white-500"
                />
                <div class="mt-3 sm:ml-4 sm:mt-0 sm:flex-shrink-0">
                  <button
                    type="submit"
                    class="flex w-full items-center justify-center rounded-md border border-transparent bg-white px-4 py-2 text-base font-medium text-black shadow-sm hover:bg-white-700 focus:outline-none focus:ring-2 focus:ring-white-500 focus:ring-offset-2 focus:ring-offset-black"
                  >
                    S'inscrire
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script setup>
const footerNavigation = {
  clothing: [
    { name: 'Robe de fete', href: '#' },
    { name: 'Robe de sortie', href: '#' },
    { name: 'Robe de de maison', href: '#' },
    { name: 'Robe de simple', href: '#' },
    { name: 'Burnous de fete', href: '#' },
    { name: 'Burnous de sortie', href: '#' },
    { name: 'Burnous de de maison', href: '#' },
    { name: 'Burnous de simple', href: '#' }
  ],
  jewelry: [
    { name: 'Colliers ', href: '#' },
    { name: 'Boucles ', href: '#' },
    { name: 'Bagues ', href: '#' },
    { name: 'Bracelets', href: '#' },
    { name: 'Broches ', href: '#' },
    { name: 'Bijoux de têtes', href: '#' },
    { name: 'Bijoux de pieds ', href: '#' },
    { name: 'Bijoux Ceintures ', href: '#' }
  ],
  accessories: [
    { name: 'Accessoires', href: '#' },
    { name: 'Sacs', href: '#' },
    { name: 'Chapeaux', href: '#' },
    { name: 'Cintures', href: '#' }
  ],
  services: [
    { name: 'Contacter nous', href: '#' },
    { name: 'Shipping', href: '#' },
    { name: 'Returns', href: '#' },
    { name: 'Warranty', href: '#' },
    { name: 'Secure Payments', href: '#' },
    { name: 'FAQ', href: '#' }
  ]
}
</script>