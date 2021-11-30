namespace LedItBe.Core.Common
{
    public class WhiteColorTemperature
    {
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }

        private WhiteColorTemperature(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        // List from https://andi-siess.de/rgb-to-color-temperature/
        public static WhiteColorTemperature K1000 => new WhiteColorTemperature(255, 56, 0);
        public static WhiteColorTemperature K1100 => new WhiteColorTemperature(255, 71, 0);
        public static WhiteColorTemperature K1200 => new WhiteColorTemperature(255, 83, 0);
        public static WhiteColorTemperature K1300 => new WhiteColorTemperature(255, 93, 0);
        public static WhiteColorTemperature K1400 => new WhiteColorTemperature(255, 101, 0);
        public static WhiteColorTemperature K1500 => new WhiteColorTemperature(255, 109, 0);
        public static WhiteColorTemperature K1600 => new WhiteColorTemperature(255, 115, 0);
        public static WhiteColorTemperature K1700 => new WhiteColorTemperature(255, 121, 0);
        public static WhiteColorTemperature K1800 => new WhiteColorTemperature(255, 126, 0);
        public static WhiteColorTemperature K1900 => new WhiteColorTemperature(255, 131, 0);
        public static WhiteColorTemperature K2000 => new WhiteColorTemperature(255, 138, 18);
        public static WhiteColorTemperature K2100 => new WhiteColorTemperature(255, 142, 33);
        public static WhiteColorTemperature K2200 => new WhiteColorTemperature(255, 147, 44);
        public static WhiteColorTemperature K2300 => new WhiteColorTemperature(255, 152, 54);
        public static WhiteColorTemperature K2400 => new WhiteColorTemperature(255, 157, 63);
        public static WhiteColorTemperature K2500 => new WhiteColorTemperature(255, 161, 72);
        public static WhiteColorTemperature K2600 => new WhiteColorTemperature(255, 165, 79);
        public static WhiteColorTemperature K2700 => new WhiteColorTemperature(255, 169, 87);
        public static WhiteColorTemperature K2800 => new WhiteColorTemperature(255, 173, 94);
        public static WhiteColorTemperature K2900 => new WhiteColorTemperature(255, 177, 101);
        public static WhiteColorTemperature K3000 => new WhiteColorTemperature(255, 180, 107);
        public static WhiteColorTemperature K3100 => new WhiteColorTemperature(255, 184, 114);
        public static WhiteColorTemperature K3200 => new WhiteColorTemperature(255, 187, 120);
        public static WhiteColorTemperature K3300 => new WhiteColorTemperature(255, 190, 126);
        public static WhiteColorTemperature K3400 => new WhiteColorTemperature(255, 193, 132);
        public static WhiteColorTemperature K3500 => new WhiteColorTemperature(255, 196, 137);
        public static WhiteColorTemperature K3600 => new WhiteColorTemperature(255, 199, 143);
        public static WhiteColorTemperature K3700 => new WhiteColorTemperature(255, 201, 148);
        public static WhiteColorTemperature K3800 => new WhiteColorTemperature(255, 204, 153);
        public static WhiteColorTemperature K3900 => new WhiteColorTemperature(255, 206, 159);
        public static WhiteColorTemperature K4000 => new WhiteColorTemperature(255, 209, 163);
        public static WhiteColorTemperature K4100 => new WhiteColorTemperature(255, 211, 168);
        public static WhiteColorTemperature K4200 => new WhiteColorTemperature(255, 213, 173);
        public static WhiteColorTemperature K4300 => new WhiteColorTemperature(255, 215, 177);
        public static WhiteColorTemperature K4400 => new WhiteColorTemperature(255, 217, 182);
        public static WhiteColorTemperature K4500 => new WhiteColorTemperature(255, 219, 186);
        public static WhiteColorTemperature K4600 => new WhiteColorTemperature(255, 221, 190);
        public static WhiteColorTemperature K4700 => new WhiteColorTemperature(255, 223, 194);
        public static WhiteColorTemperature K4800 => new WhiteColorTemperature(255, 225, 198);
        public static WhiteColorTemperature K4900 => new WhiteColorTemperature(255, 227, 202);
        public static WhiteColorTemperature K5000 => new WhiteColorTemperature(255, 228, 206);
        public static WhiteColorTemperature K5100 => new WhiteColorTemperature(255, 230, 210);
        public static WhiteColorTemperature K5200 => new WhiteColorTemperature(255, 232, 213);
        public static WhiteColorTemperature K5300 => new WhiteColorTemperature(255, 233, 217);
        public static WhiteColorTemperature K5400 => new WhiteColorTemperature(255, 235, 220);
        public static WhiteColorTemperature K5500 => new WhiteColorTemperature(255, 236, 224);
        public static WhiteColorTemperature K5600 => new WhiteColorTemperature(255, 238, 227);
        public static WhiteColorTemperature K5700 => new WhiteColorTemperature(255, 239, 230);
        public static WhiteColorTemperature K5800 => new WhiteColorTemperature(255, 240, 233);
        public static WhiteColorTemperature K5900 => new WhiteColorTemperature(255, 242, 236);
        public static WhiteColorTemperature K6000 => new WhiteColorTemperature(255, 243, 239);
        public static WhiteColorTemperature K6100 => new WhiteColorTemperature(255, 244, 242);
        public static WhiteColorTemperature K6200 => new WhiteColorTemperature(255, 245, 245);
        public static WhiteColorTemperature K6300 => new WhiteColorTemperature(255, 246, 247);
        public static WhiteColorTemperature K6400 => new WhiteColorTemperature(255, 248, 251);
        public static WhiteColorTemperature K6500 => new WhiteColorTemperature(255, 249, 253);
        public static WhiteColorTemperature K6600 => new WhiteColorTemperature(254, 249, 255);
        public static WhiteColorTemperature K6700 => new WhiteColorTemperature(252, 247, 255);
        public static WhiteColorTemperature K6800 => new WhiteColorTemperature(249, 246, 255);
        public static WhiteColorTemperature K6900 => new WhiteColorTemperature(247, 245, 255);
        public static WhiteColorTemperature K7000 => new WhiteColorTemperature(245, 243, 255);
        public static WhiteColorTemperature K7100 => new WhiteColorTemperature(243, 242, 255);
        public static WhiteColorTemperature K7200 => new WhiteColorTemperature(240, 241, 255);
        public static WhiteColorTemperature K7300 => new WhiteColorTemperature(239, 240, 255);
        public static WhiteColorTemperature K7400 => new WhiteColorTemperature(237, 239, 255);
        public static WhiteColorTemperature K7500 => new WhiteColorTemperature(235, 238, 255);
        public static WhiteColorTemperature K7600 => new WhiteColorTemperature(233, 237, 255);
        public static WhiteColorTemperature K7700 => new WhiteColorTemperature(231, 236, 255);
        public static WhiteColorTemperature K7800 => new WhiteColorTemperature(230, 235, 255);
        public static WhiteColorTemperature K7900 => new WhiteColorTemperature(228, 234, 255);
        public static WhiteColorTemperature K8000 => new WhiteColorTemperature(227, 233, 255);
        public static WhiteColorTemperature K8100 => new WhiteColorTemperature(225, 232, 255);
        public static WhiteColorTemperature K8200 => new WhiteColorTemperature(224, 231, 255);
        public static WhiteColorTemperature K8300 => new WhiteColorTemperature(222, 230, 255);
        public static WhiteColorTemperature K8400 => new WhiteColorTemperature(221, 230, 255);
        public static WhiteColorTemperature K8500 => new WhiteColorTemperature(220, 229, 255);
        public static WhiteColorTemperature K8600 => new WhiteColorTemperature(218, 229, 255);
        public static WhiteColorTemperature K8700 => new WhiteColorTemperature(217, 227, 255);
        public static WhiteColorTemperature K8800 => new WhiteColorTemperature(216, 227, 255);
        public static WhiteColorTemperature K8900 => new WhiteColorTemperature(215, 226, 255);
        public static WhiteColorTemperature K9000 => new WhiteColorTemperature(214, 225, 255);
        public static WhiteColorTemperature K9100 => new WhiteColorTemperature(212, 225, 255);
        public static WhiteColorTemperature K9200 => new WhiteColorTemperature(211, 224, 255);
        public static WhiteColorTemperature K9300 => new WhiteColorTemperature(210, 223, 255);
        public static WhiteColorTemperature K9400 => new WhiteColorTemperature(209, 223, 255);
        public static WhiteColorTemperature K9500 => new WhiteColorTemperature(208, 222, 255);
        public static WhiteColorTemperature K9600 => new WhiteColorTemperature(207, 221, 255);
        public static WhiteColorTemperature K9700 => new WhiteColorTemperature(207, 221, 255);
        public static WhiteColorTemperature K9800 => new WhiteColorTemperature(206, 220, 255);
        public static WhiteColorTemperature K9900 => new WhiteColorTemperature(205, 220, 255);
        public static WhiteColorTemperature K10000 => new WhiteColorTemperature(207, 218, 255);
        public static WhiteColorTemperature K10100 => new WhiteColorTemperature(207, 218, 255);
        public static WhiteColorTemperature K10200 => new WhiteColorTemperature(206, 217, 255);
        public static WhiteColorTemperature K10300 => new WhiteColorTemperature(205, 217, 255);
        public static WhiteColorTemperature K10400 => new WhiteColorTemperature(204, 216, 255);
        public static WhiteColorTemperature K10500 => new WhiteColorTemperature(204, 216, 255);
        public static WhiteColorTemperature K10600 => new WhiteColorTemperature(203, 215, 255);
        public static WhiteColorTemperature K10700 => new WhiteColorTemperature(202, 215, 255);
        public static WhiteColorTemperature K10800 => new WhiteColorTemperature(202, 214, 255);
        public static WhiteColorTemperature K10900 => new WhiteColorTemperature(201, 214, 255);
        public static WhiteColorTemperature K11000 => new WhiteColorTemperature(200, 213, 255);
        public static WhiteColorTemperature K11100 => new WhiteColorTemperature(200, 213, 255);
        public static WhiteColorTemperature K11200 => new WhiteColorTemperature(199, 212, 255);
        public static WhiteColorTemperature K11300 => new WhiteColorTemperature(198, 212, 255);
        public static WhiteColorTemperature K11400 => new WhiteColorTemperature(198, 212, 255);
        public static WhiteColorTemperature K11500 => new WhiteColorTemperature(197, 211, 255);
        public static WhiteColorTemperature K11600 => new WhiteColorTemperature(197, 211, 255);
        public static WhiteColorTemperature K11700 => new WhiteColorTemperature(197, 210, 255);
        public static WhiteColorTemperature K11800 => new WhiteColorTemperature(196, 210, 255);
        public static WhiteColorTemperature K11900 => new WhiteColorTemperature(195, 210, 255);
        public static WhiteColorTemperature K12000 => new WhiteColorTemperature(195, 209, 255);
    }
}
