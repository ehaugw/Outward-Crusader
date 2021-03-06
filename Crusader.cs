namespace Crusader
{
    using System.Collections.Generic;
    using UnityEngine;
    using SideLoader;
    using HarmonyLib;
    using BepInEx;
    using InstanceIDs;
    using System;
    using TinyHelper;
    using CustomWeaponBehaviour;
    using System.Linq;
    using HolyDamageManager;
    using EffectSourceConditions;
    using SynchronizedWorldObjects;

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.sinai.SideLoader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(EffectSourceConditions.GUID, EffectSourceConditions.VERSION)]
    [BepInDependency(TinyHelper.GUID, TinyHelper.VERSION)]
    [BepInDependency(HolyDamageManager.GUID, HolyDamageManager.VERSION)]
    [BepInDependency(SynchronizedWorldObjects.GUID, SynchronizedWorldObjects.VERSION)]
    [BepInDependency(CustomWeaponBehaviour.GUID, CustomWeaponBehaviour.VERSION)]

    public class Crusader : BaseUnityPlugin
    {
        //public const bool RELATED_TO_ELATT = false;

        public static Crusader Instance;

        public const string GUID = "com.ehaugw.crusaderclass";
        public const string VERSION = "4.3.0";
        public const string NAME = "The Crusader";
        public const string ModFolderName = "Crusader";

        public Skill cureWoundsInstance;
        public Skill divineFavourInstance;
        public Skill infuseBurstOfLightInstance;
        public Skill blessedDeterminationInstance;
        public Skill restorationInstance;
        public Skill thunderousSmiteInstance;
        public Skill wrathfulSmiteInstance;
        public Skill rebukingSmiteInstance;
        public Skill retrubutiveSmiteInstance;
        public Skill blessingOfProtectionInstance;
        public Skill shieldofFaithInstance;
        public Skill channelDivinityInstance;
        public Skill meditationInstance;
        public Skill celestialSurgeInstance;
        public Skill prayerOfHealingInstance;
        public Skill auraOfSmitingInstance;
        public Skill sharingIsCaringInstance;
        public Skill wrathfulSmiteCooldownReset;
        public Skill globalThunderInstance;
        public Skill holyShockInstance;

        //public StatusEffect radiatingInstance;
        public StatusEffect impendingDoomInstance;
        public StatusEffect burstOfDivinityInstance;
        public StatusEffect surgeOfDivinityInstance;
        public StatusEffect meditationStatusEffectInstance;
        public StatusEffect meditationCooldownStatusEffectInstance;
        public StatusEffect auraOfSmitingEffectInstance;
        public StatusEffect healingSurgeInstance;

        public ImbueEffectPreset classInfusion;

        public Tag AfterUseManaTagInstance;

        public static SkillSchool CrusaderSkillTreeInstance;

        internal void Awake()
        {
            Instance = this;

            var rpcGameObject = new GameObject("CrusaderRPC");
            DontDestroyOnLoad(rpcGameObject);
            rpcGameObject.AddComponent<CrusaderRPCManager>();


            KlausNPC.Init();

            SL.OnPacksLoaded += OnPackLoaded;
            SL.OnSceneLoaded += OnSceneLoaded;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        public Trainer altarTrainer;

        private void OnPackLoaded()
        {
            //try
            //{
                //radiatingInstance                   = EffectInitializer.MakeRadiatingPrefab();
                impendingDoomInstance                   = EffectInitializer.MakeImpendingDoomPrefab();
                burstOfDivinityInstance                 = EffectInitializer.MakeBurstOfDivinityPrefab();
                surgeOfDivinityInstance                 = EffectInitializer.MakeSurgeOfDivinityPrefab();
                classInfusion                           = EffectInitializer.MakeClassInfusion();
                                                      //EffectInitializer.AddCondemnToDivineLightImbue();
                meditationStatusEffectInstance          = EffectInitializer.MakeMeditationPrefab();
                meditationCooldownStatusEffectInstance  = EffectInitializer.MakeMeditationCooldownPrefab();
                auraOfSmitingEffectInstance             = EffectInitializer.MakeAuraOfSmitingPrefab();
                healingSurgeInstance                    = EffectInitializer.MakeHealingSurgePrefab();


                cureWoundsInstance                      = CureWoundsSpell.Init();
                divineFavourInstance                    = Judgement.Init();
                infuseBurstOfLightInstance              = InfuseBurstOfLight.Init();   
                blessedDeterminationInstance            = BlessedDeterminationSpell.Init();
                restorationInstance                     = RestorationSpell.Init();
                retrubutiveSmiteInstance                = RetributiveSmiteSpell.Init();
                wrathfulSmiteInstance                   = WrathfulSmiteSpell.Init();
                rebukingSmiteInstance                   = RebukingSmiteSpell.Init();
                shieldofFaithInstance                   = ShieldOfFaithSpell.Init();
                channelDivinityInstance                 = ChannelDivinitySpell.Init();
                meditationInstance                      = MeditateSpell.Init();
                celestialSurgeInstance                  = CelestialSurgeSpell.Init();
                prayerOfHealingInstance                 = PrayerOfHealingSpell.Init();
                auraOfSmitingInstance                   = AuraOfSmitingSpell.Init();
                sharingIsCaringInstance                 = SharingIsCaringSpell.Init();
                wrathfulSmiteCooldownReset              = WrathfulSmiteCooldownReset.Init();
                holyShockInstance                       = HolyShock.Init();

                AfterUseManaTagInstance                 = TinyTagManager.GetOrMakeTag(IDs.AfterUseManaTag);

                //RadiantSpark.Init();

                CrusaderSkillTree.SetupSkillTree(ref CrusaderSkillTreeInstance);

            //}
            //catch (Exception e)
            //{
            //    Debug.Log(String.Format("Unhandled {0} Crusader:\n{1}", e.GetType(), e.Message));
            //}

        }

        private void OnSceneLoaded()
        {
            SetupTrainers.SetupRufusInteraction();
            //SetupTrainers.SetupAltarInteraction(ref altarTrainer, ref CrusaderSkillTreeInstance);
        }
    }
}