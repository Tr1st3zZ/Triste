
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven
{
    internal class MenuManager
    {
        public static Menu YMenu,
            ComboMenu,
            AxeMenu,
            HarassMenu,
            LaneClearMenu,
            MiscMenu,
            FleeMenu,
            KillStealMenu,
            DrawingMenu,
            ItemMenu;

        public static void Load()
        {
            Mainmenu();
            Axemenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            Miscmenu();
            KillStealmenu();
            Drawingmenu();
            Itemmenu();
        }

        public static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("德莱文", "akasdraven");
            YMenu.AddGroupLabel("德莱文：如果你想食屎，我成全你 :3 ");
        }

        public static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("连招", "Combo");
            ComboMenu.AddGroupLabel("连招");
            ComboMenu.Add("Q", new CheckBox("使用 Q"));
            ComboMenu.Add("W", new CheckBox("使用 W"));
            ComboMenu.Add("E", new CheckBox("使用 E"));
            ComboMenu.Add("R", new CheckBox("使用 R"));
        }

        public static void Axemenu()
        {
            AxeMenu = YMenu.AddSubMenu("斧头设置", "Axesettings");
            AxeMenu.AddGroupLabel("斧头设置");
            AxeMenu.AddLabel("1: 连招 2: 当按下-空格,V,C,X 3: 任何情况");
            AxeMenu.Add("Qmode", new Slider("接斧头模式:", 3, 1, 3));
            AxeMenu.Add("Qrange", new Slider("接斧头范围:", 800, 120, 1500));
            AxeMenu.Add("Qmax", new Slider("最多斧头:", 2, 1, 3));
            AxeMenu.Add("WforQ", new CheckBox("如果斧头很远-使用W"));
            AxeMenu.Add("Qunderturret", new CheckBox("在塔下不捡斧头"));
        }

        public static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("骚扰", "Harass");
            HarassMenu.Add("E", new CheckBox("使用 E"));
            HarassMenu.Add("AutoE", new KeyBind("自动骚扰切换键", true, KeyBind.BindTypes.PressToggle, 'G'));
        }

        public static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("逃-跑", "Flee");
            FleeMenu.Add("E", new CheckBox("使用E"));
            FleeMenu.Add("W", new CheckBox("使用W"));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("清兵", "LaneClear");
            LaneClearMenu.Add("Q", new CheckBox("使用Q"));
            LaneClearMenu.Add("W", new CheckBox("使用W"));
            LaneClearMenu.Add("Mana", new Slider("蓝量控制", 50));
        }

        public static void KillStealmenu()
        {
            KillStealMenu = YMenu.AddSubMenu("抢人头", "KillSteal");
            KillStealMenu.Add("KsE", new CheckBox("使用E"));
            KillStealMenu.Add("KsIgnite", new CheckBox("使用点燃"));
        }

        public static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("杂项", "Misc");
            MiscMenu.Add("UseEInterrupt", new CheckBox("使用 E 打断技能"));
            MiscMenu.Add("UseWInstant", new CheckBox("瞬间使用 W【如果无冷却】", false));
            MiscMenu.Add("UseWSlow", new CheckBox("如果被减速，则使用W"));
            MiscMenu.Add("WMana", new Slider("W 蓝量控制", 50));
            MiscMenu.Add("autolvl", new CheckBox("开启自动升级"));
        }

        public static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("显示设置", "Drawing");
            DrawingMenu.Add("DrawE", new CheckBox("画出E"));
            DrawingMenu.Add("DrawAxe", new CheckBox("画出斧子"));
            DrawingMenu.Add("DrawAxeRange", new CheckBox("画出斧子抓取范围"));
        }

        public static void Itemmenu()
        {
            ItemMenu = YMenu.AddSubMenu("物品设置", "QSS");
            ItemMenu.Add("Items", new CheckBox("使用物品"));
            ItemMenu.Add("myhp", new Slider("使用破败当我血<或=", 70, 0, 101));
            ItemMenu.AddSeparator();
            ItemMenu.Add("use", new KeyBind("使用水银/小水银", true, KeyBind.BindTypes.PressToggle, 'K'));
            ItemMenu.Add("delay", new Slider("使用延迟", 1000, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("当被-致盲", false));
            ItemMenu.Add("Charm",
                new CheckBox("当被-魅惑"));
            ItemMenu.Add("Fear",
                new CheckBox("当被-恐惧"));
            ItemMenu.Add("Polymorph",
                new CheckBox("当被-变形"));
            ItemMenu.Add("Stun",
                new CheckBox("当被-眩晕"));
            ItemMenu.Add("Snare",
                new CheckBox("当-踩到陷阱"));
            ItemMenu.Add("Silence",
                new CheckBox("当被-沉默", false));
            ItemMenu.Add("Taunt",
                new CheckBox("当被-嘲讽"));
            ItemMenu.Add("Suppression",
                new CheckBox("当被-压制"));
        }
    }
}
