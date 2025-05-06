// https://nuxt.com/docs/api/configuration/nuxt-config

import tailwindcss from "@tailwindcss/vite";

export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  modules: [],
  css: ['~/assets/css/main.css'],
  devtools: { enabled: true },
  future: {
    compatibilityVersion: 4
  },
  vite: {
    plugins: [
      tailwindcss(),
    ],
  },
  $development: {
    routeRules: {
      '/api/**': {
        proxy: `${process.env.ApiUrl}/**`,
      }
    },
    devServer: {
      https: {
        key: import.meta.env.CERT_KEY_PATH ?? 'dev-cert.key',
        cert: import.meta.env.CERT_PATH ?? 'dev-cert.pem'
      }
    }
  },
  runtimeConfig: {
    public: {
      otelExporterOtlpEndpoint: '',
      otelExporterOtlpHeaders: '',
      otelResourceAttributes: '',
      otelServiceName: '',
      apiUrl: process.env.ApiUrl,
    }
  }
})
