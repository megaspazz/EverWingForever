using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace EverWingForever
{
    public abstract class FastBitmap : IDisposable
    {
        protected Bitmap bmp;
        protected BitmapData bmpData;
        protected byte[] data;
        protected byte bitsPerPixel;

        public abstract int[] GetPixel(int x, int y);
        public abstract Color GetColor(int x, int y);
        public abstract bool SetPixel(int x, int y, int a, int r, int g, int b);
        public abstract double CorrelationWith(FastBitmap fb);
        public abstract bool SameAs(FastBitmap fb);

        protected bool locked;

        public bool Lock()
        {
            if (locked) { return false; }
            Rectangle bmpRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            PixelFormat pxf = bmp.PixelFormat;
            bmpData = bmp.LockBits(bmpRect, ImageLockMode.ReadWrite, pxf);
            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(pxf);
            int size = bmpData.Stride * bmpData.Height;
            data = new byte[size];
            Marshal.Copy(bmpData.Scan0, data, 0, size);
            locked = true;
            return true;
        }

        public bool Unlock()
        {
            if (!locked) { return false; }
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            bmp.UnlockBits(bmpData);
            locked = false;
            return true;
        }

        public byte[] Data
        {
            get { return data; }
        }

        public int GetIndex(int x, int y)
        {
            return (y * bmpData.Stride + x * (bitsPerPixel / 8));
        }

        public Bitmap Bitmap
        {
            get { return bmp; }
        }

        // slight inefficiency due to the XOR
        public virtual bool ReplaceColor(int a1, int r1, int g1, int b1, int a2, int r2, int g2, int b2, int a3 = 0, int r3 = 0, int g3 = 0, int b3 = 0, bool replaceEqual = true)
        {
            int[] tol = { a3, r3, g3, b3 };
            if (replaceEqual)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        if (!replaceEqual ^ PixelEqual(GetPixel(x, y), new int[] { a2, r2, g2, b2 }, tol))
                        {
                            SetPixel(x, y, a2, r2, g2, b2);
                        }
                    }
                }
            }
            return true;
        }

        public static bool PixelEqual(int[] c1, int[] c2, int[] tol = null)
        {
            for (int i = 0; i < c1.Length; i++)
            {
                if (tol == null)
                {
                    if (c1[i] != c2[i])
                    {
                        return false;
                    }
                }
                else
                {
                    if (Math.Abs(c1[i] - c2[i]) <= tol[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int[] ColorToArray(Color c)
        {
            return (new int[] { c.A, c.R, c.G, c.B });
        }

        public bool SolidColor()
        {
            int[] col = GetPixel(0, 0);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (!PixelEqual(col, GetPixel(x, y)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /**
         * Doesn't include the alpha value yet...
         */
        public void ToGrayScale(double rf = 0.299, double gf = 0.587, double bf = 0.114)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    int[] col = GetPixel(x, y);
                    double gray = col[1] * rf + col[2] * gf + col[3] * bf;
                    int gs = (int)gray;
                    SetPixel(x, y, 255, gs, gs, gs);
                }
            }
        }

        public virtual bool MissingColor(int[] color, int[] threshold)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (!FastBitmap.PixelEqual(color, this.GetPixel(x, y), threshold))
                        return false;
                }
            }
            return true;
        }

        public virtual bool HasColor(int[] color, int[] threshold)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (FastBitmap.PixelEqual(color, this.GetPixel(x, y), threshold))
                        return true;
                }
            }
            return false;
        }

        public int[] GetPixel(Point pt)
        {
            return GetPixel(pt.X, pt.Y);
        }

        public virtual Color GetColor(Point pt)
        {
            return GetColor(pt.X, pt.Y);
        }

        public virtual int CountColor(int[] color, int[] threshold)
        {
            int count = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (FastBitmap.PixelEqual(color, this.GetPixel(x, y), threshold))
                        count++;
                }
            }
            return count;
        }

        public virtual void Dispose()
        {
            if (locked)
            {
                this.Unlock();
            }
            bmp.Dispose();
        }
    }
}