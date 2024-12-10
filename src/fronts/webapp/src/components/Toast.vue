<template>
  <div class="toast-container">
    <div v-for="(toast, index) in toasts" :key="index" :class="`toast toast-${toast.severity}`">
      <div class="toast-summary">{{ toast.summary }}</div>
      <div class="toast-detail">{{ toast.detail }}</div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    toasts: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style>
.toast-container {
  position: fixed;
  top: 10px;
  right: 10px;
  z-index: 9999;
}
.toast {
  margin-bottom: 10px;
  padding: 10px;
  border-radius: 4px;
  color: #fff;
}
.toast-error {
  background-color: #f44336;
}
.toast-summary {
  font-weight: bold;
}
.toast-detail {
  margin-top: 5px;
}
</style>
