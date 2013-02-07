using System;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;

namespace LeutgebAes.EllipticCurve
{
    [Serializable]
    public class X9ECParameters
    {
        private Curve curve;
        private BigInteger n, h;
        private Point g;
        private string name;

        private static Dictionary<string, X9ECParameters> named;

        private static List<X9ECParameters> predefined = new List<X9ECParameters>
        {
            new X9ECParameters(
                "secp128r1",
                new Curve(
                    BigInteger.Parse("340282366762482138434845932244680310783"),
                    BigInteger.Parse("340282366762482138434845932244680310780"),
                    BigInteger.Parse("308990863222245658030922601041482374867")
                ),
                BigInteger.Parse("29408993404948928992877151431649155974"),
                BigInteger.Parse("275621562871047521857442314737465260675"),
                BigInteger.Parse("340282366762482138443322565580356624661"),
                BigInteger.One
            ),
            new X9ECParameters(
                "secp160k1",
                new Curve(
                    BigInteger.Parse("1461501637330902918203684832716283019651637554291"),
                    BigInteger.Zero,
                    new BigInteger(7)
                ),
                BigInteger.Parse("338530205676502674729549372677647997389429898939"),
                BigInteger.Parse("842365456698940303598009444920994870805149798382"),
                BigInteger.Parse("1461501637330902918203686915170869725397159163571")
            ),
            new X9ECParameters(
                "secp160r1",
                new Curve(
                    BigInteger.Parse("1461501637330902918203684832716283019653785059327"),
                    BigInteger.Parse("1461501637330902918203684832716283019653785059324"),
                    BigInteger.Parse("163235791306168110546604919403271579530548345413")
                ),
                BigInteger.Parse("425826231723888350446541592701409065913635568770"),
                BigInteger.Parse("203520114162904107873991457957346892027982641970"),
                BigInteger.Parse("1461501637330902918203687197606826779884643492439")
            ),
            new X9ECParameters(
                "secp192k1",
                new Curve(
                    BigInteger.Parse("6277101735386680763835789423207666416102355444459739541047"),
                    BigInteger.Zero,
                    new BigInteger(3)
                ),
                BigInteger.Parse("5377521262291226325198505011805525673063229037935769709693"),
                BigInteger.Parse("3805108391982600717572440947423858335415441070543209377693"),
                BigInteger.Parse("6277101735386680763835789423061264271957123915200845512077")
            ),
            new X9ECParameters(
                "secp192r1",
                new Curve(
                    BigInteger.Parse("6277101735386680763835789423207666416083908700390324961279"),
                    BigInteger.Parse("6277101735386680763835789423207666416083908700390324961276"),
                    BigInteger.Parse("2455155546008943817740293915197451784769108058161191238065")
                ),
                BigInteger.Parse("602046282375688656758213480587526111916698976636884684818"),
                BigInteger.Parse("174050332293622031404857552280219410364023488927386650641"),
                BigInteger.Parse("6277101735386680763835789423176059013767194773182842284081")
            ),
            new X9ECParameters(
                "secp224r1",
                new Curve(
                    BigInteger.Parse("26959946667150639794667015087019630673557916260026308143510066298881"),
                    BigInteger.Parse("26959946667150639794667015087019630673557916260026308143510066298878"),
                    BigInteger.Parse("18958286285566608000408668544493926415504680968679321075787234672564")
                ),
                BigInteger.Parse("19277929113566293071110308034699488026831934219452440156649784352033"),
                BigInteger.Parse("19926808758034470970197974370888749184205991990603949537637343198772"),
                BigInteger.Parse("26959946667150639794667015087019625940457807714424391721682722368061")
            ),
            new X9ECParameters(
                "secp256r1",
                new Curve(
                    BigInteger.Parse("115792089210356248762697446949407573530086143415290314195533631308867097853951"),
                    BigInteger.Parse("115792089210356248762697446949407573530086143415290314195533631308867097853948"),
                    BigInteger.Parse("41058363725152142129326129780047268409114441015993725554835256314039467401291")
                ),
                BigInteger.Parse("48439561293906451759052585252797914202762949526041747995844080717082404635286"),
                BigInteger.Parse("36134250956749795798585127919587881956611106672985015071877198253568414405109"),
                BigInteger.Parse("115792089210356248762697446949407573529996955224135760342422259061068512044369")
            )
        };

        static X9ECParameters()
        {
            named = new Dictionary<string, X9ECParameters>(predefined.Count);

            foreach (X9ECParameters parameters in predefined)
                named.Add(parameters.Name, parameters);
        }

        public X9ECParameters(string name, Curve curve, Point g, BigInteger n, BigInteger h)
        {
            this.name = name;
            this.curve = curve;
            this.g = g;
            this.n = n;
            this.h = h;
        }

        private X9ECParameters(string name, Curve curve, BigInteger x, BigInteger y, BigInteger n, BigInteger h) : this(name, curve, new Point(curve, new FieldElement(curve.Q, x), new FieldElement(curve.Q, y)), n, h)
        {
        }

        private X9ECParameters(string name, Curve curve, BigInteger x, BigInteger y, BigInteger n)
            : this(name, curve, new Point(curve, new FieldElement(curve.Q, x), new FieldElement(curve.Q, y)), n, BigInteger.One)
        {
        }

        public string Name
        {
            get { return name; }
        }

        public Curve Curve
        {
            get { return this.curve; }
        }

        public Point G {
            get { return this.g; }
        }

        public X9ECParameters this[string name] {
            get { return named[name]; }
        }

        public static X9ECParameters Dummy
        {
            get { return new X9ECParameters("dummy", Curve.Dummy, Point.Dummy, BigInteger.Zero, BigInteger.Zero); }
        }

        public static X9ECParameters GetByName(string name)
        {
            return named[name];
        }

        public override int GetHashCode()
        {
            return curve.GetHashCode() ^ n.GetHashCode() ^ h.GetHashCode() ^ g.GetHashCode() ^ name.GetHashCode();
        }
    }
}
