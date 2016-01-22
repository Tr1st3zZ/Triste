
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaYasuo;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo
{
    internal class MenuManager
    {
        public static Menu YMenu,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            LastHitMenu,
            JungleClearMenu,
            MiscMenu,
            FleeMenu,
            KillStealMenu,
            DrawingMenu,
            DogeMenu,
            ItemMenu;

        public static string[] gapcloser;
        public static string[] interrupt;
        public static string[] notarget;
        public static Dictionary<string, Menu> SubMenu = new Dictionary<string, Menu>() { };

        public static void Load()
        {
            Mainmenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            LastHitmenu();
            JungleClearmenu();
            Miscmenu();
            KillStealmenu();
            Drawingmenu();
            Dogemenu();
            Itemmenu();
        }

        public static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("亚索", "akasyasuo");
            YMenu.AddGroupLabel("Welcome to my Yasuo Addon have fun! :)");
        }

        public static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("连招", "Combo");
            ComboMenu.AddGroupLabel("连招");
            ComboMenu.Add("Q", new CheckBox("使用 Q"));
            ComboMenu.Add("EC", new CheckBox("使用 E--看后面"));
            ComboMenu.Add("E1", new Slider("当出现敌人范围 >或=", 375, 475, 1));
            ComboMenu.Add("E2", new Slider("使用 E-接近敌人当范围>或=", 230, 1300, 1));
            ComboMenu.Add("E3", new CheckBox("模式: On = E向目标 / OFF =E向鼠标"));
            ComboMenu.Add("R", new CheckBox("使用 R"));
            ComboMenu.Add("Ignite", new CheckBox("使用点燃"));
            ComboMenu.AddGroupLabel("R 连招设置");
            foreach (var hero in EntityManager.Heroes.Enemies.Where(x => x.IsEnemy))
            {
                ComboMenu.Add(hero.ChampionName, new CheckBox("使用 R 当目标为" + hero.ChampionName));
            }
            ComboMenu.AddSeparator();
            ComboMenu.Add("R4", new CheckBox("瞬间使用R当>或=1个队友在范围内"));
            ComboMenu.Add("R2", new Slider("当敌人血<或=", 50, 0, 101));
            ComboMenu.Add("R3", new Slider("当X个敌人被击飞", 2, 0, 5));
            ComboMenu.AddGroupLabel("自动 R 设置");
            ComboMenu.Add("AutoR", new CheckBox("使用 自动 R"));
            ComboMenu.Add("AutoR2", new Slider("当X个敌人被击飞", 3, 0, 5));
            ComboMenu.Add("AutoR2HP", new Slider("当我的血>或=", 101, 0, 101));
            ComboMenu.Add("AutoR2Enemies", new Slider("当附近有敌人<或=", 2, 0, 5));
        }

        public static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("骚扰", "Harass");
            HarassMenu.Add("AutoQ", new KeyBind("自动Q切换键", true, KeyBind.BindTypes.PressToggle, 'T'));
            HarassMenu.Add("AutoQMinion", new KeyBind("自动Q小兵切换键", true, KeyBind.BindTypes.PressToggle, 'H'));
            HarassMenu.Add("AutoQ3", new CheckBox("自动使用Q第3下？"));
            HarassMenu.Add("Q", new CheckBox("使用 Q"));
            HarassMenu.Add("Q3", new CheckBox("使用Q第3下"));
            HarassMenu.Add("E", new CheckBox("使用 E"));
            HarassMenu.Add("QunderTower", new CheckBox("在塔下自动Q"));
        }

        public static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("逃跑", "Flee");
            FleeMenu.Add("EscQ", new CheckBox("使用 Q"));
            FleeMenu.Add("EscE", new CheckBox("使用 E"));
            FleeMenu.Add("WJ", new KeyBind("在逃跑模式中跳墙", false, KeyBind.BindTypes.HoldActive, 'G'));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("清兵", "LaneClear");
            LaneClearMenu.Add("Q", new CheckBox("使用 Q"));
            LaneClearMenu.Add("Q3", new CheckBox("使用 Q3"));
            LaneClearMenu.Add("E", new CheckBox("使用 E"));
            LaneClearMenu.Add("Items", new CheckBox("使用物品"));
        }

        public static void JungleClearmenu()
        {
            JungleClearMenu = YMenu.AddSubMenu("清野", "JungleClear");
            JungleClearMenu.Add("Q", new CheckBox("使用 Q"));
            JungleClearMenu.Add("E", new CheckBox("使用 E"));
            JungleClearMenu.Add("Items", new CheckBox("使用物品"));
        }

        public static void LastHitmenu()
        {
            LastHitMenu = YMenu.AddSubMenu("最后一击", "LastHit");
            LastHitMenu.Add("Q", new CheckBox("使用 Q"));
            LastHitMenu.Add("Q3", new CheckBox("使用Q第3下"));
            LastHitMenu.Add("E", new CheckBox("使用 E"));
        }

        public static void KillStealmenu()
        {
            KillStealMenu = YMenu.AddSubMenu("清兵", "KillSteal");
            KillStealMenu.Add("KsQ", new CheckBox("使用 Q"));
            KillStealMenu.Add("KsE", new CheckBox("使用 E"));
            KillStealMenu.Add("KsIgnite", new CheckBox("使用点燃"));
        }

        public static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("杂项", "Misc");
            MiscMenu.Add("InterruptQ", new CheckBox("使用Q第3下打断技能"));
            MiscMenu.Add("noEturret", new CheckBox("在敌人塔下不E"));
            MiscMenu.AddSeparator();
            MiscMenu.AddLabel("1: 主Q 2:主 E");
            MiscMenu.Add("autolvl", new CheckBox("自动升级"));
            MiscMenu.Add("autolvls", new Slider("顺序", 1, 1, 2));
            switch (MiscMenu["autolvls"].Cast<Slider>().CurrentValue)
            {
                case 1:
                    Variables.abilitySequence = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case 2:
                    Variables.abilitySequence = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
            }
            var skin = MiscMenu.Add("sID", new Slider("皮肤切换", 0, 0, 2));
            var sID = new[] { "Classic", "High-Noon Yasuo", "Project Yasuo" };
            skin.DisplayName = sID[skin.CurrentValue];

            skin.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = sID[changeArgs.NewValue];
                };
        }

        public static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("显示设置", "Drawing");
            DrawingMenu.Add("DrawQ", new CheckBox("画出 Q"));
            DrawingMenu.Add("DrawQ3", new CheckBox("画出 Q3"));
            DrawingMenu.Add("DrawE", new CheckBox("画出 E"));
            DrawingMenu.Add("DrawR", new CheckBox("画出 R"));
            DrawingMenu.Add("DrawTarget", new CheckBox("画出攻击目标"));
            DrawingMenu.Add("DrawSpots", new CheckBox("画出跳墙点"));
        }

        public static void Dogemenu()
        {
            DogeMenu = YMenu.AddSubMenu("躲避", "Doge");
            DogeMenu.Add("smartW", new CheckBox("智能风墙"));
            DogeMenu.Add("smartWD",
                new Slider("风墙释放延迟(毫秒)", 3000, 0, 3000));
            DogeMenu.Add("smartEDogue", new CheckBox("使用E躲避"));
            DogeMenu.Add("wwDanger", new CheckBox("仅危险技能使用W"));
            var skillShots = MainMenu.AddMenu("敌人技能", "aShotsSkills");

            foreach (var hero in ObjectManager.Get<AIHeroClient>())
            {
                if (hero.Team != ObjectManager.Player.Team)
                {
                    foreach (var spell in SpellDatabase.Spells)
                    {
                        if (spell.ChampionName == hero.ChampionName)
                        {
                            SubMenu["SMIN"] = skillShots.AddSubMenu(spell.MenuItemName, spell.MenuItemName);

                            SubMenu["SMIN"].Add
                                ("DangerLevel" + spell.MenuItemName,
                                    new Slider("危险等级", spell.DangerValue, 5, 1));

                            SubMenu["SMIN"].Add
                                ("IsDangerous" + spell.MenuItemName,
                                    new CheckBox("危险", spell.IsDangerous));

                            //SubMenu["SMIN"].Add("Draw" + spell.MenuItemName, new CheckBox("Draw"));
                            SubMenu["SMIN"].Add("Enabled" + spell.MenuItemName, new CheckBox("开启"));
                        }
                    }
                }
            }
        }

        public static void Itemmenu()
        {
            ItemMenu = YMenu.AddSubMenu("装备设置", "QSS");
            ItemMenu.Add("Items", new CheckBox("使用物品"));
            ItemMenu.Add("myhp", new Slider("使用破败当我血少于<=", 70, 0, 101));
            ItemMenu.AddSeparator();
            ItemMenu.Add("use", new KeyBind("使用水银/小水银", true, KeyBind.BindTypes.PressToggle, 'K'));
            ItemMenu.Add("delay", new Slider("使用延迟", 1000, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("当被致盲", false));
            ItemMenu.Add("Charm",
                new CheckBox("当被魅惑"));
            ItemMenu.Add("Fear",
                new CheckBox("当被恐惧"));
            ItemMenu.Add("Polymorph",
                new CheckBox("当被变身"));
            ItemMenu.Add("Stun",
                new CheckBox("当被眩晕"));
            ItemMenu.Add("Snare",
                new CheckBox("当踩到陷阱"));
            ItemMenu.Add("Silence",
                new CheckBox("当被沉默", false));
            ItemMenu.Add("Taunt",
                new CheckBox("当被嘲讽"));
            ItemMenu.Add("Suppression",
                new CheckBox("当被压制"));
        }
    }
}

