using System;

namespace play_fair_cipher
{
    internal static class Program
    {
        private static void Main()
        {
            var cipher = new PlayFairCipher("monarchy");
            foreach (var row in cipher.Key)
            {
                foreach (var column in row)
                {
                    Console.Write(column);
                }
                Console.Write("\n");
            }
            Console.WriteLine(cipher.Encrypt("instruments"));
            Console.WriteLine(cipher.Decrypt("GATLMZCLRQTX"));
        }
    }
}
