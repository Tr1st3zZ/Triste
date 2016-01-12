using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace TDVayne.Modules.General
{
    internal class ActivatorModule : ISOLOModule
    {
        private Item BOTRK = new Item((int) ItemId.Blade_of_the_Ruined_King, 450f);
        private Item Cutlass = new Item((int) ItemId.Bilgewater_Cutlass, 450f);
        private Item Youmuu = new Item((int) ItemId.Youmuus_Ghostblade);

        /// <summary>
        ///     Called when the module is loaded
        /// </summary>
        public void OnLoad()
        {
        }

        /// <summary>
        ///     Shoulds the module get executed.
        /// </summary>
        /// <returns></returns>
        public bool ShouldGetExecuted()
        {
            return (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                    ObjectManager.Player.HealthPercent < 10);
        }

        /// <summary>
        ///     Gets the type of the module.
        /// </summary>
        /// <returns></returns>
        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        /// <summary>
        ///     Called when the module is executed.
        /// </summary>
        public void OnExecute()
        {
            var target = Orbwalker.GetTarget();

            if (target is AIHeroClient && target.IsValidTarget(ObjectManager.Player.GetAutoAttackRange(target) + 125f))
            {
                if (target.IsValidTarget(450f))
                {
                    var targetHealth = target.HealthPercent;
                    var myHealth = ObjectManager.Player.HealthPercent;

                    if (myHealth < 50 && targetHealth > 20)
                    {
                        var spellSlot =
                            Player.Instance.InventoryItems.FirstOrDefault(a => a.Id == ItemId.Blade_of_the_Ruined_King);
                        if (spellSlot != null || Player.GetSpell(spellSlot.SpellSlot).IsReady)
                            Player.CastSpell(spellSlot.SpellSlot, (AIHeroClient) target);
                    }

                    if (targetHealth < 65)
                    {
                        var spellSlot =
                            Player.Instance.InventoryItems.FirstOrDefault(a => a.Id == ItemId.Bilgewater_Cutlass);
                        if (spellSlot != null || Player.GetSpell(spellSlot.SpellSlot).IsReady)
                            Player.CastSpell(spellSlot.SpellSlot, (AIHeroClient) target);
                    }
                }
                var spellSlott = Player.Instance.InventoryItems.FirstOrDefault(a => a.Id == ItemId.Youmuus_Ghostblade);
                if (spellSlott != null || Player.GetSpell(spellSlott.SpellSlot).IsReady)
                    Player.CastSpell(spellSlott.SpellSlot);
            }
        }
    }

    internal enum ItemType
    {
        OnAfterAA,
        OnUpdate
    }
}