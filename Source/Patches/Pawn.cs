using HarmonyLib;
using RimWorld;
using Verse;

namespace SirRandoo.MSF.Patches
{
    [HarmonyPatch(typeof(Pawn), "TickRare")]
    public static class PawnTickRare
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        public static void TickRare(Pawn __instance)
        {
            if (__instance?.RaceProps?.IsMechanoid != true)
            {
                return;
            }

            if (!__instance.Spawned || __instance.Dead || __instance.Downed)
            {
                return;
            }

            if (__instance.stances?.stunner == null || __instance.stances.stunner.Stunned)
            {
                return;
            }

            if (__instance.Map?.GameConditionManager?.ConditionIsActive(DefDatabase<GameConditionDef>.GetNamed("SolarFlare")) != true)
            {
                return;
            }

            __instance.stances.stunner.Notify_DamageApplied(new DamageInfo(DamageDefOf.EMP, 74));
        }
    }
}
