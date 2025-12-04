// scripts/generate-study-types.js
import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const studiesPath = path.resolve(__dirname, "../src/studies");

function toPascalCase(str) {
  return str
    .split(/_|-/g)
    .map((s) => s.charAt(0).toUpperCase() + s.slice(1))
    .join("");
}

const folders = fs
  .readdirSync(studiesPath)
  .filter((folder) => {
    // ignora pastas que começam com "."
    if (folder.startsWith(".")) return false;

    const full = path.join(studiesPath, folder);
    return fs.statSync(full).isDirectory();
  });

let fileContent = "// AUTO-GENERATED. DO NOT EDIT.\n\n";

for (const folder of folders) {
  const Pascal = toPascalCase(folder);
  fileContent += `export type ${Pascal}StudyType = "${folder}";\n`;
}

fileContent += `\nexport type StudyType =\n  | `;

fileContent += folders
  .map((folder) => `${toPascalCase(folder)}StudyType`)
  .join("\n  | ");

fileContent += ";\n";

const outputPath = path.resolve(
  __dirname,
  "../src/types/study/StudyType.ts"
);

fs.writeFileSync(outputPath, fileContent, "utf8");

// ============================================
// Gerar StudyInputs.ts
// ============================================

let inputsContent = "// AUTO-GENERATED. DO NOT EDIT.\n\n";

// Importa os tipos de inputs de cada estudo
const studiesWithInputs = [];
for (const folder of folders) {
  const inputsPath = path.join(studiesPath, folder, "Inputs.ts");
  if (fs.existsSync(inputsPath)) {
    const Pascal = toPascalCase(folder);
    inputsContent += `import type { Inputs as ${Pascal}Inputs } from '@/studies/${folder}/Inputs';\n`;
    studiesWithInputs.push(folder);
  }
}

inputsContent += `import type { StudyType } from './StudyType';\n\n`;

// Cria o type registry que mapeia StudyType para Inputs
inputsContent += `export type StudyInputsMap = {\n`;
for (const folder of studiesWithInputs) {
  const Pascal = toPascalCase(folder);
  inputsContent += `  '${folder}': ${Pascal}Inputs;\n`;
}
inputsContent += `};\n\n`;

// Cria um type helper para extrair os inputs de um StudyType específico
inputsContent += `export type InputsForStudyType<T extends StudyType> = T extends keyof StudyInputsMap\n`;
inputsContent += `  ? StudyInputsMap[T]\n`;
inputsContent += `  : never;\n`;

const inputsOutputPath = path.resolve(
  __dirname,
  "../src/types/study/StudyInputs.ts"
);

fs.writeFileSync(inputsOutputPath, inputsContent, "utf8");

// ============================================
// Gerar StudyOutputs.ts
// ============================================

let outputsContent = "// AUTO-GENERATED. DO NOT EDIT.\n\n";

// Importa os tipos de outputs de cada estudo
const studiesWithOutputs = [];
for (const folder of folders) {
  const outputsPath = path.join(studiesPath, folder, "Outputs.ts");
  if (fs.existsSync(outputsPath)) {
    const Pascal = toPascalCase(folder);
    outputsContent += `import type { Outputs as ${Pascal}Outputs } from '@/studies/${folder}/Outputs';\n`;
    studiesWithOutputs.push(folder);
  }
}

outputsContent += `import type { StudyType } from './StudyType';\n\n`;

// Cria o type registry que mapeia StudyType para Outputs
outputsContent += `export type StudyOutputsMap = {\n`;
for (const folder of studiesWithOutputs) {
  const Pascal = toPascalCase(folder);
  outputsContent += `  '${folder}': ${Pascal}Outputs;\n`;
}
outputsContent += `};\n\n`;

// Cria um type helper para extrair os outputs de um StudyType específico
outputsContent += `export type OutputsForStudyType<T extends StudyType> = T extends keyof StudyOutputsMap\n`;
outputsContent += `  ? StudyOutputsMap[T]\n`;
outputsContent += `  : never;\n`;

const outputsOutputPath = path.resolve(
  __dirname,
  "../src/types/study/StudyOutputs.ts"
);

fs.writeFileSync(outputsOutputPath, outputsContent, "utf8");

// ============================================
// Gerar registerLegendServices.ts
// ============================================

let legendServicesContent = "// AUTO-GENERATED. DO NOT EDIT.\n";
legendServicesContent += "// Este arquivo importa todos os LegendServices para garantir que os decorators sejam executados\n\n";

for (const folder of folders) {
  const legendServicePath = path.join(studiesPath, folder, "LegendService.ts");
  if (fs.existsSync(legendServicePath)) {
    legendServicesContent += `import '@/studies/${folder}/LegendService';\n`;
  }
}

const legendServicesOutputPath = path.resolve(
  __dirname,
  "../src/studies/.lib/registerLegendServices.ts"
);

fs.writeFileSync(legendServicesOutputPath, legendServicesContent, "utf8");