using TMPro;
using System;
using Zenject;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Enhancements
{
    public class ELoader : IInitializable
    {
        private const string BUNDLE_PATH = "Enhancements.Resources.enhancements3.asset";

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
            // Loading the asset bundle.
            AssetBundle bundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(BUNDLE_PATH));

            // Load Custom Text Shader
            var shader = bundle.LoadAsset<Shader>("Assets/TextMesh Pro/Resources/Shaders/TMP_SDF ZFix.shader");
            ZFixTextShader = shader;// UnityEngine.Object.Instantiate(shader);

            // Load Fonts
            _cachedTekoFont = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Last(f2 => f2.name == "Teko-Medium SDF No Glow");
            var fonts = bundle.LoadAllAssets<TMP_FontAsset>();
            for (int i = 0; i < fonts.Length; i++)
            {
                var font = Setup(fonts[i]);
                _fonts.Add(font.name.Split(new string[] { "SDF" }, StringSplitOptions.RemoveEmptyEntries)[0], font);
                
            }

            bundle.Unload(true);
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