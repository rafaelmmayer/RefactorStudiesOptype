<script setup lang="ts">
import { useTypedStudy } from '@/composables/useStudy'

const { study } = useTypedStudy<'alvenaria'>()
</script>

<template>
  <div>
    <h2>Inputs do Estudo de Alvenaria</h2>
    <div>
      <label>Tipo de Bloco:</label>
      <input v-model="study.inputs.tipoDeBloco" type="text" />
    </div>
  </div>
</template>
