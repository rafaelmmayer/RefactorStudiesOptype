<script setup lang="ts">
import { useSpeckleViewer } from '@/speckle/useSpeckleViewer';
import { watch } from 'vue';

interface Props {
  legendType: string
}

const props = defineProps<Props>()

const { getViewer } = useSpeckleViewer()

watch(
  () => props.legendType,
  (type) => {
    const viewer = getViewer()
    viewer.getLegends(type)
  },
  { immediate: true }
)
</script>

<template>
  <div>
    <p>Legendas - Tipo: {{ props.legendType }}</p>
  </div>
</template> 