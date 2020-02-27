using HarmonyLib;

using RimWorld;

using Verse;

namespace SirRandoo.MSF.Patches
{
    [HarmonyPatch(typeof(Pawn), "TickRare")]
    public static class Pawn__TickRare
    {
        [HarmonyPostfix]
        public static void TickRare(Pawn __instance)
        {
            if(__instance == null) return;
            if(__instance.stances == null) return;
            if(__instance.stances.stunner == null) return;
            if(!__instance.Spawned) return;
            if(__instance.Dead) return;
            if(__instance.Downed) return;
            if(__instance.MapHeld == null) return;
            if(__instance.MapHeld.GameConditionManager == null) return;
            if(!__instance.RaceProps.IsMechanoid) return;
            if(__instance.stances.stunner.Stunned) return;
            if(!__instance.MapHeld.GameConditionManager.ConditionIsActive(GameConditionDefOf.SolarFlare)) return;

            __instance.stances.stunner.Notify_DamageApplied(new DamageInfo(DamageDefOf.EMP, 74), true);
        }
    }
}
