using System.Collections.Generic;
using HarmonyLib;
using SML;
using System;

using Game.Interface;
using TMPro;
using UnityEngine.UI;

namespace CustomCrossOut {
    [SML.Mod.SalemMod]
    public class CustomCrossOut {
        public void Start() {
            try {
                Harmony.CreateAndPatchAll(typeof(CustomCrossOut));
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("common foxfox W");
        }
    }

    [HarmonyPatch(typeof(RoleListItem), "ValidateCrossOut")]
    class ValidateCrossOut_Patch {
        public static bool Prefix(ref RoleListItem __instance) {
            TMP_Text roleLabelText = __instance.roleLabel.GetComponent<TMP_Text>();

            if (__instance.isCrossedOut) {
                int crossedOutOpacity = ModSettings.GetInt("Crossed Out Roles Opacity %");
                bool addStrikethrough = ModSettings.GetBool("Strikethrough Crossed Out Roles");
                bool addItalics = ModSettings.GetBool("Italicize Crossed Out Roles");
                int fontSize = ModSettings.GetInt("Crossed Out Roles size");

                if (addStrikethrough) {
                    roleLabelText.fontStyle |= FontStyles.Strikethrough;
                }

                if (addItalics) {
                    roleLabelText.fontStyle |= FontStyles.Italic;
                }

                roleLabelText.alpha = crossedOutOpacity / 100f;
                roleLabelText.fontSizeMin = fontSize;
                roleLabelText.fontSizeMax = fontSize;
            }
            else {
                roleLabelText.fontStyle &= ~FontStyles.Strikethrough;
                roleLabelText.fontStyle &= ~FontStyles.Italic;
                roleLabelText.alpha = 1.0f;
                roleLabelText.fontSizeMin = 25;
                roleLabelText.fontSizeMax = 25;
            }
            return false;
        }
    }

}
