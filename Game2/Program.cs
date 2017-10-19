//using OpenGL;
using Pencil.Gaming;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Threading;
using Pencil.Gaming.Graphics;
using System.Diagnostics;

namespace Game2
{
    class Program
    {
        int fps = 0;
        int good, bad, half = 0;
        static void Main(string[] args)
        {
            new Program().init();
        }  
        
        private void fps_Tick(object sender)
        {

            Console.Title = "FPS: " + fps + "   \t" + (ElapsedTime * (1.0 / Stopwatch.Frequency) * 1000).ToString().Substring(0, 15) + "ms | +" + good + " / ?" + half + " / -" + bad  + " / " + fps + " = " + (good + bad + half);
            fps = 0;
            good = 0;
            bad = 0;
            half = 0;
        }
        
        long StartingTime;
        long EndingTime;
        long ElapsedTime;
        float r = 5, g = 5, b = 5;
        private void init()
        {
            Timer checkTimer = new System.Threading.Timer(fps_Tick,
                               null, 1000, 1000);
            if (!Glfw.Init())
                Environment.Exit(-1);
            var window = Glfw.CreateWindow(640, 480, "Hello World", new GlfwMonitorPtr(), new GlfwWindowPtr());
            Glfw.MakeContextCurrent(window);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            
            while (!Glfw.WindowShouldClose(window))
            {
                StartingTime = Stopwatch.GetTimestamp();
                fps++;
                // Render here
                //  GL.Clear(ClearBufferMask.ColorBufferBit);
                
                // Swap front and back buffers
                Glfw.SwapBuffers(window);
               
                // Poll for and process events
                Glfw.PollEvents();

                // clear everything

                //Thread.Sleep(1);
                r += (new Program().randFloat(0.0f, 0.2f) - 0.1f) % 0.1f;
                r = Math.Abs(r % 1);
                g += (new Program().randFloat(0.0f, 0.2f) - 0.1f) % 0.1f;
                g = Math.Abs(g % 1);
                b += (new Program().randFloat(0.0f, 0.2f) - 0.1f) % 0.1f;
                b = Math.Abs(b % 1);
                
                if ((r == g && r == b && g == b))
                {
                    Console.WriteLine("!R=G=B! R: " + r + " G: " + g + " B: " + b);
                    bad++;
                }
                else if (!(r == g && r == b && g == b))
                {
                    Console.Write("\r+R=G=B! R: " + (r + "    ").Substring(0, 5) + " G: " + (g + "    ").Substring(0, 5) + " B: " + (b + "    ").Substring(0, 5) + "\t\t\t\t\t\t\t\t");
                    good++;
                }
                else
                {
                    Console.WriteLine("?R=G=B! R: " + r + " G: " + g + " B: " + b);
                    half++;
                }
                
                GL.ClearColor(r, g, b,0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                //   OpenGL.              // Thread.Sleep(1);
                EndingTime = Stopwatch.GetTimestamp();
                ElapsedTime = EndingTime - StartingTime;

            }

            Glfw.Terminate();
            
            
        }
        public float randFloat(float min, float max)
        {
            return (float)new Random((int)DateTime.Now.Ticks).Next((int)(min * 100), (int)(max * 100)) / 100f;
        }

    }
}
