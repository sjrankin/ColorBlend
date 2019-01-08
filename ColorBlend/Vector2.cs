using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend
{
    public class Vector2
    {
        public readonly double X;
        public readonly double Y;

        public Vector2 (double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Vector2 operator - (Vector2 A, Vector2 B)
        {
            return new Vector2(B.X - A.X, B.Y - A.Y);
        }

        public static Vector2 operator + (Vector2 A, Vector2 B)
        {
            return new Vector2(A.X + B.X, A.Y + B.Y);
        }

        public static Vector2 operator * (Vector2 A, double Op)
        {
            return new Vector2(A.X * Op, A.Y * Op);
        }

        public override string ToString ()
        {
            return string.Format("[{0},{1}]", X, Y);
        }
    }
}
