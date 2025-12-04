<script setup lang="ts">
import { useSpeckleViewer } from '@/speckle/useSpeckleViewer'
import { useFloatPanel } from '@/components/floatPanel/useFloatPanel'
import DockerPanelButton from './DockerPanelButton.vue'
import DockerPanelButtonWithPopover from './DockerPanelButtonWithPopover.vue'
import type { PopoverOption } from './DockerPanelButtonWithPopover.vue'
import {
  Home,
  Search,
  Settings,
  User,
  FileText,
  Bell,
  Mail,
} from 'lucide-vue-next'

const { isLoading, getViewer } = useSpeckleViewer()
const { togglePanel } = useFloatPanel()

const handleButtonClick = (action: string) => {
  if (action === 'search') {
    togglePanel({ type: 'explorer' })
  }
}

// Função que será chamada quando o popover abrir
const getLegendOptions = (): PopoverOption[] => {
  try {
    const viewer = getViewer()
    const legendTypes = viewer.getLegendsTypes()

    if (!legendTypes || !Array.isArray(legendTypes)) {
      console.warn('getLegendsTypes() retornou valor inválido:', legendTypes)
      return []
    }

    return legendTypes.map(t => ({
      id: t,
      label: t
    } as PopoverOption))
  } catch (error) {
    console.error('Erro ao buscar opções de legenda:', error)
    return []
  }
}

// Método chamado quando uma opção é selecionada
const handleLegendSelect = (optionId: string) => {
  togglePanel({ type: 'legends', legendType: optionId })
}
</script>

<template>
  <div
    v-if="!isLoading"
    class="flex items-center gap-1 px-1 py-1 bg-card/95 backdrop-blur-sm border rounded-xl shadow-lg pointer-events-auto"
    style="align-self: center;"
  >
    <DockerPanelButton
      :icon="Home"
      label="Home"
      @click="handleButtonClick('home')"
    />

    <DockerPanelButton
      :icon="Search"
      label="Search"
      @click="handleButtonClick('search')"
    />

    <DockerPanelButtonWithPopover
      :icon="FileText"
      label="Legendas"
      :on-open="getLegendOptions"
      @select="handleLegendSelect"
    />

    <!-- Divider -->
    <div class="h-6 w-px bg-border mx-1" />

    <DockerPanelButton
      :icon="Mail"
      label="Mail"
      @click="handleButtonClick('mail')"
    />

    <DockerPanelButton
      :icon="Bell"
      label="Notifications"
      :show-badge="true"
      @click="handleButtonClick('notifications')"
    />

    <!-- Divider -->
    <div class="h-6 w-px bg-border mx-1" />

    <DockerPanelButton
      :icon="User"
      label="User Profile"
      @click="handleButtonClick('user')"
    />

    <DockerPanelButton
      :icon="Settings"
      label="Settings"
      @click="handleButtonClick('settings')"
    />
  </div>
</template>