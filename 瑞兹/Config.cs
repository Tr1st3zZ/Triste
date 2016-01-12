// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace Rice
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    public static class Config
    {
        public static class Modes
        {
            public static class AutoStack
            {
                private static readonly Slider _AutoStackMana;

                private static readonly Slider _MaxStacks;

                private static readonly Slider _StackTimer;

                private static readonly KeyBind _AutoStackQ;

                public static int AutoStackMana
                {
                    get
                    {
                        return _AutoStackMana.CurrentValue;
                    }
                }

                public static bool AutoStackQ
                {
                    get
                    {
                        return _AutoStackQ.CurrentValue;
                    }
                }

                public static int MaxStacks
                {
                    get
                    {
                        return _MaxStacks.CurrentValue;
                    }
                }

                public static int StackTimer
                {
                    get
                    {
                        return _StackTimer.CurrentValue;
                    }
                }

                static AutoStack()
                {
                    MiscMenu.AddGroupLabel("自动叠层数");
                    _AutoStackQ = MiscMenu.Add(
                        "自动Q【推荐自己手动叠层数】",
                        new KeyBind("热键", false, KeyBind.BindTypes.PressToggle, 'Z'));

                    _AutoStackQ.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                        {
                            Program.StackingStatus.TextValue = args.NewValue
                                                                   ? "Passive Stacking On"
                                                                   : "Passive Stacking Off";

                            Program.StackingStatus.Color = args.NewValue ? Color.LimeGreen : Color.Red;
                        };

                    _AutoStackMana = MiscMenu.Add("AutoStackMana", new Slider("蓝量控制 %", 30));

                    _MaxStacks = MiscMenu.Add("MaxStacks", new Slider("保持层数于", 3, 1, 4));

                    _StackTimer = MiscMenu.Add("StackTimer", new Slider("使用Q间隔秒数", 5, 1, 10));
                }

                public static void Initialize()
                {
                }
            }

            public static class Humanizer
            {
                private static readonly Slider _MinDelay;

                private static readonly Slider _MaxDelay;

                private static readonly CheckBox _Humanize;

                public static int MinDelay
                {
                    get
                    {
                        return _MinDelay.CurrentValue;
                    }
                }

                public static int MaxDelay
                {
                    get
                    {
                        return _MaxDelay.CurrentValue;
                    }
                }

                public static bool Humanize
                {
                    get
                    {
                        return _Humanize.CurrentValue;
                    }
                }

                static Humanizer()
                {
                    MiscMenu.AddGroupLabel("人性化");
                    _MinDelay = MiscMenu.Add("minDelay", new Slider("最小延迟", 10, 0, 200));
                    _MaxDelay = MiscMenu.Add("maxDelay", new Slider("最大延迟", 75, 0, 250));
                    _Humanize = MiscMenu.Add("humanize", new CheckBox("开启", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                private static readonly CheckBox _useR;

                private static readonly CheckBox _blockAA;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return _useR.CurrentValue;
                    }
                }

                public static bool blockAA
                {
                    get
                    {
                        return _blockAA.CurrentValue;
                    }
                }

                static Combo()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("连招");
                    _useQ = ModesMenu.Add("comboQ", new CheckBox("使用 Q"));
                    _useW = ModesMenu.Add("comboW", new CheckBox("使用 W"));
                    _useE = ModesMenu.Add("comboE", new CheckBox("使用 E"));
                    _useR = ModesMenu.Add("comboR", new CheckBox("使用 R"));
                    _blockAA = ModesMenu.Add("blockAA", new CheckBox("连招时不A"));
                    _minMana = ModesMenu.Add("comboMana", new Slider("蓝量控制 %"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawHealth;

                private static readonly CheckBox _drawQ;

                private static readonly CheckBox _drawW;

                private static readonly CheckBox _drawE;

                private static readonly CheckBox _drawR;

                private static readonly CheckBox _drawReady;

                private static readonly CheckBox _drawStackStatus;

                public static bool DrawHealth
                {
                    get
                    {
                        return _drawHealth.CurrentValue;
                    }
                }

                public static bool DrawQ
                {
                    get
                    {
                        return _drawQ.CurrentValue;
                    }
                }

                public static bool DrawW
                {
                    get
                    {
                        return _drawW.CurrentValue;
                    }
                }

                public static bool DrawE
                {
                    get
                    {
                        return _drawE.CurrentValue;
                    }
                }

                public static bool DrawR
                {
                    get
                    {
                        return _drawR.CurrentValue;
                    }
                }

                public static bool DrawReady
                {
                    get
                    {
                        return _drawReady.CurrentValue;
                    }
                }

                public static bool DrawStackStatus
                {
                    get
                    {
                        return _drawStackStatus.CurrentValue;
                    }
                }

                public static Color colorHealth
                {
                    get
                    {
                        return DrawMenu.GetColor("colorHealth");
                    }
                }

                public static Color colorQ
                {
                    get
                    {
                        return DrawMenu.GetColor("colorQ");
                    }
                }

                public static Color colorW
                {
                    get
                    {
                        return DrawMenu.GetColor("colorW");
                    }
                }

                public static Color colorE
                {
                    get
                    {
                        return DrawMenu.GetColor("colorE");
                    }
                }

                public static Color colorR
                {
                    get
                    {
                        return DrawMenu.GetColor("colorR");
                    }
                }

                public static float _widthQ
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthQ");
                    }
                }

                public static float _widthW
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthW");
                    }
                }

                public static float _widthE
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthE");
                    }
                }

                public static float _widthR
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthR");
                    }
                }

                static Draw()
                {
                    DrawMenu.AddGroupLabel("显示");
                    _drawReady = DrawMenu.Add("drawReady", new CheckBox("仅在技能无冷却时显示范围.", false));
                    _drawHealth = DrawMenu.Add("drawHealth", new CheckBox("在敌人血条上显示伤害"));
                    _drawStackStatus = DrawMenu.Add("drawStackStatus", new CheckBox("显示叠层数状态"));
                    DrawMenu.AddColorItem("colorHealth");
                    DrawMenu.AddSeparator();
                    //Q
                    _drawQ = DrawMenu.Add("drawQ", new CheckBox("画出 Q"));
                    DrawMenu.AddColorItem("colorQ");
                    DrawMenu.AddWidthItem("widthQ");
                    //W
                    _drawW = DrawMenu.Add("drawW", new CheckBox("画出 W"));
                    DrawMenu.AddColorItem("colorW");
                    DrawMenu.AddWidthItem("widthW");
                    //E
                    _drawE = DrawMenu.Add("drawE", new CheckBox("画出 E"));
                    DrawMenu.AddColorItem("colorE");
                    DrawMenu.AddWidthItem("widthE");
                    //R
                    _drawR = DrawMenu.Add("drawR", new CheckBox("画出 R"));
                    DrawMenu.AddColorItem("colorR");
                    DrawMenu.AddWidthItem("widthR");
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                static Harass()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("骚扰");
                    _useQ = ModesMenu.Add("harassQ", new CheckBox("使用 Q"));
                    _minMana = ModesMenu.Add("harassMana", new Slider("蓝量控制"));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                private static readonly CheckBox _useR;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return _useR.CurrentValue;
                    }
                }

                static JungleClear()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("清野");
                    _useQ = ModesMenu.Add("jungleQ", new CheckBox("使用 Q"));
                    _useW = ModesMenu.Add("jungleW", new CheckBox("使用 W"));
                    _useE = ModesMenu.Add("jungleE", new CheckBox("使用 E"));
                    _useR = ModesMenu.Add("jungleR", new CheckBox("使用 R"));
                    _minMana = ModesMenu.Add("jungleMana", new Slider("蓝量控制"));
                }

                public static void Initialize()
                {
                }
            }

            public static class KillSteal
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                static KillSteal()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("抢人头");
                    _useQ = ModesMenu.Add("killQ", new CheckBox("使用 Q"));
                    _useW = ModesMenu.Add("killW", new CheckBox("使用 W"));
                    _useE = ModesMenu.Add("killE", new CheckBox("使用 E"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                private static readonly CheckBox _useR;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return _useR.CurrentValue;
                    }
                }

                static LaneClear()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("清兵");
                    _useQ = ModesMenu.Add("laneQ", new CheckBox("使用 Q"));
                    _useW = ModesMenu.Add("laneW", new CheckBox("使用 W"));
                    _useE = ModesMenu.Add("laneE", new CheckBox("使用 E"));
                    _useR = ModesMenu.Add("laneR", new CheckBox("使用 R"));
                    _minMana = ModesMenu.Add("laneMana", new Slider("蓝量控制"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                static LastHit()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("补兵最后一击");
                    _useQ = ModesMenu.Add("lastQ", new CheckBox("使用 Q"));
                    _minMana = ModesMenu.Add("lastMana", new Slider("蓝量控制"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly Slider _QCollision;

                private static readonly CheckBox _ChangeNames;

                private static readonly CheckBox _AutoWInterruptible;

                private static readonly CheckBox _AutoWGapCloser;

                public static List<string> EnemyNames, AllyNames;

                public static int QCollision
                {
                    get
                    {
                        return _QCollision.CurrentValue;
                    }
                }

                public static bool ChangeNames
                {
                    get
                    {
                        return _ChangeNames.CurrentValue;
                    }
                }

                public static bool AutoWInterruptible
                {
                    get
                    {
                        return _AutoWInterruptible.CurrentValue;
                    }
                }

                public static bool AutoWGapCloser
                {
                    get
                    {
                        return _AutoWGapCloser.CurrentValue;
                    }
                }

                static Misc()
                {
                    MiscMenu.AddGroupLabel("Miscellaneous");
                    _QCollision = MiscMenu.Add("QCollision", new Slider("Q碰撞: 总是Q向敌人", 0, 0, 1));
                    _ChangeNames = MiscMenu.Add("ChangeNames", new CheckBox("改变英雄名称【没鸡巴用】", false));
                    _AutoWGapCloser = MiscMenu.Add("AutoWGapCloser", new CheckBox("自动开启被动来使用W防止敌人接近"));
                    _AutoWInterruptible = MiscMenu.Add("AutoWInterruptible", new CheckBox("自动开启被动来使用W打断敌人技能"));

                    _QCollision.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                        {
                            switch (args.NewValue)
                            {
                                case 0:
                                    _QCollision.DisplayName = "QCollision: Always Q At Enemy";
                                    break;
                                case 1:
                                    _QCollision.DisplayName = "QCollision: Only Q If No Collision";
                                    break;
                            }
                        };
                    _ChangeNames.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                        {
                            if (args.NewValue == false)
                            {
                                foreach (var enemy in EntityManager.Heroes.Enemies)
                                {
                                    enemy.Name =
                                        EnemyNames[
                                            EntityManager.Heroes.Enemies.OrderBy(x => x.NetworkId)
                                                .ToList()
                                                .IndexOf(enemy)];
                                }
                                foreach (var ally in EntityManager.Heroes.Allies)
                                {
                                    ally.Name =
                                        AllyNames[
                                            EntityManager.Heroes.Enemies.OrderBy(x => x.NetworkId)
                                                .ToList()
                                                .IndexOf(ally)];
                                }
                            }
                            else
                            {
                                foreach (var enemy in EntityManager.Heroes.Enemies)
                                {
                                    enemy.Name = "Combo Me Pls";
                                }
                                foreach (var ally in EntityManager.Heroes.Allies)
                                {
                                    ally.Name = "Don't Let Me KS";
                                }
                                Player.Instance.Name = "Best Rice EB";
                            }
                        };
                }

                public static void Initialize()
                {
                }
            }

            public static class TearStack
            {
                private static readonly Slider _AutoStackMana;

                private static readonly Slider _MaxStacks;

                private static readonly Slider _StackTimer;

                private static readonly CheckBox _OnlyFountain;

                private static readonly KeyBind _AutoStackQ;

                public static int AutoStackMana
                {
                    get
                    {
                        return _AutoStackMana.CurrentValue;
                    }
                }

                public static bool AutoStackQ
                {
                    get
                    {
                        return _AutoStackQ.CurrentValue;
                    }
                }

                public static bool OnlyFountain
                {
                    get
                    {
                        return _OnlyFountain.CurrentValue;
                    }
                }

                public static int MaxStacks
                {
                    get
                    {
                        return _MaxStacks.CurrentValue;
                    }
                }

                public static int StackTimer
                {
                    get
                    {
                        return _StackTimer.CurrentValue;
                    }
                }

                static TearStack()
                {
                    MiscMenu.AddGroupLabel("自动叠水滴");
                    _AutoStackQ = MiscMenu.Add(
                        "自动叠水滴",
                        new KeyBind("热键", false, KeyBind.BindTypes.PressToggle, 'T'));

                    _AutoStackMana = MiscMenu.Add("AutoTearMana", new Slider("蓝量控制 %", 30));

                    _MaxStacks = MiscMenu.Add("MaxStacksTear", new Slider("控制层数于", 3, 1, 4));

                    _StackTimer = MiscMenu.Add("TearStackTimer", new Slider("使用Q间隔秒数", 5, 1, 10));

                    _OnlyFountain = MiscMenu.Add("OnlyFountain", new CheckBox("仅在泉水处叠水滴", false));
                }

                public static void Initialize()
                {
                }
            }

            private static readonly Menu ModesMenu, DrawMenu, MiscMenu;

            static Modes()
            {
                ModesMenu = Menu.AddSubMenu("Modes");

                Combo.Initialize();
                Menu.AddSeparator();
                Harass.Initialize();
                Menu.AddSeparator();
                LaneClear.Initialize();
                Menu.AddSeparator();
                LastHit.Initialize();

                MiscMenu = Menu.AddSubMenu("Misc");
                Misc.Initialize();
                Menu.AddSeparator();
                AutoStack.Initialize();
                Menu.AddSeparator();
                Humanizer.Initialize();
                //Menu.AddSeparator();
                //TearStack.Initialize();

                DrawMenu = Menu.AddSubMenu("Draw");
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }
        }

        private const string MenuName = "Rice";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Rice");
            Menu.AddLabel("10/10 with Darakath", 50);
            Menu.AddLabel("Thank you for your suggestions.", 50);

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }
    }
}