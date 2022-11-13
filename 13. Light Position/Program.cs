using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL.Legacy;
using Silk.NET.Windowing;

using System.Numerics;

namespace _13;

public class Program
{
    private static IWindow window;
    private static GL openGLApi;

    private static float sceneRotationAngle = 60.0f;

    private static float lightPositionAngle = 0.0f;
    private static float lightX = 0.0f;
    private static float lightY = 0.0f;

    private static bool specularOn = false;
    private static bool diffuseOn = true;
    private static bool emissiveOn = false;

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

        window.Run();
    }

    private static void OnLoad()
    {
        openGLApi = GL.GetApi(window);

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

        openGLApi.Enable(EnableCap.Lighting);
        openGLApi.Enable(EnableCap.Light0);

        openGLApi.Light(GLEnum.Light0, GLEnum.Specular, new float[] { Lighting.Lights.WhiteSpecularLight.X, Lighting.Lights.WhiteSpecularLight.Y, Lighting.Lights.WhiteSpecularLight.Z });
        openGLApi.Light(GLEnum.Light0, GLEnum.Ambient, new float[] { Lighting.Lights.BlackAmbientLight.X, Lighting.Lights.BlackAmbientLight.Y, Lighting.Lights.BlackAmbientLight.Z });
        openGLApi.Light(GLEnum.Light0, GLEnum.Diffuse, new float[] { Lighting.Lights.WhiteDiffuseLight.X, Lighting.Lights.WhiteDiffuseLight.Y, Lighting.Lights.WhiteDiffuseLight.Z });

        openGLApi.ClearColor(0.0f, 0.5f, 1.0f, 1.0f);
        openGLApi.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        openGLApi.LoadIdentity();

        // Position around 0,0 instead of the offset and rotation applied to the cube
        openGLApi.Light(GLEnum.Light0, GLEnum.Position, new float[] { lightX, lightY, 0f });

        openGLApi.Translate(0.0f, 0.0f, -5.0f); // Push eveything 5 units back into the scene, otherwise we won't see the primitive  
        openGLApi.Rotate(sceneRotationAngle, 1, 1, 0); // Rotate everything -60 degrees on the X and Y axis, to show off multiple sides of the sube

        if (specularOn)
        {
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Specular, new float[] { Lighting.Materials.WhiteSpecularMaterial.X, Lighting.Materials.WhiteSpecularMaterial.Y, Lighting.Materials.WhiteSpecularMaterial.Z });
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Shininess, 0.5f);
        }
        else
        {
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Specular, new float[] { Lighting.Materials.BlankMaterial.X, Lighting.Materials.BlankMaterial.Y, Lighting.Materials.BlankMaterial.Z });
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Shininess, 0f);
        }

        if (diffuseOn)
        {
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Diffuse, new float[] { Lighting.Materials.RedDiffuseMaterial.X, Lighting.Materials.RedDiffuseMaterial.Y, Lighting.Materials.RedDiffuseMaterial.Z });
        }
        else
        {
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Diffuse, new float[] { Lighting.Materials.BlankMaterial.X, Lighting.Materials.BlankMaterial.Y, Lighting.Materials.BlankMaterial.Z });
        }

        if (emissiveOn)
        {
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Emission, new float[] { Lighting.Materials.GreenEmissiveMaterial.X, Lighting.Materials.GreenEmissiveMaterial.Y, Lighting.Materials.GreenEmissiveMaterial.Z });
        }
        else
        {
            openGLApi.Material(GLEnum.FrontAndBack, GLEnum.Emission, new float[] { Lighting.Materials.BlankMaterial.X, Lighting.Materials.BlankMaterial.Y, Lighting.Materials.BlankMaterial.Z });
        }

        openGLApi.Begin(GLEnum.Quads); // Start drawing a quad primitive  

        foreach (var side in Objects.Cube.Vertices)
        {
            var normal = Objects.Cube.Normals[side.Key];
            openGLApi.Normal3(normal.X, normal.Y, normal.Z);

            foreach (var vertex in side.Value)
            {
                openGLApi.Vertex3(vertex.X, vertex.Y, vertex.Z);
            }
        }

        openGLApi.End();

        openGLApi.Flush();
    }

    private static void OnUpdate(double obj) {
        if (lightPositionAngle > 360f)
            lightPositionAngle = 0f;
        
        lightPositionAngle += 0.05f;

        lightX = MathF.Cos(lightPositionAngle) * 10.0f;
        lightY = MathF.Sin(lightPositionAngle) * 10.0f;
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

        if (key == Key.W)
        {
            window.Position = new Vector2D<int>(100, 50);
            window.Size = new Vector2D<int>(500, 600);
        }

        if (key == Key.S)
        {
            window.Position = new Vector2D<int>(100, 100);
            window.Size = new Vector2D<int>(500, 500);
        }

        if (key == Key.Z)
        {
            specularOn = !specularOn;
        }

        if (key == Key.X)
        {
            diffuseOn = !diffuseOn;
        }

        if (key == Key.C)
        {
            emissiveOn = !emissiveOn;
        }
    }
}