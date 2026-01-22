using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace UncappedSpeedMod
{
    [BepInPlugin("com.renshei.uncapped", "Uncapped Speed", "1.1.3")]
    public class UncappedSpeedPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(SpeedCapPatch));
            Logger.LogInfo("speed cap successfully removed from ultrakill");
            Logger.LogInfo("visit BlueDevGH on GitHub for the source code!");
        }
    }

    [HarmonyPatch(typeof(NewMovement))]
    public class SpeedCapPatch
    {
        // patch upd method to constantly ensure cap for speed is increased
        // ensures broken cap upon level change or reload/restart/respawn/honkydoodle
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void RaiseSpeedLimit(NewMovement __instance)
        {
            // set to 1000 to "remove" speed cap (just set it really high so it is never reached)
            
            __instance.walkSpeed = 5000f;

            if (__instance.rb != null)
            {
                __instance.rb.drag = 100f;
            }
            
        }
    }
}

// this is my first real program/mod using C# so if anything breaks, please give me some time to learn!
