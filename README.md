<h1 align="center">⚔️ RPG-ERA: 2D Action Framework</h1>

<p align="center">
  A highly modular and scalable 2D Action-RPG framework built with Unity and C#. Designed with strong Object-Oriented Programming (OOP) principles, this project focuses on creating a robust foundation for character controllers, artificial intelligence, and complex RPG mechanics.
</p>

<hr>

<h2 align="center">⚙️ Core Systems & Architecture (Completed)</h2>

<p>
  The project utilizes a custom <strong>Finite State Machine (FSM)</strong> architecture, heavily relying on inheritance (via a core <code>Entity</code> class) and interfaces to ensure clean, maintainable, and decoupled code.
</p>

<ul>
  <li>
    ✅ <strong>Advanced Player Controller (FSM):</strong> A highly responsive player movement system completely decoupled into isolated state scripts (e.g., <code>Player_DashState</code>, <code>Player_WallSlideState</code>, <code>Player_AirState</code>) for maximum control and bug prevention.
  </li>
  <li>
    ✅ <strong>Modular AI Behaviors:</strong> Enemies utilize their own independent state machines. Behaviors like patrolling, engaging, stunning, and attacking are handled dynamically based on the player's distance and actions.
  </li>
  <li>
    ✅ <strong>Responsive Combat Engine:</strong> A fluid melee combat system featuring combo attacks, directional hits, and counter-attack mechanics. Built using interfaces like <code>IDamageable</code> and <code>ICounterable</code> for universal interaction across all entities.
  </li>
  <li>
    ✅ <strong>Scalable RPG Attributes (Stat Engine):</strong> A deeply customizable math-based stat system divided into logical categories (<code>Stat_Major</code>, <code>Stat_Offense</code>, <code>Stat_Defense</code>, <code>ElementType</code>). It calculates critical strikes, elemental damage, and evasion seamlessly.
  </li>
  <li>
    ✅ <strong>Environment & Parallax Camera:</strong> A multi-layered parallax background system that gives depth and scale to the 2D environments.
  </li>
</ul>

<hr>

<h2 align="center">🗺️ Development Roadmap (Work in Progress)</h2>

<p>
  The foundational architecture is complete. The next phases focus on integrating deep RPG progression systems and world interactions:
</p>

<table>
  <tr>
    <td width="50%" valign="top">
      <h3>🔜 Progression & Abilities</h3>
      <ul>
        <li>⬜ <strong>Skill Tree System:</strong> Unlockable active/passive abilities with a custom UI.</li>
        <li>⬜ <strong>Core Mechanics Expansion:</strong> Integrating magic casting, parrying, and advanced mobility skills into the FSM.</li>
        <li>⬜ <strong>Upgrades:</strong> Enhancing existing skills through an experience (XP) system.</li>
      </ul>
    </td>
    <td width="50%" valign="top">
      <h3>🔜 Economy & Equipment</h3>
      <ul>
        <li>⬜ <strong>Dynamic Inventory:</strong> Item management, equipment slots, and real-time stat recalculation.</li>
        <li>⬜ <strong>Loot & Drops:</strong> Randomized enemy drops and interactable chests.</li>
        <li>⬜ <strong>Crafting & Merchants:</strong> Resource gathering, item creation, and an in-game economy system.</li>
      </ul>
    </td>
  </tr>
  <tr>
    <td width="50%" valign="top">
      <h3>🔜 Game Flow & Data</h3>
      <ul>
        <li>⬜ <strong>Serialization (Save/Load):</strong> Saving player progress, inventory state, and world data permanently.</li>
        <li>⬜ <strong>Scene Management:</strong> Seamless transitions between levels, main menus, and dungeons.</li>
        <li>⬜ <strong>Dynamic Audio System:</strong> Centralized audio manager for BGM, SFX, and combat feedback.</li>
      </ul>
    </td>
    <td width="50%" valign="top">
      <h3>🔜 Narrative & World </h3>
      <ul>
        <li>⬜ <strong>Quest Engine:</strong> Main storyline and side quest tracking.</li>
        <li>⬜ <strong>Dialogue System:</strong> NPC interactions with branching choices.</li>
        <li>⬜ <strong>Expanded Bestiary:</strong> Adding ranged enemies, spellcasters, and multi-phase boss fights.</li>
      </ul>
    </td>
  </tr>
</table>

<hr>

<h2>🛠️ Tech Stack & Design Patterns</h2>
<ul>
  <li><strong>Engine:</strong> Unity (2D)</li>
  <li><strong>Language:</strong> C#</li>
  <li><strong>Architecture:</strong> Finite State Machine (State Pattern), Component-Based Design, Inheritance hierarchy (Entity -> Player/Enemy).</li>
  <li><strong>Key Concepts:</strong> Object Pooling (planned for VFX/Projectiles), Interface-driven combat, Encapsulation.</li>
</ul>
