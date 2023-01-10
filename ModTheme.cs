using InstanceIDs;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    public class ModTheme
    {
        private static int m_skillTreeNumber = 0;
        public enum Theme
        {
            Elatt,
            DawnWeaver,
            Atheist,
            Crusader
        }

        public static Theme? modTheme = null;

        public static Theme GetTheme
        {
            get
            {
                return modTheme ?? Theme.Crusader;
            }
        }

        public static string SkillTreeName
        {
            get
            {
                //Return just the name if it's the first tree, else append a number
                return SkillTreeNameReadable + (m_skillTreeNumber++ > 0 ? m_skillTreeNumber.ToString() : "");
            }
        }

        public static string SkillTreeNameReadable
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                        return "Templar";
                    case Theme.DawnWeaver:
                        return "Dawn Weaver Templar";
                    case Theme.Atheist:
                        return "Pilgrim";
                    case Theme.Crusader:
                        return "Crusader";
                    default:
                        return "UNTHEMED STRING";
                }
            }
        }

        public static string ConsecratedGroundEffectName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                    case Theme.Atheist:
                    case Theme.Crusader:
                    default:
                        return "Consecrated Ground";
                }
            }
        }

        public static string MeditationCooldownNotification
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "You get the feeling that nobody are even listening to your prayers.";
                    case Theme.Atheist:
                    case Theme.Crusader:
                        return "You are unable to focus.";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string BlessedDeterminationSpellName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Blessed Determination";
                    case Theme.Atheist:
                        return "Resolute Determination";
                    case Theme.Crusader:
                        return "Blessed Determination";
                }
                return "UNTHEMED STRING";
            }
        }
        public static string DivineFavorSpellName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Divine Favor";
                    case Theme.Atheist:
                        return "Condemn";
                    case Theme.Crusader:
                        return "Judgement";
                }
                return "UNTHEMED STRING";
            }
        }
        public static string BlessedDeterminationRequiredBoonName
        {
            get
            {
                //switch (GetTheme)
                //{
                //    case Theme.Elatt:
                //    case Theme.DawnWeaver:
                //        return "Bless";
                //    case Theme.Atheist:
                //        return "Discipline";
                //    case Theme.Crusader:
                //        return "Bless";
                //}
                return null;
            }
        }
        public static string PrayForSkillTreeMissingCondition
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                    case Theme.Atheist:
                    case Theme.Crusader:
                        return "There is something about this place that makes you unable to connect with yourself. Maybe you should return to this place at a later stage in your training?";
                }
                return "UNTHEMED STRING";
            }
        }

        public const string BurstOfDivinityEffectIdentifierName = IDs.burstOfDivinityNameID;
        public static string BurstOfDivinityEffectName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Burst of Divinity";
                    case Theme.Atheist:
                        return "Burst of Clarity";
                    case Theme.Crusader:
                        return "Burst of Divinity";
                }
                return "UNTHEMED STRING";
            }
        }

        public const string AncestralMemoryEffectName = "Ancestral Memory";

        public static string ChannelDivinitySpellName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Channel Divinity";
                    case Theme.Atheist:
                        return "Channel Soul";
                    case Theme.Crusader:
                        return "Channel";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string MeditationSkillName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Pray";
                    case Theme.Atheist:
                    case Theme.Crusader:
                        return "Meditate";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string MeditationDescription
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Prayers are all good...\n\nBut do not expect anyone to reply!";
                    case Theme.Atheist:
                    case Theme.Crusader:
                        return "Meditate for a moment.";
                        //return "Reflect on your course of action.";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string InfusionSpellName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                        return "Infuse Burst of Light";
                    case Theme.DawnWeaver:
                        return "Infuse Dawn";
                    case Theme.Atheist:
                        return "Infuse Steel Heart";
                    case Theme.Crusader:
                        return "Infuse Doom";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string InfusionSpellDescriptionType
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                        return "Light";
                    case Theme.Atheist:
                        break;
                    case Theme.Crusader:
                        return "Doom";
                }
                return "UNTHEMED STRING";
            }
        }
        public static string ImbueEffectName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                        return "Radiant Light Imbue";
                    case Theme.DawnWeaver:
                        return "Dawn Imbue";
                    case Theme.Atheist:
                        return "Steel Heart Imbue";
                    case Theme.Crusader:
                        return "Zealous Weapon";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string BlueChamberImbueName
        {
            get
            {
                return BoneChillName + " Weapon";
            }
        }

        public static string BoneChillName
        {
            get
            {
                return "Bone Shivering";
            }
        }

        public static string RadiatingEffectName
        {
            get
            {
                switch (GetTheme)
                {
                    case Theme.Elatt:
                    case Theme.DawnWeaver:
                    case Theme.Atheist:
                    case Theme.Crusader:
                        return "Radiating";
                }
                return "UNTHEMED STRING";
            }
        }

        public static string ImpendingDoomEffectName
        {
            get
            {
                return "Impending Doom";
            }
        }
        public static string Themifization() { return null; }
    }
}
