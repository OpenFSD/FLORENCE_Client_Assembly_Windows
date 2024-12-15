﻿using FLORENCE.Frame.Cli.Dat.Out.Gfx;
using FLORENCE.Frame.Cli.Exe.Wrt;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat.Out
{
    public class Graphics : GameWindow
    {
        private int ElementBufferObject;
        private int VertexArrayObject;
        private int VertexBufferObject;

        private FLORENCE.Frame.Cli.Dat.Out.Gfx.Shader shader;

        private FLORENCE.Frame.Cli.Dat.Out.Gfx.Texture texture0;
        private FLORENCE.Frame.Cli.Dat.Out.Gfx.Texture texture1;

        // Get the mouse state
        MouseState mouse = null;

        private Matrix4 view;
        private Matrix4 projection;

        private static int nrAttributes;
        private static double time;

        public Graphics(OpenTK.Windowing.Desktop.GameWindowSettings gws, OpenTK.Windowing.Desktop.NativeWindowSettings nws) : base(
           gws,
           nws
        )
        {
            mouse = MouseState;
            System.Console.WriteLine("FLORENCE: Graphics & GameWindow");
        }

        ~Graphics()
        {

        }
        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(
                BufferTarget.ArrayBuffer,
                VertexBufferObject
            );
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfOutBufferWrite()).Get_Vertices().Length * sizeof(float),
                Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfOutBufferWrite()).Get_Vertices(),
                BufferUsageHint.StaticDraw
            );

            // draw square \/ \/ \/
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(
                BufferTarget.ElementArrayBuffer,
                ElementBufferObject
            );
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfOutBufferWrite()).Get_Indices().Length * sizeof(uint),
                Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfOutBufferWrite()).Get_Indices(),
                BufferUsageHint.StaticDraw
            );
            // draw square /\ /\ /\

            shader = new FLORENCE.Frame.Cli.Dat.Out.Gfx.Shader(
                "..\\..\\..\\gFX\\shader_vert.glsl",
                "..\\..\\..\\gFX\\shader_frag.glsl"
            );
            shader.Use();

            GL.VertexAttribPointer(
                0,
                3,
                VertexAttribPointerType.Float,
                false,
                5 * sizeof(float),
                0
            );
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(
                1,
                3,
                VertexAttribPointerType.Float,
                false,
                5 * sizeof(float),
                3 * sizeof(float)
            );
            GL.EnableVertexAttribArray(1);

            texture0 = FLORENCE.Frame.Cli.Dat.Out.Gfx.Texture.LoadFromFile("..\\..\\..\\resources\\Textures\\container.jpg");
            texture0.Use((OpenTK.Graphics.OpenGL4.TextureUnit)TextureUnit.Texture0);

            texture1 = FLORENCE.Frame.Cli.Dat.Out.Gfx.Texture.LoadFromFile("..\\..\\..\\resources\\Textures\\awesomeface.png");
            texture1.Use((OpenTK.Graphics.OpenGL4.TextureUnit)TextureUnit.Texture1);

            nrAttributes = 0;
            GL.GetInteger(GetPName.MaxVertexAttribs, out nrAttributes);
            Console.WriteLine("Maximum number of vertex attributes supported: " + nrAttributes);

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);


            //CursorState = CursorState.Grabbed;
            /*
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f), 
                Size.X / (float)Size.Y, 
                0.1f, 
                100.0f
            );
            */
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            time += 4.0 * e.Time;

            GL.Clear(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit
            );
            GL.BindVertexArray(VertexArrayObject);

            texture0.Use((OpenTK.Graphics.OpenGL4.TextureUnit)TextureUnit.Texture0);
            texture1.Use((OpenTK.Graphics.OpenGL4.TextureUnit)TextureUnit.Texture1);
            shader.Use();

            var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(time));
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetCamera().GetViewMatrix());
            shader.SetMatrix4("projection", Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetCamera().GetProjectionMatrix());

            /*
            // change colour with time \/ \/ \/
                                        float greenValue = Get_New_greenValue();
                                        int vertexColorLocation = GL.GetUniformLocation(shader.Get_Handle(), "ourColor");
                                        GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);
            // change colour with time /\ /\ /\
            */
            Framework.GetClient().GetData().GetMapDefault().Draw_Triangle();
            //Framework..Get_Client().Get_Data().Get_Map_Default().Draw_Square(Framework..Get_Client().Get_Data());

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteVertexArray(VertexArrayObject);

            GL.DeleteProgram(shader.Get_Handle());

            shader.Dispose();

            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Close();
            }

            if (KeyboardState.IsKeyDown(Keys.W))
            {
                Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Move_Fowards(e);
            }

            if (KeyboardState.IsKeyDown(Keys.S))
            {
                Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Move_Backwards(e);
                //
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Move_Left(e);
                //
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Move_Right(e);
                //
            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                //
            }
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                //
            }

            Framework.GetClient().GetData().Flip_InBufferToWrite();
            //set buffer(s) to default values


            if (Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Get_isFirstMove()) // This bool variable is initially set to true.
            {
                //TODO
                Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Set_isFirstMove(false);
                Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Set_isFirstMove(false);
            }
            else
            {
                Framework.GetClient().GetExecute().GetWriteEnable().Write_Start(
                    Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                    0,
                    Framework.GetClient().GetGlobal().Get_NumCores(),
                    Framework.GetClient().GetGlobal()
                );

                Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetInputControl().CheckBufferAnomalyInFlagArray();
                Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetInputControl().GenerateStackOfInputActions();
                //TODO
               
                Framework.GetClient().GetExecute().GetWriteEnable().Write_End(
                    Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                    0,
                   Framework.GetClient().GetGlobal().Get_NumCores(),
                    Framework.GetClient().GetGlobal()
                );
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetCamera().Fov -= e.OffsetY;
        }
        /*
public static float Get_New_greenValue()
{
periodOfRefresh += 0.0166666666666667;//period per frame - settings gws.UpdateFrequency = 60
if (periodOfRefresh == 2000) periodOfRefresh = 0;
return (float)Math.Sin(periodOfRefresh) / (2.0f + 0.5f);
}
*/
        public Vector2 GetNewMousePosition()
        {
            return new Vector2(mouse.Position.X, mouse.Position.Y);
        }
    }
}
