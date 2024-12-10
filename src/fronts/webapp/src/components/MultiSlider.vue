<script setup>
import { ref, onMounted, watch } from 'vue'
import BaseInput from '../components/BaseInput.vue'

const props = defineProps({
  min: {
    type: Number,
    required: true,
    default: 10
  },
  max: {
    type: Number,
    required: true,
    default: 200
  },
  minVal: {
    type: Number
  },
  maxVal: {
    type: Number
  }
})
const emit = defineEmits(['handleRangeChange'])

const minVal = ref(props.minVal)
const maxVal = ref(props.maxVal)
const range = ref(null)

const getPercent = (value) => Math.round(((value - props.min) / (props.max - props.min)) * 100)

const updateMinVal = () => {
  minVal.value = Math.min(Number(minVal.value), maxVal.value - 1)
  updateRange()
}

const updateMaxVal = () => {
  maxVal.value = Math.max(Number(maxVal.value), minVal.value + 1)
  updateRange()
}

const updateRange = () => {
  if (range.value) {
    const minPercent = getPercent(minVal.value)
    const maxPercent = getPercent(maxVal.value)
    range.value.style.left = `${minPercent}%`
    range.value.style.width = `${maxPercent - minPercent}%`
  }
}

onMounted(() => {
  updateRange()
})

watch([minVal, maxVal], () => {
  emit('handleRangeChange', [minVal.value, maxVal.value])
  updateRange()
})
</script>

<template>
  <div>
    <div class="mt-4 grid grid-cols-1 gap-y-6 sm:grid-cols-2 sm:gap-x-4">
      <div>
        <div class="mt-1 flex justify-start">
          <BaseInput
            v-model.number="minVal"
            :min="min"
            :max="maxVal - 1"
            type="number"
            id="minVal"
            name="minVal"
            autocomplete="min-val"
            class="block w-1/2 rounded-md border-gray-300 shadow-sm focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
          />
        </div>
      </div>

      <div>
        <div class="mt-1 flex justify-end">
          <BaseInput
            v-model.number="maxVal"
            :min="minVal + 1"
            :max="max"
            type="number"
            id="maxVal"
            name="maxVal"
            autocomplete="max-val"
            class="block w-1/2 rounded-md border-gray-300 shadow-sm focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
          />
        </div>
      </div>
    </div>
    <div class="container relative">
      <input
        type="range"
        :min="min"
        :max="max"
        v-model.number="minVal"
        @input="updateMinVal"
        class="thumb thumb--left"
        :style="{ zIndex: minVal > max - 100 ? 5 : '' }"
      />
      <input
        type="range"
        :min="min"
        :max="max"
        v-model.number="maxVal"
        @input="updateMaxVal"
        class="thumb thumb--right"
      />
      <div class="slider">
        <div class="slider__track"></div>
        <div ref="range" class="slider__range"></div>
        <div class="slider__left-value">{{ min }}</div>
        <div class="slider__right-value">{{ max }}</div>
      </div>
    </div>
  </div>
</template>



<style scoped>
.container {
  height: 10vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

.slider {
  position: relative;
  width: 500px;
}

.slider__track,
.slider__range,
.slider__left-value,
.slider__right-value {
  position: absolute;
}

.slider__track,
.slider__range {
  border-radius: 3px;
  height: 5px;
}

.slider__track {
  background-color: #ced4da;
  width: 100%;
  z-index: 1;
}

.slider__range {
  background-color: #000;
  z-index: 2;
}

.slider__left-value,
.slider__right-value {
  color: #dee2e6;
  font-size: 12px;
  margin-top: 20px;
}

.slider__left-value {
  left: 6px;
}

.slider__right-value {
  right: -4px;
}

/* Removing the default appearance */
.thumb,
.thumb::-webkit-slider-thumb {
  -webkit-appearance: none;
}

.thumb {
  pointer-events: none;
  position: absolute;
  height: 0;
  width: 500px;
  outline: none;
}

.thumb--left {
  z-index: 3;
}

.thumb--right {
  z-index: 4;
}

/* For Chrome browsers */
.thumb::-webkit-slider-thumb {
  background-color: #f1f5f7;
  border: none;
  border-radius: 50%;
  box-shadow: 0 0 1px 1px #ced4da;
  cursor: pointer;
  height: 18px;
  width: 18px;
  margin-top: 4px;
  pointer-events: all;
  position: relative;
}

/* For Firefox browsers */
.thumb::-moz-range-thumb {
  background-color: #f1f5f7;
  border: none;
  border-radius: 50%;
  box-shadow: 0 0 1px 1px #ced4da;
  cursor: pointer;
  height: 18px;
  width: 18px;
  margin-top: 4px;
  pointer-events: all;
  position: relative;
}

.bubble {
  background: red;
  color: white;
  padding: 4px 12px;
  position: absolute;
  border-radius: 4px;
  left: 50%;
  transform: translateX(-50%);
}
.bubble::after {
  content: '';
  position: absolute;
  width: 2px;
  height: 2px;
  background: red;
  top: -1px;
  left: 50%;
}
</style>
