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

        private readonly Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
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
                _cachedTekoFont = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().LastOrDefault(f2 => f2.name == "Teko-Medium SDF No Glow");
                var fonts = bundle.LoadAllAssets<TMP_FontAsset>();
                for (int i = 0; i < fonts.Length; i++)
                {
                    var font = Setup(fonts[i]);
                    _fonts.Add(font.name.Split(new string[] { "SDF" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim(' '), font);
                }
                _didLoad = true;
            }


        }

        public Texture2D GetIcon(string name)
        {
            string source = name + ".png";
            if (!_textures.TryGetValue(source, out Texture2D texture))
            {
                texture = BeatSaberMarkupLanguage.Utilities.FindTextureInAssembly(RESOURCE_PATH + source);
                if (texture != null)
                {
                    _textures.Add(source, texture);
                }
            }
            return texture;
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