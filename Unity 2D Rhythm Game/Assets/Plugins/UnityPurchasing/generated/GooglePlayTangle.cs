#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("YYCNXvI7bnAIWRDFQuRazq+QwxN2wGy//vu396B1AjV7zCC5/EXUfhvifqTKDeLF7sT3SwZbHx8gMZrw85+xVtZLYYCNXvI7bnAIWRDFQuRT0N7R4VPQ29NT0NDRaCFITZnogA26U9NN9grVrrDil4J2obDIKbWAP1tvvc04dGcm3Jq41XFAcLEoZ3oUCiVeZ5IoJyBKtPjmfFUZPCUp9VrOr5DDE7DBPFqtIc2SyKpEhKw4gkYCHvpJPOJaXggi6PYNulPTTfbQ29NT0NDRaCFITZnogPbXMKP67rDBPFqtIc2SyKpEhKw4/dmwsHtbdGcm3Jq41XFAcLEoZ3p2wGy//vsEwkQ5CkFO8hNE7npi5BE73CyglP3ZsLB7W0IggtPS0NHQ4VPQ8+HcghL1wiffFAolXmeSKCcgSrT45nyFZ0FBGMoEwkQ5CkFO8hNE7npi5PbXMKP67oJGAh76STziWl4IIuj24VPQ8+Hc19j7V5lXJtzQ0NDU0dLX2PtXmVcm3NDQ0NTR0lPQ3tHhU5ghiXlkQ4vE8D0PJMFOP1tvvc04w+HYKunkqadUmN6Sh6Dzn7FW1ktVGTwlKfWTpSFwPw6KbvWKkg2yXME4Pu7DscPh2Crp5KmnVJjekoegCtWusOKXgnahsMgptYAb4n6kyg0RO9wsoJSYIYl5ZEOLxPA9DyTBTrf3oHUCNXvMILn8RdR+wTg+7sOxk6UhcD8Oim71ipINslyFZ0FBGMrixe7E90sGWx8fIDGa8IIS9cIn30IggtPS0NHQ");
        private static int[] order = new int[] { 27,10,4,12,10,12,9,20,13,17,16,28,24,22,14,24,20,20,27,24,23,26,23,28,27,28,28,28,28,29 };
        private static int key = 209;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
