<script setup lang="ts">
import { useTypedStudy } from '@/composables/useStudy'

const { study } = useTypedStudy<'parede_de_concreto'>()
</script>

<template>
  <div>
    <h2>Outputs do Estudo de Parede de Concreto</h2>
    <div v-if="study.outputs">
      <p>Aco Medio: {{ study.outputs.acoMedio }}</p>
    </div>
    <div v-else>
      <p>Nenhum output calculado ainda</p>
    </div>
  </div>
</template>
