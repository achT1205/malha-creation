<script>
import { ref, watch } from 'vue'

export default {
  setup() {
    const sliderValue = ref(50)
    const tooltipPosition = ref(50)
    const showTooltip = ref(true)

    const updateTooltip = () => {
      // Update tooltip position based on the slider value
      tooltipPosition.value = sliderValue.value
    }

    // Show tooltip on initial load and update
    watch(sliderValue, () => {
      updateTooltip()
    })

    return {
      sliderValue,
      tooltipPosition,
      showTooltip,
      updateTooltip
    }
  }
}
</script>

<template>
  <div class="relative w-full max-w-md mx-auto h-96 mt-80">
    <input
      type="range"
      min="0"
      max="100"
      v-model="sliderValue"
      @input="updateTooltip"
      class="w-full appearance-none bg-gray-200 h-2 rounded-full outline-none"
    />
    <div
      v-show="showTooltip"
      :style="{ left: `${tooltipPosition}%` }"
      class="absolute -top-8 transform -translate-x-1/2 px-2 py-1 bg-black text-white text-sm rounded"
    >
      {{ sliderValue }}
    </div>
  </div>
</template>
