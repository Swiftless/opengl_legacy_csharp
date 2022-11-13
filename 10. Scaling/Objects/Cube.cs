using System.Numerics;

namespace _10.Objects
{
    internal static class Cube
    {
        public static Dictionary<string, List<Vector3>> Vertices => new() {
            {
                "Front", new() {
                    new Vector3(-1, -1, -1),
                    new Vector3(1, -1, -1),
                    new Vector3(1, 1, -1),
                    new Vector3(-1, 1, -1)
                }
            },
            {
                "Back", new() {
                    new Vector3(-1, -1, 1),
                    new Vector3(1, -1, 1),
                    new Vector3(1, 1, 1),
                    new Vector3(-1, 1, 1)
                }
            },
            {
                "Left", new() {
                    new Vector3(-1, -1, -1),
                    new Vector3(-1, 1, -1),
                    new Vector3(-1, 1, 1),
                    new Vector3(-1, -1, 1)
                }
            },
            {
                "Right", new() {
                    new Vector3(1, -1, -1),
                    new Vector3(1, 1, -1),
                    new Vector3(1, 1, 1),
                    new Vector3(1, -1, 1)
                }
            },
            {
                "Top", new() {
                    new Vector3(-1, 1, -1),
                    new Vector3(1, 1, -1),
                    new Vector3(1, 1, 1),
                    new Vector3(-1, 1, 1)
                }
            },
            {
                "Bottom", new() {
                    new Vector3(-1, -1, -1),
                    new Vector3(1, -1, -1),
                    new Vector3(1, -1, 1),
                    new Vector3(-1, -1, 1)
                }
            },
        };

        public static Dictionary<string, Vector4> Colours => new() {
            {
                "Front",
                new Vector4(1, 0, 0, 0.8f)
            },
            {
                "Back",
                new Vector4(0, 1, 0, 0.5f)
            },
            {
                "Left",
                new Vector4(0, 0, 1, 0.2f)
            },
            {
                "Right",
                new Vector4(0, 1, 1, 0.5f)
            },
            {
                "Top",
                new Vector4(1, 1, 1, 0.2f)
            },
            {
                "Bottom",
                new Vector4(1, 0, 1, 0.8f)
            },
        };
    }
}
