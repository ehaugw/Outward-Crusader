[Mods by ehaugw](https://outward.thunderstore.io/package/ehaugw/ "Mods by ehaugw") | Support me on [Patreon](https://www.patreon.com/ehaugw "Patreon")


This mods was confirmed to work with the (at that time) current base game (Outward Definitive Edition) at January 07, 2023. Due to significant changes in a few mod frameworks' infrastructures, it is recommended that you update all mods that are older than November 08, 2022.



# Summary

What's the purpose of "The Crusader"?

* Enable the user to play as a holy warrior with divine magic.
* Give the user a legit reason to sacrifice health and stamina on a melee character.
* Enable the user to play an all out melee character without mana, while still casting a few spells.

Manual Installation

* Ensure that you are opted in to the **default-mono** branch.
* Ensure that [BepInEx](https://outward.thunderstore.io/package/BepInEx/BepInExPack_Outward/ "BepInEx") is installed.
* Make sure all **dependencies** are installed. The mod **depends** on these and will not work without them!

  * **[Outward SideLoader](https://outward.thunderstore.io/package/sinai-dev/SideLoader/ "Outward SideLoader")**
  * **[BepInEx](https://outward.thunderstore.io/package/BepInEx/BepInExPack_Outward/ "BepInEx")**
* Uninstall previous versions of the mod.
* Download "Crusader.zip".
* Move "Crusader.zip" into the "Outward" game folder.
* Right click "Crusader.zip" and click "Extract Here".

Manual Uninstallation

* Version 4.3.0 and onwards

  * Navigate to "Outward/BepInEx/plugins/".
  * Delete the "Crusader" folder.
* Version 1.1.0 to 4.1.0

  * Navigate to "Outward/BepInEx/plugins/".
  * Delete the "Templar" folder.
* Version 1.0.0

  * Navigate to "Outward/BepInEx/plugins/".
  * Delete the "Templar" folder.
  * Navigate to "Outward/Mods/SideLoader/".
  * Delete the "Templar" folder.




# Features

"The Crusader" implements a new skill tree with a total of 8 skills (like every other skill tree). The skill tree has a focus on healing the user and his/her allies, resource management and dealing [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage"). A melee character is typcally very strong against a single enemy and struggles against larger groups. This skill tree addresses that issue by offering less single target power in exchange for a weapon infusion ([Zealous Weapon](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Zealous Weapon")) that brings some AoE damage to the table. The skills are as following:

**Cure Wounds** (Mana cost: 14)

* Heals the caster and all allies of the caster within 15 units for 10 health, scaling with the caster's [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") bonus.
* The player is partially mobile during the entire spell casting animation.

**Rebuking Smite** (Cooldown: 20 seconds, Mana cost: 7, Stamina cost: 7)

* A sweeping weapon attack.
* Deals 1.5x impact damage.
* Deals 1.5x weapon damage.

**Meditate** (Passive)

* The caster is set in a meditating pose until he/she moves.
* Meditating for 3 seconds buffs the caster with [Burst of Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Burst of Divinity").
* Receiving the buffs from [Meditate](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Meditate") has a 100 seconds internal cooldown, despite the [Meditate](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Meditate") skill having no cooldown itself.

**Blessed Determination** (Passive)

* Restores 100% of mana spent as stamina.
* Each point of stamina spent gives about 5% [Burst of Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Burst of Divinity") build-up.

**Judgement** (Passive)

* When you expend [Burst of Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Burst of Divinity") to cast a spell, your primary weapon becomes infused with [Zealous Weapon](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Zealous Weapon") for 15 seconds.

**Wrathful Smite** (Cooldown: 40 seconds, Mana cost: 7, Stamina cost: 10)

* A leaping attack that deals more damage to wounded enemies, and instantly becomes available for another use if it kills its target.
* Deals 2.0x impact damage.
* Deals from 2.0x to 5.0x weapon damage depending on the target's missing health.
* The cooldown resets if the skill kills a target.

**Holy Shock** (Cooldown: 30 seconds, Mana cost: 14)

* A blast that damages opponents and heals allies.
* Deals 40 [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") and 40 Impact Damage to enemies.
* Applies 15 seconds of [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") to enemies. Enemies already affected by [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") will spread the effect to nearby enemies.
* Heals allies for 10 health, scaling with the caster's [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") bonus.

**Divinity** (Cooldown: 300 seconds, Mana cost: 7)
This skill can be combined with [Rune Magic](https://outward.gamepedia.com/Rune_Magic "Rune Magic") to make vastly different effects.

* [Rage](https://outward.fandom.com/wiki/Rage "Rage")

  * Adds 50 seconds of [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") to nearby enemies.
* [Discipline](https://outward.fandom.com/wiki/Discipline "Discipline")

  * Buffs the caster with [Healing Surge](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Healing Surge").
* Default

  * Buffs the caster with [Surge of Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Surge of Divinity").



Some of the skills above cause status effects, and some of these effects are added to the game by this mod. The new effects are:

**Zealous Weapon** (Duration: 60 seconds)

* A weapon infusion that adds 5 + 25% of weapon damage as [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") to the affected weapon.
* Applies 15 seconds of [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") on a hit.

**Impending Doom** (Duration: 15 seconds, ticks once every 1 second)

* A status effect that deals 1 [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") per second for 15 seconds to the affected creature.
* Multiple [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") effects stack up, increasing the duration of the effect by 15 seconds for each stack.
* Summons a thunderbolt if the remaining damage of the effect exceeds both 40 and the target's maximum health.

**Burst of Divinity** (Duration: 30 seconds)

* This effect can be stacked indefinitely. Each effect has individual timers.
* Reduces the mana cost of the next spell cast by 7 per stack (before reductions).
* One stack of [Burst of Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Burst of Divinity") will be consumed for every 7 mana that was not spend due to this status effect. This number is always rounded up.

**Surge of Divinity** (Duration: 90 seconds)

* Doubles the build-up rate of [Burst of Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Burst of Divinity").

**Healing Surge** (Duration: 120 seconds)

* Doubles the healing and range of [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds").




# Is it possible to learn this power?

Not from a jedi, but I suggest seeking out [Klaus](https://staticdelivery.nexusmods.com/mods/2775/images/136/136-1610466050-2031884270.png "Klaus").



# Patch notes


* Version 5.1.2

  * Use the correct root folder when launched with anyhting else than manual BepInEx.
  * Moved Claus to a more fitting location nearby.
  * Renamed [Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Divinity") to **Channel**.
* Version 5.1.1

  * Fix README
* Version 5.1.0

  * Uses a standalone DLL for [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom").
* Version 5.0.0

  * Updated to work with Outward Definitive Edition
  * Renamed [Impending Doom Imbue](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom Imbue") to [Zealous Weapon](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Zealous Weapon").
  * Replaced [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity") with **Consecration**.
  * [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity"):

    * Updated visual effects.
    * Staggers creatures that gets too close.
    * Changed to only provide 3 effects based on Discipline and Rager rather than runes.
* Version 4.3.0

  * The mod should no longer require The Three Brothers DLC.
  * [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds"):

    * Is now always AoE.
    * Changed from pre breakthrough to post breakthrough.
    * Does no longer require an empty offhand.
    * Movement speed modifier changed from 60% to 70%.
  * [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity") can be cast with a drawn main hand and an item in the offhand.
  * [Holy Shock](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Holy Shock") does no longer require an empty offhand.
  * [Judgement](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Judgement") was moved from post breakthough to pre breakthrough.
* Version 4.2.2

  * [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") can be applied by guests in multiplayer.
  * AoE healing works as intended.
* Version 4.2.1

  * [Meditate](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Meditate") animations work properly in multiplayer.
* Version 4.2.0

  * [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds") healing changed from 7 to 10.
  * [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") duration increased from 10 to 15.
  * [Blessed Determination](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Blessed Determination") does no long require [Blessed](https://outward.gamepedia.com/Blessed "Blessed").
  * [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity"):

    * Mana cost: 14 - > 7.
    * Rune combo effects were changed.
  * [Prayer](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Prayer") renamed to [Meditate](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Meditate").
  * [Infuse Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Infuse Doom") was removed.
  * [Retributive Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Retributive Smite") was replaced by [Holy Shock](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Holy Shock").
  * [Judgement](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Judgement") was reworked.
  * [Wrathful Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Wrathful Smite"):

    * Stamina cost: 7 -> 10.
    * Durability cost: 5% -> 5
  * [Meditate](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Meditate") animations work properly in multiplayer.
* Version 4.1.0

  * Patched to work with [Outward SideLoader](https://outward.thunderstore.io/package/sinai-dev/SideLoader/ "Outward SideLoader") v3.1.9.
  * Removed [Divine Light Imbue](https://outward.gamepedia.com/Divine_Light_Imbue "Divine Light Imbue"), [Infuse Burst of Light](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Infuse Burst of Light") and [Radiating](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiating").
  * Added **Rebuking Smite**, [Infuse Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Infuse Doom"), [Impending Doom Imbue](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom Imbue"), [Impending Doom](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Impending Doom") and [Healing Surge](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Healing Surge").
  * [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds") was changed from AoE to target self only.
  * Changed the [Shim](https://outward.gamepedia.com/Rune:_Shim "Shim") combination of [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity").
  * Changed the behavior of [Wrathful Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Wrathful Smite").
  * Changed most icons in the mod.
* Version 4.0.0

  * Can be played with (and only with) the base game update related to The Three Brothers.
  * [Prayer](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Prayer") effect is gained after 3 seconds (from 10)
  * Shield of Faith has been replaced with **Aura of Smiting**.
  * The Monsoon Altar interaction has been replaced with a trainer that can be found North-east of the easter Huge Tree in the Hallowed Marsh.
  * New skill icons for many skills
* Version 3.1.0

  * [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds")

    * Mobility when casting: 50% -> 60%
    * Requires empty offhand to be used
  * [Wrathful Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Wrathful Smite") and [Retributive Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Retributive Smite") can now be used with **Gauntlet**s.
  * **HolyDamageManager**

    * Added support for **HolyDamageManager**, which is used to set conveniently set the damage type of damage sources that are considered to be of divine origin.
    * The following skills and effects are affected by the type specificed by **HolyDamageManager**:

      * [Wrathful Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Wrathful Smite")
      * [Retributive Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Retributive Smite")
      * [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds") healing modifier
      * [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity") damage and healing modifiers
      * [Radiating](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiating") damage
      * [Radiant Light Imbue](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiant Light Imbue")
* Version 3.0.1

  * [Radiating](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiating")

    * Damage: 10 -> 15
  * [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity")

    * All combinations

      * Mana cost: 20 -> 14
    * [Shim](https://outward.gamepedia.com/Rune:_Shim "Shim")

      * Targets entities of other factions than the user's faction, rather than entities that are not of the Player faction.
      * Heavily damaged entities are knocked prone, while less damages entities are just briefly staggered.
      * Range: 50 -> 35
      * Base damage: 30 -> 40
  * [Prayer](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Prayer")

    * Fixed a bug causing it to require a [Dagger](https://outward.gamepedia.com/Daggers "Dagger") in your offhand.
  * [Radiating](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiating")

    * AoE damage targets entities of other factions than the source's faction, rather than entities that are not of the Player faction.
    * Damage is amplified by the source's lightning damage bonus.
* Version 3.0.0

  * Can be played with (and only with) the base game update that happened on 16th of June, 2020.
  * [Radiating](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiating")

    * Damage is amplified by the electric damage bonus of the character that caused it.
* Version 2.0.2

  * [Divine Favour](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Divine Favour") has been split into two skills
  * [Prayer](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Prayer") is taught by [Rufus](https://outward.gamepedia.com/Rufus "Rufus")
* Version 2.0.1

  * Patched to support the most recent Outward SideLoader﻿
  * [Radiant Light Imbue](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Radiant Light Imbue")

    * Added support for [Elemental Discharge](https://outward.gamepedia.com/Elemental_Discharge "Elemental Discharge") and [Shield Gong](https://outward.gamepedia.com/Shield_Gong "Shield Gong")
* Version 2.0.0

  * [Cure Wounds](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Cure Wounds")

    * Mana cost: 15 -> 14
    * Healing: 10 -> 8
    * Solo target -> AoE
    * Removed: Cure Bleeding
    * Divine Favour
    * Mana cost: 15 -> 7
  * [Restoration](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Restoration")

    * Mana cost: 15 -> 14
    * Added: Cure Bleeding
    * Removed from skill tree
  * [Blessed Determination](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Blessed Determination")

    * Removed: 15% of spent stamina regained as mana
    * Added: Spending stamina builds up a Burst of Divinity
  * [Channel Divinity](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Channel Divinity")

    * A whole lot!
  * [Wrathful Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Wrathful Smite")

    * Mana cost: 12 -> 7
    * Cooldown: 15 -> 40
    * Damage multiplier: 1.3x -> 1x
    * Damage multiplier to prone entities: 1.5x -> 2.0x
    * Flat damage bonus: 15 -> 30
    * Added: cooldown is reset if the Wrathful Smite kills a target
  * [Retributive Smite](https://outward.thunderstore.io/package/ehaugw/Crusader/ "Retributive Smite")

    * Cooldown: 15 -> 30
    * Mana cost: 12 -> 7
    * Damage multiplier: 1.3x -> 1.5x
    * Flat damage bonus: 15 -> 30
* Version 1.0.0

  * No data.




# Other mods by [ehaugw](https://www.nexusmods.com/users/51266516 "ehaugw") (this list may be outdated)


* [Anti Alchemy Abuse](https://outward.thunderstore.io/package/ehaugw/AntiAlchemyAbuse/ "Anti Alchemy Abuse")

  * Remove alchemy loopholes that obliterates the game economy.
* [Crusader's Equipment](https://outward.thunderstore.io/package/ehaugw/CrusadersEquipment/ "Crusader's Equipment")

  * Provide interesting gear options for people who with to play with knight-ish builds
  * Provide more gear options for players who wish to increase their [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") bonus.
  * Ensuring that the highest possible [Holy Damage](https://www.nexusmods.com/outward/mods/221 "Holy Damage") bonus is not affected by this mod.
  * Provide more options for players who wish to look like someone doing Elatt's bidding
* [Custom Moveset Pack](https://outward.thunderstore.io/package/ehaugw/CustomMovesetPack/ "Custom Moveset Pack")

  * Provide interesting moveset combinations to the user.
  * Introduce significant variations between weapons of the same type.
  * Make rarely used weapons more viable by giving them better movesets rather than better stats.
* [The Juggernaut](https://www.nexusmods.com/outward/mods/143 "The Juggernaut")

  * Enable the user to play as a heavily armored warrior that is in control of the battlefield.
  * Provide other defensive options than Rune Sage, Monk Warrior and Cabal Hermit.
* [Radiant Damage Gear](https://www.nexusmods.com/outward/mods/135 "Radiant Damage Gear")

  * Provide more gear options for players who wish to increase their lightning damage bonus.
  * Ensuring that the highest possible lightning damage bonus is not affected by this mod.
* [Runic Scrolls](https://www.nexusmods.com/outward/mods/132 "Runic Scrolls")

  * Provide Runic Scrolls, which are consumed alternatives to lexicons that goes into your quiver slot.
  * Said alternative should be expensive to not make [Internalized Lexicon](https://outward.gamepedia.com/Internalized_Lexicon "Internalized Lexicon") redundant.
* [Skilled at Sitting](https://www.nexusmods.com/outward/mods/127 "Skilled at Sitting")

  * Enable a coop player to mimic a rest while his sorry excuse of a partner is wasting time organizing his backpack and talking to merchants.
  * Provide a ever so slightly useful alternative to just waiting when you need an important cooldown before a dangerous fight.
  * Provide an alternative to other sitting mods, that is balanced and consistent with the game design (thus ruling out "Medidation", as passive mana regen in Outward is related to sleep).
* [The Crusader](https://outward.thunderstore.io/package/ehaugw/Crusader/ "The Crusader")

  * Enable the user to play as a holy warrior with divine magic.
  * Give the user a legit reason to sacrifice health and stamina on a melee character.
  * Enable the user to play an all out melee character without mana, while still casting a few spells.
* [Martial Artist](https://outward.thunderstore.io/package/ehaugw/MartialArtist/ "Martial Artist")

  * Provide some simple mechanics, locked behind basic skills, to add some depth to combat




# Credits

Thanks to **Sinai** for making the [Outward Explorer](https://github.com/sinaioutlander/Outward-Mods/tree/master/Explorer "Outward Explorer"), and always beeing up for discussing solutions to anything.
Thanks to IggyTheMad for designing most of the icons in the mod!
