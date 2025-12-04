import { ref, provide, inject, type InjectionKey, type Ref } from 'vue'
import type { Study, StudyType } from '@/types/study'

type StudyContext<T extends StudyType = StudyType> = {
  study: Ref<Study<T>>
}

const studySymbol: InjectionKey<StudyContext> = Symbol('study')

function createStudyContext<T extends StudyType>(initialStudy: Study<T>): StudyContext<T> {
  const study = ref<Study<T>>(initialStudy)

  return {
    study: study as Ref<Study<T>>,
  }
}

export function useStudyProvider<T extends StudyType>(initialStudy: Study<T>): void {
  const context = createStudyContext(initialStudy)
  provide(studySymbol, context)
}

// Versão genérica tipada - use quando você sabe o tipo do Study
export function useTypedStudy<T extends StudyType>(): StudyContext<T> {
  const context = inject(studySymbol)

  if (!context) {
    throw new Error(
      'useTypedStudy deve ser usado dentro de um ' +
      'componente filho que tenha o StudyProvider'
    )
  }

  return context as StudyContext<T>
}

// Versão sem tipo - use quando você não sabe o tipo do Study
export function useStudy(): StudyContext {
  const context = inject(studySymbol)

  if (!context) {
    throw new Error(
      'useStudy deve ser usado dentro de um ' +
      'componente filho que tenha o StudyProvider'
    )
  }

  return context
}
