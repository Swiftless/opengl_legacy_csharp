using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL.Legacy;
using Silk.NET.Windowing;

using System.Numerics;

namespace _17;

public class Program
{
    private static IWindow window;
    private static GL openGLApi;

    private static float sceneRotationAngle = 0.0f;
    private static float sceneTranslationY = 0.0f;
    private static bool movingUp = true;

    private static uint textureReference;

    private static void Main(string[] args)
    {
        var windowOptions = WindowOptions.Default;
        windowOptions.API = new GraphicsAPI(ContextAPI.OpenGL, new APIVersion(2, 1));
        windowOptions.Position = new Vector2D<int>(100, 100);
        windowOptions.Size = new Vector2D<int>(500, 500);
        windowOptions.Title = "Your first OpenGL Window";

        window = Window.Create(windowOptions);

        window.Load += OnLoad;
        window.Update += OnUpdate;
        window.Render += OnRender;
        window.Resize += OnResize;

        window.Closing += OnClosing;

        window.Run();
    }

    private static void OnLoad()
    {
        openGLApi = GL.GetApi(window);

        // Load in our texture
        textureReference = openGLApi.GenTexture();
        openGLApi.BindTexture(GLEnum.Texture2D, textureReference);

        openGLApi.TexEnv(GLEnum.TextureEnv, GLEnum.TextureEnvMode, (int)GLEnum.Modulate);

        openGLApi.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Linear);
        openGLApi.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Linear);

        openGLApi.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GLEnum.Repeat);
        openGLApi.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GLEnum.Repeat);

        var data = new Vector3[] {
            new Vector3(255, 255, 255), // White
            new Vector3(255, 0, 0), // Red
            new Vector3(0, 255, 0), // Green
            new Vector3(0, 0, 255), // Blue
        };

        // 2 pixels x 2 pixels
        openGLApi.TexImage2D<Vector3>(GLEnum.Texture2D, 0, InternalFormat.Rgb, 2, 2, 0, GLEnum.Rgb, GLEnum.Float, data);

        // Iterate over all keyboards and bind to key down event
        IInputContext input = window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += OnKeyDown;
        }

        OnResize(window.Size);
    }

    private static void OnRender(double obj)
    {
        openGLApi.Enable(EnableCap.DepthTest);
        openGLApi.Enable(EnableCap.Texture2D);
        openGLApi.Enable(EnableCap.TextureGenS);
        openGLApi.Enable(EnableCap.TextureGenT);

        openGLApi.ClearColor(0.0f, 0.5f, 1.0f, 1.0f);
        openGLApi.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        openGLApi.LoadIdentity();

        openGLApi.Translate(0.0f, sceneTranslationY, -5.0f); // Push eveything 5 units back into the scene, otherwise we won't see the primitive
        openGLApi.Rotate(sceneRotationAngle, 1, 1, 0); // Rotate everything -60 degrees on the X and Y axis, to show off multiple sides of the cube

        openGLApi.Scale(2.0f, 2.0f, 2.0f);

        openGLApi.BindTexture(GLEnum.Texture2D, textureReference);

        openGLApi.Begin(GLEnum.Quads); // Start drawing a quad primitive

        foreach (var side in Objects.Square.Vertices)
        {
            var textureCoordinates = Objects.Square.UVs[side.Key];
            for (int vertex = 0; vertex < side.Value.Count; vertex++)
            {
                openGLApi.TexCoord2(textureCoordinates[vertex].X, textureCoordinates[vertex].Y);
                openGLApi.Vertex3(side.Value[vertex].X, side.Value[vertex].Y, side.Value[vertex].Z);
            }
        }

        openGLApi.End();

        openGLApi.Flush();
    }

    private static void OnUpdate(double obj)
    {
        if (sceneRotationAngle > 360f)
            sceneRotationAngle = 0f;
        else
            sceneRotationAngle += 0.5f;

        if (sceneTranslationY > 4f)
        {
            movingUp = false;
            sceneTranslationY = 4f;
        }
        else if (sceneTranslationY < -4f)
        {
            movingUp = true;
            sceneTranslationY = -4f;
        }
        else
        {
            if (movingUp)
                sceneTranslationY += 0.05f;
            else
                sceneTranslationY -= 0.05f;
        }
    }

    private static void OnResize(Vector2D<int> windowSize)
    {
        openGLApi.Viewport(0, 0, (uint)windowSize.X, (uint)windowSize.Y);
        openGLApi.MatrixMode(GLEnum.Projection);

        var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView((MathF.PI / 180f) * 60f, windowSize.X / (float)windowSize.Y, 0.1f, 100.0f);

        openGLApi.LoadMatrix(new float[] {
            projectionMatrix.M11, projectionMatrix.M12, projectionMatrix.M13, projectionMatrix.M14,
            projectionMatrix.M21, projectionMatrix.M22, projectionMatrix.M23, projectionMatrix.M24,
            projectionMatrix.M31, projectionMatrix.M32, projectionMatrix.M33, projectionMatrix.M34,
            projectionMatrix.M41, projectionMatrix.M42, projectionMatrix.M43, projectionMatrix.M44
        });

        openGLApi.MatrixMode(GLEnum.Modelview);
    }

    private static void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
    {
        if (key == Key.Escape)
        {
            window.Close();
        }
    }

    private static void OnClosing()
    {
        openGLApi.DeleteTexture(textureReference);
    }
}