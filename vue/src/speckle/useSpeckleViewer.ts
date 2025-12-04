import { ref, provide, inject, type InjectionKey, type Ref, watch } from 'vue'
import { SpeckleViewer } from './SpeckleViewer'
import { useEventListener } from '@vueuse/core'
import { getAppConfig } from '../config'
import type { StudyModel } from '../types/study/StudyModel'
import type { SpeckleModel } from './SpeckleModel'

type SpeckleViewerContext = ReturnType<typeof createSpeckleViewerContext>

const viewerSymbol: InjectionKey<SpeckleViewerContext> = Symbol('speckleViewer')

function createSpeckleViewerContext(container: Ref<HTMLElement | null>) {
  const cfg = getAppConfig()

  let _viewer: SpeckleViewer | null = null

  function getViewer() {
    if (!_viewer) {
      throw new Error('Classe viewer não criada')
    }

    return _viewer
  }

  watch(
    () => container.value,
    (el) => {
      if (!el) return

      _viewer = new SpeckleViewer({
        container: el,
        token: cfg.speckleViewerToken
      })
    },
    { immediate: true }
  )

  const isInitialized = ref(false)

  const isLoading = ref(false)
  const loadingProgress = ref(0)

  const isDragging = ref(false)

  async function dipose() {
    isInitialized.value = false
    isLoading.value = false
    loadingProgress.value = 0
    isDragging.value = false

    await _viewer?.dispose()
  }

  async function init() {
    if (!_viewer) {
      throw new Error('Classe viewer não criada')
    }

    if (isInitialized.value) {
      return
    }

    try {
      await _viewer.init({
        onLoadComplete() {
          loadingProgress.value = 0
          isLoading.value = false
        },
        
        onFilteringStateSet() {
          
        },

        onPointerMove(e) {
          if (e.buttons === 2) {
            isDragging.value = true 
          } else {
            isDragging.value = false
          }
        },
      })

      isInitialized.value = true
    } catch (err) {
      console.log('init erro', err)
    }
  }

  async function loadStudyModel(studyModel: StudyModel, mainModel = false): Promise<SpeckleModel | null> {
    if (!_viewer) {
      throw new Error('Classe viewer não criada')
    }

    if (isLoading.value) {
      return null
    }

    let res: SpeckleModel | null = null

    try {
      isLoading.value = true
      loadingProgress.value = 0

      if (mainModel) {
        res = await _viewer.loadMainStudyModel({
          studyModel,
          onLoadProgress(progress) {
            loadingProgress.value = progress
          },
        })
      } else {
        res = await _viewer.loadStudyModel({
          studyModel,
          onLoadProgress(progress) {
            loadingProgress.value = progress
          },
        })
      }

    } catch (err) {
      console.log('loadStudyModel erro', err)
    }

    return res
  }

  async function cancelLoading() {
    if (!_viewer) {
      return
    }

    if (!isLoading.value) {
      return
    }

    await _viewer.cancelLoading()
    isLoading.value = false
  }

  useEventListener(container, "contextmenu", (e: Event) => {
    e.preventDefault();

    if (isDragging) {
      isDragging.value = false;
      return;
    }
  });

  return {
    isLoading,
    loadingProgress,
    isDragging,
    getViewer,
    init,
    loadStudyModel,
    dipose,
    cancelLoading
  }
}

export function useSpeckleViewerProvider(
  container: Ref<HTMLElement | null>
): void {
  const context = createSpeckleViewerContext(container)
  provide(viewerSymbol, context)
}

export function useSpeckleViewer(): SpeckleViewerContext {
  const context = inject(viewerSymbol)
  
  if (!context) {
    throw new Error(
      'useSpeckleViewer deve ser usado dentro de um ' +
      'componente filho do SpeckleViewerProvider'
    )
  }

  return context
}