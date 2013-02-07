using System;
using System.Numerics;

namespace LeutgebAes.EllipticCurve
{
    public class FieldElement
    {
        private BigInteger x, q;

        public FieldElement(BigInteger q, BigInteger x)
        {
            this.x = x;
            this.q = q;
        }

        public BigInteger ToBigInteger()
        {
            return this.x;
        }

        public BigInteger Value
        {
            get { return this.x; }
        }

        public static implicit operator BigInteger(FieldElement f)
        {
            return f.Value;
        }

        public static bool operator ==(FieldElement a, FieldElement b)
        {
            if ((object)b != null ^ (object)a != null)
                return false;

            return a.x == b.x && a.q == b.q;
        }

        public static bool operator !=(FieldElement a, FieldElement b)
        {
            return a.x != b.x || a.q != b.q;
        }

        public static FieldElement operator ~(FieldElement o)
        {
            return new FieldElement(o.q, Tools.SafeMod(~o.x + BigInteger.One, o.q));
        }

        public static FieldElement operator +(FieldElement a, FieldElement b)
        {
            return new FieldElement(a.q, (a.x + b.x) % a.q);
        }
        
        public static FieldElement operator -(FieldElement a, FieldElement b) {
            return new FieldElement(a.q, (a.x - b.x) % a.q);
        }

        public static FieldElement operator *(FieldElement a, FieldElement b) {
            return new FieldElement(a.q, (a.x * b.x) % a.q);
        }

        public static FieldElement operator /(FieldElement a, FieldElement b) {
            return new FieldElement(a.q, (a.x * b.ModularInverse()) % a.q);
        }

        private BigInteger ModularInverse()
        {
            return Tools.ModularInverse(x, q);
        }

        public static FieldElement Dummy
        {
            get { return new FieldElement(BigInteger.Zero, BigInteger.Zero); } 
        }

        public override string ToString()
        {
            return x.ToString();
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ q.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            return this == (FieldElement)obj;
        }
    }
}
