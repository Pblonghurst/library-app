<template>
  <div class="rounded-xl border border-gray-200 bg-white p-6">
    <h2 v-if="title" class="font-semibold mb-4">
      {{ title }}
    </h2>
    <div class="h-72">
      <Chart
        v-if="chartData?.labels?.length"
        :type="type"
        :data="chartData"
        :options="options"
        class="w-full h-full"
      />
      <p v-else class="flex items-center justify-center h-full text-sm text-gray-500">
        No data to display
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import Chart from 'primevue/chart'
import type { ChartData, ChartOptions } from 'chart.js'
import { computed } from 'vue'

const props = withDefaults(
  defineProps<{
    title?: string
    type?: 'bar' | 'line' | 'pie' | 'doughnut' | 'radar' | 'polarArea'
    chartData?: ChartData | null
  }>(),
  { title: '', type: 'bar' },
)

const options = computed<ChartOptions>(() => {
  const base: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { position: 'bottom' },
    },
  }
  if (props.type === 'bar') {
    return {
      ...base,
      indexAxis: 'y',
      scales: {
        x: { beginAtZero: true },
      },
    }
  }
  return base
})
</script>
