<script setup lang="ts">
import { ref, watch } from 'vue'
import { Button } from '@/components/ui/button'
import {
  Popover,
  PopoverContent,
  PopoverTrigger
} from '@/components/ui/popover'
import type { Component } from 'vue'

export interface PopoverOption {
  id: string
  label: string
  icon?: Component
}

interface Props {
  icon: Component
  variant?: 'default' | 'ghost'
  showBadge?: boolean
  label?: string
  options?: PopoverOption[]
  onOpen?: () => PopoverOption[]
}

const props = withDefaults(defineProps<Props>(), {
  variant: 'ghost',
  showBadge: false,
  options: () => []
})

const emit = defineEmits<{
  select: [optionId: string]
}>()

const isOpen = ref(false)
const dynamicOptions = ref<PopoverOption[]>([])

// Quando o popover abre, chama a função onOpen se existir
watch(isOpen, (newValue) => {
  if (newValue && props.onOpen) {
    dynamicOptions.value = props.onOpen()
  }
})

// Usa options dinâmicas se onOpen foi fornecido, caso contrário usa options da prop
const displayOptions = ref<PopoverOption[]>([])

watch(
  [isOpen, () => props.options, dynamicOptions],
  () => {
    if (props.onOpen) {
      displayOptions.value = dynamicOptions.value
    } else {
      displayOptions.value = props.options || []
    }
  },
  { immediate: true }
)

const handleOptionClick = (optionId: string) => {
  emit('select', optionId)
  isOpen.value = false
}
</script>

<template>
  <Popover v-model:open="isOpen">
    <PopoverTrigger as-child>
      <Button
        :variant="variant"
        size="icon"
        :class="[
          'rounded-xl size-10 transition-colors relative',
          variant === 'ghost' ? 'hover:bg-accent' : 'bg-primary hover:bg-primary/90'
        ]"
      >
        <component :is="icon" class="size-5" />
        <span
          v-if="showBadge"
          class="absolute top-1.5 right-1.5 h-2 w-2 bg-red-500 rounded-full"
        />
      </Button>
    </PopoverTrigger>

    <PopoverContent class="w-fit p-1">
      <div v-if="displayOptions.length > 0" class="flex flex-col gap-1">
        <button
          v-for="option in displayOptions"
          :key="option.id"
          class="flex items-center gap-2 px-2 py-1 text-sm rounded-md hover:bg-accent transition-colors text-left"
          @click="handleOptionClick(option.id)"
        >
          <component
            v-if="option.icon"
            :is="option.icon"
            class="size-4"
          />
          <span>{{ option.label }}</span>
        </button>
      </div>
      <div v-else class="px-3 py-2 text-sm text-muted-foreground">
        Nenhuma opção disponível
      </div>
    </PopoverContent>
  </Popover>
</template>
