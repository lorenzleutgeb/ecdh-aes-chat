using System;
using System.Numerics;

namespace LeutgebAes.EllipticCurve
{
	public class Point
	{
        private FieldElement x, y;
        private BigInteger z;
        private BigInteger? zInv = null;
        private Curve curve;

        public Point(Curve curve, FieldElement x, FieldElement y) : this(curve, x, y, BigInteger.One)
        {
        }

        public Point(Curve curve, FieldElement x, FieldElement y, BigInteger z)
        {
            this.curve = curve;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public FieldElement X {
            get {
                if (zInv == null) zInv = Tools.ModularInverse(z, curve.Q);
                return new FieldElement(curve.Q, (x * (BigInteger)zInv) % curve.Q);
            }
        }

        public FieldElement Y
        {
            get
            {
                if (zInv == null) zInv = Tools.ModularInverse(z, curve.Q);
                return new FieldElement(curve.Q, (y * (BigInteger)zInv) % curve.Q);
            }
        }

        public static Point Dummy
        {
            get { return new Point(Curve.Dummy, FieldElement.Dummy, FieldElement.Dummy); }
        }

        public bool IsInfinity
        {
            get {
                if(x == null && y == null) return true;
                return z == BigInteger.Zero && y != BigInteger.Zero;
            }
        }

        public static bool operator ==(Point a, Point b)
        {
            if ((object)b == null) return (object)a == null;
            if ((object)a == null) return (object)b == null;

            if (a.IsInfinity) return b.IsInfinity;
            if (b.IsInfinity) return a.IsInfinity;

            BigInteger u, v;
            // u = Y2 * Z1 - Y1 * Z2
            u = ((b.y * a.z) - (a.y * b.z)) % (a.curve.Q);
            if (u != BigInteger.Zero) return false;
            // v = X2 * Z1 - X1 * Z2
            v = ((b.x * a.z) - (a.x * (b.z)))  % (a.curve.Q);
            return v == BigInteger.Zero;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static Point operator ~(Point p)
        {
            return new Point(p.curve, p.x, ~p.y, p.z);
        }

        public static Point operator +(Point a, Point b)
        {
            if (a.IsInfinity) return b;
            if (b.IsInfinity) return a;

            var ax = (b.y * a.z);
            var bx = (a.y * b.z);
            var test = ax - bx;
            var u = Tools.SafeMod(test, a.curve.Q);
            var v = Tools.SafeMod((b.x * a.z) - (a.x * b.z), a.curve.Q);

            if (BigInteger.Zero == v)
                return BigInteger.Zero == u ? a * 2 : a.curve.Infinity;

            var v2 = v * v;
            var v3 = v2 * v;
            var x1v2 = a.x * v2;
            var zu2 = u * u * a.z;

            BigInteger x3 = Tools.SafeMod((((zu2 - (x1v2 << 1)) * b.z) - v3) * v, a.curve.Q);
            BigInteger y3 = Tools.SafeMod(((x1v2 * 3 * u) - (a.y * v3) - (zu2 * u)) * b.z + (u * v3), a.curve.Q);
            BigInteger z3 = Tools.SafeMod(v3 * a.z * b.z, a.curve.Q);

            return new Point(a.curve, new FieldElement(a.curve.Q, x3), new FieldElement(a.curve.Q, y3), z3);
        }

        public Point Twice() {
            if (this.IsInfinity) return this;
            if (this.y == BigInteger.Zero) return this.curve.Infinity;

            BigInteger y1z1 = y * z;
            BigInteger y1sqz1 = (y1z1 * y) % curve.Q;
            BigInteger w = x.Value * x.Value * 3;

            if (curve.A != BigInteger.Zero)
                w = Tools.SafeMod(w + z * z * curve.A, curve.Q);

            BigInteger x3 = Tools.SafeMod(((((w * w) - ((x.Value << 3) * y1sqz1)) << 1) * y1z1), curve.Q);
            BigInteger y3 = Tools.SafeMod(((((3 * w * x) - ((y1sqz1) << 1)) << 2) * y1sqz1) - (w * w * w), curve.Q);

            return new Point(curve, new FieldElement(curve.Q, x3), new FieldElement(curve.Q, y3), ((y1z1 * y1z1 * y1z1) << 3) % curve.Q);
        }

        public static Point operator *(Point p, BigInteger k) {
            if (p.IsInfinity || k == 1) return p;
            if (k.IsZero) return p.curve.Infinity;
            if (k == 2) return p.Twice();

            Point s = ~p, r = p;
            byte[] h = (k * 3).ToByteArray(), e = k.ToByteArray();
            bool check;

            // Initialize i to point at highest bit.
            int i = h.Length * 8 - 2;

            // Decrease i until it points to the first set bit.
            while (!Tools.IsSet(h, i--));

            do
            {
                r *= 2;

                if ((check = Tools.IsSet(h, i)) != Tools.IsSet(e, i))
                    r += check ? p : s;
            }
            while (--i > 0);

            return r;
        }

        public override string ToString()
        {
            return "[" + X.ToString() + " " +  Y.ToString() + "]";
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            return this == (Point)obj;
        }
	}
}
