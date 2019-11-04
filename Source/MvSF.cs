using System.Reflection;

using Harmony;

using Verse;

namespace SirRandoo.MSF
{
    public class MvSF : Mod
    {
        public static string ID => "Mechanoid v Solar Flare";
        internal static HarmonyInstance Harmony;

        public MvSF(ModContentPack content) : base(content)
        {
            Harmony = HarmonyInstance.Create("com.sirrandoo.mvsf");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message(string.Format("{0} :: Initialized!", ID));
        }
    }
}
