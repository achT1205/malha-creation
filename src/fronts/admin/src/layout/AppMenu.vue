<script setup>
import { ref } from 'vue';
import AppMenuItem from './AppMenuItem.vue';

const model = ref([
    {
        label: 'Dashboards',
        icon: 'pi pi-home',
        items: [
            {
                label: 'E-Commerce',
                icon: 'pi pi-fw pi-warehouse',
                to: '/'
            }
        ]
    },
    { separator: true },
    {
        label: 'Product Management',
        icon: 'pi pi-fw pi-wallet',
        items: [
            {
                label: 'New Product',
                icon: 'pi pi-fw pi-plus',
                to: '/product-anagement/new-product'
            },
            {
                label: 'Products',
                icon: 'pi pi-fw pi-list',
                to: '/product-anagement/product-list'
            },
            {
                label: 'Parametres',
                icon: 'pi pi-fw pi-user',
                items: [
                    {
                        label: 'Categories',
                        icon: 'pi pi-fw pi-list',
                        to: '/product-anagement/parameters/categories'
                    },
                    {
                        label: 'Brands',
                        icon: 'pi pi-fw pi-list',
                        to: '/product-anagement/parameters/brands'
                    },
                    {
                        label: 'Occasions',
                        icon: 'pi pi-fw pi-list',
                        to: '/product-anagement/parameters/occasions'
                    },
                    {
                        label: 'Materials',
                        icon: 'pi pi-fw pi-list',
                        to: '/product-anagement/parameters/materials'
                    },
                    {
                        label: 'Collections',
                        icon: 'pi pi-fw pi-list',
                        to: '/product-anagement/parameters/collections'
                    }
                ]
            },

        ]
    },
    { separator: true },
    {
        label: 'Orders',
        icon: 'pi pi-fw pi-wallet',
        items: [
            {
                label: 'Orders',
                icon: 'pi pi-fw pi-list',
                to: '/profile/list'
            }
        ]
    },
    { separator: true },
    {
        label: 'User Management',
        icon: 'pi pi-fw pi-user',
        items: [
            {
                label: 'List',
                icon: 'pi pi-fw pi-list',
                to: '/profile/list'
            },
            {
                label: 'Create',
                icon: 'pi pi-fw pi-plus',
                to: '/profile/create'
            }
        ]
    },
    { separator: true },
    {
        label: 'Customers',
        icon: 'pi pi-fw pi-user',
        items: [
            {
                label: 'List',
                icon: 'pi pi-fw pi-list',
                to: '/profile/list'
            }
        ]
    }
]);
</script>

<template>
    <ul class="layout-menu">
        <template v-for="(item, i) in model" :key="item">
            <AppMenuItem v-if="!item.separator" :item="item" root :index="i" />

            <li v-else class="menu-separator" />
        </template>
    </ul>
</template>
