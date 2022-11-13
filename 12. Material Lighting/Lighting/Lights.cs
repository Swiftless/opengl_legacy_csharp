using System.Numerics;

namespace _12.Lighting
{
    internal static class Lights
    {
        public static Vector3 BlackAmbientLight => new Vector3(0, 0, 0);
        public static Vector3 WhiteDiffuseLight => new Vector3(1, 1, 1);
        public static Vector3 WhiteSpecularLight => new Vector3(1, 1, 1);
    }
}
