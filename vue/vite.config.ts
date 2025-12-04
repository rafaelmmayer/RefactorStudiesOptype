import path from 'node:path'
import { defineConfig } from 'vite'
import tailwindcss from '@tailwindcss/vite'
import vue from '@vitejs/plugin-vue'
import { execSync } from 'node:child_process'
import type { Plugin } from 'vite'

function generateStudyTypes(): Plugin {
  const runScript = () => {
    try {
      execSync('node scripts/generate-study-types.js', { stdio: 'pipe' })
    } catch (error: any) {
      console.error('Error generating study types:', error.message)
    }
  }

  return {
    name: 'generate-study-types',
    buildStart() {
      runScript()
    },
    configureServer(server) {
      // Observa mudanças nas pastas de studies
      const studiesPath = path.resolve(__dirname, 'src/studies')

      server.watcher.add(studiesPath)

      server.watcher.on('addDir', (dirPath) => {
        if (dirPath.startsWith(studiesPath) && dirPath !== studiesPath) {
          runScript()
        }
      })

      server.watcher.on('unlinkDir', (dirPath) => {
        if (dirPath.startsWith(studiesPath) && dirPath !== studiesPath) {
          runScript()
        }
      })

      server.watcher.on('add', (filePath) => {
        if (filePath.includes('src/studies/') &&
            (filePath.endsWith('Inputs.ts') ||
             filePath.endsWith('Outputs.ts') ||
             filePath.endsWith('LegendService.ts'))) {
          runScript()
        }
      })

      server.watcher.on('unlink', (filePath) => {
        if (filePath.includes('src/studies/') &&
            (filePath.endsWith('Inputs.ts') ||
             filePath.endsWith('Outputs.ts') ||
             filePath.endsWith('LegendService.ts'))) {
          runScript()
        }
      })
    }
  }
}

// https://vite.dev/config/
export default defineConfig({
  plugins: [generateStudyTypes(), vue(), tailwindcss()],
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
