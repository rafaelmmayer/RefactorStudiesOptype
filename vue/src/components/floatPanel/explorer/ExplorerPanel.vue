<script setup lang="ts">
import { Button } from '@/components/ui/button';
import { Spinner } from '@/components/ui/spinner';
import { useStudy } from '@/composables/useStudy';
import { db, type StudyModelDb } from '@/data';
import type { StudyModel } from '@/types/study';
import { useSpeckleViewer } from '@/speckle/useSpeckleViewer';
import { Eye, EyeOff } from 'lucide-vue-next';
import { onMounted, ref } from 'vue';

type ViewStudyModel = StudyModel & {
  isLoaded: boolean
  isLoading: boolean
}

const { study } = useStudy()
const { getViewer, loadStudyModel, isLoading } = useSpeckleViewer()

const models = ref<ViewStudyModel[]>([])

function loadModels(dbModels: StudyModelDb[]) {
  const viewer = getViewer()

  const list: ViewStudyModel[] = []
  for (const m of dbModels) {
    const study = db.studies.getStudy(m.studyId)
    if (!study) {
      continue
    }

    list.push({
      id: m.id,
      type: m.type,
      speckleProjectId: m.speckleProjectId,
      study,
      isLoaded: viewer.isStudyModelLoaded(m.id),
      isLoading: false
    })
  }

  models.value = list
}

function loadStudyModels() {
  const dbModels = db.models.getModelsByStudyId(study.value.id)
  loadModels(dbModels)
}

function loadAllModels() {
  const dbModels = db.models.getModels()
  loadModels(dbModels)
}

async function toggleLoadModel(model: ViewStudyModel) {
  const viewer = getViewer()

  if (model.isLoaded) {
    await viewer.unLoadModel(model.id)
    model.isLoaded = false
  } else {
    model.isLoading = true
    try {
      const speckleModel = await loadStudyModel(model)
      if (speckleModel) {
        model.isLoaded = true
      }
    } catch(err) {
      console.log('Erro ao carregar o modelo', err)
    } finally {
      model.isLoading = false
    }
  }
}

onMounted(() => {
  loadStudyModels()
})
</script>

<template>
  <div v-for="model in models" :key="model.id" class="flex justify-between items-center px-2 pb-2">
    <div>
      <p>{{ model.id }}</p> 
      <p class="text-sm text-foreground">{{ model.study.name }} | {{ model.type }} </p> 
    </div>
    
    <Button 
      class="size-7"
      variant="ghost"
      @click="() => toggleLoadModel(model)"
      :disabled="isLoading"
    >
      <template v-if="model.isLoading">
        <Spinner />
      </template>
      <template v-else-if="model.isLoaded">
        <EyeOff />
      </template>
      <template v-else>
        <Eye />
      </template>
    </Button>
  </div>
  <Button
    size="sm"
    variant="outline"
    @click="loadAllModels"
  >
    Carregar mais modelos
  </Button>
</template>