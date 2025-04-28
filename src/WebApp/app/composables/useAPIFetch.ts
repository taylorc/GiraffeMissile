import { useFetch } from "#app"

type useFetchType = typeof useFetch

// wrap useFetch with configuration needed to talk to our API
export const useAPIFetch: useFetchType = (path, options: any = {}) => {
  const config = useRuntimeConfig()

  // modify options as needed
  options.baseURL = config.public.apiUrl
  options.headers = { 'Content-Type': 'application/json' };
  return useFetch(path, options)
}