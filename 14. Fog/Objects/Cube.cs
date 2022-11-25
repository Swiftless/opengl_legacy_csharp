using System.Numerics;

namespace _14.Objects
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

        public static Dictionary<string, Vector3> Colours => new() {
            {
                "Front",
                new Vector3(1, 0, 0)
            },
            {
                "Back",
                new Vector3(0, 1, 0)
            },
            {
                "Left",
                new Vector3(0, 0, 1)
            },
            {
                "Right",
                new Vector3(0, 1, 1)
            },
            {
                "Top",
                new Vector3(1, 1, 1)
            },
            {
                "Bottom",
                new Vector3(1, 0, 1)
            },
        };
    }
}
