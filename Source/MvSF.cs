using System.Reflection;

using HarmonyLib;

using Verse;

namespace SirRandoo.MSF
{
    public class MvSF : Mod
    {
        internal static Harmony Harmony;

        public MvSF(ModContentPack content) : base(content)
        {
            Harmony = new Harmony("com.sirrandoo.mvsf");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message(string.Format("{0} :: Initialized!", ID));
        }

        public static string ID => "Mechanoid v Solar Flare";
    }
}
