using System.Collections.Generic;
using HarmonyLib;
using SML;
using System;

using Game.Interface;
using TMPro;
using UnityEngine.UI;

namespace CustomCrossOut
{

    [SML.Mod.SalemMod]
    public class CustomCrossOut
    {

        public void Start()
        {
            try
            {
                Harmony.CreateAndPatchAll(typeof(CustomCrossOut));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("common foxfox W");
        }
    }

    [HarmonyPatch(typeof(RoleListItem), "ValidateCrossOut")]
    class ValidateCrossOut_Patch 
    {
        public static bool Prefix(ref RoleListItem __instance)
        {
            if (__instance.isCrossedOut)
			{
                int crossedOutOpacity = ModSettings.GetInt("Crossed Out Opacity %");
                bool addStrikethrough = ModSettings.GetBool("Strikethrough Line");

                if (addStrikethrough) {
				    __instance.roleLabel.GetComponent<TMP_Text>().fontStyle |= FontStyles.Strikethrough;
                }

				__instance.roleLabel.GetComponent<TMP_Text>().alpha = crossedOutOpacity / 100f;
			}
            else {
                __instance.roleLabel.GetComponent<TMP_Text>().fontStyle &= ~FontStyles.Strikethrough;
                __instance.roleLabel.GetComponent<TMP_Text>().alpha = 1.0f;
            }
            return false;
        }
    }

}
