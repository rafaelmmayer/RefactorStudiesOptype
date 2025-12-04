<script setup lang="ts">
import { computed, ref, useTemplateRef, watch } from 'vue'
import { useFloatPanel } from './useFloatPanel'
import ExplorerPanel from './explorer/ExplorerPanel.vue'
import LegendsPanel from './legends/LegendsPanel.vue'
import { X, Minus, Maximize2 } from 'lucide-vue-next'
import { Button } from '@/components/ui/button'
import { useDraggable } from '@vueuse/core'

const { isOpen, currentPanel, closePanel } = useFloatPanel()

const floatContainerRef = useTemplateRef('floatContainer')
const handleRef = useTemplateRef('dragHandle')
const isMinimized = ref(false)

const initialPosition = { x: 390, y: 12 }

const { style, position } = useDraggable(floatContainerRef, {
  initialValue: initialPosition,
  preventDefault: true,
  handle: handleRef
})

// Reseta a posição quando o painel abre
watch(isOpen, (newValue) => {
  if (newValue) {
    resetPosition()
    isMinimized.value = false
  }
})

// Função para resetar posição (usada no duplo clique)
function resetPosition() {
  position.value = { ...initialPosition }
}

// Toggle minimizar
function toggleMinimize() {
  isMinimized.value = !isMinimized.value
}

const panelTitle = computed(() => {
  if (!currentPanel.value) return ''

  if (currentPanel.value.type === 'explorer') {
    return 'Explorador de Modelo'
  }

  if (currentPanel.value.type === 'legends') {
    return `Legendas - ${currentPanel.value.legendType}`
  }

  return ''
})
</script>

<template>
  <div
    v-if="isOpen && currentPanel"
    ref="floatContainer"
    :class="[
      'pointer-events-auto w-[360px] bg-card border rounded shadow-sm flex flex-col fixed z-99',
      isMinimized ? 'h-auto' : 'min-h-[300px] max-h-[360px]'
    ]"
    :style="style"
  >
    <!-- Header -->
    <div
      ref="dragHandle"
      :class="[
        'flex items-center justify-between px-4 py-2',
        !isMinimized && 'border-b'
      ]"
    >
      <h2
        class="font-semibold select-none flex-1"
        @dblclick="resetPosition"
      >
        {{ panelTitle }}
      </h2>
      <div class="flex items-center gap-1">
        <Button
          variant="ghost"
          size="icon"
          class="size-7"
          @click="toggleMinimize"
        >
          <Minus v-if="!isMinimized" class="size-4" />
          <Maximize2 v-else class="size-4" />
        </Button>
        <Button
          variant="ghost"
          size="icon"
          class="size-7"
          @click="closePanel"
        >
          <X class="size-4" />
        </Button>
      </div>
    </div>

    <!-- Content -->
    <div
      v-if="!isMinimized"
      class="flex-1 overflow-y-auto p-2 min-h-0"
    >
      <ExplorerPanel v-if="currentPanel.type === 'explorer'" />
      <LegendsPanel
        v-if="currentPanel.type === 'legends'"
        :legend-type="currentPanel.legendType"
      />
    </div>
  </div>
</template>
