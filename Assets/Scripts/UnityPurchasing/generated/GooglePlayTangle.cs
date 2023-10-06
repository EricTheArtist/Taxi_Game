// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("hRcBb8/mTfkhl8flt4QAmtQtB76CCzIdB+n/k5m/LtYUpK779m37stXZZRhy1IZON1ige3E4tO0ykuBJrtxkUT2GFvRgYQFoXJ1brsw3Kf35plGNejAtUF9EfATLuYKJBCNMTrhO/KkSPpjXEhwF1Y5dRaae4a4i0WPgw9Hs5+jLZ6lnFuzg4ODk4eJj4O7h0WPg6+Nj4ODhE8relyxffw8r8sLAjtsyhmkzwRi+htkoZEOx6S3+JaWD+/WmeEQ3DJfoRJCPT6JxMdi8+ycEgblXdTq1jDZWm4BSiBSvClKoUDnqvu36mCdjyWSfYpXOi+cK98AWOT2mCjhcwNVM45FGHFbvywDrPTBlryutmj2XrFcAKLcBKURz0p133e5HxuPi4OHg");
        private static int[] order = new int[] { 4,12,5,8,10,5,10,12,10,11,11,13,13,13,14 };
        private static int key = 225;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
