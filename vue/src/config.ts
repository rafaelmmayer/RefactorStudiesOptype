// src/plugins/configPlugin.ts
export interface AppConfig {
  baseUrl: string;
  apiUrl: string;
  minioUrl: string;
  speckleUrl: string;
  speckleViewerToken: string;
}

let appConfig: AppConfig | null = null;

export async function fetchConfig(): Promise<void> {
  let apiUrl = "";
  if (import.meta.env.VITE_API_URL) {
    apiUrl = import.meta.env.VITE_API_URL;
  }

  const response = await fetch(apiUrl + "/api/client-configs", {
    headers: { Accept: "application/json" },
  });

  if (!response.ok) {
    throw new Error(
      `[Config] Erro ao obter configs: ${response.statusText}`
    );
  }

  appConfig = (await response.json()) as AppConfig;
}

export function fetchFakeConfig() {
  appConfig = {
    apiUrl: 'http://localhost:5174',
    baseUrl: 'http://localhost:5173',
    minioUrl: 'https://files-optype.mayerafa.com',
    speckleUrl: 'https://speckle-optype.mayerafa.com',
    speckleViewerToken: 'e9b81318c36ac7b75a0d633ac4ba6da4410e70b1ab'
  }
}

export function getAppConfig(): AppConfig {
  if (!appConfig) {
    throw new Error('App config not initialized');
  }

  return appConfig;
}
