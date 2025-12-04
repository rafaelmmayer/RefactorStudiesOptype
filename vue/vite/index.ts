import path from 'node:path'
import fs from 'node:fs'
import type { Plugin, ResolvedConfig } from 'vite'

function toPascalCase(str: string): string {
  return str
    .split(/_|-/g)
    .map((s) => s.charAt(0).toUpperCase() + s.slice(1))
    .join('')
}

export function generateStudyTypesPlugin(): Plugin {
  let config: ResolvedConfig

  const generateTypes = () => {
    const rootDir = config.root
    const studiesPath = path.resolve(rootDir, 'src/studies')
    try {
      // Get all study folders
      const folders = fs
        .readdirSync(studiesPath)
        .filter((folder) => {
          if (folder.startsWith('.')) return false
          const full = path.join(studiesPath, folder)
          return fs.statSync(full).isDirectory()
        })

      // Generate StudyType.ts
      let fileContent = '// AUTO-GENERATED. DO NOT EDIT.\n\n'
      for (const folder of folders) {
        const Pascal = toPascalCase(folder)
        fileContent += `export type ${Pascal}StudyType = "${folder}";\n`
      }
      fileContent += '\nexport type StudyType =\n  | '
      fileContent += folders
        .map((folder) => `${toPascalCase(folder)}StudyType`)
        .join('\n  | ')
      fileContent += ';\n'

      const outputPath = path.resolve(rootDir, 'src/types/study/StudyType.ts')
      fs.writeFileSync(outputPath, fileContent, 'utf8')

      // Generate StudyInputs.ts
      let inputsContent = '// AUTO-GENERATED. DO NOT EDIT.\n\n'
      const studiesWithInputs: string[] = []
      for (const folder of folders) {
        const inputsPath = path.join(studiesPath, folder, 'Inputs.ts')
        if (fs.existsSync(inputsPath)) {
          const Pascal = toPascalCase(folder)
          inputsContent += `import type { Inputs as ${Pascal}Inputs } from '@/studies/${folder}/Inputs';\n`
          studiesWithInputs.push(folder)
        }
      }
      inputsContent += "import type { StudyType } from './StudyType';\n\n"
      inputsContent += 'export type StudyInputsMap = {\n'
      for (const folder of studiesWithInputs) {
        const Pascal = toPascalCase(folder)
        inputsContent += `  '${folder}': ${Pascal}Inputs;\n`
      }
      inputsContent += '};\n\n'
      inputsContent += 'export type InputsForStudyType<T extends StudyType> = T extends keyof StudyInputsMap\n'
      inputsContent += '  ? StudyInputsMap[T]\n'
      inputsContent += '  : never;\n'

      const inputsOutputPath = path.resolve(rootDir, 'src/types/study/StudyInputs.ts')
      fs.writeFileSync(inputsOutputPath, inputsContent, 'utf8')

      // Generate StudyOutputs.ts
      let outputsContent = '// AUTO-GENERATED. DO NOT EDIT.\n\n'
      const studiesWithOutputs: string[] = []
      for (const folder of folders) {
        const outputsPath = path.join(studiesPath, folder, 'Outputs.ts')
        if (fs.existsSync(outputsPath)) {
          const Pascal = toPascalCase(folder)
          outputsContent += `import type { Outputs as ${Pascal}Outputs } from '@/studies/${folder}/Outputs';\n`
          studiesWithOutputs.push(folder)
        }
      }
      outputsContent += "import type { StudyType } from './StudyType';\n\n"
      outputsContent += 'export type StudyOutputsMap = {\n'
      for (const folder of studiesWithOutputs) {
        const Pascal = toPascalCase(folder)
        outputsContent += `  '${folder}': ${Pascal}Outputs;\n`
      }
      outputsContent += '};\n\n'
      outputsContent += 'export type OutputsForStudyType<T extends StudyType> = T extends keyof StudyOutputsMap\n'
      outputsContent += '  ? StudyOutputsMap[T]\n'
      outputsContent += '  : never;\n'

      const outputsOutputPath = path.resolve(rootDir, 'src/types/study/StudyOutputs.ts')
      fs.writeFileSync(outputsOutputPath, outputsContent, 'utf8')

      // Generate registerLegendServices.ts
      let legendServicesContent = '// AUTO-GENERATED. DO NOT EDIT.\n'
      legendServicesContent += '// Este arquivo importa todos os LegendServices para garantir que os decorators sejam executados\n\n'
      for (const folder of folders) {
        const legendServicePath = path.join(studiesPath, folder, 'LegendService.ts')
        if (fs.existsSync(legendServicePath)) {
          legendServicesContent += `import '@/studies/${folder}/LegendService';\n`
        }
      }

      const legendServicesOutputPath = path.resolve(rootDir, 'src/studies/.lib/registerLegendServices.ts')
      fs.writeFileSync(legendServicesOutputPath, legendServicesContent, 'utf8')
    } catch (error: any) {
      console.error('Error generating study types:', error.message)
    }
  }

  return {
    name: 'generate-study-types',
    configResolved(resolvedConfig) {
      config = resolvedConfig
    },
    buildStart() {
      generateTypes()
    },
    configureServer(server) {
      const studiesPath = path.resolve(config.root, 'src/studies')
      server.watcher.add(studiesPath)

      server.watcher.on('addDir', (dirPath) => {
        if (dirPath.startsWith(studiesPath) && dirPath !== studiesPath) {
          generateTypes()
        }
      })

      server.watcher.on('unlinkDir', (dirPath) => {
        if (dirPath.startsWith(studiesPath) && dirPath !== studiesPath) {
          generateTypes()
        }
      })

      server.watcher.on('add', (filePath) => {
        if (
          filePath.includes('src/studies/') &&
          (
            filePath.endsWith('Inputs.ts') ||
            filePath.endsWith('Outputs.ts') ||
            filePath.endsWith('LegendService.ts')
          )
        ) {
          generateTypes()
        }
      })

      server.watcher.on('unlink', (filePath) => {
        if (
          filePath.includes('src/studies/') &&
          (
            filePath.endsWith('Inputs.ts') ||
            filePath.endsWith('Outputs.ts') ||
            filePath.endsWith('LegendService.ts')
          )
        ) {
          generateTypes()
        }
      })
    }
  }
}
