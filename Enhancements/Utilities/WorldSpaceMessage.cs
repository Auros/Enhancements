using TMPro;
using UnityEngine;

namespace Enhancements.Utilities
{
    public class WorldSpaceMessage : MonoBehaviour
    {
        public TextMeshPro _messagePrompt;

        public float Font
        {
            get => _messagePrompt.fontSize;
            set => _messagePrompt.fontSize = value;
        }

        public Color Color
        {
            get => _messagePrompt.color;
            set => _messagePrompt.color = value;
        }

        public string Text
        {
            get => _messagePrompt.text;
            set => _messagePrompt.text = value;
        }

        public TMP_FontAsset FontX
        {
            get => _messagePrompt.font;
            set => _messagePrompt.font = value;
        }

        public static WorldSpaceMessage Create(string text, Vector3 position, float fontSize = 10f)
        {
            var wsmgo = new GameObject("EnhancementsWorldSpaceMessage");
            var wsm = wsmgo.AddComponent<WorldSpaceMessage>();
            wsmgo.transform.position = position;
            wsm._messagePrompt = Utilities.Extensions.CreateWorldText(wsmgo.transform, text);
            wsm._messagePrompt.fontSize = fontSize;

            return wsm;
        }
    }
}
