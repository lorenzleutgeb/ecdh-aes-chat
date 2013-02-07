using System;
using System.Numerics;

namespace LeutgebAes.EllipticCurve
{
	public class Curve
	{
		private BigInteger q;
		private FieldElement a, b;
		
		public Curve(BigInteger q, BigInteger a, BigInteger b)
		{
			this.q = q;
			this.a = new FieldElement(q, a);
			this.b = new FieldElement(q, b);
		}
		
		public FieldElement A {
			get { return this.a; }
		}
		
		public FieldElement B {
			get { return this.b; }
		}

        public BigInteger Q
        {
            get { return q; }
        }

		public static bool operator ==(Curve a, Curve b)
		{
			return a.q == b.q && a.a == b.a && a.b == b.b;
		}

        public static bool operator !=(Curve a, Curve b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            return this == (Curve)obj;
        }

        public FieldElement GenerateFieldElement(BigInteger x)
        {
            return new FieldElement(q, x);
        }

        public Point Infinity {
            get { return new Point(this, null, null); }
        }

        public static Curve Dummy
        {
            get { return new Curve(BigInteger.Zero, BigInteger.Zero, BigInteger.Zero); }
        }

        public override int GetHashCode()
        {
            return a.GetHashCode() ^ b.GetHashCode() ^ q.GetHashCode();
        }
	}
}

