using BepInEx;
using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;
using BepInEx.KeyboardShortcut;

namespace UncappedSpeedMod
{
    [BepInPlugin("com.renshei.uncapped", "Uncapped Speed", "1.1.3")]
    public class UncappedSpeedPlugin : BaseUnityPlugin
    {
        // make the logger a field so other methods (like Update) can use it
        private ManualLogSource speedLogs;

        public void Awake()
        {
            speedLogs = new ManualLogSource("speedLogs");
            BepInEx.Logging.Logger.Sources.Add(speedLogs);
            Harmony.CreateAndPatchAll(typeof(SpeedCapPatch));
            Logger.LogInfo("speed cap successfully removed from ultrakill");
            Logger.LogInfo("visit BlueDevGH on GitHub for the source code!");
        }

        // unity Update runs every frame to keep input handling here
        public void Update()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    speedLogs.LogInfo("U key pressed");
                }
            }
            catch (System.Exception ex)
            {
                speedLogs.LogError($"err with {ex}");
            }
        }
    }

    [HarmonyPatch(typeof(NewMovement))]
    public class SpeedCapPatch
    {
        // patch upd method to constantly ensure cap for speed is increased
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void RaiseSpeedLimit(NewMovement __instance)
        {
            // set to a very high value to effectively "remove" the speed cap
            __instance.walkSpeed = 1000f;

            if (__instance.rb != null)
            {
                __instance.rb.drag = 0f;
            }
        }
    }
}
