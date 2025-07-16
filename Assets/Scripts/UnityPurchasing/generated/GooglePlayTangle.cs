// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("W0E+5dBlkJd/TiOBRnWOqHIc/nx+8drya95A0C0qZx32+c+NisEGNtjge0eCW7tr1l9noTxmmRGOeLLagDKxkoC9trmaNvg2R72xsbG1sLMHxJnVqJUcUahsnwRRDI/4i/M3bl5YKy4na0lwGOMsjDAmUtkCtOGFaNNvprh3QgzTyMofXWHhhOspsNYfXigVG6zQCYjsIvnvR7sVCQgKQMDHB2cmaKL+iB33JG6Bl8cDrTb75WDs/RQ99Uq5z4oZrpmS2JhGZvoysb+wgDKxurIysbGwVcYD0AmxcA50hxwCVAK5pZlBWw5ZsMhC1tG6GyudFCgy/MQhu4Mv3n5KWWnYQVLN8DLt5864RrOqns5ta8wWySwh/N2dkZYqmLQkT7KzsbCx");
        private static int[] order = new int[] { 4,5,11,4,8,13,11,8,12,12,13,12,12,13,14 };
        private static int key = 176;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
