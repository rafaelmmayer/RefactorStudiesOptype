<script setup lang="ts">
import { useSpeckleViewer } from '@/speckle/useSpeckleViewer';
import { useStudyProvider } from '@/composables/useStudy';
import { db } from '@/data';
import type { Study, StudyModel } from '@/types/study';
import { onMounted, ref, defineAsyncComponent } from 'vue';
import { Button } from '@/components/ui/button';
import { LayoutTemplate, List, PanelLeft, Share } from 'lucide-vue-next'
import DockPanel from '@/components/dockPanel/DockPanel.vue';
import FloatPanel from '@/components/floatPanel/FloatPanel.vue';
import Separator from '@/components/ui/separator/Separator.vue';
import { Tooltip, TooltipTrigger, TooltipContent } from '@/components/ui/tooltip';
import { loadInputsComponent, loadOutputsComponent } from '@/studies/.lib';

const props = defineProps<{
  study: Study
}>()

useStudyProvider(props.study)

const InputsComponent = defineAsyncComponent(() => loadInputsComponent(props.study.type))
const OutputsComponent = defineAsyncComponent(() => loadOutputsComponent(props.study.type))

const { init, loadStudyModel, loadingProgress, isLoading, cancelLoading } = useSpeckleViewer()

const tab = ref<'input' | 'output'>('input')
const leftSidebarMax = ref(true)

onMounted(async () => {
  const inputModel = db.models.getModelByStudyId(props.study.id, 'input')

  if (!inputModel) {
    return
  }

  const studyModel: StudyModel = {
    id: inputModel.id,
    type: inputModel.type,
    speckleProjectId: inputModel.speckleProjectId,
    study: props.study
  }

  await init()
  await loadStudyModel(studyModel, true)
})
</script>

<template>
  <div 
    ref="studyContainer"
    class="absolute top-0 max-h-dvh h-dvh w-dvw grid grid-cols-[auto_1fr_300px] pointer-events-none"
  >
    <template v-if="leftSidebarMax">
      <div class="h-full grid grid-cols-[60px_1fr] pointer-events-auto bg-card border-r w-[380px]">
        <div class="border-r">
          <div class="h-12 flex items-center justify-center" >
            <img src="/logo-circular.svg" class="size-7" />
          </div>
          <div class="flex flex-col gap-2 items-center px-2">
            <Separator />

            <Tooltip>
              <TooltipTrigger as-child>
                <Button
                  size="icon"
                  variant="ghost"
                  :class="{
                    'bg-[#2F4C43] hover:bg-[#2F4C43]/90 text-white hover:text-white': tab === 'input'
                  }"
                  @click="tab = 'input'"
                >
                  <List />
                </Button>
              </TooltipTrigger>
              <TooltipContent align="center" side="right">Inputs</TooltipContent>
            </Tooltip>

            <Tooltip>
              <TooltipTrigger as-child>
                <Button
                  size="icon"
                  variant="ghost"
                  :class="{
                    'bg-[#2F4C43] hover:bg-[#2F4C43]/90 text-white hover:text-white': tab === 'output'
                  }"
                  @click="tab = 'output'"
                  :disabled="!study.outputs"
                >
                  <LayoutTemplate />
                </Button>
              </TooltipTrigger>
              <TooltipContent align="center" side="right">Outputs</TooltipContent>
            </Tooltip>

            <Separator />

            <Tooltip>
              <TooltipTrigger as-child>
                <Button
                  size="icon"
                  variant="ghost"
                >
                  <Share />
                </Button>
              </TooltipTrigger>
              <TooltipContent align="center" side="right">Compartilhar</TooltipContent>
            </Tooltip>
          </div>
        </div>
        <div class="flex flex-col">
          <div class="h-12 p-3 flex items-center justify-between">
            {{ props.study.name }}
            <Button
              size="icon"
              variant="ghost"
              class="size-8"
              @click="leftSidebarMax = false"
            >
              <PanelLeft />
            </Button>
          </div>
          <Separator />
          <div class="flex-1 py-2">
            <template v-if="tab === 'input'">
              <InputsComponent />
            </template>
            <template v-else-if="tab === 'output'">
              <OutputsComponent />
            </template>
          </div>
        </div>
      </div>
    </template>
    <template v-else>
      <div class="flex pt-3 pl-4">
        <div class="flex h-12 bg-card w-fit rounded-2xl items-center gap-3 px-2 pointer-events-auto border shadow">
          <div class="flex items-center justify-center" >
            <img src="/logo-circular.svg" class="size-7" />
          </div>
          {{ props.study.name }}
          <Button
            size="icon"
            variant="ghost"
            class="size-8"
            @click="leftSidebarMax = true"
          >
            <PanelLeft />
          </Button>
        </div>
      </div>
    </template>

    <div class="flex flex-col p-3">
      <div class="flex-1">
        <FloatPanel />
      </div>
    </div>

    <div class="h-full grid">
      <div v-if="false" class="pointer-events-auto bg-card border-l p-2">

      </div>
    </div>
    
  </div>

  <div v-if="isLoading" class="absolute top-0 w-dvw flex justify-center items-center gap-2 pt-1">
    <div>{{ (loadingProgress  * 100).toFixed(0) }} %</div>
    <div>
      <Button size="sm" @click="cancelLoading" variant="ghost">Cancelar</Button>
    </div>
  </div>
  
  <div class="absolute bottom-3 w-full flex justify-center">
    <DockPanel />
  </div>
</template>