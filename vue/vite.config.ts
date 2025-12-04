import path from 'node:path'
import { defineConfig } from 'vite'
import tailwindcss from '@tailwindcss/vite'
import vue from '@vitejs/plugin-vue'
import { generateStudyTypesPlugin } from './vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [generateStudyTypesPlugin(), vue(), tailwindcss()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
		proxy: {
			"/api": process.env.VITE_API_URL as string,
		},
	},
})
