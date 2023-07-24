// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("51UVGUkX80XDJ+OEFM3WEyaxBMO1c2k9VYfS5hwlHW0hbJjw78QDRXX2+PfHdfb99XX29vdaeeMsLkKE9z4iMMxyycYuS3UbvM9UeymZkVX3ClPdg8Lyffdu/I7QQaYr857ELkrlidObuX7OtYAkuFdmjLoNV/hbBruI+ipnxiv3z2GTOKWXllhYFQda/B83eFY0wJZfzOFbNfPu8hlTNwJrKGRnxYFPpoGP5pt3MDZU6nwBx3X21cf68f7dcb9xAPr29vby9/Tk3iqGyp4j0Uo+LEYjWPZpA07zXlMoBhYsdCePY6Qfw3OUjGaUOtEV3Fnbd3hRHtqzwY0A22uJmCCN/SbuEfKVZO0cBb0QuR446iDbx3EWc3eRrRaStlBupvX09vf2");
        private static int[] order = new int[] { 9,3,3,4,11,6,8,13,13,9,13,11,12,13,14 };
        private static int key = 247;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
