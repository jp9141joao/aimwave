## 👨‍💻 Informações do Desenvolvedor
* **Nome do Aluno:** João Pedro Rosa de Paula  
* **Contexto:** Trabalho Prático do 2º Bimestre – Criação de Jogo 3D no Unity  

---

## 📝 Sobre o Projeto

O **AimWave** é um jogo de tiro em primeira pessoa (FPS) totalmente em 3D desenvolvido utilizando a engine **Unity 3D**. O objetivo principal do jogo é testar e aprimorar os reflexos e a precisão do jogador. Dentro de um limite de tempo dinâmico de 30 segundos, o jogador deve localizar e destruir alvos que surgem aleatoriamente pelas paredes da arena, buscando alcançar a maior pontuação possível antes do cronômetro zerar.

---

## 🎥 Gameplay (Vídeo)

Assista abaixo à demonstração completa do funcionamento do jogo, incluindo navegação pelos menus, mecânicas de tiro, recarga e a interface em execução:

👉 **[Clique aqui para assistir ao vídeo de Gameplay no YouTube](https://youtu.be/K5cLGAWOW6E)**

---

## 🎮 Gameplay e Regras

* **Sistema de Pontuação:** Cada alvo destruído concede exatamente **1 ponto** ao jogador.
* **Comportamento dos Alvos:** Os alvos surgem em grupos de 3 nas paredes da arena enfrentada pelo jogador. Para que uma nova leva de alvos apareça na mesma parede, todos os 3 alvos ativos dela devem ser obrigatoriamente destruídos. Há um pequeno delay estratégico antes do respawn.
* **Gerenciamento de Munição:** O rifle do jogador possui um pente com capacidade estendida de **10 balas**. O jogador pode recarregar a arma a qualquer momento de forma manual, ou esperar que a munição acabe para zerar o contador.
* **Condição de Fim de Jogo:** A rodada é encerrada imediatamente assim que o cronômetro de 30 segundos chega a zero.

---

## ⌨️ Controles

O jogo foi projetado para uma jogabilidade fluida e responsiva utilizando o mapeamento clássico de teclado e mouse da plataforma PC:

| Ação | Comando / Tecla |
| :--- | :--- |
| **Mirar / Rotacionar Câmera** | Movimento do Mouse |
| **Atirar** | <kbd>Botão Esquerdo do Mouse</kbd> |
| **Movimentar Personagem** | Teclas <kbd>W</kbd>, <kbd>A</kbd>, <kbd>S</kbd>, <kbd>D</kbd> |
| **Recarregar Arma** | Tecla <kbd>R</kbd> |
| **Iniciar Jogo (No Menu)** | Tecla <kbd>Espaço</kbd> ou Clique no Botão |
| **Sair do Jogo** | Tecla <kbd>Esc</kbd> |

---

## 🖥️ Interface do Usuário (HUD)

A interface de usuário projetada dentro do jogo fornece informações críticas e limpas em tempo real, permitindo que o jogador gerencie seus recursos de forma ágil:
* **Contador de Pontos:** Localizado no topo da tela, exibe a pontuação atualizada.
* **Cronômetro:** Mostra a contagem regressiva dos 30 segundos restantes.
* **Indicador de Munição:** Exibe as balas disponíveis no pente atual (Ex: 10/10).
* **Retícula (Crosshair):** Centralizada perfeitamente na tela para guiar a mira do jogador.

---

## 🎵 Menu Principal (Com Música de Fundo)

Conforme os requisitos do trabalho, o jogo inicia em uma cena de **Menu Principal** estilizada e funcional. O menu conta com uma **música de fundo (BGM) em loop contínuo** gerenciada de forma centralizada pelo sistema de áudio, botões interativos para iniciar a gameplay e a opção de fechar o jogo de forma limpa.

---

## 🚀 Funcionalidades em Destaque (Explicação e Código)

Conforme as diretrizes técnicas do projeto, abaixo estão detalhadas duas das principais funcionalidades customizadas implementadas no core do jogo, acompanhadas de seus respectivos códigos-fonte em C# e capturas de tela correlacionadas.

### 1. Sistema Inteligente de Spawn de Alvos (`TargetSpawner`)
* **Descrição:** Essa funcionalidade gerencia o surgimento automatizado dos alvos na arena. Em vez de simplesmente gerar alvos infinitos em locais aleatórios causando sobreposição (clipping de malhas), o script utiliza sensores baseados em **Box Colliders** aplicados nas paredes. O sistema verifica em tempo real se uma parede está totalmente limpa de alvos antes de liberar uma nova onda de 3 alvos com coordenadas sorteadas dentro dos limites físicos válidos.

```csharp
using UnityEngine;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour 
{
    [Header("Configurações dos Alvos")]
    public GameObject targetPrefab;
    public Transform[] wallSpawnAreas;
    private List<GameObject> activeTargets = new List<GameObject>();

    // Inicializa o spawn ao iniciar a partida através do TimeManager
    public void StartSpawning() 
    {
        SpawnNewWave();
    }

    public void SpawnNewWave() 
    {
        // Garante a limpeza de referências nulas antigas
        activeTargets.RemoveAll(item => item == null);

        if (activeTargets.Count == 0) 
        {
            for (int i = 0; i < 3; i++) 
            {
                // Escolhe uma parede aleatória e calcula posição randômica interna
                Transform selectedWall = wallSpawnAreas[Random.Range(0, wallSpawnAreas.Length)];
                Vector3 spawnPosition = selectedWall.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 1f), 0);
                
                GameObject newTarget = Instantiate(targetPrefab, spawnPosition, selectedWall.rotation);
                activeTargets.Add(newTarget);
            }
            Logger.Instance.LogEvent("TargetSpawner", "Nova leva de 3 alvos instanciada com sucesso.");
        }
    }
}
