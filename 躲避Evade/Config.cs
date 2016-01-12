// Copyright 2014 - 2014 Esk0r
// Config.cs is part of Evade.
// 
// Evade is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Evade is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Evade. If not, see <http://www.gnu.org/licenses/>.

#region

using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

#endregion

namespace Evade
{
    internal static class Config
    {
        public const bool PrintSpellData = false;
        public const bool TestOnAllies = false;
        public static int SkillShotsExtraRadius
        {
            get { return 9 + humanizer["ExtraEvadeRange"].Cast<Slider>().CurrentValue; }
        }
        public static int SkillShotsExtraRange
        {
            get { return 20 + humanizer["ExtraEvadeRange"].Cast<Slider>().CurrentValue; }
        }
        public const int GridSize = 10;
        public static int ExtraEvadeDistance
        {
            get { return 15 + humanizer["ExtraEvadeRange"].Cast<Slider>().CurrentValue; }
        }

        public static int ActivationDelay
        {
            get { return 0; /*return humanizer["ActivationDelay"].Cast<Slider>().CurrentValue;*/ }
        }
        public const int PathFindingDistance = 60;
        public const int PathFindingDistance2 = 35;

        public const int DiagonalEvadePointsCount = 7;
        public const int DiagonalEvadePointsStep = 20;

        public const int CrossingTimeOffset = 250;

        public const int EvadingFirstTimeOffset = 250;
        public const int EvadingSecondTimeOffset = 80;

        public const int EvadingRouteChangeTimeOffset = 250;

        public const int EvadePointChangeInterval = 300;
        public static int LastEvadePointChangeT = 0;

        public static Menu Menu, evadeSpells, skillShots, shielding, collision, drawings, misc, humanizer;
        public static Color EnabledColor, DisabledColor, MissileColor;

        public static void CreateMenu()
        {
            Menu = MainMenu.AddMenu("躲避", "evade");

            if (Menu == null)
            {
                Chat.Print("LOAD FAILED", Color.Red);
                Console.WriteLine("Evade:: LOAD FAILED");
                throw new NullReferenceException("Menu NullReferenceException");
            }

            //Create the evade spells submenus.
            evadeSpells = Menu.AddSubMenu("技能设置", "evadeSpells");
            foreach (var spell in EvadeSpellDatabase.Spells)
            {
                evadeSpells.AddGroupLabel(spell.Name);

                try
                {
                    evadeSpells.Add("DangerLevel" + spell.Name, new Slider("危险等级", spell._dangerLevel, 1, 5));
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (spell.IsTargetted && spell.ValidTargets.Contains(SpellValidTargets.AllyWards))
                {
                    evadeSpells.Add("WardJump" + spell.Name, new CheckBox("跳眼"));
                }

                evadeSpells.Add("Enabled" + spell.Name, new CheckBox("开启"));
            }

            //Create the skillshots submenus.
            skillShots = Menu.AddSubMenu("躲避设置", "Skillshots");

            foreach (var hero in ObjectManager.Get<AIHeroClient>())
            {
                if (hero.Team != ObjectManager.Player.Team || Config.TestOnAllies)
                {
                    foreach (var spell in SpellDatabase.Spells)
                    {
                        if (String.Equals(spell.ChampionName, hero.ChampionName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            skillShots.AddGroupLabel(spell.SpellName);
                            skillShots.Add("DangerLevel" + spell.MenuItemName, new Slider("危险等级", spell.DangerValue, 1, 5));

                            skillShots.Add("IsDangerous" + spell.MenuItemName, new CheckBox("危险", spell.IsDangerous));

                            skillShots.Add("Draw" + spell.MenuItemName, new CheckBox("显示躲避圈"));
                            skillShots.Add("Enabled" + spell.MenuItemName, new CheckBox("开启", !spell.DisabledByDefault));
                        }
                    }
                }
            }

            shielding = Menu.AddSubMenu("给予护盾", "Shielding");

            foreach (var ally in ObjectManager.Get<AIHeroClient>())
            {
                if (ally.IsAlly && !ally.IsMe)
                {
                    shielding.Add("shield" + ally.ChampionName, new CheckBox("护盾 " + ally.ChampionName));
                }
            }

            collision = Menu.AddSubMenu("碰撞预测", "Collision");
            collision.Add("MinionCollision", new CheckBox("小兵"));
            collision.Add("HeroCollision", new CheckBox("英雄"));
            collision.Add("YasuoCollision", new CheckBox("亚索墙"));
            collision.Add("EnableCollision", new CheckBox("开启碰撞预测"));
            //TODO add mode.

            drawings = Menu.AddSubMenu("显示设置", "Drawings");
            //EnabledColor = drawings.AddColor(new[] { "ECA", "ECR", "ECG", "ECB"}, "Enabled Spell Color", Color.White);
            //DisabledColor = drawings.AddColor(new[] {"DCA", "DCR", "DCG", "DCB"}, "Disabled Spell Color", Color.Red);
            //MissileColor = drawings.AddColor(new[] { "MCA", "MCR", "MCG", "MCB" }, "Missile Color", Color.Red);

            drawings.AddLabel("已启用的显示颜色");
            drawings.Add("EnabledDrawA", new Slider("A", 255, 1, 255));
            drawings.Add("EnabledDrawR", new Slider("R", 255, 1, 255));
            drawings.Add("EnabledDrawG", new Slider("G", 255, 1, 255));
            drawings.Add("EnabledDrawB", new Slider("B", 255, 1, 255));

            EnabledColor = Color.FromArgb(drawings["EnabledDrawA"].Cast<Slider>().CurrentValue,
                drawings["EnabledDrawR"].Cast<Slider>().CurrentValue,
                drawings["EnabledDrawG"].Cast<Slider>().CurrentValue,
                drawings["EnabledDrawB"].Cast<Slider>().CurrentValue);

            drawings.AddSeparator(5);

            drawings.AddLabel("不开启显示的颜色");
            drawings.Add("DisabledDrawA", new Slider("A", 255, 1, 255));
            drawings.Add("DisabledDrawR", new Slider("R", 255, 1, 255));
            drawings.Add("DisabledDrawG", new Slider("G", 255, 1, 255));
            drawings.Add("DisabledDrawB", new Slider("B", 255, 1, 255));

            DisabledColor = Color.FromArgb(drawings["DisabledDrawA"].Cast<Slider>().CurrentValue,
                drawings["DisabledDrawR"].Cast<Slider>().CurrentValue,
                drawings["DisabledDrawG"].Cast<Slider>().CurrentValue,
                drawings["DisabledDrawB"].Cast<Slider>().CurrentValue);

            drawings.AddSeparator(5);

            drawings.AddLabel("导弹颜色");
            drawings.Add("MissileDrawA", new Slider("A", 255, 1, 255));
            drawings.Add("MissileDrawR", new Slider("R", 255, 1, 255));
            drawings.Add("MissileDrawG", new Slider("G", 255, 1, 255));
            drawings.Add("MissileDrawB", new Slider("B", 255, 1, 255));

            MissileColor = Color.FromArgb(drawings["MissileDrawA"].Cast<Slider>().CurrentValue,
                drawings["MissileDrawR"].Cast<Slider>().CurrentValue,
                drawings["MissileDrawG"].Cast<Slider>().CurrentValue,
                drawings["MissileDrawB"].Cast<Slider>().CurrentValue);

            drawings.AddSeparator(5);

            drawings.Add("EnabledDraw", new CheckBox("显示已开启显示"));
            drawings.Add("DisabledDraw", new CheckBox("显示已关闭的显示"));
            drawings.Add("MissileDraw", new CheckBox("显示导弹"));

            //drawings.AddItem(new MenuItem("EnabledColor", "Enabled spell color").SetValue(Color.White));
            //drawings.AddItem(new MenuItem("DisabledColor", "Disabled spell color").SetValue(Color.Red));
            //drawings.AddItem(new MenuItem("MissileColor", "Missile color").SetValue(Color.LimeGreen));
            drawings.Add("Border", new Slider("边框宽度", 1, 5, 1));

            drawings.Add("EnableDrawings", new CheckBox("启用"));
            drawings.Add("ShowEvadeStatus", new CheckBox("显示躲避状态"));

            humanizer = Menu.AddSubMenu("人性化[不要碰]");

            //humanizer.Add("ActivationDelay", new Slider("Activation Delay"));
            humanizer.Add("ExtraEvadeRange", new Slider("额外躲避范围"));

            misc = Menu.AddSubMenu("杂项", "Misc");
            misc.AddStringList("BlockSpells", "躲避时停止技能", new[] { "关闭此项", "仅危险技能", "总是不使用" }, 1);
            //misc.Add("BlockSpells", "Block spells while evading").SetValue(new StringList(new []{"No", "Only dangerous", "Always"}, 1)));
            misc.Add("DisableFow", new CheckBox("关闭这个就可以躲看不见的技能了"));
            misc.Add("ShowEvadeStatus", new CheckBox("显示躲避状态"));
            if (ObjectManager.Player.BaseSkinName == "Olaf")
            {
                misc.Add("DisableEvadeForOlafR", new CheckBox("当奥拉夫使用R时关闭躲避!"));
            }

            Menu.Add("Enabled", new KeyBind("开启与关闭躲避", true, KeyBind.BindTypes.PressToggle, "K".ToCharArray()[0]));

            Menu.Add("OnlyDangerous", new KeyBind("仅躲避危险技能", false, KeyBind.BindTypes.HoldActive, 32)); //Space
        }
    }
}
