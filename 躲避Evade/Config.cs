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
            Menu = MainMenu.AddMenu("���", "evade");

            if (Menu == null)
            {
                Chat.Print("LOAD FAILED", Color.Red);
                Console.WriteLine("Evade:: LOAD FAILED");
                throw new NullReferenceException("Menu NullReferenceException");
            }

            //Create the evade spells submenus.
            evadeSpells = Menu.AddSubMenu("��������", "evadeSpells");
            foreach (var spell in EvadeSpellDatabase.Spells)
            {
                evadeSpells.AddGroupLabel(spell.Name);

                try
                {
                    evadeSpells.Add("DangerLevel" + spell.Name, new Slider("Σ�յȼ�", spell._dangerLevel, 1, 5));
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (spell.IsTargetted && spell.ValidTargets.Contains(SpellValidTargets.AllyWards))
                {
                    evadeSpells.Add("WardJump" + spell.Name, new CheckBox("����"));
                }

                evadeSpells.Add("Enabled" + spell.Name, new CheckBox("����"));
            }

            //Create the skillshots submenus.
            skillShots = Menu.AddSubMenu("�������", "Skillshots");

            foreach (var hero in ObjectManager.Get<AIHeroClient>())
            {
                if (hero.Team != ObjectManager.Player.Team || Config.TestOnAllies)
                {
                    foreach (var spell in SpellDatabase.Spells)
                    {
                        if (String.Equals(spell.ChampionName, hero.ChampionName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            skillShots.AddGroupLabel(spell.SpellName);
                            skillShots.Add("DangerLevel" + spell.MenuItemName, new Slider("Σ�յȼ�", spell.DangerValue, 1, 5));

                            skillShots.Add("IsDangerous" + spell.MenuItemName, new CheckBox("Σ��", spell.IsDangerous));

                            skillShots.Add("Draw" + spell.MenuItemName, new CheckBox("��ʾ���Ȧ"));
                            skillShots.Add("Enabled" + spell.MenuItemName, new CheckBox("����", !spell.DisabledByDefault));
                        }
                    }
                }
            }

            shielding = Menu.AddSubMenu("���軤��", "Shielding");

            foreach (var ally in ObjectManager.Get<AIHeroClient>())
            {
                if (ally.IsAlly && !ally.IsMe)
                {
                    shielding.Add("shield" + ally.ChampionName, new CheckBox("���� " + ally.ChampionName));
                }
            }

            collision = Menu.AddSubMenu("��ײԤ��", "Collision");
            collision.Add("MinionCollision", new CheckBox("С��"));
            collision.Add("HeroCollision", new CheckBox("Ӣ��"));
            collision.Add("YasuoCollision", new CheckBox("����ǽ"));
            collision.Add("EnableCollision", new CheckBox("������ײԤ��"));
            //TODO add mode.

            drawings = Menu.AddSubMenu("��ʾ����", "Drawings");
            //EnabledColor = drawings.AddColor(new[] { "ECA", "ECR", "ECG", "ECB"}, "Enabled Spell Color", Color.White);
            //DisabledColor = drawings.AddColor(new[] {"DCA", "DCR", "DCG", "DCB"}, "Disabled Spell Color", Color.Red);
            //MissileColor = drawings.AddColor(new[] { "MCA", "MCR", "MCG", "MCB" }, "Missile Color", Color.Red);

            drawings.AddLabel("�����õ���ʾ��ɫ");
            drawings.Add("EnabledDrawA", new Slider("A", 255, 1, 255));
            drawings.Add("EnabledDrawR", new Slider("R", 255, 1, 255));
            drawings.Add("EnabledDrawG", new Slider("G", 255, 1, 255));
            drawings.Add("EnabledDrawB", new Slider("B", 255, 1, 255));

            EnabledColor = Color.FromArgb(drawings["EnabledDrawA"].Cast<Slider>().CurrentValue,
                drawings["EnabledDrawR"].Cast<Slider>().CurrentValue,
                drawings["EnabledDrawG"].Cast<Slider>().CurrentValue,
                drawings["EnabledDrawB"].Cast<Slider>().CurrentValue);

            drawings.AddSeparator(5);

            drawings.AddLabel("��������ʾ����ɫ");
            drawings.Add("DisabledDrawA", new Slider("A", 255, 1, 255));
            drawings.Add("DisabledDrawR", new Slider("R", 255, 1, 255));
            drawings.Add("DisabledDrawG", new Slider("G", 255, 1, 255));
            drawings.Add("DisabledDrawB", new Slider("B", 255, 1, 255));

            DisabledColor = Color.FromArgb(drawings["DisabledDrawA"].Cast<Slider>().CurrentValue,
                drawings["DisabledDrawR"].Cast<Slider>().CurrentValue,
                drawings["DisabledDrawG"].Cast<Slider>().CurrentValue,
                drawings["DisabledDrawB"].Cast<Slider>().CurrentValue);

            drawings.AddSeparator(5);

            drawings.AddLabel("������ɫ");
            drawings.Add("MissileDrawA", new Slider("A", 255, 1, 255));
            drawings.Add("MissileDrawR", new Slider("R", 255, 1, 255));
            drawings.Add("MissileDrawG", new Slider("G", 255, 1, 255));
            drawings.Add("MissileDrawB", new Slider("B", 255, 1, 255));

            MissileColor = Color.FromArgb(drawings["MissileDrawA"].Cast<Slider>().CurrentValue,
                drawings["MissileDrawR"].Cast<Slider>().CurrentValue,
                drawings["MissileDrawG"].Cast<Slider>().CurrentValue,
                drawings["MissileDrawB"].Cast<Slider>().CurrentValue);

            drawings.AddSeparator(5);

            drawings.Add("EnabledDraw", new CheckBox("��ʾ�ѿ�����ʾ"));
            drawings.Add("DisabledDraw", new CheckBox("��ʾ�ѹرյ���ʾ"));
            drawings.Add("MissileDraw", new CheckBox("��ʾ����"));

            //drawings.AddItem(new MenuItem("EnabledColor", "Enabled spell color").SetValue(Color.White));
            //drawings.AddItem(new MenuItem("DisabledColor", "Disabled spell color").SetValue(Color.Red));
            //drawings.AddItem(new MenuItem("MissileColor", "Missile color").SetValue(Color.LimeGreen));
            drawings.Add("Border", new Slider("�߿���", 1, 5, 1));

            drawings.Add("EnableDrawings", new CheckBox("����"));
            drawings.Add("ShowEvadeStatus", new CheckBox("��ʾ���״̬"));

            humanizer = Menu.AddSubMenu("���Ի�[��Ҫ��]");

            //humanizer.Add("ActivationDelay", new Slider("Activation Delay"));
            humanizer.Add("ExtraEvadeRange", new Slider("�����ܷ�Χ"));

            misc = Menu.AddSubMenu("����", "Misc");
            misc.AddStringList("BlockSpells", "���ʱֹͣ����", new[] { "�رմ���", "��Σ�ռ���", "���ǲ�ʹ��" }, 1);
            //misc.Add("BlockSpells", "Block spells while evading").SetValue(new StringList(new []{"No", "Only dangerous", "Always"}, 1)));
            misc.Add("DisableFow", new CheckBox("�ر�����Ϳ��Զ㿴�����ļ�����"));
            misc.Add("ShowEvadeStatus", new CheckBox("��ʾ���״̬"));
            if (ObjectManager.Player.BaseSkinName == "Olaf")
            {
                misc.Add("DisableEvadeForOlafR", new CheckBox("��������ʹ��Rʱ�رն��!"));
            }

            Menu.Add("Enabled", new KeyBind("������رն��", true, KeyBind.BindTypes.PressToggle, "K".ToCharArray()[0]));

            Menu.Add("OnlyDangerous", new KeyBind("�����Σ�ռ���", false, KeyBind.BindTypes.HoldActive, 32)); //Space
        }
    }
}
