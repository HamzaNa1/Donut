using System;
using HConsole;

namespace Donut
{
    public class Donut
    {
        private const float theta_spacing = 0.07f;
        private const float phi_spacing = 0.02f;

        private const float R1 = 1;
        private const float R2 = 2;
        private const float K2 = 5;

        private readonly int screen_width;
        private readonly  int screen_height;
        private readonly float K1;

        public Donut(int screen_width, int screen_height)
        {
            this.screen_width = screen_width;
            this.screen_height = screen_height;
            K1 = screen_width * K2 * 3 / (8 * (R1 + R2));
        } 
        
        public void RenderFrame(float A, float B)
        {
            float cosA = MathF.Cos(A);
            float sinA = MathF.Sin(A);

            float cosB = MathF.Cos(B);
            float sinB = MathF.Sin(B);

            char[,] output = new char[screen_width, screen_height];
            float[,] zbuffer = new float[screen_width, screen_height];

            for (var i = 0; i < screen_width; i++)
            {
                for (var j = 0; j < screen_height; j++)
                {
                    output[i, j] = ' ';
                    zbuffer[i, j] = 0;
                }
            }

            for (var theta = 0f; theta < 2f * MathF.PI; theta += theta_spacing)
            {
                float costheta = MathF.Cos(theta);
                float sintheta = MathF.Sin(theta);

                for (var phi = 0f; phi < 2f * MathF.PI; phi += phi_spacing)
                {
                    float cosphi = MathF.Cos(phi);
                    float sinphi = MathF.Sin(phi);

                    float circlex = R2 + R1 * costheta;
                    float circley = R1 * sintheta;

                    float x = circlex * (cosB * cosphi + sinA * sinB * sinphi) - circley * cosA * sinB;
                    float y = circlex * (sinB * cosphi - sinA * cosB * sinphi) + circley * cosA * cosB;
                    float z = K2 + cosA * circlex * sinphi + circley * sinA;
                    float ooz = 1f / z;

                    int xp = (int) (screen_width / 2f + K1 * ooz * x);
                    int yp = (int) (screen_height / 2f - K1 * ooz * y);

                    float L = cosphi * costheta * sinB - cosA * costheta * sinphi - sinA * sintheta +
                              cosB * (cosA * sintheta - costheta * sinA * sinphi);

                    if (L > 0)
                    {
                        if (ooz > zbuffer[xp, yp])
                        {
                            zbuffer[xp, yp] = ooz;
                            int luminance_index = (int) (L * 8);
                            
                            output[xp, yp] = ".,-~:;=!*#$@"[luminance_index];
                        }
                    }
                }
            }
            
            MyConsole.ClearBuffer();
            for (int j = 0; j < screen_height; j++) {
                for (int i = 0; i < screen_width; i++) {
                    MyConsole.Write(output[i,j]);
                }
                MyConsole.WriteLine();
            }
            
            MyConsole.UpdateWindow();
        }
    }
}