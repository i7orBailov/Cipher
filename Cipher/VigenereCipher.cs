using System;

namespace Cipher
{
    static class VigenereCipher
    {
        static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        static string Cipher(string input, string key, bool encrypt)
        {
            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = encrypt ? k : -k;
                    char ch = (char)(Mod(input[i] + k - offset, 26) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                    ++nonAlphaCharCount;
                }
            }
            return output;
        }

        public static string Encrypt(string input, string key) => Cipher(input, key, true);

        public static string Decrypt(string input, string key) => Cipher(input, key, false);
    }
}