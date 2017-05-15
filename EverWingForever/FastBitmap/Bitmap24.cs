using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace EverWingForever
{
    public class Bitmap24 : FastBitmap
    {
        private bool isOrig;

        private Bitmap24(Bitmap img, bool orig)
        {
            Rectangle bounds = new Rectangle(Point.Empty, img.Size);
            bmp = img;
            bitsPerPixel = (byte)Image.GetPixelFormatSize(bmp.PixelFormat);
            isOrig = orig;
        }

        public static Bitmap24 FromImage(Bitmap img)
        {
            return new Bitmap24(img, true);
        }

        public static Bitmap24 FromImageCopy(Bitmap img)
        {
            Bitmap imgCopy = new Bitmap(img);
            return new Bitmap24(imgCopy, false);
        }

        public override int[] GetPixel(int x, int y)
        {
            int index = GetIndex(x, y);
            return (new int[] { data[index + 2], data[index + 1], data[index] });
        }

        public override Color GetColor(int x, int y)
        {
            int index = GetIndex(x, y);
            return (Color.FromArgb(data[index + 2], data[index + 1], data[index]));
        }

        public override bool SetPixel(int x, int y, int a, int r, int g, int b)
        {
            return (SetPixel(x, y, (byte)r, (byte)g, (byte)b));
        }

        public bool SetPixel(int x, int y, byte r, byte g, byte b)
        {
            int index = GetIndex(x, y);
            data[index] = b;
            data[index + 1] = g;
            data[index + 2] = r;
            return true;
        }

        public override double CorrelationWith(FastBitmap fb)
        {
            return (CorrelationWith((Bitmap24)fb));
        }

        public double CorrelationWith(Bitmap24 b)
        {
            double avg1 = 0;
            double avg2 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                avg1 += data[i];
                avg2 += b.data[i];
            }
            avg1 /= data.Length;
            avg2 /= b.data.Length;
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                double d1 = data[i] - avg1;
                double d2 = b.data[i] - avg2;
                double prod = d1 * d2;
                sum1 += prod;
                sum2 += (d1 * d1);
                sum3 += (d2 * d2);
            }
            double r = sum1 / Math.Sqrt(sum2 * sum3);
            return r;
        }

        public static double CalculateLuminance(int[] px)
        {
            return (0.2126 * px[0]) + (0.7152 * px[1]) + (0.0722 * px[2]);
        }

        public override bool SameAs(FastBitmap fb)
        {
            return (SameAs((Bitmap24)fb));
        }

        public bool SameAs(Bitmap24 b)
        {
            return (Bitmap24.Same(this, b));
        }

        public static bool Same(Bitmap24 b1, Bitmap24 b2)
        {
            for (int i = 0; i < b1.data.Length; i++)
            {
                if (b1.data[i] != b2.data[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool ReplaceColor(int a1, int r1, int g1, int b1, int a2, int r2, int g2, int b2, int a3 = 0, int r3 = 0, int g3 = 0, int b3 = 0, bool replaceEqual = true)
        {
            return (ReplaceColor((byte)r1, (byte)g1, (byte)b1, (byte)r2, (byte)g2, (byte)b2, (byte)r3, (byte)g2, (byte)b3, replaceEqual));
        }

        // slight inefficiency due to the XOR
        public bool ReplaceColor(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2, byte r3 = 0, byte g3 = 0, byte b3 = 0, bool replaceEqual = true)
        {
            int step = bitsPerPixel / 8;
            for (int i = 0; i < data.Length; i += step)
            {
                if (!replaceEqual ^ (Math.Abs(data[i] - b1) < b3 && Math.Abs(data[i + 1] - g1) < g3 && Math.Abs(data[i + 2] - r1) < r3))
                {
                    data[i] = b2;
                    data[i + 1] = g2;
                    data[i + 2] = r2;
                }
            }
            return true;
        }

        public static new int[] ColorToArray(Color c)
        {
            return (new int[] { c.R, c.G, c.B });
        }

        public static new bool PixelEqual(int[] p1, int[] p2, int[] p3 = null)
        {
            if (p3 == null)
                return (p1[0] == p2[0] && p1[1] == p2[1] && p1[2] == p2[2]);
            else
                return (Math.Abs(p1[0] - p2[0]) <= p3[0] && Math.Abs(p1[1] - p2[1]) <= p3[1] && Math.Abs(p1[2] - p2[2]) <= p3[2]);
        }

        public static bool PixelEqual(Color c1, Color c2, int[] p3 = null)
        {
            return PixelEqual(new int[] { c1.R, c1.G, c1.B }, new int[] { c2.R, c2.G, c2.B }, p3);
        }

        public override bool MissingColor(int[] color, int[] threshold)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (!Bitmap24.PixelEqual(color, this.GetPixel(x, y), threshold))
                        return false;
                }
            }
            return true;
        }

        public override bool HasColor(int[] color, int[] threshold)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (Bitmap24.PixelEqual(color, this.GetPixel(x, y), threshold))
                        return true;
                }
            }
            return false;
        }

        public override int CountColor(int[] color, int[] threshold)
        {
            int count = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (Bitmap24.PixelEqual(color, this.GetPixel(x, y), threshold))
                        count++;
                }
            }
            return count;
        }

        public override void Dispose()
        {
            if (locked)
            {
                this.Unlock();
            }
            if (!isOrig)
            {
                bmp.Dispose();
            }
        }
    }
}