using TMPro;
using System;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Enhancements
{
    public class XLoader
    {
        private const string RESOURCE_PATH = "Enhancements.Resources.";
        private const string BUNDLE_PATH = RESOURCE_PATH + "enhancements3.asset";

        private readonly Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
        private static readonly Dictionary<string, TMP_FontAsset> _fonts = new Dictionary<string, TMP_FontAsset>();
        private static TMP_FontAsset _cachedTekoFont;

        public Shader ZFixTextShader { get; private set; }
        private bool _didLoad = false;

        public void Initialize()
        {
            if (_fonts.Count > 0)
            {
                return;
            }
            if (!_didLoad)
            {
                // Loading the asset bundle.
                AssetBundle bundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(BUNDLE_PATH));

                // Load Fonts
                _cachedTekoFont = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().LastOrDefault(f2 => f2.name == "Teko-Medium SDF");
                var fonts = bundle.LoadAllAssets<TMP_FontAsset>();
                for (int i = 0; i < fonts.Length; i++)
                {
                    var font = Setup(fonts[i]);
                    _fonts.Add(font.name.Split(new string[] { "SDF" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim(' '), font);
                }
                _didLoad = true;
            }
        }

        public Sprite GetIcon(string name)
        {
            string source = name + ".png";
            if (!_sprites.TryGetValue(source, out Sprite sprite))
            {
                var tex = BeatSaberMarkupLanguage.Utilities.FindTextureInAssembly(RESOURCE_PATH + source);
                tex.wrapMode = TextureWrapMode.Clamp;
                sprite = BeatSaberMarkupLanguage.Utilities.LoadSpriteFromTexture(tex, 300);
                if (sprite != null)
                {
                    _sprites.Add(source, sprite);
                }
            }
            return sprite;
        }

        public TMP_FontAsset GetFont(string name)
        {
            Initialize();
            _ = _fonts.TryGetValue(name, out TMP_FontAsset font);
            return font;
        }

        public string[] GetFontNames()
        {
            Initialize();
            return _fonts.Keys.ToArray();
        }

        private TMP_FontAsset Setup(TMP_FontAsset f)
        {
            var originalFont = _cachedTekoFont;
            
            var matCopy = UnityEngine.Object.Instantiate(originalFont.material);
            matCopy.mainTexture = f.material.mainTexture;
            matCopy.mainTextureOffset = f.material.mainTextureOffset;
            matCopy.mainTextureScale = f.material.mainTextureScale;
            f.material = matCopy;
            f = UnityEngine.Object.Instantiate(f);
            MaterialReferenceManager.AddFontAsset(f);
            return f;
        }
    }
}