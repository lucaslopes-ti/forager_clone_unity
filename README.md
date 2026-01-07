# Forager Clone - Unity Project

Projeto clone do jogo Forager desenvolvido em Unity.

## 📋 Requisitos

- Unity 2022.3 LTS ou superior
- Visual Studio ou Rider (recomendado para desenvolvimento C#)

## 🚀 Como Começar

1. **Clone o repositório:**
   ```bash
   git clone [URL_DO_REPOSITORIO]
   cd forager_clone
   ```

2. **Abra o projeto no Unity:**
   - Abra o Unity Hub
   - Clique em "Add" e selecione a pasta do projeto
   - O Unity irá detectar a versão necessária automaticamente

3. **Aguarde a importação:**
   - O Unity irá importar todos os assets e dependências automaticamente
   - Isso pode levar alguns minutos na primeira vez

4. **Abra a cena:**
   - Vá para `Assets/Scenes/SampleScene.unity`
   - Pressione Play para testar o jogo

## 📁 Estrutura do Projeto

```
Assets/
├── Scripts/          # Todos os scripts C# do jogo
├── Scenes/           # Cenas do jogo
├── Prefab/           # Prefabs reutilizáveis
├── Sprites/          # Sprites e imagens
├── Animations/       # Animações e controllers
├── Itens/            # ScriptableObjects dos itens
└── Settings/         # Configurações do projeto
```

## 🎮 Sistema de Jogo

- **Mineração**: Clique e segure para minerar recursos
- **Inventário**: Pressione ESC para abrir/fechar
- **Coleta**: Passe por cima dos itens no chão para coletá-los
- **Consumíveis**: Clique nos itens consumíveis no inventário para restaurar energia

## 🔧 Configuração Importante

Antes de começar a trabalhar, certifique-se de que os seguintes campos estão atribuídos no Inspector:

### CoreGame (objeto principal da cena):
- `playerController` - Referência ao PlayerController
- `gameManager` - Referência ao GameManager
- `inventory` - Referência ao Inventory

### GameManager:
- `actionCursor` - GameObject do cursor de ação
- `interactionDistance` - Distância de interação
- `distanceToSpawnResource` - Distância para spawnar recursos
- `timeToSpawnResource` - Tempo entre spawns de recursos

### Inventory:
- `inventoryPanel` - Painel do inventário
- `SlotGrid` - Grid onde os slots são criados
- `slotPrefab` - Prefab do slot de inventário
- `ItemInfoWindow` - Janela de informações do item
- `ItemImage`, `ItemName`, `ItemType`, `ItemUseText` - Componentes UI

## 📝 Notas Importantes

- **NÃO commite** arquivos da pasta `Library/`, `Temp/`, `Logs/`, `UserSettings/`
- **SEMPRE commite** arquivos `.meta` junto com os assets
- O Unity gera automaticamente os arquivos `.meta` - não os edite manualmente
- Se houver conflitos de merge nos arquivos `.meta`, geralmente é seguro aceitar ambas as versões

## 🐛 Troubleshooting

Se o projeto não abrir corretamente:
1. Delete as pastas `Library/` e `Temp/` (serão recriadas automaticamente)
2. Abra o projeto novamente no Unity
3. Aguarde a reimportação completa

Se houver erros de compilação:
1. Vá em `Assets > Reimport All`
2. Verifique o Console do Unity para mensagens de erro
3. Certifique-se de que todos os campos necessários estão atribuídos no Inspector

## 👥 Contribuindo

1. Crie uma branch para sua feature: `git checkout -b feature/nome-da-feature`
2. Faça suas alterações
3. Commit suas mudanças: `git commit -m "Descrição das mudanças"`
4. Push para a branch: `git push origin feature/nome-da-feature`
5. Abra um Pull Request

## 📄 Licença

[Adicione informações de licença aqui se necessário]

