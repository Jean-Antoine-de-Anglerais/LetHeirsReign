﻿using ai.behaviours;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace LetHeirsReign_NativeModloader
{
    internal class Main : MonoBehaviour
    {
        public static Harmony harmony = new Harmony(MethodBase.GetCurrentMethod().DeclaringType.Namespace);
        private bool _initialized = false;

        public void Update()
        {
            if (global::Config.gameLoaded && !_initialized)
            {
                harmony.Patch(AccessTools.Method(typeof(Clan), "fitToRule"),
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), "fitToRule_Transpiler")));

                _initialized = true;
            }
        }
    }
}
