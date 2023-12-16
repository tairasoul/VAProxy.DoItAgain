using BepInEx;
using HarmonyLib;
using UnityEngine;
using VAP_API;

namespace DoItAgain__
{
    [BepInPlugin("doitagain", "DoItAgain", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static AudioSource source;
        internal void Awake()
        {
            Logger.LogInfo("doitagain.awake");
            Harmony harmony = new Harmony("doitagainspucnhob");
            harmony.PatchAll();
        }

        internal void OnDestroy()
        {
            BundleLoader.LoadComplete += BundleLoader_LoadComplete;
        }

        private void BundleLoader_LoadComplete()
        {
            GameObject obj = new GameObject("Patrick.DoItAgain");
            DontDestroyOnLoad(obj);
            source = obj.AddComponent<AudioSource>();
            source.volume = 2;
            source.clip = BundleLoader.GetLoadedAsset<AudioClip>("assets/PatrickDoItAgain.mp3");
        }

        [HarmonyPatch(typeof(HomeScreen))]
        static class homescreen
        {
            [HarmonyPatch("Erase")]
            [HarmonyPrefix]
            static void erase()
            {
                source.Play();
            }
        }
    }
}
