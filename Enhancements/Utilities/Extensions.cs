using Enhancements.Settings;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace Enhancements.Utilities
{
    public static class Extensions
    {
        public static Vector3 ToVector3(this Float3 float3) => new Vector3()
        {
            x = float3.x,
            y = float3.y,
            z = float3.z
        };

        public static Float3 ToFloat3(this Vector3 vector3) => new Float3()
        {
            x = vector3.x,
            y = vector3.y,
            z = vector3.z
        };

        public static Color ToColor(this Color4 color4) => new Color()
        {
            r = color4.r,
            g = color4.g,
            b = color4.b,
            a = color4.a
        };

        public static Color4 ToColor4(Color color) => new Color4()
        {
            r = color.r,
            g = color.g,
            b = color.b,
            a = color.a
        };

        private static Shader _customTextShader;
        public static Shader CustomTextShader
        {
            get
            {
                if (_customTextShader == null)
                {
                    AssetBundle assetBundle = AssetBundle.LoadFromStream(Assembly.GetCallingAssembly().GetManifestResourceStream("Enhancements.Utilities.Shader.asset"));
                    _customTextShader = assetBundle.LoadAsset<Shader>("Assets/TextMesh Pro/Resources/Shaders/TMP_SDF_ZeroAlphaWrite_ZWrite.shader");
                    assetBundle.Unload(true);
                }
                return _customTextShader;
            }
        }

        private static AssetBundle _assets = null;
        private static AssetBundle FontAssets
        {
            get
            {
                if (!_assets)
                    _assets = AssetBundle.LoadFromMemory(BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Utilities.Fonts"));
                return _assets;
            }
        }

        public static string GetPath(this Transform current)
        {
            if (current.parent == null)
                return "/" + current.name;
            return current.parent.GetPath() + "/" + current.name;
        }


        private static TMP_FontAsset _arcadePix;
        public static TMP_FontAsset ArcadePix
        {
            get
            {
                if (_arcadePix == null)
                {
                    _arcadePix = FontAssets.LoadAsset<TMP_FontAsset>("ArcadePix SDF").Setup();
                }
                return _arcadePix;
            }
        }

        private static TMP_FontAsset _assistant;
        public static TMP_FontAsset Assistant
        {
            get
            {
                if (_assistant == null)
                {
                    _assistant = FontAssets.LoadAsset<TMP_FontAsset>("Assistant SDF").Setup();
                }
                return _assistant;
            }
        }

        private static TMP_FontAsset _BLACKMETAL;
        public static TMP_FontAsset BLACKMETAL
        {
            get
            {
                if (_BLACKMETAL == null)
                {
                    _BLACKMETAL = FontAssets.LoadAsset<TMP_FontAsset>("BLACK METAL SDF").Setup();
                }
                return _BLACKMETAL;
            }
        }

        private static TMP_FontAsset _caveat;
        public static TMP_FontAsset Caveat
        {
            get
            {
                if (_caveat == null)
                {
                    _caveat = FontAssets.LoadAsset<TMP_FontAsset>("Caveat SDF").Setup();
                }
                return _caveat;
            }
        }

        private static TMP_FontAsset _comicSans;
        public static TMP_FontAsset ComicSans
        {
            get
            {
                if (_comicSans == null)
                {
                    _comicSans = FontAssets.LoadAsset<TMP_FontAsset>("Comic Sans SDF").Setup();
                }
                return _comicSans;
            }
        }

        private static TMP_FontAsset _literallyNatural;
        public static TMP_FontAsset LiterallyNatural
        {
            get
            {
                if (_literallyNatural == null)
                {
                    _literallyNatural = FontAssets.LoadAsset<TMP_FontAsset>("Literally Natural SDF").Setup();
                }
                return _literallyNatural;
            }
        }

        private static TMP_FontAsset _minecraftEnchantment;
        public static TMP_FontAsset MinecraftEnchantment
        {
            get
            {
                if (_minecraftEnchantment == null)
                {
                    _minecraftEnchantment = FontAssets.LoadAsset<TMP_FontAsset>("Minecraft Enchantment SDF").Setup();
                }
                return _minecraftEnchantment;
            }
        }

        private static TMP_FontAsset _minecrafter3;
        public static TMP_FontAsset Minecrafter3
        {
            get
            {
                if (_minecrafter3 == null)
                {
                    _minecrafter3 = FontAssets.LoadAsset<TMP_FontAsset>("Minecrafter 3 SDF").Setup();
                }
                return _minecrafter3;
            }
        }

        private static TMP_FontAsset _minecraftia;
        public static TMP_FontAsset Minecraftia
        {
            get
            {
                if (_minecraftia == null)
                {
                    _minecraftia = FontAssets.LoadAsset<TMP_FontAsset>("Minecraftia SDF").Setup();
                }
                return _minecraftia;
            }
        }

        private static TMP_FontAsset _permanentMarker;
        public static TMP_FontAsset PermanentMarker
        {
            get
            {
                if (_permanentMarker == null)
                {
                    _permanentMarker = FontAssets.LoadAsset<TMP_FontAsset>("PermanentMarker SDF").Setup();
                }
                return _permanentMarker;
            }
        }

        private static TMP_FontAsset _spicyRice;
        public static TMP_FontAsset SpicyRice
        {
            get
            {
                if (_spicyRice == null)
                {
                    _spicyRice = FontAssets.LoadAsset<TMP_FontAsset>("SpicyRice SDF").Setup();
                }
                return _spicyRice;
            }
        }

        private static TMP_FontAsset Setup(this TMP_FontAsset f)
        {
            var originalFont = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Last(f2 => f2.name == "Teko-Medium SDF No Glow");
            var matCopy = Object.Instantiate(originalFont.material);
            matCopy.mainTexture = f.material.mainTexture;
            matCopy.mainTextureOffset = f.material.mainTextureOffset;
            matCopy.mainTextureScale = f.material.mainTextureScale;
            f.material = matCopy;
            f = Object.Instantiate(f);
            MaterialReferenceManager.AddFontAsset(f);
            return f;
        }

        public static TextMeshPro CreateWorldText(this Transform parent, string text = "TEXT")
        {
            GameObject textMeshGO = new GameObject("EnhancementsWSText");

            textMeshGO.SetActive(false);

            TextMeshPro textMesh = textMeshGO.AddComponent<TextMeshPro>();
            TMP_FontAsset font = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(x => x.name == "Teko-Medium SDF No Glow"));
            textMesh.renderer.sharedMaterial = font.material;
            textMesh.fontSharedMaterial = font.material;
            textMesh.font = font;

            textMesh.transform.SetParent(parent, false);
            textMesh.text = text;
            textMesh.fontSize = 5f;
            textMesh.color = Color.white;
            textMesh.renderer.material.shader = CustomTextShader;
            textMesh.rectTransform.sizeDelta = new Vector2(0f, 0f);
            textMesh.gameObject.SetActive(true);
            textMesh.alignment = TextAlignmentOptions.Center;
            return textMesh;
        }
    }
}
