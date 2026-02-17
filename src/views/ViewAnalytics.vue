<template>
  <div class="p-8 flex flex-col">
    <div class="flex flex-col justify-between items-start gap-10">
      <div class="flex flex-col gap-3">
        <h1>My Analytics</h1>
        <p class="text-gray-500">View your book statistics and be blown away.</p>
      </div>
      <Spinner v-if="loading" class="mt-24 m-auto w-full" />
      <template v-else>
        <!-- Summary cards -->
        <div class="grid grid-cols-1 sm:grid-cols-2 gap-4 w-full">
          <StatCard title="Total books" :value="totalBooks" />
          <StatCard title="Average rating" :value="averageRating" />
        </div>

        <!-- Charts grid -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-6 w-full">
          <BookChart title="Books by rating" type="doughnut" :chart-data="ratingChartData" />
          <BookChart title="Top authors" type="bar" :chart-data="authorsChartData" />
        </div>
      </template>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import type { ChartData } from 'chart.js'
import { fetchBooks } from '@/api/books'
import BookChart from '@/components/views/analytics/BookChart.vue'
import StatCard from '@/components/ui/StatCard.vue'
import Spinner from '@/components/ui/Spinner.vue'

const loading = ref(true)
const books = ref<Awaited<ReturnType<typeof fetchBooks>>['items']>([])
const totalCount = ref(0)

async function loadAnalyticsData() {
  loading.value = true
  try {
    const { items, total } = await fetchBooks(1, 500)
    books.value = items
    totalCount.value = total
  } catch {
    books.value = []
    totalCount.value = 0
  } finally {
    loading.value = false
  }
}

const totalBooks = computed(() => totalCount.value)

const averageRating = computed(() => {
  const list = books.value
  if (!list.length) return '—'
  const sum = list.reduce((acc, b) => acc + b.rating, 0)
  return (sum / list.length).toFixed(1)
})

const ratingChartData = computed<ChartData | null>(() => {
  const list = books.value
  if (!list.length) return null
  const counts: Record<number, number> = { 1: 0, 2: 0, 3: 0, 4: 0, 5: 0 }
  list.forEach((b) => {
    const r = Math.min(5, Math.max(1, Math.round(b.rating)))
    counts[r] = (counts[r] ?? 0) + 1
  })
  return {
    labels: ['1 star', '2 stars', '3 stars', '4 stars', '5 stars'],
    datasets: [
      {
        label: 'Books',
        data: [counts[1] ?? 0, counts[2] ?? 0, counts[3] ?? 0, counts[4] ?? 0, counts[5] ?? 0],
        backgroundColor: [
          'rgb(239, 68, 68)',
          'rgb(249, 115, 22)',
          'rgb(234, 179, 8)',
          'rgb(34, 197, 94)',
          'rgb(22, 163, 74)',
        ],
        borderWidth: 0,
      },
    ],
  }
})

const authorsChartData = computed<ChartData | null>(() => {
  const list = books.value
  if (!list.length) return null
  const byAuthor: Record<string, number> = {}
  list.forEach((b) => {
    const name = b.author?.trim() || 'Unknown'
    byAuthor[name] = (byAuthor[name] ?? 0) + 1
  })
  const sorted = Object.entries(byAuthor)
    .sort(([, a], [, b]) => b - a)
    .slice(0, 10)
  return {
    labels: sorted.map(([name]) => name),
    datasets: [
      {
        label: 'Books',
        data: sorted.map(([, count]) => count as number),
        backgroundColor: 'rgb(20, 184, 166)',
        borderWidth: 0,
      },
    ],
  }
})

onMounted(() => loadAnalyticsData())
</script>
