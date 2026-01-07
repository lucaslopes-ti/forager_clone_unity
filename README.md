# Forager Clone

Clone do Forager que estou desenvolvendo em Unity. Projeto ainda em desenvolvimento.

## Como rodar

Primeiro, clone o repo. Depois abre no Unity (2022.3 ou mais recente). O Unity vai importar tudo sozinho, pode demorar um pouco na primeira vez.

Depois é só abrir a cena `SampleScene.unity` e dar play.

## O que tem no jogo

- Sistema de mineração: clica e segura pra minerar recursos
- Inventário: aperta ESC pra abrir/fechar
- Coleta de itens: passa por cima dos loots no chão
- Consumíveis: clica nos itens no inventário pra restaurar energia
- Sistema de ilhas com spawn automático de recursos

## Estrutura das pastas

```
Assets/
├── Scripts/       # Código C#
├── Scenes/        # Cenas
├── Prefab/        # Prefabs
├── Sprites/       # Imagens
├── Animations/    # Animações
├── Itens/         # ScriptableObjects dos itens
└── Settings/      # Configs do projeto
```

## Configuração necessária

Antes de começar a mexer, tem alguns campos que precisam estar configurados no Inspector:

**CoreGame** (objeto principal):
- playerController
- gameManager  
- inventory

**GameManager**:
- actionCursor (o cursor que aparece quando pode interagir)
- interactionDistance
- distanceToSpawnResource
- timeToSpawnResource

**Inventory**:
- inventoryPanel
- SlotGrid
- slotPrefab
- ItemInfoWindow e os componentes de UI (ItemImage, ItemName, etc)

Se algum desses não estiver configurado, o jogo pode dar erro ou não funcionar direito.

## Dicas

- Sempre commita os arquivos `.meta` junto com os assets
- Não commita nada da pasta `Library/` ou `Temp/` - o Unity gera isso sozinho
- Se der problema, tenta deletar `Library/` e `Temp/` e abrir de novo (o Unity recria tudo)

## Bugs conhecidos

- Às vezes o loot não aparece quando destroi um recurso (verifica se o prefab do loot está configurado no ScriptableObject do item)
- Se o inventory não encontrar o painel automaticamente, precisa atribuir manualmente no Inspector

## Contribuindo

Se for ajudar, cria uma branch nova pra sua feature e manda um PR quando terminar.
