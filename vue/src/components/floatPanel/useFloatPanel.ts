import { ref, provide, inject, type InjectionKey, type Ref } from 'vue'

export type PanelConfig =
  | { type: 'explorer' }
  | { type: 'legends'; legendType: string }
  | null

type FloatPanelContext = ReturnType<typeof createFloatPanelContext>

const floatPanelSymbol: InjectionKey<FloatPanelContext> = Symbol('floatPanel')

function createFloatPanelContext() {
  const isOpen = ref(false)
  const currentPanel = ref<PanelConfig>(null)

  function openPanel(panelConfig: PanelConfig) {
    if (panelConfig === null) {
      closePanel()
      return
    }

    currentPanel.value = panelConfig
    isOpen.value = true
  }

  function closePanel() {
    isOpen.value = false
    currentPanel.value = null
  }

  function togglePanel(panelConfig: PanelConfig) {
    if (!panelConfig) {
      closePanel()
      return
    }

    // Se o mesmo painel está aberto, fecha
    if (isOpen.value && currentPanel.value?.type === panelConfig.type) {
      // Para legends, verifica também o legendType
      if (panelConfig.type === 'legends' && currentPanel.value.type === 'legends') {
        if (currentPanel.value.legendType === panelConfig.legendType) {
          closePanel()
          return
        }
      } else if (panelConfig.type === 'explorer') {
        closePanel()
        return
      }
    }

    openPanel(panelConfig)
  }

  return {
    isOpen,
    currentPanel: currentPanel as Ref<PanelConfig>,
    openPanel,
    closePanel,
    togglePanel
  }
}

export function useFloatPanelProvider(): void {
  const context = createFloatPanelContext()
  provide(floatPanelSymbol, context)
}

export function useFloatPanel(): FloatPanelContext {
  const context = inject(floatPanelSymbol)

  if (!context) {
    throw new Error(
      'useFloatPanel deve ser usado dentro de um ' +
      'componente filho do FloatPanelProvider'
    )
  }

  return context
}
