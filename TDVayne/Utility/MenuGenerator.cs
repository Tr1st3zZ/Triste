using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace TDVayne.Utility
{
    internal class MenuGenerator
    {
        public static Menu ComboMenu, HarassMenu, FarmMenu, QMenu, EMenu, MiscMenu;

        /// <summary>
        ///     The main menu
        /// </summary>
        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuGenerator" /> class.
        /// </summary>
        public MenuGenerator()
        {
            if (Variables.MenuPrincipal == null)
            {
                Variables.MenuPrincipal = MainMenu.AddMenu("[TD]薇恩", "td.vayne");
            }
        }

        /// <summary>
        ///     Generates the menu.
        /// </summary>
        public void GenerateMenu()
        {
            ComboMenu = Variables.MenuPrincipal.AddSubMenu("连招设置");
            ComboMenu.Add("useqcombo", new CheckBox("使用 Q"));
            ComboMenu.Add("useecombo", new CheckBox("使用 E"));
            ComboMenu.Add("usercombo", new CheckBox("释放R后自动Q"));

            HarassMenu = Variables.MenuPrincipal.AddSubMenu("骚扰设置");
            StringList(HarassMenu, "TDVaynemixedmode", "骚扰模式", new[] {"被动的", "侵略性的"}, 1);
            HarassMenu.Add("useqharass", new CheckBox("使用 Q"));
            HarassMenu.Add("useeharass", new CheckBox("使用 E"));

            FarmMenu = Variables.MenuPrincipal.AddSubMenu("刷钱设置");
            FarmMenu.Add("useqfarm", new CheckBox("使用 Q"));
            FarmMenu.Add("TDVaynelaneclearcondemnjungle", new CheckBox("E墙野怪"));

            QMenu = Variables.MenuPrincipal.AddSubMenu("Q 设置");
            QMenu.Add("TDVaynemisctumblenoqintoenemies", new CheckBox("不向敌人之中Q"));
            QMenu.Add("TDVaynemisctumbleqks", new CheckBox("使用Q击杀"));
            QMenu.Add("TDVaynemisctumblesmartQ", new CheckBox("使用单挑Q逻辑"));

            EMenu = Variables.MenuPrincipal.AddSubMenu("E 设置");
            EMenu.Add("TDVaynemisccondemnautoe", new CheckBox("自动 E"));
            EMenu.Add("TDVaynemisccondemncurrent", new CheckBox("仅E当前攻击目标"));
            EMenu.Add("TDVaynemisccondemnsave", new CheckBox("保护自己"));

            MiscMenu = Variables.MenuPrincipal.AddSubMenu("杂项设置");
            MiscMenu.Add("TDVaynemiscmiscellaneousantigapcloser", new CheckBox("防止敌人接近【使用E】"));
            MiscMenu.Add("TDVaynemiscmiscellaneousinterrupter", new CheckBox("打断敌人技能【使用E】"));
            MiscMenu.Add("TDVaynemiscmiscellaneousnoaastealth", new CheckBox("隐身时不A【消失立马A】"));
            MiscMenu.Add("TDVaynemiscmiscellaneousdelay", new Slider("防止敌人接近 / 打断敌人技能-延迟", 300, 0, 1000));
        }

        public static void StringList(Menu menu, string uniqueId, string displayName, string[] values, int defaultValue)
        {
            var mode = menu.Add(uniqueId, new Slider(displayName, defaultValue, 0, values.Length - 1));
            mode.DisplayName = displayName + ": " + values[mode.CurrentValue];
            mode.OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                {
                    sender.DisplayName = displayName + ": " + values[args.NewValue];
                };
        }
    }
}