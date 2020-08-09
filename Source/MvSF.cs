using System.Reflection;

using HarmonyLib;

using Verse;

namespace SirRandoo.MSF
{
    // ReSharper disable once InconsistentNaming
    public class MvSF : Mod
    {
        public MvSF(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("com.sirrandoo.mvsf");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
