using System.Numerics;

namespace _16.Objects
{
    internal static class Square
    {
        public static Dictionary<string, List<Vector3>> Vertices => new() {
            {
                "Front", new() {
                    new Vector3(-1, -1, -1),
                    new Vector3(1, -1, -1),
                    new Vector3(1, 1, -1),
                    new Vector3(-1, 1, -1)
                }
            }
        };

        public static Dictionary<string, List<Vector2>> UVs => new() {
            {
                /////////////////////////////////////////
                ///
                ///   This is how texture coordinates are arranged
                ///
                ///   0,1  x —–- x  1,1
                ///        |     |
                ///        |     |
                ///        |     |
                ///   0,0  x —–- x  1,0
                ///
                /////////////////////////////////////////
                "Front", new() {
                    new Vector2(0, 1),
                    new Vector2(1, 1),
                    new Vector2(1, 0),
                    new Vector2(0, 0)
                }
            }
        };
    }
}
