<script setup>
const props = defineProps({
  label: { type: String, default: '' },
  id: { type: String, required: true },
  modelValue: {
    type: [String, Number],
    default: ''
  }
})
</script>


<template>
  <input
    v-bind="$attrs"
    :value="modelValue"
    @input="$emit('update:modelValue', $event.target.value)"
    :id="props.id"
  />
</template>

