# DockPanel Components

Componentes para criar botões de dock com tooltips e popovers.

## Componentes

### DockerPanelButton
Botão simples com tooltip e ação direta ao clicar.

### DockerPanelButtonWithPopover
Botão que abre um popover com múltiplas opções dinâmicas.

## Uso

### Botão Simples

```vue
<script setup lang="ts">
import DockerPanelButton from './DockerPanelButton.vue'
import { Home } from 'lucide-vue-next'

const handleClick = () => {
  console.log('Clicked!')
}
</script>

<template>
  <DockerPanelButton
    :icon="Home"
    label="Home"
    @click="handleClick"
  />
</template>
```

### Botão com Popover (Opções Estáticas)

```vue
<script setup lang="ts">
import DockerPanelButtonWithPopover from './DockerPanelButtonWithPopover.vue'
import type { PopoverOption } from './DockerPanelButtonWithPopover.vue'
import { FileText, Palette, Layers } from 'lucide-vue-next'

const options: PopoverOption[] = [
  { id: 'option1', label: 'Opção 1', icon: Palette },
  { id: 'option2', label: 'Opção 2', icon: Layers },
  { id: 'option3', label: 'Opção 3', icon: FileText }
]

const handleSelect = (optionId: string) => {
  console.log('Selected:', optionId)
}
</script>

<template>
  <DockerPanelButtonWithPopover
    :icon="FileText"
    label="Opções"
    :options="options"
    @select="handleSelect"
  />
</template>
```

### Botão com Popover (Usando função onOpen - RECOMENDADO)

```vue
<script setup lang="ts">
import DockerPanelButtonWithPopover from './DockerPanelButtonWithPopover.vue'
import type { PopoverOption } from './DockerPanelButtonWithPopover.vue'
import { FileText, Palette, Layers } from 'lucide-vue-next'

// Função que será chamada APENAS quando o popover abrir
const getLegendOptions = (): PopoverOption[] => {
  // Esta função é chamada sob demanda, quando o usuário clica no botão
  // Pode buscar de uma API, store, ou qualquer fonte
  return [
    { id: 'materials', label: 'Materiais', icon: Layers },
    { id: 'colors', label: 'Cores', icon: Palette },
    { id: 'dimensioning', label: 'Dimensionamento', icon: FileText }
  ]
}

const handleSelect = (optionId: string) => {
  console.log('Selected:', optionId)
}
</script>

<template>
  <DockerPanelButtonWithPopover
    :icon="FileText"
    label="Legendas"
    :on-open="getLegendOptions"
    @select="handleSelect"
  />
</template>
```

### Botão com Popover (Opções Assíncronas)

```vue
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import DockerPanelButtonWithPopover from './DockerPanelButtonWithPopover.vue'
import type { PopoverOption } from './DockerPanelButtonWithPopover.vue'
import { FileText } from 'lucide-vue-next'

const options = ref<PopoverOption[]>([])

// Buscar opções de uma API
const fetchOptions = async () => {
  try {
    const response = await fetch('/api/legend-types')
    const data = await response.json()

    options.value = data.map((item: any) => ({
      id: item.id,
      label: item.name,
      icon: FileText // pode ser dinâmico também
    }))
  } catch (error) {
    console.error('Erro ao buscar opções:', error)
  }
}

onMounted(() => {
  fetchOptions()
})

const handleSelect = (optionId: string) => {
  console.log('Selected:', optionId)
}
</script>

<template>
  <DockerPanelButtonWithPopover
    :icon="FileText"
    label="Legendas"
    :options="options"
    @select="handleSelect"
  />
</template>
```

### Botão com Popover (Com Store/Composable - usando onOpen)

```vue
<script setup lang="ts">
import DockerPanelButtonWithPopover from './DockerPanelButtonWithPopover.vue'
import type { PopoverOption } from './DockerPanelButtonWithPopover.vue'
import { useSpeckleViewer } from '@/speckle/useSpeckleViewer'
import { FileText } from 'lucide-vue-next'

const { getViewer } = useSpeckleViewer()

// Função chamada quando o popover abre
const getLegendOptions = (): PopoverOption[] => {
  try {
    const viewer = getViewer()
    const legendTypes = viewer.getLegendsTypes()

    return legendTypes.map((type: string) => ({
      id: type,
      label: type,
      icon: FileText
    }))
  } catch (error) {
    console.error('Erro ao buscar legendas:', error)
    return []
  }
}

const handleSelect = (optionId: string) => {
  const viewer = getViewer()
  viewer.setLegendType(optionId)
}
</script>

<template>
  <DockerPanelButtonWithPopover
    :icon="FileText"
    label="Tipo de Legenda"
    :on-open="getLegendOptions"
    @select="handleSelect"
  />
</template>
```

## Props

### DockerPanelButton

| Prop | Tipo | Default | Descrição |
|------|------|---------|-----------|
| `icon` | `Component` | - | Ícone do lucide-vue-next |
| `label` | `string?` | - | Texto do tooltip |
| `variant` | `'default' \| 'ghost'` | `'ghost'` | Variante do botão |
| `showBadge` | `boolean` | `false` | Mostrar badge de notificação |

### DockerPanelButtonWithPopover

| Prop | Tipo | Default | Descrição |
|------|------|---------|-----------|
| `icon` | `Component` | - | Ícone do lucide-vue-next |
| `label` | `string?` | - | Texto do tooltip |
| `variant` | `'default' \| 'ghost'` | `'ghost'` | Variante do botão |
| `showBadge` | `boolean` | `false` | Mostrar badge de notificação |
| `options` | `PopoverOption[]?` | `[]` | Array estático de opções (alternativa ao onOpen) |
| `onOpen` | `() => PopoverOption[]?` | - | Função chamada quando o popover abre (recomendado) |

## Events

### DockerPanelButton
- `@click` - Emitido quando o botão é clicado

### DockerPanelButtonWithPopover
- `@select` - Emitido quando uma opção é selecionada, recebe o `optionId` como parâmetro

## Tipos

```typescript
interface PopoverOption {
  id: string
  label: string
  icon?: Component
}
```
