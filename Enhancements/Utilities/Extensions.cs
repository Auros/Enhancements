using Enhancements.Settings;
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
    }
}
