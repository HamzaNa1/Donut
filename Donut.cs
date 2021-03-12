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
        private readonly int screen_height;
        private readonly float K1;


        private float ts => theta_spacing;
        private float ps => phi_spacing;
        private int w => screen_width;
        private int h => screen_height;

        public Donut(int screen_width, int screen_height)
        {
            this.screen_width = screen_width;
            this.screen_height = screen_height;
            K1 = screen_width * K2 * 3 / (8 * (R1 + R2));
        } 
        
        public void RenderFrame(float A, float B)
        {
            float cosA = cos(A);
            float sinA = sin(A);

            float cosB = cos(B);
            float sinB = sin(B);

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
                float costheta = cos(theta);
                float sintheta = sin(theta);

                for (var phi = 0f; phi < 2f * MathF.PI; phi += phi_spacing)
                {
                    float cosphi = cos(phi);
                    float sinphi = sin(phi);

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

        private float cosa, sina, cosb, sinb, cost, sint, cosp, sinp, cx, cy, x, y, z, ooz, l, t, p;
        private int xp, yp, li, i, j;
        private char[,] o;
        private float[,] zb;

        public void RenderFrameDonutShaped(float A, float B)
        {     

                            cosa=cos(A);//
                     sina=sin(A);cosb=/***/
                   cos(B);sinb=sin(B);o=/**/
                 new char[w,h];zb=new float[w,h]
              ;for(i=0;i<w;i++){for(j=0;j<h;j++){o[i,j]=' ';zb[i,j]=0;}}
            for(t=0f;t<2f*MathF.PI;t+=ts){cost=cos(t);sint=sin(t);for(p=0f;p<2f*MathF.PI;p+=ps){cosp=cos(p);sinp=sin(p);cx=R2+R1*cost;cy=R1*sint;
            x=cx*(cosb*cosp+sina*sinb*sinp)-cy*cosa*sinb;y=cx*(sinb*cosp-sina*cosb*sinp)+cy*cosa*cosb;z=K2+cosa*cx*sinp+cy*sina;ooz=1f/z;xp=(int)(w/2f+K1*ooz*x);
            yp=(int)(h/2f-K1*ooz*y);l=cosp*cost*sinb-cosa*cost*sinp-sina*sint+cosb*(cosa*sint-cost*sina*sinp);if(l>0){if(ooz>zb[xp,yp]){zb[xp,yp]=ooz;li=(int)(l*8);
            o[xp,yp]=".,-~:;=!*#$@"[li];}}}}MyConsole.ClearBuffer();for(j=0;j<h;j++){for(i=0;i<w;i++){MyConsole.Write(o[i,j]);}MyConsole.WriteLine();}MyConsole.UpdateWindow();
            /*********************/
        }

        private float cos(float x) => MathF.Cos(x);
        private float sin(float x) => MathF.Sin(x);
        private void clr() => MyConsole.ClearBuffer();
        private void wrt(char c) => MyConsole.Write(c);
        private void updt() => MyConsole.UpdateWindow();

    }
}