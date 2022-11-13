using System.Numerics;

namespace _13.Lighting
{
    internal static class Materials
    {
        public static Vector3 BlankMaterial => new Vector3(0, 0, 0);
        public static Vector3 RedDiffuseMaterial => new Vector3(1, 0, 0);
        public static Vector3 WhiteSpecularMaterial => new Vector3(1, 1, 1);
        public static Vector3 GreenEmissiveMaterial => new Vector3(0, 1, 0);
    }
}
