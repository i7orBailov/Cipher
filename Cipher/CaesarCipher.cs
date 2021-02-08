using System;

namespace Cipher
{
    static class CaesarCipher
    {
        static string Cipher(string input, int rotation, bool encrypt)
        {
            string output = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                int result = 0;

                if (input[i] < 65 || input[i] > 122 || input[i] > 90 && input[i] < 97)
                {
                    output += input[i];
                    continue;
                }

                if (encrypt)
                {
                    result += input[i] + rotation;
                    result = char.IsLower(input[i]) ? result > 122 ? 97 + (Math.Abs(97 - result) - 26) : result : result > 90 ? 65 + (Math.Abs(65 - result) - 26) : result;
                }
                else
                {
                    result += input[i] - rotation;

                    if (char.IsLower(input[i]))
                        result = result < 97 ? 97 - (97 - result) + 26 : result;
                    else
                        result = result < 65 ? 65 - (65 - result) + 26 : result;
                }

                output += (char)result;
            }

            return output;
        }

        public static string Encrypt(string input, int rotation) => Cipher(input, rotation, true);

        public static string Decrypt(string input, int rotation) => Cipher(input, rotation, false);
    }
}