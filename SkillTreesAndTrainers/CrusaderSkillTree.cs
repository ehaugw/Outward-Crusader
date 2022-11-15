using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SideLoader;

namespace Crusader
{
    using EffectSourceConditions;

    public class TrainingLocation
    {
        public Vector3 positionUpper;
        public Vector3 positionLower;
        public float direction;
        public float angle;
        public string scene;
        public float? range;
        public SkillSchool skillSchool;
        public SourceCondition requirement;
    }

    public class CrusaderSkillTree
    {
        public static SkillSchool KlausSkillSchool;

        private const bool includeBreakthrough = true;
        public static List<TrainingLocation> TrainingLocations = new List<TrainingLocation>();

        public static void SetupSkillTree(ref SkillSchool skillTreeInstance)
        {
            //Monsoon player hous
            //TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(-375.8f, 0, 764.5f), positionUpper = new Vector3(-374, 0, 766.5f), direction = 165f, angle = 50f, scene = "Monsoon", skillSchool = null });
            
            //HOly mission path
            //TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(-16.5f, 0, -20), positionUpper = new Vector3(-12.5f, 0, -18), direction = 345f, angle = 50f, scene = "Chersonese_Dungeon4_HolyMission", skillSchool = null });


            SL_SkillTree myskilltree;

            // --------------- Default Trainer Skill Tree --------- // Works
            myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,
                
                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                            //new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 50, SkillID = IDs.cureWoundsID,                Breakthrough = false,   RequiredSkillSlot = Vector2.zero, },
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 50, SkillID = IDs.meditationID,                  Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 50, SkillID = IDs.rebukingSmiteID,               Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                    } },

                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 100, SkillID = IDs.divineFavourID,               Breakthrough = false,   RequiredSkillSlot = new Vector2(1,2) },
                    } },

                    new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.blessedDeterminationID,       Breakthrough = true,  RequiredSkillSlot = Vector2.zero, },
                    } },

                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 600, SkillID = IDs.consecrationID,               Breakthrough = false,   RequiredSkillSlot = new Vector2(1, 1)},
                            //new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 600, SkillID = IDs.divineFavourID,             Breakthrough = false,   RequiredSkillSlot = new Vector2(3, 2)},
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 600, SkillID = IDs.cureWoundsID,                 Breakthrough = false,  RequiredSkillSlot = new Vector2(3, 2)},

                            //new SL_SkillSlotFork() {
                            //    ColumnIndex = 2,
                            //    RequiredSkillSlot = Vector2.zero,
                            //    Choice2 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.holyShockSkillID,         Breakthrough = false,   RequiredSkillSlot = new Vector2(3, 2)}

                            //},
                        }
                    },

                    new SL_SkillRow() {
                        RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlotFork() 
                            {
                                ColumnIndex = 2,
                                RequiredSkillSlot = Vector2.zero,
                                Choice1 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.holyShockSkillID, RequiredSkillSlot = new Vector2(3, 2)},
                                Choice2 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.wrathfulSmiteID, RequiredSkillSlot = new Vector2(3, 2)},
                            }
                        } 
                    }
                }
            };

            skillTreeInstance = myskilltree.CreateBaseSchool();
            myskilltree.ApplyRows();

            //TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(0,0,0), positionUpper = new Vector3(0, 0, 0), direction = 0, angle = 0, scene = null, skillSchool = skillTreeInstance });


            // --------------- Klaus in Hallowed Marsh ------------ // Works
            /*myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,

                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 50, SkillID = IDs.infuseBurstOfLightID,  Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                    } },
                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                    includeBreakthrough ? new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.blessedDeterminationID, Breakthrough = true,  RequiredSkillSlot = Vector2.zero, },
                    } } : new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() { } },

                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.auraOfSmitingID,    Breakthrough = false,   RequiredSkillSlot = includeBreakthrough ? new Vector2(3, 2) : Vector2.zero},
                    } },

                    new SL_SkillRow() { RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                        new SL_SkillSlotFork() {
                            ColumnIndex = 2,
                            RequiredSkillSlot = new Vector2(4, 2),
                            Choice1 = new SL_SkillSlot() {
                                ColumnIndex = 2,
                                SilverCost = 600,
                                SkillID = IDs.retributiveSmiteID,
                                RequiredSkillSlot = new Vector2(4, 2),
                            },
                            Choice2 = new SL_SkillSlot() {
                                ColumnIndex = 2,
                                SilverCost = 600,
                                SkillID = IDs.wrathfulSmiteID,
                                RequiredSkillSlot = new Vector2(4, 2),
                            }
                        },
                        new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 600, SkillID = IDs.wrathfulSmiteCooldownResetID,    Breakthrough = false,   RequiredSkillSlot = new Vector2(5, 2)},
                    } },
                }
            };

            KlausSkillSchool = myskilltree.CreateBaseSchool();
            myskilltree.ApplyRows();*/
            KlausSkillSchool = skillTreeInstance;

            /*
            // --------------- Cierzo Light House Night Table ------------ // Works
            myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,

                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 50, SkillID = IDs.cureWoundsID,          Breakthrough = false,   RequiredSkillSlot = Vector2.zero, },
                            //new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 50, SkillID = IDs.infuseBurstOfLightID,  Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                    } },
                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                    new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                    new SL_SkillRow() { RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                }
            };

            TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(-172f, 0, 759.5f), positionUpper = new Vector3(-171f, 0, 760.4f), direction = 67.5f, angle = 45f, scene = "CierzoNewTerrain", skillSchool = myskilltree.CreateBaseSchool() });
            myskilltree.ApplyRows();


            // ---------------    Conflux Chambers ------------ // Works
            myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,

                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.blessedDeterminationID, Breakthrough = true,  RequiredSkillSlot = Vector2.zero, },
                    } },

                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                }
            };

            TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(-410, -8.7f, -90), range=2, direction = 87.5f, angle = 70f, scene = "Chersonese_Dungeon4_CommonPath", skillSchool = myskilltree.CreateBaseSchool() });
            myskilltree.ApplyRows();


            // ---------------    Monsoon Temple Altar Without Breakthrough  ------------ // Works
            myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,

                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 50, SkillID = IDs.infuseBurstOfLightID,  Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                    } },

                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                }
            };

            TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(-176.5f, 0, 753), positionUpper = new Vector3(-174, 0, 755), direction = 355f, angle = 60f, scene = "Monsoon", skillSchool = myskilltree.CreateBaseSchool(), requirement = new SourceConditionSkill() { RequiredSkillID = IDs.blessedDeterminationID, Inverted = true} });
            myskilltree.ApplyRows();

            // ---------------    Monsoon Temple Altar   ------------ // Works
            myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,

                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 50, SkillID = IDs.infuseBurstOfLightID,  Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                    } },

                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    includeBreakthrough ? new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.blessedDeterminationID, Breakthrough = true,  RequiredSkillSlot = Vector2.zero, },
                    } } : new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() { } },

                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 600, SkillID = IDs.channelDivinityID,    Breakthrough = false,   RequiredSkillSlot = includeBreakthrough ? new Vector2(3, 2) : Vector2.zero},
                    } },

                    new SL_SkillRow() { RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                }
            };

            TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(-176.5f, 0, 753), positionUpper = new Vector3(-174, 0, 755), direction = 355f, angle = 60f, scene = "Monsoon", skillSchool = myskilltree.CreateBaseSchool(), requirement = new SourceConditionSkill() { RequiredSkillID = IDs.blessedDeterminationID } });
            myskilltree.ApplyRows();


            // ---------------    Ancestors resting place   ------------ // Works
            myskilltree = new SL_SkillTree()
            {
                Name = ModTheme.SkillTreeName,

                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    includeBreakthrough ? new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.blessedDeterminationID, Breakthrough = true,  RequiredSkillSlot = Vector2.zero, },
                    } } : new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() { } },

                    new SL_SkillRow() { RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 600, SkillID = IDs.divineFavourID,       Breakthrough = false,   RequiredSkillSlot = includeBreakthrough ? new Vector2(3, 2) : Vector2.zero},
                    } },

                    new SL_SkillRow() { RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                    } },
                }
            };
            TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(27.92f, 0.03f, 0.7f), range = 1.5f, direction = 90f, angle = 50f, scene = "Emercar_Dungeon5", skillSchool = myskilltree.CreateBaseSchool(), requirement = new SourceConditionSkill() { RequiredSkillID = IDs.blessedDeterminationID } });
            //TrainingLocations.Add(new TrainingLocation() { positionLower = new Vector3(0f, 0, 2), range = 1.5f, direction = 180f, angle = 50f, scene = "Emercar_Dungeon5", skillSchool = myskilltree.CreateBaseSchool(), requirement = new SourceConditionSkill() { RequiredSkillID = IDs.blessedDeterminationID } });
            myskilltree.ApplyRows();
            */
        }
    }
}
