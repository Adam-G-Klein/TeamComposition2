using System.Linq;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace TeamComposition2.CardRoles
{
    /// <summary>
    /// MonoBehaviour that adds role text to a card.
    /// Attached automatically via Harmony patch.
    /// </summary>
    public class CardRoleTextMono : MonoBehaviour
    {
        private bool initialized = false;
        private GameObject roleTextObj;

        private void Start()
        {
            if (initialized) return;
            initialized = true;

            CardInfo cardInfo = GetComponent<CardInfo>();
            if (cardInfo == null) return;

            string roleAbbrev = CardRoleManager.GetRoleAbbreviation(cardInfo);

            // Find bottom left edge object (same location CustomCard uses for mod name)
            RectTransform[] allChildrenRecursive = gameObject.GetComponentsInChildren<RectTransform>();
            var edgeTransform = allChildrenRecursive.FirstOrDefault(obj => obj.gameObject.name == "EdgePart (2)");

            if (edgeTransform != null)
            {
                // Check if there's already a ModNameText object and remove/replace it
                Transform existingModNameText = edgeTransform.Find("ModNameText");
                if (existingModNameText != null)
                {
                    // Replace the existing mod name text with role text
                    TextMeshProUGUI existingText = existingModNameText.GetComponent<TextMeshProUGUI>();
                    if (existingText != null)
                    {
                        existingText.text = roleAbbrev;
                    }
                    return;
                }

                // Create new role text object
                roleTextObj = new GameObject("ModNameText");
                GameObject bottomLeftCorner = edgeTransform.gameObject;
                roleTextObj.transform.SetParent(bottomLeftCorner.transform);

                TextMeshProUGUI roleText = roleTextObj.AddComponent<TextMeshProUGUI>();
                roleText.text = roleAbbrev;
                roleTextObj.transform.localEulerAngles = new Vector3(0f, 0f, 135f);
                roleTextObj.transform.localScale = Vector3.one;
                roleTextObj.AddComponent<SetLocalPosForRoleText>();
                roleText.alignment = TextAlignmentOptions.Bottom;
                roleText.alpha = 0.1f;
                roleText.fontSize = 54;
            }
        }

        private void OnDestroy()
        {
            if (roleTextObj != null)
            {
                Destroy(roleTextObj);
            }
        }
    }

    /// <summary>
    /// Helper component to set the local position of the role text.
    /// Same approach as CustomCard uses.
    /// </summary>
    internal class SetLocalPosForRoleText : MonoBehaviour
    {
        private readonly Vector3 localpos = new Vector3(-50f, -50f, 0f);

        private void Update()
        {
            if (gameObject.transform.localPosition == localpos) return;
            gameObject.transform.localPosition = localpos;
            Destroy(this, 1f);
        }
    }

    /// <summary>
    /// Harmony patch to add CardRoleTextMono to all cards when they are instantiated.
    /// </summary>
    [HarmonyPatch(typeof(CardInfo), "Awake")]
    internal static class CardInfoAwakePatch
    {
        [HarmonyPostfix]
        private static void Postfix(CardInfo __instance)
        {
            // Add our role text component if it doesn't already exist
            if (__instance.gameObject.GetComponent<CardRoleTextMono>() == null)
            {
                __instance.gameObject.AddComponent<CardRoleTextMono>();
            }
        }
    }
}
