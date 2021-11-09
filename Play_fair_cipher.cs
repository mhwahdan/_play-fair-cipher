
using System.Collections.Generic;

namespace play_fair_cipher
{
    public class PlayFairCipher
    {
        public char[][] Key { get; set; }
        private static readonly char bogus_character = 'Z';
        public PlayFairCipher(string key)
        {
            this.Key = PlayFairCipher.generate_key(key);
        }
        private static char[][] generate_key(string key)
        {
            var output = new char[5][];
            key = key.ToUpper().Replace("J","K");
            for (var k = 0; k < 5; k++)
                output[k] = new char[5];
            var iterator = 0;
            var flag = false;
            int i, j = 0;
            for (i = 0; i < 5 ; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    output[i][j] = key[iterator];
                    if (iterator == key.Length - 1)
                    {
                        flag = true;
                        break;
                    }
                    iterator++;

                }
                if (flag)
                    break;
            }
            ++j;
            for (var current = '@'; i < 5; i++)
            {
                for (; j < 5; j++)
                {
                    while(true)
                    {
                        current ++;
                        if (key.Contains(current) || current == 'J')
                            continue;
                        output[i][j] = current;
                        break;
                    }
                }
                j = 0;
            }
            return output;
        }

        private static List<string> Split(string text)
        {
            var strings = new List<string>();
            if(text.Length % 2 != 0)
                text += bogus_character;
            for(int i = 0; i < text.Length; i += 2)
                if(i == text.Length -1 || text[i] == text[i + 1])
                {
                    strings.Add((text[i] + bogus_character.ToString()));
                    i--;
                }
                else
                    strings.Add(
                        text[i] + text[i + 1].ToString()
                    );
            string last = strings[^1];
            if(last == "XX")
                strings.Remove(last);
            return strings;
        }
        
        private static int[] GetIndex(char key, IReadOnlyList<char[]> matrix)
        {
            int[] output = {-1, -1};
            for(var i = 0; i < 5; i++)
            for(var j = 0; j < 5; j++)
                if(key == matrix[i][j])
                {
                    output[0] = i;
                    output[1] = j;
                    return output;
                }
            return output;
        }
        
        public string Encrypt(string plaintext)
        {
            var strings = Split(plaintext.ToUpper());
            var output = "";
            foreach(var x in strings)
            {
                var index1 = GetIndex(x[0], Key);
                var index2 = GetIndex(x[1], Key);
                if(index1[0] == index2[0])
                    output += Key[index1[0]][(index1[1] + 1) % 5] + Key[index2[0]][(index2[1] + 1) % 5].ToString();
                else if(index1[1] == index2[1])
                    output += Key[(index1[0] + 1) % 5][index1[1]] + Key[(index2[0] + 1) % 5][index2[1]].ToString();
                else
                    output += Key[index1[0]][index2[1]] + Key[index2[0]][index1[1]].ToString();
            }
            return output;
        }
        public string Decrypt(string cipher)
        {
            var strings = Split(cipher);
            var output = "";
            foreach(var x in strings)
            {
                var index1 = GetIndex(x[0], Key);
                var index2 = GetIndex(x[1], Key);
                if(index1[0] == index2[0])
                    output += Key[index1[0]][(index1[1] + 4) % 5] + Key[index2[0]][(index2[1] + 4) % 5].ToString();
                else if(index1[1] == index2[1])
                    output += Key[(index1[0] + 4) % 5][index1[1]] + Key[(index2[0] + 4) % 5][index2[1]].ToString();
                else
                    output += Key[index1[0]][index2[1]] + Key[index2[0]][index1[1]].ToString();
            }

            return output[..^(output[^1] == bogus_character? 1:0)].ToLower();
        }
    }
    

}