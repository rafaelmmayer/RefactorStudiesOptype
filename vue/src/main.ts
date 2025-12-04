// src/main.ts
import './style.css'
import { createApp } from 'vue';
import App from './App.vue';
import { fetchFakeConfig } from './config';
import '@/studies/.lib/registerLegendServices';

async function initApp() {
  try {
    fetchFakeConfig();
    
    const app = createApp(App);

    app.mount('#app');
  } catch (error) {
    console.error('Falha ao inicializar app:', error);
  }
}

initApp();