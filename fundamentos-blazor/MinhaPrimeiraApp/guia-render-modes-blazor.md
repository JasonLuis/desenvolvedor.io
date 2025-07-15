
# 🧭 Guia de Consulta Rápida sobre Render Modes no Blazor

Mantenha esses conceitos sempre claros para arquitetar seus componentes da forma certa!

---

## 🎯 Render Modes

Para aplicar um modo de renderização a um componente, utilize a diretiva `@rendermode` no componente.

### 📊 Tabela de Modos de Renderização

| Nome                   | Descrição                                                                                     | Lado da Renderização         | Interativo? |
|------------------------|-----------------------------------------------------------------------------------------------|-------------------------------|-------------|
| Static Server          | Static server-side rendering (static SSR)                                                    | Server                        | ❌ Não      |
| Interactive Server     | Interactive server-side rendering (interactive SSR) usando Blazor Server                     | Server                        | ✔️ Sim      |
| Interactive WebAssembly| Client-side rendering (CSR) usando Blazor WebAssembly                                        | Client                        | ✔️ Sim      |
| Interactive Auto       | Renderiza via server (SSR) enquanto baixa suporte a WebAssembly. Depois, responde via WebAssembly | Server → Client               | ✔️ Sim      |

---

### ℹ️ Observações

- Um modo **interativo** permite manipular o estado do componente no navegador através de eventos DOM e código C#.
- O modo **Static Server** não suporta atualizações de estado via `@bind`, pois é renderizado apenas uma vez no servidor.
- Indicado para conteúdos estáticos como **páginas institucionais**, **layouts**, etc.

---

## 💡 Uso da Diretiva `@rendermode`

| Render Mode            | Diretiva                               | Projeto Usado |
|------------------------|----------------------------------------|----------------|
| Static Server          | *Não é necessário usar*                | Server         |
| Interactive Server     | `@rendermode InteractiveServer`        | Server         |
| Interactive WebAssembly| `@rendermode InteractiveWebAssembly`   | Client         |
| Interactive Auto       | `@rendermode InteractiveAuto`          | Client         |

---

## 🧬 Herança do Modo de Renderização

- Se um **componente pai** usa, por exemplo, `@rendermode InteractiveServer`, **todos os filhos herdam** esse comportamento.
- ❌ **Não é recomendado** misturar modos diferentes entre pai e filhos que funcionam como uma única unidade.
- ❗ Evite aplicar um render mode interativo diferente nos filhos do que o definido no pai.

---

## ⚙️ Configuração de Projeto

Para suportar todos os modos de renderização, o projeto deve ser dividido em dois:

- **Server** (projeto de startup)
- **Client**

### Exemplo de configuração no `Program.cs` do projeto Server:

```csharp
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MinhaApp.Client._Imports).Assembly);
```

> O método `AddAdditionalAssemblies` permite o mapeamento dos componentes do projeto Client.

---

## 🌐 Renderização Global

- **Não é possível** aplicar `@rendermode` diretamente no componente raiz `App.razor`.
- Em vez disso, defina o modo no componente `Routes.razor`:

```razor
<Routes @rendermode="InteractiveServer" />
```

> Isso propaga o modo de renderização para todas as páginas roteadas.

---

## ❓ E os componentes CSR do projeto Client?

Para aplicações que usam **CSR (WebAssembly ou Auto)**, siga os passos:

1. Mova os layouts e navegação da pasta `Components/Layout` do Server para uma nova pasta `Layout` no projeto Client.
2. Mova os componentes de `Components/Pages` do Server para uma pasta `Pages` no Client.
3. Mova o componente `Routes` do Server para o diretório raiz do projeto Client.

> ✅ Assim, você mantém a estrutura organizada e flexível para projetos com `InteractiveAuto`.

---

## 📚 Documentação Oficial

[Render Modes no Blazor - Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes)
