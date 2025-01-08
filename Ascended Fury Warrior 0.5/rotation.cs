using AimsharpWow.API; //needed to access Aimsharp API
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO; // Used for Licensing
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using AimsharpWow.Modules;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks; // Hinzugefügt für Task


namespace AimsharpWow.Modules
{
    public class AscendedFuryWarrior : Rotation
    {
        private static string Language = "English";
        bool authorized = false;
        private readonly LicenseServer licenseServer;
        public AscendedFuryWarrior()
        {
            licenseServer = new LicenseServer();
        }
        public override void LoadSettings()
        {
            #region Lizenzierung
            authorized = licenseServer.check();
            #endregion
            Settings.Add(new Setting("General Settings"));
            Settings.Add(new Setting("Use Settings on Hero Rotation inGame!"));
            Settings.Add(new Setting("Game Client Language", new List<string>()
            {
                "English",
                "Deutsch",
                "Español",
                "Français",
                "Italiano",
                "Português Brasileiro",
                "Русский",
                "한국어",
                "简体中文"
            }, "English"));
            Settings.Add(new Setting("Race:", new List<string>()
            {
                "nightelf",
                "orc",
                "nightborne",
                "troll",
                "bloodelf",
                "lightforgeddraenei",
                "magharorc",
                "darkirondwarf",
                "vulpera",
            }, "nightelf"));

            Settings.Add(new Setting("Delay Settings"));
            Settings.Add(new Setting("Latency: ", 0, 1000, 70));
            Settings.Add(new Setting("Spell Delay: ", 50, 1000, 250));
        }
        public override void Initialize()
        {
            if (authorized == false)
            {
                Aimsharp.PrintMessage(licenseServer.RotationName, Color.Purple);
                Aimsharp.PrintMessage("no valid license found", Color.Red);
            }
            if (authorized == true)
            {
                Language = GetDropDown("Game Client Language");
                Aimsharp.PrintMessage(licenseServer.RotationName, Color.Purple);
                Aimsharp.PrintMessage("https://discord.gg/p4h2h8GVtc", Color.Purple);
                int Latency = GetSlider("Latency: ");
                int QuickDelay = GetSlider("Spell Delay: ");
                int SlowDelay = GetSlider("Spell Delay: ");
                Aimsharp.Latency = Latency;
                Aimsharp.QuickDelay = QuickDelay;
                Aimsharp.SlowDelay = SlowDelay;
                List<string> Racials = new List<string>
                {
                    BloodFury_SpellName(),Berserking_SpellName(),Fireblood_SpellName(),AncestralCall_SpellName(),BagofTricks_SpellName(), ArcaneTorrent_SpellName()
                };
                List<string> CovenantAbilities = new List<string>
                {
                };
                List<string> SpellsList = new List<string>
                {
                    Avatar_SpellName(),
                    DefensiveStance_SpellName(),
                    BattleShout_SpellName(),
                    BerserkerRage_SpellName(),
                    BerserkerShout_SpellName(),
                    BitterImmunity_SpellName(),
                    ChampionsSpear_SpellName(),
                    Charge_SpellName(),
                    Demolish_SpellName(),
                    DemoralizingShout_SpellName(),
                    Devastate_SpellName(),
                    Execute_SpellName(),
                    Fireblood_SpellName(),
                    HeroicThrow_SpellName(),
                    ImpendingVictory_SpellName(),
                    Intervene_SpellName(),
                    IntimidatingShout_SpellName(),
                    LightsJudgment_SpellName(),
                    PiercingHowl_SpellName(),
                    Pummel_SpellName(),
                    RallyingCry_SpellName(),
                    Ravager_SpellName(),
                    Rend_SpellName(),
                    Revenge_SpellName(),
                    ShatteringThrow_SpellName(),
                    Shockwave_SpellName(),
                    Slam_SpellName(),
                    SpellBlock_SpellName(),
                    SpellReflection_SpellName(),
                    StormBolt_SpellName(),
                    ThunderBlast_SpellName(),
                    ThunderClap_SpellName(),
                    ThunderousRoar_SpellName(),
                    TitanicThrow_SpellName(),
                    VictoryRush_SpellName(),
                    WarStomp_SpellName(),
                    Whirlwind_SpellName(),
                    WreckingThrow_SpellName(),
                    Onslaught_SpellName(),
                    OdynsFury_SpellName(),
                    Rampage_SpellName(),
                    RagingBlow_SpellName(),
                    Recklessness_SpellName(),
                    BerserkerStance_SpellName(),
                    Bloodthirst_SpellName(),
                    Bladestorm_SpellName(),
                    Bloodbath_SpellName(),
                    CrushingBlow_SpellName(),
                    EnragedRegeneration_SpellName(),

                };
                List<string> MacroCommands = new List<string>
                {
                };

                foreach (string Spell in SpellsList)
                {
                    Spellbook.Add(Spell);
                }

                foreach (string Spell in CovenantAbilities)
                {
                    Spellbook.Add(Spell);
                }

                #region Racial Spells

                if (GetDropDown("Race:") == "lightforgeddraenei")
                {
                    Spellbook.Add(LightsJudgment_SpellName()); //255647
                }

                if (GetDropDown("Race:") == "darkirondwarf")
                {
                    Spellbook.Add(Fireblood_SpellName()); //265221
                }

                if (GetDropDown("Race:") == "troll")
                {
                    Spellbook.Add(Berserking_SpellName()); //26297
                }

                if (GetDropDown("Race:") == "nightborne")
                {
                    Spellbook.Add(ArcanePulse_SpellName()); //260364
                }

                if (GetDropDown("Race:") == "magharorc")
                {
                    Spellbook.Add(AncestralCall_SpellName()); //274738
                }

                if (GetDropDown("Race:") == "vulpera")
                {
                    Spellbook.Add(BagofTricks_SpellName()); //312411
                }

                if (GetDropDown("Race:") == "orc")
                {
                    Spellbook.Add(BloodFury_SpellName()); //20572, 33702, 33697
                }

                if (GetDropDown("Race:") == "bloodelf")
                {
                    Spellbook.Add(ArcaneTorrent_SpellName()); //28730, 25046, 50613, 69179, 80483, 129597
                }

                if (GetDropDown("Race:") == "nightelf")
                {
                    Spellbook.Add(Shadowmeld_SpellName()); //58984
                }
                #endregion
                int SpellsAdd = 0;
                foreach (string Spell in Spellbook)
                {
                    SpellsAdd++;
                }
                Aimsharp.PrintMessage("Ascended Rotation Loaded " + SpellsAdd + " Spells", Color.Purple);
                #region Item & Macros
                Items.Add(Healthstone_SpellName());
                Items.Add(TemperedPotion_SpellName());
                Items.Add(AlgariHealingPotion_SpellName());
                Items.Add(DemonicHealthstone_SpellName());
                Macros.Add("DemonicHealthstone", "/use " + DemonicHealthstone_SpellName());
                Macros.Add("TopTrinket", "/use 13");
                Macros.Add("BottomTrinket", "/use 14");
                Macros.Add("WeaponUse", "/use 16");
                Macros.Add("Healthstone", "/use " + Healthstone_SpellName());
                Macros.Add("DPSPot", "/use " + TemperedPotion_SpellName());
                Macros.Add("HealPot", "/use " + AlgariHealingPotion_SpellName());
                Macros.Add("TabTarget", "/targetenemy");

                Macros.Add("ExecuteMouseover", "/cast [@mouseover]" + Execute_SpellName());
                Macros.Add("HeroicThrowMouseover", "/cast [@mouseover]" + HeroicThrow_SpellName());
                Macros.Add("InterveneMouseover", "/cast [@mouseover]" + Intervene_SpellName());
                Macros.Add("PummelMouseover", "/cast [@mouseover]" + Pummel_SpellName());
                Macros.Add("RendMouseover", "/cast [@mouseover]" + Rend_SpellName());
                Macros.Add("StormboltMouseover", "/cast [@mouseover]" + StormBolt_SpellName());

                Macros.Add("ChampionsSpearPlayer", "/cast [@player]" + ChampionsSpear_SpellName());
                Macros.Add("RavagePlayer", "/cast [@player]" + Ravager_SpellName());
                Macros.Add("AssistFocus", "/assist focus");




                foreach (string MacroCommand in MacroCommands)
                {
                    CustomCommands.Add(MacroCommand);
                }
                #endregion
                #region CustomFunctionsADD
                CustomFunctions.Add("SpellID", "local HR = HeroRotation() \n return HR.Ascended.SpellID");
                CustomFunctions.Add("TargetOffSet", "local HR = HeroRotation() \n return HR.Ascended.TargetOffset");
                CustomFunctions.Add("SpecID", "local HR = HeroRotation() \n return HR.Ascended.SpecID");
                CustomFunctions.Add("AutoTab", "local HR = HeroRotation() \n return HR.Ascended.AutoTab");
                CustomFunctions.Add("BotOn", "local HR = HeroRotation()\nlocal HL = HeroLib\nlocal Unit = HL.Unit\nlocal Target = Unit.Target\nlocal on = 0\nlocal Everyone = HR.Commons().Everyone\nif HeroRotationCharDB.Toggles[3] then\non = 1\nend\nreturn on");
                #endregion
            }



        }
        public void AscendedCast(string Spell)
        {
            Aimsharp.Cast(Spell);
            Logger("Cast: " + Spell);
        }
        private bool HandleRacialSpells(int spellID)
        {
            switch (spellID)
            {
                case 20572:
                    AscendedCast(BloodFury_SpellName());
                    return true;
                case 25046:
                    AscendedCast(ArcaneTorrent_SpellName());
                    return true;
                case 26297:
                    AscendedCast(Berserking_SpellName());
                    return true;
                case 265221:
                    AscendedCast(Fireblood_SpellName());
                    return true;
                case 274738:
                    AscendedCast(AncestralCall_SpellName());
                    return true;
                case 155145:
                    AscendedCast(ArcaneTorrent_SpellName());
                    return true;
                case 260364:
                    AscendedCast(ArcanePulse_SpellName());
                    return true;
                case 255647:
                    AscendedCast(LightsJudgment_SpellName());
                    return true;
                case 312411:
                    AscendedCast(BagofTricks_SpellName());
                    return true;
                default:
                    return false;
            }
        }
        private bool HandleStandardSpells(int spellID)
        {
            switch (spellID)
            {
                case 3:
                    AscendedCast("DPSPot");
                    return true;
                case 4:
                    AscendedCast("Healthstone");
                    return true;
                case 5:
                    AscendedCast("HealPot");
                    return true;
                case 6:
                    AscendedCast("StopCasting");
                    return true;
                case 7:
                    AscendedCast("AssistFocus");
                    return true;
                case 10:
                    AscendedCast("DemonicHealthstone");
                    return true;
                case 13:
                    AscendedCast("TopTrinket");
                    return true;
                case 14:
                    AscendedCast("BottomTrinket");
                    return true;
                default:
                    return false;
            }
        }
        private bool HandleMouseOverSpells(int spellID)
        {
            switch (spellID)
            {
                case 163201:
                    AscendedCast("ExecuteMouseover");
                    return true;
                case 57755:
                    AscendedCast("HeroicThrowMouseover");
                    return true;
                case 6552:
                    AscendedCast("PummelMouseover");
                    return true;
                case 394062:
                    AscendedCast("RendMouseover");
                    return true;
                case 107570:
                    AscendedCast("StormboltMouseover");
                    return true;
                default:
                    return false;
            }
        }
        private bool HandleFocusSpells(int spellID)
        {
            switch (spellID)
            {
                default:
                    return false;
            }
        }
        private bool HandlePlayerSpells(int spellID)
        {
            switch (spellID)
            {
                case 228920:
                    AscendedCast("RavagePlayer");
                    return true;
                case 376079:
                    AscendedCast("ChampionsSpearPlayer");
                    return true;
                default:
                    return false;
            }
        }
        public bool Spells()
        {
            int SpecID = Aimsharp.CustomFunction("SpecID");
            if (authorized && SpecID == licenseServer.specID)
            {
                #region Important Vars to RUN with Aimsharp
                int SpellID = Aimsharp.CustomFunction("SpellID");
                int TargetOffSet = Aimsharp.CustomFunction("TargetOffSet");
                int AutoTab = Aimsharp.CustomFunction("AutoTab");
                bool BotOn = Aimsharp.CustomFunction("BotOn") == 1;
                #endregion

                int nextSpellID = 0, nextSpecID = 0, nextTargetOffSet = 0, nextAutoTab = 0;
                if (SpellID != nextSpellID || TargetOffSet != nextTargetOffSet || AutoTab != nextAutoTab)
                {
                    Logger("[AscendedLog] [SpellID: " + SpellID + ", TargetOffSet: " + TargetOffSet + ",AutoTab: " + AutoTab + "]");
                    Aimsharp.PrintMessage("[AscendedLog] [" + SpellID + "," + TargetOffSet + "," + AutoTab + "]", Color.Blue);
                    nextSpecID = SpecID;
                    nextAutoTab = AutoTab;
                    nextSpellID = SpellID;
                    nextTargetOffSet = TargetOffSet;
                }

                if (!BotOn)
                {
                    return false;
                }

                if (SpellID == 7)
                {
                    AscendedCast("AssistFocus");
                }

                if (AutoTab == 1)
                {
                    AscendedCast("TabTarget");
                }

                //MouseOver Targets
                if (TargetOffSet == 1)
                {
                    if (HandleMouseOverSpells(SpellID))
                    {
                        return true;
                    }
                }
                //Player Spells
                if (TargetOffSet == 3)
                {
                    if (HandlePlayerSpells(SpellID))
                    {
                        return true;
                    }
                }
                //Focus Spells
                if (TargetOffSet != 0)
                {
                    if (HandleFocusSpells(SpellID))
                    {
                        return true;
                    }
                }

                if (TargetOffSet == 0)
                {
                    if (HandleRacialSpells(SpellID) || HandleStandardSpells(SpellID))
                    {
                        return true;
                    }
                    switch (SpellID)
                    {
                        case 386208:
                            AscendedCast(DefensiveStance_SpellName());
                            return true;
                        case 315720:
                            AscendedCast(Onslaught_SpellName());
                            return true;
                        case 385059:
                            AscendedCast(OdynsFury_SpellName());
                            return true;
                        case 184367:
                            AscendedCast(Rampage_SpellName());
                            return true;
                        case 85288:
                            AscendedCast(RagingBlow_SpellName());
                            return true;
                        case 1719:
                            AscendedCast(Recklessness_SpellName());
                            return true;
                        case 6552:
                            AscendedCast(Pummel_SpellName());
                            return true;
                        case 12323:
                            AscendedCast(PiercingHowl_SpellName());
                            return true;
                        case 5246:
                            AscendedCast(IntimidatingShout_SpellName());
                            return true;
                        case 97462:
                            AscendedCast(RallyingCry_SpellName());
                            return true;
                        case 435222:
                            AscendedCast(ThunderBlast_SpellName());
                            return true;
                        case 46968:
                            AscendedCast(Shockwave_SpellName());
                            return true;
                        case 64382:
                            AscendedCast(ShatteringThrow_SpellName());
                            return true;
                        case 384318:
                            AscendedCast(ThunderousRoar_SpellName());
                            return true;
                        case 384090:
                            AscendedCast(TitanicThrow_SpellName());
                            return true;
                        case 23920:
                            AscendedCast(SpellReflection_SpellName());
                            return true;
                        case 1464:
                            AscendedCast(Slam_SpellName());
                            return true;
                        case 107570:
                            AscendedCast(StormBolt_SpellName());
                            return true;
                        case 34428:
                            AscendedCast(VictoryRush_SpellName());
                            return true;
                        case 20549:
                            AscendedCast(WarStomp_SpellName());
                            return true;
                        case 384110:
                            AscendedCast(WreckingThrow_SpellName());
                            return true;
                        case 6343:
                            AscendedCast(ThunderClap_SpellName());
                            return true;
                        case 190411:
                            AscendedCast(Whirlwind_SpellName());
                            return true;
                        case 50613:
                            AscendedCast(ArcaneTorrent_SpellName());
                            return true;
                        case 384100:
                            AscendedCast(BerserkerShout_SpellName());
                            return true;
                        case 386196:
                            AscendedCast(BerserkerStance_SpellName());
                            return true;
                        case 107574:
                            AscendedCast(Avatar_SpellName());
                            return true;
                        case 23881:
                            AscendedCast(Bloodthirst_SpellName());
                            return true;
                        case 383762:
                            AscendedCast(BitterImmunity_SpellName());
                            return true;
                        case 18499:
                            AscendedCast(BerserkerRage_SpellName());
                            return true;
                        case 227847:
                            AscendedCast(Bladestorm_SpellName());
                            return true;
                        case 335096:
                            AscendedCast(Bloodbath_SpellName());
                            return true;
                        case 335097:
                            AscendedCast(CrushingBlow_SpellName());
                            return true;
                        case 184364:
                            AscendedCast(EnragedRegeneration_SpellName());
                            return true;
                        case 202168:
                            AscendedCast(ImpendingVictory_SpellName());
                            return true;
                        case 100:
                            AscendedCast(Charge_SpellName());
                            return true;
                        case 57755:
                            AscendedCast(HeroicThrow_SpellName());
                            return true;
                        case 3411:
                            AscendedCast(Intervene_SpellName());
                            return true;
                        case 5308:
                            AscendedCast(Execute_SpellName());
                            return true;
                        case 401150:
                            AscendedCast(Avatar_SpellName());
                            return true;
                        case 6673:
                            AscendedCast(BattleShout_SpellName());
                            return true;
                            AscendedCast(BerserkerShout_SpellName());
                            return true;
                        case 376079:
                            AscendedCast("ChampionsSpearPlayer");
                            return true;
                        case 1160:
                            AscendedCast(DemoralizingShout_SpellName());
                            return true;
                        case 20243:
                            AscendedCast(Devastate_SpellName());
                            return true;
                        case 163201:
                            AscendedCast(Execute_SpellName());
                            return true;
                        case 190456:
                            AscendedCast(IgnorePain_SpellName());
                            return true;
                        case 394062:
                            AscendedCast(Rend_SpellName());
                            return true;
                        case 6572:
                            AscendedCast(Revenge_SpellName());
                            return true;
                        case 2565:
                            AscendedCast(ShieldBlock_SpellName());
                            return true;
                        case 385952:
                            AscendedCast(ShieldCharge_SpellName());
                            return true;
                        case 23922:
                            AscendedCast(ShieldSlam_SpellName());
                            return true;
                        case 871:
                            AscendedCast(ShieldWall_SpellName());
                            return true;
                        case 392966:
                            AscendedCast(SpellBlock_SpellName());
                            return true;
                    }
                }
                else
                {
                    if (authorized == false)
                    {
                        Aimsharp.PrintMessage("No License Found", Color.Red);
                    }

                    if (SpecID != licenseServer.specID)
                    {
                        Aimsharp.PrintMessage("Wrong Class or Wrong Spec", Color.Red);
                    }
                }
            }
            return false;
        }
        public override bool CombatTick()
        {
            return Spells();
        }
        public override bool OutOfCombatTick()
        {
            return Spells();
        }

        #region übersetzungen
        ///<summary>ASCROT Spell=224464</summary>
        private static string DemonicHealthstone_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Dämonischer Gesundheitsstein";
                case "Español": return "Piedra de salud demoníaca";
                case "Français": return "Pierre de soins démoniaque";
                case "Italiano": return "Pietra della Salute Demoniaca";
                case "Português Brasileiro": return "Pedra de Vida Demoníaca";
                case "Русский": return "Демонический камень здоровья";
                case "한국어": return "악마의 생명석";
                case "简体中文": return "恶魔治疗石";
                default: return "Demonic Healthstone";
            }
        }
        ///<summary>ASCROT Spell=58984</summary>
        private static string Shadowmeld_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schattenmimik";
                case "Español": return "Fusión de las sombras";
                case "Français": return "Camouflage dans l'ombre";
                case "Italiano": return "Fondersi nelle Ombre";
                case "Português Brasileiro": return "Fusão Sombria";
                case "Русский": return "Слиться с тенью";
                case "한국어": return "그림자 숨기";
                case "简体中文": return "影遁";
                default: return "Shadowmeld";
            }
        }
        private static string Healthstone_SpellName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Gesundheitsstein";
                case "Español":
                    return "Piedra de salud";
                case "EspañolLA":
                    return "Piedra de salud";
                case "Français":
                    return "Pierre de soins";
                case "Italiano":
                    return "Pietra della Salute";
                case "Português Brasileiro":
                    return "Pedra de Vida";
                case "Русский":
                    return "Камень здоровья";
                case "한국어":
                    return "생명석";
                case "简体中文":
                    return "治疗石";
                default:
                    return "Healthstone";
            }
        }
        private static string BloodFury_SpellName()
        {
            // spell=33697
            switch (Language)
            {
                case "English":
                    return "Blood Fury";
                case "Deutsch":
                    return "Kochendes Blut";
                case "Español":
                    return "Furia sangrienta";
                case "Français":
                    return "Fureur sanguinaire";
                case "Italiano":
                    return "Furia Sanguinaria";
                case "Português Brasileiro":
                    return "Fúria Sangrenta";
                case "Русский":
                    return "Кровавое неистовство";
                case "한국어":
                    return "피의 격노";
                case "简体中文":
                    return "血性狂怒";
                default:
                    return "Blood Fury";
            }
        }

        private static string Berserking_SpellName()
        {
            // spell=26297
            switch (Language)
            {
                case "English":
                    return "Berserking";
                case "Deutsch":
                    return "Berserker";
                case "Español":
                    return "Rabiar";
                case "Français":
                    return "Berserker";
                case "Italiano":
                    return "Berserker";
                case "Português Brasileiro":
                    return "Berserk";
                case "Русский":
                    return "Берсерк";
                case "한국어":
                    return "광폭화";
                case "简体中文":
                    return "狂暴";
                default:
                    return "Berserking";
            }
        }

        private static string Fireblood_SpellName()
        {
            // spell=265221
            switch (Language)
            {
                case "English":
                    return "Fireblood";
                case "Deutsch":
                    return "Feuerblut";
                case "Español":
                    return "Sangrardiente";
                case "Français":
                    return "Sang de feu";
                case "Italiano":
                    return "Sangue Infuocato";
                case "Português Brasileiro":
                    return "Sangue de Fogo";
                case "Русский":
                    return "Огненная кровь";
                case "한국어":
                    return "불꽃피";
                case "简体中文":
                    return "烈焰之血";
                default:
                    return "Fireblood";
            }
        }

        private static string AncestralCall_SpellName()
        {
            // spell=274738
            switch (Language)
            {
                case "English":
                    return "Ancestral Call";
                case "Deutsch":
                    return "Ruf der Ahnen";
                case "Español":
                    return "Llamada ancestral";
                case "Français":
                    return "Appel ancestral";
                case "Italiano":
                    return "Richiamo Ancestrale";
                case "Português Brasileiro":
                    return "Chamado Ancestral";
                case "Русский":
                    return "Призыв предков";
                case "한국어":
                    return "고대의 부름";
                case "简体中文":
                    return "先祖召唤";
                default:
                    return "Ancestral Call";
            }
        }

        private static string LightsJudgment_SpellName()
        {
            // spell=255647
            switch (Language)
            {
                case "English":
                    return "Light's Judgment";
                case "Deutsch":
                    return "Urteil des Lichts";
                case "Español":
                    return "Sentencia de la Luz";
                case "Français":
                    return "Jugement de la Lumière";
                case "Italiano":
                    return "Giudizio della Luce";
                case "Português Brasileiro":
                    return "Julgamento da Luz";
                case "Русский":
                    return "Правосудие Света";
                case "한국어":
                    return "빛의 심판";
                case "简体中文":
                    return "圣光裁决者";
                default:
                    return "Light's Judgment";
            }
        }

        private static string ArcaneTorrent_SpellName()
        {
            // spell=28730
            switch (Language)
            {
                case "English":
                    return "Arcane Torrent";
                case "Deutsch":
                    return "Arkaner Strom";
                case "Español":
                    return "Torrente Arcano";
                case "Français":
                    return "Torrent arcanique";
                case "Italiano":
                    return "Torrente Arcano";
                case "Português Brasileiro":
                    return "Torrente Arcana";
                case "Русский":
                    return "Волшебный поток";
                case "한국어":
                    return "비전 격류";
                case "简体中文":
                    return "奥术洪流";
                default:
                    return "Arcane Torrent";
            }
        }
        private static string ArcanePulse_SpellName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Arkaner Puls";
                case "Español":
                    return "Pulso Arcano";
                case "Français":
                    return "Impulsion arcanique";
                case "Italiano":
                    return "Impulso Arcano";
                case "Português Brasileiro":
                    return "Pulso Arcano";
                case "Русский":
                    return "Чародейский импульс";
                case "한국어":
                    return "비전 파동";
                case "简体中文":
                    return "奥术脉冲";
                default:
                    return "Arcane Pulse";
            }
        }
        private static string BagofTricks_SpellName()
        {
            // spell=312411
            switch (Language)
            {
                case "English":
                    return "Bag of Tricks";
                case "Deutsch":
                    return "Trickkiste";
                case "Español":
                    return "Bolsa de trucos";
                case "Français":
                    return "Sac à malice";
                case "Italiano":
                    return "Borsa di Trucchi";
                case "Português Brasileiro":
                    return "Bolsa de Truques";
                case "Русский":
                    return "Набор хитростей";
                case "한국어":
                    return "비장의 묘수";
                case "简体中文":
                    return "袋里乾坤";
                default:
                    return "Bag of Tricks";
            }
        }

        private static string TrainingDummy_NPCName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Trainingsattrappe";
                case "Español":
                    return "Muñeco de entrenamiento";
                case "Français":
                    return "Mannequin d’entraînement";
                case "Italiano":
                    return "Manichino d'Allenamento";
                case "Português Brasileiro":
                    return "Boneco de Treinamento";
                case "Русский":
                    return "Тренировочный манекен";
                case "한국어":
                    return "훈련용 허수아비";
                case "简体中文":
                    return "训练假人";
                default:
                    return "Training Dummy";
            }
        }
        private static string RaidersTrainingDummy_NPCName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Trainingsattrappe des Schlachtzuges";
                case "Español":
                    return "Muñeco de entrenamiento de banda";
                case "Français":
                    return "Mannequin d’entraînement d'écumeur de raids";
                case "Italiano":
                    return "Manichino d'Allenamento da Incursione";
                case "Português Brasileiro":
                    return "Boneco de Treinamento de Raide";
                case "Русский":
                    return "Тренировочный манекен рейдера";
                case "한국어":
                    return "공격대원의 훈련용 허수아비";
                case "简体中文":
                    return "团队副本训练假人";
                default:
                    return "Raider's Training Dummy";
            }
        }
        private static string SwarmTrainingDummy_NPCName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Trainingsattrappe des Schwarms";
                case "Español":
                    return "Muñeco de entrenamiento de enjambre";
                case "Français":
                    return "Mannequin d'entraînement en groupe";
                case "Italiano":
                    return "Manichino d'Allenamento per Aree d'Effetto";
                case "Português Brasileiro":
                    return "Boneco de Treinamento do Enxame";
                case "Русский":
                    return "Тренировочный манекен (стая)";
                case "한국어":
                    return "광역 훈련용 허수아비";
                case "简体中文":
                    return "群攻训练假人";
                default:
                    return "Swarm Training Dummy";
            }
        }
        private static string CleaveTrainingDummy_NPCName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Trainingsattrappe des Spaltens";
                case "Español":
                    return "Muñeco de entrenamiento para rajar";
                case "Français":
                    return "Mannequin d’entraînement aux dégâts de zone";
                case "Italiano":
                    return "Manichino d'Allenamento per Bersagli Secondari";
                case "Português Brasileiro":
                    return "Boneco de Treinamento de Cutilada";
                case "Русский":
                    return "Тренировочный манекен для рассекающих ударов";
                case "한국어":
                    return "절단 훈련용 허수아비";
                case "简体中文":
                    return "顺劈训练假人";
                default:
                    return "Cleave Training Dummy";
            }
        }
        private static string GlaccialSpike_NPCName()
        {
            switch (Language)
            {
                case "Deutsch":
                    return "Gletscherstachel";
                case "Español":
                    return "Pica glacial";
                case "Français":
                    return "Pointe glaciaire";
                case "Italiano":
                    return "Aculeo Glaciale";
                case "Português Brasileiro":
                    return "Punhal Glacial";
                case "Русский":
                    return "Ледовый шип";
                case "한국어":
                    return "혹한의 쐐기";
                case "简体中文":
                    return "冰川尖刺";
                default:
                    return "Glacial Spike";
            }
        }
        private static string TemperedPotion_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Gemäßigter Trank";
                case "Español": return "Poción templada";
                case "Français": return "Potion tempérée";
                case "Italiano": return "Pozione Temprata";
                case "Português Brasileiro": return "Poção Temperada";
                case "Русский": return "Охлажденное зелье";
                case "한국어": return "절제된 물약";
                case "简体中文": return "淬火药水";
                default: return "Tempered Potion";
            }
        }
        private static string AlgariHealingPotion_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Algarischer Heiltrank";
                case "Español": return "Poción de sanación algariana";
                case "Français": return "Potion de soins algarie";
                case "Italiano": return "Pozione di Cura Algari";
                case "Português Brasileiro": return "Poção de Cura Algari";
                case "Русский": return "Алгарийское лечебное зелье";
                case "한국어": return "알가르 치유 물약";
                case "简体中文": return "阿加治疗药水";
                default: return "Algari Healing Potion";
            }
        }

        #endregion
        #region classSpells

        ///<summary>ASCROT Spell=401150</summary>
        private static string Avatar_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Avatar";
                case "Español": return "Avatar";
                case "Français": return "Avatar";
                case "Italiano": return "Avatar";
                case "Português Brasileiro": return "Avatar";
                case "Русский": return "Аватара";
                case "한국어": return "투신";
                case "简体中文": return "天神下凡";
                default: return "Avatar";
            }
        }

        ///<summary>ASCROT Spell=6673</summary>
        private static string BattleShout_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schlachtruf";
                case "Español": return "Grito de batalla";
                case "Français": return "Cri de guerre";
                case "Italiano": return "Urlo di Battaglia";
                case "Português Brasileiro": return "Brado de Batalha";
                case "Русский": return "Боевой крик";
                case "한국어": return "전투의 외침";
                case "简体中文": return "战斗怒吼";
                default: return "Battle Shout";
            }
        }

        ///<summary>ASCROT Spell=386164</summary>
        private static string BattleStance_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Kampfhaltung";
                case "Español": return "Actitud de batalla";
                case "Français": return "Posture de combat";
                case "Italiano": return "Postura da Battaglia";
                case "Português Brasileiro": return "Postura de Batalha";
                case "Русский": return "Боевая стойка";
                case "한국어": return "전투 태세";
                case "简体中文": return "战斗姿态";
                default: return "Battle Stance";
            }
        }

        ///<summary>ASCROT Spell=18499</summary>
        private static string BerserkerRage_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Berserkerwut";
                case "Español": return "Ira rabiosa";
                case "Français": return "Rage de berserker";
                case "Italiano": return "Furia del Berserker";
                case "Português Brasileiro": return "Raiva Incontrolada";
                case "Русский": return "Ярость берсерка";
                case "한국어": return "광전사의 격노";
                case "简体中文": return "狂暴之怒";
                default: return "Berserker Rage";
            }
        }

        ///<summary>ASCROT Spell=384100</summary>
        private static string BerserkerShout_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Berserkerschrei";
                case "Español": return "Grito rabioso";
                case "Français": return "Cri de berserker";
                case "Italiano": return "Urlo del Berserker";
                case "Português Brasileiro": return "Brado do Berserker";
                case "Русский": return "Крик берсерка";
                case "한국어": return "광전사의 외침";
                case "简体中文": return "狂暴之吼";
                default: return "Berserker Shout";
            }
        }

        ///<summary>ASCROT Spell=383762</summary>
        private static string BitterImmunity_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Bittere Immunität";
                case "Español": return "Inmunidad amarga";
                case "Français": return "Immunité amère";
                case "Italiano": return "Immunità Amara";
                case "Português Brasileiro": return "Imunidade Mordaz";
                case "Русский": return "Горестная невосприимчивость";
                case "한국어": return "사기적인 면역";
                case "简体中文": return "苦痛免疫";
                default: return "Bitter Immunity";
            }
        }


        ///<summary>ASCROT Spell=376079</summary>
        private static string ChampionsSpear_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Speer des Champions";
                case "Español": return "Lanza del campeón";
                case "Français": return "Lance du champion";
                case "Italiano": return "Lancia del Campione";
                case "Português Brasileiro": return "Lança do Campeão";
                case "Русский": return "Копье защитника";
                case "한국어": return "용사의 창";
                case "简体中文": return "勇士之矛";
                default: return "Champion's Spear";
            }
        }

        ///<summary>ASCROT Spell=100</summary>
        private static string Charge_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Sturmangriff";
                case "Español": return "Cargar";
                case "Français": return "Charge";
                case "Italiano": return "Carica";
                case "Português Brasileiro": return "Investida";
                case "Русский": return "Рывок";
                case "한국어": return "돌진";
                case "简体中文": return "冲锋";
                default: return "Charge";
            }
        }

        ///<summary>ASCROT Spell=386208</summary>
        private static string DefensiveStance_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Verteidigungshaltung";
                case "Español": return "Actitud defensiva";
                case "Français": return "Posture défensive";
                case "Italiano": return "Postura da Difesa";
                case "Português Brasileiro": return "Postura de Defesa";
                case "Русский": return "Оборонительная стойка";
                case "한국어": return "방어 태세";
                case "简体中文": return "防御姿态";
                default: return "Defensive Stance";
            }
        }

        ///<summary>ASCROT Spell=436358</summary>
        private static string Demolish_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Demolieren";
                case "Español": return "Demoler";
                case "Français": return "Démolissage";
                case "Italiano": return "Demolizione";
                case "Português Brasileiro": return "Demolir";
                case "Русский": return "Разрушение";
                case "한국어": return "쇄파";
                case "简体中文": return "崩摧";
                default: return "Demolish";
            }
        }

        ///<summary>ASCROT Spell=1160</summary>
        private static string DemoralizingShout_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Demoralisierender Ruf";
                case "Español": return "Grito desmoralizador";
                case "Français": return "Cri démoralisant";
                case "Italiano": return "Urlo Demoralizzante";
                case "Português Brasileiro": return "Brado Desmoralizador";
                case "Русский": return "Деморализующий крик";
                case "한국어": return "사기의 외침";
                case "简体中文": return "挫志怒吼";
                default: return "Demoralizing Shout";
            }
        }

        ///<summary>ASCROT Spell=20243</summary>
        private static string Devastate_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Verwüsten";
                case "Español": return "Devastar";
                case "Français": return "Dévaster";
                case "Italiano": return "Sfondamento";
                case "Português Brasileiro": return "Devastar";
                case "Русский": return "Сокрушение";
                case "한국어": return "압도";
                case "简体中文": return "毁灭打击";
                default: return "Devastate";
            }
        }

        ///<summary>ASCROT Spell=163201</summary>
        private static string Execute_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Hinrichten";
                case "Español": return "Ejecutar";
                case "Français": return "Exécution";
                case "Italiano": return "Esecuzione";
                case "Português Brasileiro": return "Executar";
                case "Русский": return "Казнь";
                case "한국어": return "마무리 일격";
                case "简体中文": return "斩杀";
                default: return "Execute";
            }
        }


        ///<summary>ASCROT Spell=57755</summary>
        private static string HeroicThrow_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Heldenhafter Wurf";
                case "Español": return "Lanzamiento heroico";
                case "Français": return "Lancer héroïque";
                case "Italiano": return "Lancio Eroico";
                case "Português Brasileiro": return "Arremesso Heroico";
                case "Русский": return "Героический бросок";
                case "한국어": return "영웅의 투척";
                case "简体中文": return "英勇投掷";
                default: return "Heroic Throw";
            }
        }

        ///<summary>ASCROT Spell=190456</summary>
        private static string IgnorePain_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Zähne zusammenbeißen";
                case "Español": return "Ignorar dolor";
                case "Français": return "Dur au mal";
                case "Italiano": return "Insensibilità";
                case "Português Brasileiro": return "Ignorar Dor";
                case "Русский": return "Стойкость к боли";
                case "한국어": return "고통 감내";
                case "简体中文": return "无视苦痛";
                default: return "Ignore Pain";
            }
        }

        ///<summary>ASCROT Spell=202168</summary>
        private static string ImpendingVictory_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Bevorstehender Sieg";
                case "Español": return "Victoria inminente";
                case "Français": return "Victoire imminente";
                case "Italiano": return "Vittoria Imminente";
                case "Português Brasileiro": return "Vitória Iminente";
                case "Русский": return "Верная победа";
                case "한국어": return "예견된 승리";
                case "简体中文": return "胜利在望";
                default: return "Impending Victory";
            }
        }


        ///<summary>ASCROT Spell=3411</summary>
        private static string Intervene_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Einschreiten";
                case "Español": return "Intervenir";
                case "Français": return "Intervention";
                case "Italiano": return "Intervento";
                case "Português Brasileiro": return "Comprar Briga";
                case "Русский": return "Вмешательство";
                case "한국어": return "가로막기";
                case "简体中文": return "援护";
                default: return "Intervene";
            }
        }

        ///<summary>ASCROT Spell=5246</summary>
        private static string IntimidatingShout_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Drohruf";
                case "Español": return "Grito intimidador";
                case "Français": return "Cri d’intimidation";
                case "Italiano": return "Urlo Intimidatorio";
                case "Português Brasileiro": return "Brado Intimidador";
                case "Русский": return "Устрашающий крик";
                case "한국어": return "위협의 외침";
                case "简体中文": return "破胆怒吼";
                default: return "Intimidating Shout";
            }
        }

        ///<summary>ASCROT Spell=12975</summary>
        private static string LastStand_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Letztes Gefecht";
                case "Español": return "Última carga";
                case "Français": return "Dernier rempart";
                case "Italiano": return "Ultima Difesa";
                case "Português Brasileiro": return "Último Recurso";
                case "Русский": return "Ни шагу назад";
                case "한국어": return "최후의 저항";
                case "简体中文": return "破釜沉舟";
                default: return "Last Stand";
            }
        }

        ///<summary>ASCROT Spell=12323</summary>
        private static string PiercingHowl_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Durchdringendes Heulen";
                case "Español": return "Aullido perforador";
                case "Français": return "Hurlement perçant";
                case "Italiano": return "Urlo Penetrante";
                case "Português Brasileiro": return "Uivo Perfurante";
                case "Русский": return "Пронзительный вой";
                case "한국어": return "날카로운 고함";
                case "简体中文": return "刺耳怒吼";
                default: return "Piercing Howl";
            }
        }


        ///<summary>ASCROT Spell=6552</summary>
        private static string Pummel_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Zuschlagen";
                case "Español": return "Zurrar";
                case "Français": return "Volée de coups";
                case "Italiano": return "Pugno Diversivo";
                case "Português Brasileiro": return "Murro";
                case "Русский": return "Зуботычина";
                case "한국어": return "들이치기";
                case "简体中文": return "拳击";
                default: return "Pummel";
            }
        }

        ///<summary>ASCROT Spell=97462</summary>
        private static string RallyingCry_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Anspornender Schrei";
                case "Español": return "Berrido de convocación";
                case "Français": return "Cri de ralliement";
                case "Italiano": return "Chiamata a Raccolta";
                case "Português Brasileiro": return "Brado de Convocação";
                case "Русский": return "Ободряющий клич";
                case "한국어": return "재집결의 함성";
                case "简体中文": return "集结呐喊";
                default: return "Rallying Cry";
            }
        }

        ///<summary>ASCROT Spell=228920</summary>
        private static string Ravager_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Verheerer";
                case "Español": return "Devastador";
                case "Français": return "Ravageur";
                case "Italiano": return "Impeto Devastatore";
                case "Português Brasileiro": return "Assolador";
                case "Русский": return "Опустошитель";
                case "한국어": return "쇠날발톱";
                case "简体中文": return "破坏者";
                default: return "Ravager";
            }
        }


        ///<summary>ASCROT Spell=394062</summary>
        private static string Rend_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Verwunden";
                case "Español": return "Desgarrar";
                case "Français": return "Pourfendre";
                case "Italiano": return "Squartamento";
                case "Português Brasileiro": return "Dilacerar";
                case "Русский": return "Кровопускание";
                case "한국어": return "분쇄";
                case "简体中文": return "撕裂";
                default: return "Rend";
            }
        }

        ///<summary>ASCROT Spell=6572</summary>
        private static string Revenge_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Rache";
                case "Español": return "Revancha";
                case "Français": return "Revanche";
                case "Italiano": return "Rivincita";
                case "Português Brasileiro": return "Revanche";
                case "Русский": return "Реванш";
                case "한국어": return "복수";
                case "简体中文": return "复仇";
                default: return "Revenge";
            }
        }

        ///<summary>ASCROT Spell=64382</summary>
        private static string ShatteringThrow_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Zerschmetternder Wurf";
                case "Español": return "Lanzamiento destrozador";
                case "Français": return "Lancer fracassant";
                case "Italiano": return "Lancio Frantumante";
                case "Português Brasileiro": return "Arremesso Estilhaçante";
                case "Русский": return "Сокрушительный бросок";
                case "한국어": return "분쇄의 투척";
                case "简体中文": return "碎裂投掷";
                default: return "Shattering Throw";
            }
        }

        ///<summary>ASCROT Spell=2565</summary>
        private static string ShieldBlock_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schildblock";
                case "Español": return "Bloquear con escudo";
                case "Français": return "Maîtrise du blocage";
                case "Italiano": return "Scudo Saldo";
                case "Português Brasileiro": return "Levantar Escudo";
                case "Русский": return "Блок щитом";
                case "한국어": return "방패 올리기";
                case "简体中文": return "盾牌格挡";
                default: return "Shield Block";
            }
        }

        ///<summary>ASCROT Spell=385952</summary>
        private static string ShieldCharge_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schildansturm";
                case "Español": return "Carga con escudo";
                case "Français": return "Charge de bouclier";
                case "Italiano": return "Carica di Scudo";
                case "Português Brasileiro": return "Investida com Escudo";
                case "Русский": return "Атака щитом";
                case "한국어": return "방패 돌격";
                case "简体中文": return "盾牌冲锋";
                default: return "Shield Charge";
            }
        }

        ///<summary>ASCROT Spell=23922</summary>
        private static string ShieldSlam_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schildschlag";
                case "Español": return "Embate con escudo";
                case "Français": return "Heurt de bouclier";
                case "Italiano": return "Colpo di Scudo";
                case "Português Brasileiro": return "Escudada";
                case "Русский": return "Мощный удар щитом";
                case "한국어": return "방패 밀쳐내기";
                case "简体中文": return "盾牌猛击";
                default: return "Shield Slam";
            }
        }

        ///<summary>ASCROT Spell=871</summary>
        private static string ShieldWall_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schildwall";
                case "Español": return "Muro de escudo";
                case "Français": return "Mur protecteur";
                case "Italiano": return "Muro di Scudi";
                case "Português Brasileiro": return "Muralha de Escudos";
                case "Русский": return "Глухая оборона";
                case "한국어": return "방패의 벽";
                case "简体中文": return "盾墙";
                default: return "Shield Wall";
            }
        }



        ///<summary>ASCROT Spell=46968</summary>
        private static string Shockwave_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schockwelle";
                case "Español": return "Ola de choque";
                case "Français": return "Onde de choc";
                case "Italiano": return "Onda d'Urto";
                case "Português Brasileiro": return "Onda de Choque";
                case "Русский": return "Ударная волна";
                case "한국어": return "충격파";
                case "简体中文": return "震荡波";
                default: return "Shockwave";
            }
        }

        ///<summary>ASCROT Spell=1464</summary>
        private static string Slam_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Zerschmettern";
                case "Español": return "Embate";
                case "Français": return "Heurtoir";
                case "Italiano": return "Contusione";
                case "Português Brasileiro": return "Batida";
                case "Русский": return "Мощный удар";
                case "한국어": return "격돌";
                case "简体中文": return "猛击";
                default: return "Slam";
            }
        }

        ///<summary>ASCROT Spell=392966</summary>
        private static string SpellBlock_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Zauberblock";
                case "Español": return "Bloqueo de hechizos";
                case "Français": return "Blocage de sorts";
                case "Italiano": return "Blocca Incantesimo";
                case "Português Brasileiro": return "Bloqueio de Feitiço";
                case "Русский": return "Блокирование заклинаний";
                case "한국어": return "주문 막기";
                case "简体中文": return "法术格挡";
                default: return "Spell Block";
            }
        }

        ///<summary>ASCROT Spell=23920</summary>
        private static string SpellReflection_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Zauberreflexion";
                case "Español": return "Reflejo de hechizos";
                case "Français": return "Renvoi de sort";
                case "Italiano": return "Rifletti Incantesimo";
                case "Português Brasileiro": return "Reflexão de Feitiço";
                case "Русский": return "Отражение заклинаний";
                case "한국어": return "주문 반사";
                case "简体中文": return "法术反射";
                default: return "Spell Reflection";
            }
        }

        ///<summary>ASCROT Spell=107570</summary>
        private static string StormBolt_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Sturmblitz";
                case "Español": return "Descarga tormentosa";
                case "Français": return "Éclair de tempête";
                case "Italiano": return "Dardo della Tempesta";
                case "Português Brasileiro": return "Seta Tempestuosa";
                case "Русский": return "Удар громовержца";
                case "한국어": return "폭풍망치";
                case "简体中文": return "风暴之锤";
                default: return "Storm Bolt";
            }
        }

        ///<summary>ASCROT Spell=435222</summary>
        private static string ThunderBlast_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Donnerexplosion";
                case "Español": return "Explosión de trueno";
                case "Français": return "Explosion de tonnerre";
                case "Italiano": return "Boato del Tuono";
                case "Português Brasileiro": return "Impacto Trovejante";
                case "Русский": return "Разряд грома";
                case "한국어": return "우레 작렬";
                case "简体中文": return "雷霆轰击";
                default: return "Thunder Blast";
            }
        }

        ///<summary>ASCROT Spell=6343</summary>
        private static string ThunderClap_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Donnerknall";
                case "Español": return "Atronar";
                case "Français": return "Coup de tonnerre";
                case "Italiano": return "Schianto del Tuono";
                case "Português Brasileiro": return "Trovoada";
                case "Русский": return "Удар грома";
                case "한국어": return "천둥벼락";
                case "简体中文": return "雷霆一击";
                default: return "Thunder Clap";
            }
        }

        ///<summary>ASCROT Spell=384318</summary>
        private static string ThunderousRoar_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Donnerndes Gebrüll";
                case "Español": return "Rugido de trueno";
                case "Français": return "Rugissement vibrant";
                case "Italiano": return "Rombo di Tuono";
                case "Português Brasileiro": return "Rugido Trovejante";
                case "Русский": return "Громогласный рык";
                case "한국어": return "천둥의 포효";
                case "简体中文": return "雷鸣之吼";
                default: return "Thunderous Roar";
            }
        }

        ///<summary>ASCROT Spell=384090</summary>
        private static string TitanicThrow_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Titanischer Wurf";
                case "Español": return "Lanzamiento titánico";
                case "Français": return "Lancer titanesque";
                case "Italiano": return "Lancio Titanico";
                case "Português Brasileiro": return "Arremesso Titânico";
                case "Русский": return "Титанический бросок";
                case "한국어": return "티탄의 투척";
                case "简体中文": return "泰坦投掷";
                default: return "Titanic Throw";
            }
        }

        ///<summary>ASCROT Spell=34428</summary>
        private static string VictoryRush_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Siegesrausch";
                case "Español": return "Ataque de la victoria";
                case "Français": return "Ivresse de la victoire";
                case "Italiano": return "Frenesia di Vittoria";
                case "Português Brasileiro": return "Ímpeto da Vitória";
                case "Русский": return "Победный раж";
                case "한국어": return "연전연승";
                case "简体中文": return "乘胜追击";
                default: return "Victory Rush";
            }
        }

        ///<summary>ASCROT Spell=20549</summary>
        private static string WarStomp_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Kriegsdonner";
                case "Español": return "Pisotón de guerra";
                case "Français": return "Choc martial";
                case "Italiano": return "Zoccolo di Guerra";
                case "Português Brasileiro": return "Pisada de Guerra";
                case "Русский": return "Громовая поступь";
                case "한국어": return "전투 발구르기";
                case "简体中文": return "战争践踏";
                default: return "War Stomp";
            }
        }

        ///<summary>ASCROT Spell=190411</summary>
        private static string Whirlwind_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Wirbelwind";
                case "Español": return "Torbellino";
                case "Français": return "Tourbillon";
                case "Italiano": return "Turbine";
                case "Português Brasileiro": return "Redemoinho";
                case "Русский": return "Вихрь";
                case "한국어": return "소용돌이";
                case "简体中文": return "旋风斩";
                default: return "Whirlwind";
            }
        }

        ///<summary>ASCROT Spell=384110</summary>
        private static string WreckingThrow_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Abrisswurf";
                case "Español": return "Lanzamiento demoledor";
                case "Français": return "Lancer destructeur";
                case "Italiano": return "Lancio Demolitore";
                case "Português Brasileiro": return "Arremesso Avassalador";
                case "Русский": return "Разрушающий бросок";
                case "한국어": return "격파의 투척";
                case "简体中文": return "破裂投掷";
                default: return "Wrecking Throw";
            }
        }
        ///<summary>ASCROT Spell=315720</summary>
        private static string Onslaught_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Ansturm";
                case "Español": return "Irrupción";
                case "Français": return "Assaut";
                case "Italiano": return "Massacro";
                case "Português Brasileiro": return "Ofensiva";
                case "Русский": return "Натиск";
                case "한국어": return "맹공";
                case "简体中文": return "强攻";
                default: return "Onslaught";
            }
        }

        ///<summary>ASCROT Spell=385059</summary>
        private static string OdynsFury_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Odyns Zorn";
                case "Español": return "Furia de Odyn";
                case "Français": return "Fureur d’Odyn";
                case "Italiano": return "Furia di Odyn";
                case "Português Brasileiro": return "Fúria de Odyn";
                case "Русский": return "Ярость Одина";
                case "한국어": return "오딘의 격노";
                case "简体中文": return "奥丁之怒";
                default: return "Odyn's Fury";
            }
        }

        ///<summary>ASCROT Spell=184367</summary>
        private static string Rampage_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Toben";
                case "Español": return "Desenfreno";
                case "Français": return "Saccager";
                case "Italiano": return "Scatto d'Ira";
                case "Português Brasileiro": return "Alvoroço";
                case "Русский": return "Буйство";
                case "한국어": return "광란";
                case "简体中文": return "暴怒";
                default: return "Rampage";
            }
        }

        ///<summary>ASCROT Spell=85288</summary>
        private static string RagingBlow_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Wütender Schlag";
                case "Español": return "Arremetida enfurecida";
                case "Français": return "Coup déchaîné";
                case "Italiano": return "Colpo Furente";
                case "Português Brasileiro": return "Golpe Furioso";
                case "Русский": return "Яростный выпад";
                case "한국어": return "분노의 강타";
                case "简体中文": return "怒击";
                default: return "Raging Blow";
            }
        }

        ///<summary>ASCROT Spell=1719</summary>
        private static string Recklessness_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Tollkühnheit";
                case "Español": return "Temeridad";
                case "Français": return "Témérité";
                case "Italiano": return "Avventatezza";
                case "Português Brasileiro": return "Temeridade";
                case "Русский": return "Безрассудство";
                case "한국어": return "무모한 희생";
                case "简体中文": return "鲁莽";
                default: return "Recklessness";
            }
        }


        ///<summary>ASCROT Spell=386196</summary>
        private static string BerserkerStance_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Berserkerhaltung";
                case "Español": return "Actitud rabiosa";
                case "Français": return "Posture berserker";
                case "Italiano": return "Postura da Berserker";
                case "Português Brasileiro": return "Postura de Berserker";
                case "Русский": return "Стойка берсерка";
                case "한국어": return "광폭 태세";
                case "简体中文": return "狂暴姿态";
                default: return "Berserker Stance";
            }
        }

        ///<summary>ASCROT Spell=23881</summary>
        private static string Bloodthirst_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Blutdurst";
                case "Español": return "Sed de sangre";
                case "Français": return "Sanguinaire";
                case "Italiano": return "Sete di Sangue";
                case "Português Brasileiro": return "Sede de Sangue";
                case "Русский": return "Кровожадность";
                case "한국어": return "피의 갈증";
                case "简体中文": return "嗜血";
                default: return "Bloodthirst";
            }
        }


        ///<summary>ASCROT Spell=227847</summary>
        private static string Bladestorm_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Klingensturm";
                case "Español": return "Filotormenta";
                case "Français": return "Tempête de lames";
                case "Italiano": return "Tempesta di Lame";
                case "Português Brasileiro": return "Tornado de Aço";
                case "Русский": return "Вихрь клинков";
                case "한국어": return "칼날폭풍";
                case "简体中文": return "剑刃风暴";
                default: return "Bladestorm";
            }
        }

        ///<summary>ASCROT Spell=335096</summary>
        private static string Bloodbath_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Blutbad";
                case "Español": return "Baño de sangre";
                case "Français": return "Bain de sang";
                case "Italiano": return "Bagno di Sangue";
                case "Português Brasileiro": return "Banho de Sangue";
                case "Русский": return "Кровавая баня";
                case "한국어": return "피범벅";
                case "简体中文": return "浴血奋战";
                default: return "Bloodbath";
            }
        }


        ///<summary>ASCROT Spell=335097</summary>
        private static string CrushingBlow_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Schmetterschlag";
                case "Español": return "Golpe aplastante";
                case "Français": return "Coup écrasant";
                case "Italiano": return "Colpo Devastante";
                case "Português Brasileiro": return "Golpe Triturante";
                case "Русский": return "Сокрушающий удар";
                case "한국어": return "분쇄의 타격";
                case "简体中文": return "碎甲猛击";
                default: return "Crushing Blow";
            }
        }

        ///<summary>ASCROT Spell=184364</summary>
        private static string EnragedRegeneration_SpellName()
        {
            switch (Language)
            {
                case "Deutsch": return "Wütende Regeneration";
                case "Español": return "Regeneración enfurecida";
                case "Français": return "Régénération enragée";
                case "Italiano": return "Rigenerazione Furente";
                case "Português Brasileiro": return "Regeneração Enfurecida";
                case "Русский": return "Безудержное восстановление";
                case "한국어": return "격노의 재생력";
                case "简体中文": return "狂怒回复";
                default: return "Enraged Regeneration";
            }
        }




        #endregion
        #region logger
        public void Logger(string lines)
        {
            string path = Directory.GetCurrentDirectory() + "\\Rotations\\AscendedRotationsLogs\\";
            VerifyDir(path);
            string rotationNameWithoutWhiteSpaces = string.Join("", licenseServer.RotationName.Split(' '));
            string fileName = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + rotationNameWithoutWhiteSpaces + "_ASCRots.txt";
            string fullPath = path + fileName;

            try
            {
                if (!File.Exists(fullPath) || !File.ReadAllLines(fullPath).Contains(DateTime.Now.ToString() + ": " + lines))
                {
                    using (StreamWriter file = new StreamWriter(fullPath, true))
                    {
                        file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                    }
                }
            }
            catch (Exception) { }
        }
        public void VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }
        #endregion

    }
    #region Serverzeug
    public class RequestAuth
    {
        public string ip { get; set; }
        public string hostname { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
        public string productid { get; set; }
        public string hwid { get; set; }


        public RequestAuth(string ip, string hostname, string city, string region, string country, string loc, string org, string postal, string timezone, string hwid, string productid)
        {
            this.ip = ip;
            this.hostname = hostname;
            this.city = city;
            this.region = region;
            this.country = country;
            this.loc = loc;
            this.org = org;
            this.postal = postal;
            this.timezone = timezone;
            this.hwid = hwid;
            this.productid = productid;
        }
        public RequestAuth(string ip, string hostname, string city, string region, string country, string loc, string org, string postal, string timezone)
        {
            this.ip = ip;
            this.hostname = hostname;
            this.city = city;
            this.region = region;
            this.country = country;
            this.loc = loc;
            this.org = org;
            this.postal = postal;
            this.timezone = timezone;
        }
        public RequestAuth() { }
    }
    public class ResponseAuth
    {
        public string body { get; set; }
        public int status { get; set; }
        public object headers { get; set; }
    }
    public class LicenseServer
    {
        private RequestAuth requestAuth;
        private ResponseAuth responseAuth;
        private bool authorized = false;
        public string RotationName = "Ascended Fury Warrior";
        private string productID = "d0YNaY9tSn";
        public int specID = 72;
        HttpWebRequest RequestIP;

        public LicenseServer()
        {
            getConnectionInformation();
            authorized = getLicenseCheck();

        }

        private void getConnectionInformation()
        {
            RequestIP = (HttpWebRequest)WebRequest.Create("https://ipinfo.io/json");
            RequestIP.Method = "GET";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://ipinfo.io/json");
            }
            try
            {
                var ResponseIP = (HttpWebResponse)RequestIP.GetResponse();
                using (var streamReader = new StreamReader(ResponseIP.GetResponseStream()))
                {
                    var resultIP = streamReader.ReadToEnd();
                    requestAuth = JsonConvert.DeserializeObject<RequestAuth>(resultIP);
                    ResponseIP.Close();
                }
            }
            catch (Exception)
            {

            }

        }
        private bool getLicenseCheck()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string Request = "https://geek-tutorial.de:8080/auth/";
            requestAuth.hwid = Aimsharp.GetHWID();
            requestAuth.productid = productID;
            string requestJson = "";
            requestJson = JsonConvert.SerializeObject(requestAuth);
            using (HttpResponseMessage response = Post(Request, requestJson))
            {
                ResponseAuth responseAuth = JsonConvert.DeserializeObject<ResponseAuth>(response.Content.ReadAsStringAsync().Result);
                if (responseAuth.status == 200)
                {
                    Aimsharp.PrintMessage(responseAuth.body, Color.Purple);
                    killBattleNetProcess();
                    return authorized = true;

                }
                else
                {
                    Aimsharp.PrintMessage(responseAuth.body, Color.Red);
                    return authorized = false;

                }
            }
        }
        private async Task<bool> GetLicenseCheckAsync()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string requestUrl = "https://geek-tutorial.de:8080/auth/";
            requestAuth.hwid = Aimsharp.GetHWID();
            requestAuth.productid = productID;
            string requestJson = JsonConvert.SerializeObject(requestAuth);

            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                ResponseAuth responseAuth = JsonConvert.DeserializeObject<ResponseAuth>(responseContent);

                if (responseAuth.status == 200)
                {
                    Aimsharp.PrintMessage(responseAuth.body, Color.Purple);
                    killBattleNetProcess();
                    return true;

                }
                else
                {
                    Aimsharp.PrintMessage(responseAuth.body, Color.Red);
                    return false;
                }
            }
        }
        public bool check()
        {
            return authorized;
        }
        private void killBattleNetProcess()
        {
            foreach (var process in Process.GetProcessesByName("battle.net"))
            {
                process.Kill();
            }

        }
        private HttpResponseMessage Post(string apiUrl, string requestBody)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync(apiUrl, content).Result;
                return response;
            }
        }

    }
    #endregion
}
