<script setup>
const props = defineProps({
  label: {
    type: String,
    default: ''
  },
  modelValue: {
    type: [String, Number],
    default: ''
  },
  options: {
    type: Array,
    required: true
  }
})
</script>


<template>
  <div class="sm:col-span-3">
    <label
      v-if="props.label"
      :for="props.label"
      class="block text-sm font-medium leading-6 text-gray-900"
      >{{ props.label }}</label
    >
    <div class="mt-2">
      <select
        :value="modelValue"
        v-bind="{
          ...$attrs,
          onChange: ($event) => {
            $emit('update:modelValue', $event.target.value)
          }
        }"
        class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-inset focus:ring-gray-600 sm:max-w-xs sm:text-sm sm:leading-6"
      >
        <option
          v-for="option in options"
          :value="option"
          :key="option"
          :selected="option === modelValue"
        >
          {{ option }}
        </option>
      </select>
    </div>
  </div>
</template>
