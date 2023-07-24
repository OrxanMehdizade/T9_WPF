using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace T9_WPF
{
    public class T9Converter
    {
        private Dictionary<char, string> t9Dictionary;
        private Dictionary<char, int> keyPressCount;
        private char[] keyPressOrder;

        public T9Converter()
        {
            InitializeT9Dictionary();
            InitializeKeyPressOrder();
        }

        private void InitializeT9Dictionary()
        {
            // T9 harf kombinasyonlarını belirleyen bir sözlük oluşturuyoruz.
            t9Dictionary = new Dictionary<char, string>
            {
                {'2', "abc"},
                {'3', "def"},
                {'4', "ghi"},
                {'5', "jkl"},
                {'6', "mno"},
                {'7', "pqrs"},
                {'8', "tuv"},
                {'9', "wxyz"}
            };
        }

        private void InitializeKeyPressOrder()
        {
            // Her tuşun kaç defa basılması durumunda sıradaki harfi alacağını belirleyen bir sözlük oluşturuyoruz.
            keyPressCount = new Dictionary<char, int>
            {
                {'2', 1},
                {'3', 1},
                {'4', 1},
                {'5', 1},
                {'6', 1},
                {'7', 1},
                {'8', 1},
                {'9', 1}
            };

            // Her tuşun üzerinde dolaşırken sıradaki harfini almak için kullanılacak dizi.
            keyPressOrder = new char[] { ' ', ' ', ' ', ' ' };
        }

        public async Task<string> ConvertT9ToTextAsync(string inputNumbers)
        {
            return await Task.Run(() =>
            {
                string result = "";
                foreach (char number in inputNumbers)
                {
                    if (t9Dictionary.TryGetValue(number, out string letters))
                    {
                        int pressCount = keyPressCount[number];

                        // Tuşa basıldıkça sıradaki harfi alıyoruz ve sayacı güncelliyoruz.
                        char currentLetter = t9Dictionary[number][pressCount - 1];
                        keyPressCount[number] = (pressCount % letters.Length) + 1;

                        // Önceki tuşa basıldıysa, önceki tuşun sıradaki harfini tekrar alıyoruz.
                        if (number == keyPressOrder[pressCount])
                            currentLetter = t9Dictionary[number][pressCount];

                        result += currentLetter;
                        keyPressOrder[pressCount] = number;
                    }
                    else
                    {
                        // Eğer sayı geçersizse aynı sayıyı sonuca ekliyoruz.
                        result += number;
                        keyPressCount[number] = 1;
                        keyPressOrder[1] = ' ';
                        keyPressOrder[2] = ' ';
                        keyPressOrder[3] = ' ';
                    }
                }

                return result;
            });
        }
    }
}