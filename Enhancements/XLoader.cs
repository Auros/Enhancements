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
        private readonly Dictionary<string, TMP_FontAsset> _fonts = new Dictionary<string, TMP_FontAsset>();
        private TMP_FontAsset _cachedTekoFont;

        public Shader ZFixTextShader { get; private set; }

        public static string GetGameObjectPath(GameObject obj)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }
            return path;
        }

        public void Initialize()
        {
            _fonts.Clear();
            // Loading the asset bundle.
            AssetBundle bundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(BUNDLE_PATH));

            // Load Custom Text Shader
            var shader = bundle.LoadAsset<Shader>("Assets/TextMesh Pro/Resources/Shaders/TMP_SDF ZFix.shader");
            ZFixTextShader = shader;// UnityEngine.Object.Instantiate(shader);

            // Load Fonts
            _cachedTekoFont = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().LastOrDefault(f2 => f2.name == "Teko-Medium SDF No Glow");
            var fonts = bundle.LoadAllAssets<TMP_FontAsset>();
            for (int i = 0; i < fonts.Length; i++)
            {
                var font = Setup(fonts[i]);
                //if (font != null)
                //{
                    _fonts.Add(font.name.Split(new string[] { "SDF" }, StringSplitOptions.RemoveEmptyEntries)[0], font);
                //}
            }

            bundle.Unload(true);
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

        public void GetFonts()
        {
            Initialize();
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