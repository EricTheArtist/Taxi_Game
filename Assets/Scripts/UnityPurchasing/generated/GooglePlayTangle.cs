// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("VfqWzISmYdGqnzunSHmTpRJI50T7wTWZ1YE8zlUhM1k8R+l2HFHsQR10N3t42p5QuZ6Q+YRoLylL9WMeaunn6Nhq6eLqaunp6EVm/DMxXZsZpJflNXjZNOjQfownuoiJR0cKGEw3GQkzaziQfLsA3GyLk3mLJc4K+EoKBlYI7FrcOPybC9LJDDmuG9zDRsRoZ04Bxazekh/EdJaHP5LiOdhq6crY5e7hwm6gbh/l6enp7ejr6CE9L9Nt1tkxVGoEo9BLZDaGjkroFUzCnN3tYuhx45HPXrk07IHbMfEO7Yp78gMaog+mASf1P8TYbglsqmx2IkqYzfkDOgJyPnOH7/DbHFpF4wAoZ0kr34lA0/5EKuzx7QZMKGiOsgmNqU9xuerr6ejp");
        private static int[] order = new int[] { 6,5,7,5,8,7,9,12,9,9,11,11,12,13,14 };
        private static int key = 232;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
