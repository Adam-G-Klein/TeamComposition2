using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace TeamComposition2.CardRoles
{
    /// <summary>
    /// MonoBehaviour that adds a role icon above a card.
    /// Attached automatically via Harmony patch.
    /// </summary>
    public class CardRoleIconMono : MonoBehaviour
    {
        private bool initialized = false;
        private GameObject iconObj;

        // Cached sprites loaded from asset bundle
        private static Sprite reticleSprite;
        private static Sprite shieldSprite;
        private static Sprite healSprite;
        private static bool spritesLoaded = false;

        // Role colors
        private static readonly Color AtkColor = new Color(1f, 0f, 0f, 1f);       // Bright red
        private static readonly Color TankColor = new Color(0f, 0.5f, 1f, 1f);    // Bright blue
        private static readonly Color HealColor = new Color(0f, 1f, 0f, 1f);      // Bright green

        private static void LoadSprites()
        {
            if (spritesLoaded) return;

            var asset = MyPlugin.asset;
            if (asset == null)
            {
                UnityEngine.Debug.LogWarning("[CardRoleIconMono] Asset bundle is null");
                return;
            }

            reticleSprite = asset.LoadAsset<Sprite>("reticle");
            shieldSprite = asset.LoadAsset<Sprite>("shield");
            healSprite = asset.LoadAsset<Sprite>("heal");

            if (reticleSprite == null) UnityEngine.Debug.LogWarning("[CardRoleIconMono] Failed to load reticle sprite");
            if (shieldSprite == null) UnityEngine.Debug.LogWarning("[CardRoleIconMono] Failed to load shield sprite");
            if (healSprite == null) UnityEngine.Debug.LogWarning("[CardRoleIconMono] Failed to load heal sprite");

            spritesLoaded = true;
        }

        private void Start()
        {
            if (initialized) return;
            initialized = true;

            LoadSprites();

            CardInfo cardInfo = GetComponent<CardInfo>();
            if (cardInfo == null) return;

            CardRole role = CardRoleManager.GetCardRole(cardInfo);

            // Only show icons for ATK, TANK, and HEAL roles
            if (role != CardRole.Atk && role != CardRole.Tank && role != CardRole.Heal)
                return;

            // Get the sprite and color for this role
            Sprite sprite = GetSpriteForRole(role);
            Color color = GetColorForRole(role);

            if (sprite == null) return;

            // Find the Canvas/Front container to add the icon to
            Transform frontTransform = transform.Find("Canvas/Front");
            if (frontTransform == null)
            {
                // Try searching children for "Front"
                RectTransform[] allChildren = gameObject.GetComponentsInChildren<RectTransform>();
                var frontRect = allChildren.FirstOrDefault(obj => obj.gameObject.name == "Front");
                if (frontRect != null)
                    frontTransform = frontRect.transform;
            }

            if (frontTransform == null)
            {
                UnityEngine.Debug.LogWarning("[CardRoleIconMono] Could not find Front transform");
                return;
            }

            // Create the icon GameObject
            iconObj = new GameObject("RoleIcon");
            iconObj.transform.SetParent(frontTransform, false);

            // Add RectTransform for UI positioning
            RectTransform rectTransform = iconObj.AddComponent<RectTransform>();

            // Position above the card content (top center)
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 0f);
            rectTransform.anchoredPosition = new Vector2(0f, 100f); // 100 units above the top
            rectTransform.sizeDelta = new Vector2(400f, 400f); // Icon size (5x scale)

            // Add Image component and set the sprite
            Image image = iconObj.AddComponent<Image>();
            image.sprite = sprite;
            image.color = color;
            image.preserveAspect = true;

            // Add helper to maintain position
            iconObj.AddComponent<SetLocalPosForRoleIcon>();
        }

        private Sprite GetSpriteForRole(CardRole role)
        {
            switch (role)
            {
                case CardRole.Atk:
                    return reticleSprite;
                case CardRole.Tank:
                    return shieldSprite;
                case CardRole.Heal:
                    return healSprite;
                default:
                    return null;
            }
        }

        private Color GetColorForRole(CardRole role)
        {
            switch (role)
            {
                case CardRole.Atk:
                    return AtkColor;
                case CardRole.Tank:
                    return TankColor;
                case CardRole.Heal:
                    return HealColor;
                default:
                    return Color.white;
            }
        }

        private void OnDestroy()
        {
            if (iconObj != null)
            {
                Destroy(iconObj);
            }
        }
    }

    /// <summary>
    /// Helper component to ensure the icon stays in the correct local position.
    /// </summary>
    internal class SetLocalPosForRoleIcon : MonoBehaviour
    {
        private RectTransform rectTransform;
        private readonly Vector2 targetPosition = new Vector2(0f, 100f);

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (rectTransform == null) return;

            if (rectTransform.anchoredPosition != targetPosition)
            {
                rectTransform.anchoredPosition = targetPosition;
            }
        }
    }

    /// <summary>
    /// Harmony patch to add CardRoleIconMono to all cards when they are instantiated.
    /// </summary>
    [HarmonyPatch(typeof(CardInfo), "Awake")]
    internal static class CardInfoIconAwakePatch
    {
        [HarmonyPostfix]
        private static void Postfix(CardInfo __instance)
        {
            // Add our role icon component if it doesn't already exist
            if (__instance.gameObject.GetComponent<CardRoleIconMono>() == null)
            {
                __instance.gameObject.AddComponent<CardRoleIconMono>();
            }
        }
    }
}
