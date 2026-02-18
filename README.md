#  RoguelikeYago — YAGO GAME

Roguelike en **C# consola** con combate por turnos, generación determinista por seed y contenido 100% basado en JSON.

---
## Ejecutar
Clicka en code. despues en download ZIP. Una vez descargado ver a descargas en tu PC y descomprime el archivo. Abre la carpeta descomprimida y doble click en YagoGame.exe. A disfrutar crack.


En caso de que no funcione abre la consola de comandos en la carpeta y ejecura `dotne run`

Paso 1: <img width="113" height="33" alt="image" src="https://github.com/user-attachments/assets/4ddf7bc7-73ed-4995-9001-dbf859724a7e" />
<img width="379" height="290" alt="image" src="https://github.com/user-attachments/assets/d8f9a10a-6342-424b-adb8-041d982bdd97" />



Paso 2:
<img width="147" height="27" alt="image" src="https://github.com/user-attachments/assets/2c82bdb4-64b7-45e8-ab1a-9d1b68cb7537" />
<img width="337" height="463" alt="image" src="https://github.com/user-attachments/assets/bdd744ed-6fee-4723-96d0-2d804815659a" />

Paso 3:

<img width="632" height="367" alt="image" src="https://github.com/user-attachments/assets/bb119cee-63c6-470c-a694-e7295588af20" />


#  Descripción

Yago se levanta tranquilamente… y descubre que **Lander le ha robado la cama**.

Tu misión es avanzar por salas, derrotar enemigos, superar bosses y recuperar lo más sagrado:  
🛏️ **La cama.**

El juego es simple, directo y completamente data-driven.  
No hay mecánicas ocultas ni expansiones futuras: solo añadir nuevos ítems, enemigos o habilidades.

---

#  Características

-  Combate por turnos
-  Orden de ataque por **Speed**
-  Salas generadas juntando enemigos independientes (1 a 5)
-  3 clases jugables
-  4 bosses
-  3 salas entre cada boss
-  Recompensas tras cada sala
-  NPCs tras bosses
-  Run determinista por seed
-  Contenido completamente definido en JSON
-  Uso obligatorio de LINQ para obtener datos

---

#  Sistema de combate

Stats principales:

- `Hp`
- `Damage`
- `Armor`
- `Speed`

### Orden de turnos

El que tenga **más Speed ataca primero**.  
Los ataques siempre se resuelven por orden descendente de Speed.

### Enemigos

- Cada enemigo tiene **1 único ataque**
- Se agrupan para formar salas
- El jugador elige a qué enemigo atacar

### Post-combate

Tras ganar una sala:
- La vida del jugador se restaura al máximo
- Aparece selección de recompensa

---

# Estructura del proyecto

```bash
YagoGame/
│
├── Data/                      
│   ├── bosses.json
│   ├── classes.json
│   ├── config.json
│   ├── enemies.json
│   ├── items.json
│   ├── npcs.json
│   └── skills.json
│
├── Saves/                      
│   └── save_1.json
│
├── Src/
│   │
│   ├── Combat/                   
│   │   └── CombatService.cs
│   │
│   ├── Config/                 
│   │   ├── EnemyGenerationConfig.cs
│   │   ├── GameConfig.cs
│   │   ├── JsonOptions.cs
│   │   ├── PathConfig.cs
│   │   └── RngConfig.cs
│   │
│   ├── Definitions/              
│   │   ├── AttackDef.cs
│   │   ├── BossDef.cs
│   │   ├── ClassDef.cs
│   │   ├── EnemyDef.cs
│   │   ├── ItemDef.cs
│   │   ├── NpcDef.cs
│   │   ├── SkillDef.cs
│   │   └── StatsDef.cs
│   │
│   ├── Drops/                 
│   │   ├── DropsConfig.cs
│   │   └── RewardService.cs
│   │
│   ├── Npcs/                    
│   │   └── NpcsService.cs
│   │
│   ├── Persistence/              
│   │   ├── SaveFile.cs
│   │   └── SaveService.cs
│   │
│   ├── Player/                   
│   │   ├── PlayerFactory.cs
│   │   └── PlayerState.cs
│   │
│   ├── Runs/                    
│   │   └── Run.cs
│   │
│   ├── Services/                
│   │   ├── ContentService.cs
│   │   └── JsonFileLoader.cs
│   │
│   └── UI/                       
│       ├── ArrowMenu.cs
│       ├── MainMenu.cs
│       ├── Story.cs
│       └── Typewriter.cs
│
└── Program.cs

```


---

#  Sistema Data-Driven (JSON)

Todo el contenido del juego se define en `Data/`:

- Clases
- Enemigos
- Bosses
- Skills
- Ítems
- NPCs
- Configuración

No se hardcodea contenido.

Si se quiere añadir algo nuevo:
- Se modifica el JSON correspondiente
- No se crean nuevas mecánicas


---


#  Controles

- Flechas → mover selección
- Enter → confirmar

  
---

# Idea de proyecto

Proyecto simple, organizado y coherente.

Separación clara por carpetas:
- Definitions → modelos
- Services → carga de contenido
- Combat → lógica de combate
- Player → estado del jugador
- UI → interacción consola
- Persistence → guardado (no implementado todavia)
- Drops / Npcs → sistemas auxiliares
- Runs → flujo principal de partida

Todo el comportamiento debe respetar la estructura del PDF base del proyecto.

---

#  Estado actual

Sistema de combate funcional  
Generación de salas  
Bosses implementados  
Sistema de recompensas  
NPCs funcionales  
UI por consola operativa  

---

# Futuras implementaciones

- Eleccion de claes para el jugador
- El jugador tendra 4 ataque distintos
- Guardado
- Utilizacion de seed
- Objetos consumibles 

---

RoguelikeYago — Recupera la cama. Derrota a Lander.

Creador Emilio Soto Alzamora


