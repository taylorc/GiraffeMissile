<script setup lang="ts">
interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const config = useRuntimeConfig();

const { data: weatherForecasts, error: error } = await useAPIFetch<WeatherForecast[]>("/api/WeatherForecasts");
</script>
<template>
  <div class="flex flex-col ">
    <div v-if="weatherForecasts" class>
      <table class="table-lg w-full text-left">
        <thead class="bg-amber-800">
        <tr>
          <th>Date</th>
          <th>Summary</th>
          <th>T (°C)</th>
          <th>T (°F)</th>
        </tr>
        </thead>
        <tbody >
        <tr v-for="item in weatherForecasts" :key="item.date" class="odd:bg-amber-100 odd:text-black even:bg-amber-400">
          <td>{{ item.date }}</td>
          <td>{{ item.summary }}</td>
          <td>{{ item.temperatureC }}</td>
          <td>{{ item.temperatureF }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>