using System.Text;


namespace Common
{
    public static class CommonMethods
    {
        private static string key = "abv#7896@gd";


        public static string EncryptPassword(string Pass, string korIme)
        {
            if (string.IsNullOrWhiteSpace(Pass) == true)
                return "Los password za enkripciju";

            if (string.IsNullOrWhiteSpace(korIme) == true)
                return "Lose korisnicko ime za enkripciju";

            Pass = Pass + korIme + key;
            byte[] newPas = Encoding.UTF8.GetBytes(Pass);
            return Convert.ToBase64String(newPas);
        }

        public static string DecryptPassword(string Passa)
        {
            if (string.IsNullOrWhiteSpace(Passa) == true)
                return "Losa deskripcija passworda";

            byte[] Pass = Convert.FromBase64String(Passa);
            string result = Encoding.UTF8.GetString(Pass);
            result = result[..^key.Length];
            return result;
        }

        public static string GenerateRandomString()
        {
            int length = 5;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Use the StringBuilder for better performance when concatenating strings in a loop
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                result.Append(chars[index]);
            }

            return result.ToString();
        }
    }
}