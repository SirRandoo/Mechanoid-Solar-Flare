using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SirRandoo.MSF.Patches;

[HarmonyPatch]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class PawnTickRare
{
    private static readonly AccessTools.FieldRef<object, int> StunTicksLeftField = AccessTools.FieldRefAccess<int>(typeof(StunHandler), fieldName: "stunTicksLeft");
    private static readonly AccessTools.FieldRef<object, bool> StunByEmpField = AccessTools.FieldRefAccess<bool>(typeof(StunHandler), fieldName: "stunFromEMP");
    private static readonly AccessTools.FieldRef<object, bool> ShowStunMoteField = AccessTools.FieldRefAccess<bool>(typeof(StunHandler), fieldName: "showStunMote");

    public static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(Pawn), nameof(Pawn.TickRare));
    }

    // ReSharper disable once InconsistentNaming
    public static void Postfix(Pawn __instance)
    {
        if (__instance?.RaceProps is not { IsMechanoid: true }) return;
        if (!__instance.Spawned || __instance.Dead || __instance.Downed) return;
        if (__instance.stances == null || __instance.stances.stunner is { Stunned: true }) return;
        if (__instance.Map?.GameConditionManager == null) return;
        if (!__instance.Map.GameConditionManager.ConditionIsActive(MvSfDefOfs.SolarFlare)) return;

        // To work around mods, like Combat Extended, from damaging mechanoids to death, we'll
        // bypass regular checks to stun the mechanoid directly. This also has the side effect of
        // bypassing a mechanoid's natural resistance to EMP, and EMP-like, stuns.
        StunByEmpField(__instance.stances.stunner) = true;
        ShowStunMoteField(__instance.stances.stunner) = true;
        StunTicksLeftField(__instance.stances.stunner) = __instance.Map.GameConditionManager.GetActiveCondition<GameCondition_DisableElectricity>().TicksLeft;
    }
}
