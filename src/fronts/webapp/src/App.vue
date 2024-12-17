<script setup>
import { RouterLink, RouterView } from 'vue-router'
import AppHeader from '@/components/AppHeader.vue'
</script>

<template>
  <!-- Header -->
  <AppHeader />
  <!-- /Header -->
  <RouterView />
</template>
