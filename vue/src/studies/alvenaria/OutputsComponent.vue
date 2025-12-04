<script setup lang="ts">
import { useTypedStudy } from '@/composables/useStudy'

const { study } = useTypedStudy<'alvenaria'>()
</script>

<template>
  <div>
    <h2>Outputs do Estudo de Alvenaria</h2>
    <div v-if="study.outputs">
      <p>Aco Global: {{ study.outputs.acoGlobal }}</p>
    </div>
    <div v-else>
      <p>Nenhum output calculado ainda</p>
    </div>
  </div>
</template>
