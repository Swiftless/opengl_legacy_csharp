using Silk.NET.Maths;
using Silk.NET.OpenGL.Legacy;
using Silk.NET.Windowing;

namespace _1;

public class Program
{
    private static IWindow window;
    private static GL openGLApi;

    private static void Main(string[] args)
    {
        var windowOptions = WindowOptions.Default;
        windowOptions.Position = new Vector2D<int>(100, 100);
        windowOptions.Size = new Vector2D<int>(500, 500);
        windowOptions.Title = "Your first OpenGL Window";

        window = Window.Create(windowOptions);

        window.Load += OnLoad;
        window.Update += OnUpdate;
        window.Render += OnRender;

        window.Run();
    }

    private static void OnLoad()
    {
        openGLApi = GL.GetApi(window);
    }

    private static void OnRender(double obj)
    {
        openGLApi.ClearColor(0.0f, 0.5f, 1.0f, 1.0f);
        openGLApi.Clear(ClearBufferMask.ColorBufferBit);

        openGLApi.LoadIdentity();

        openGLApi.Flush();
    }

    private static void OnUpdate(double obj) { }
}