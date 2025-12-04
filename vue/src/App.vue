<script setup lang="ts">
import { onMounted, ref, shallowRef } from 'vue';
import StudyComponent from './studies/.lib/StudyComponent.vue'
import { useSpeckleViewerProvider } from './speckle/useSpeckleViewer';
import { useFloatPanelProvider } from './components/floatPanel/useFloatPanel';
import TooltipProvider from './components/ui/tooltip/TooltipProvider.vue';
import type { Study } from './types/study';
import { db } from './data';

const speckleViewerRef = shallowRef<HTMLElement|null>(null)
useSpeckleViewerProvider(speckleViewerRef)

useFloatPanelProvider()

const study = ref<Study>()

onMounted(() => {
  const studyDb = db.studies.getStudy('02579415e04a474781cae7499b64ee8b')
  study.value = studyDb
})
</script>

<template>
  <div ref="speckleViewerRef" class="h-dvh w-dvw bg-gray-100"></div>
  <TooltipProvider :delay-duration="800">
    <StudyComponent v-if="speckleViewerRef && study" :study="study" />
  </TooltipProvider>
</template>
