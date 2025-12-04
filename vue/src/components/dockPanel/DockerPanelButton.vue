<script setup lang="ts">
import { Button } from '@/components/ui/button'
import {
  Tooltip,
  TooltipContent,
  TooltipTrigger
} from '@/components/ui/tooltip'
import type { Component } from 'vue'

interface Props {
  icon: Component
  variant?: 'default' | 'ghost'
  showBadge?: boolean
  label?: string
}

withDefaults(defineProps<Props>(), {
  variant: 'ghost',
  showBadge: false
})

const emit = defineEmits<{
  click: []
}>()

const handleClick = () => {
  emit('click')
}
</script>

<template>
  <Tooltip>
    <TooltipTrigger as-child>
      <Button
        :variant="variant"
        size="icon"
        :class="[
          'rounded-xl size-10 transition-colors relative',
          variant === 'ghost' ? 'hover:bg-accent' : 'bg-primary hover:bg-primary/90'
        ]"
        @click="handleClick"
      >
        <component :is="icon" class="size-5" />
        <span
          v-if="showBadge"
          class="absolute top-1.5 right-1.5 h-2 w-2 bg-red-500 rounded-full"
        />
      </Button>
    </TooltipTrigger>
    <TooltipContent v-if="label">{{ label }}</TooltipContent>
  </Tooltip>
</template>
