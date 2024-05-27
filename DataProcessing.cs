using System.IO;;
using System.Text;

namespace DataProcessing
{
    public class TextProcessing
    {
        private string key;

        // Arrays for mapping keys and values
        private string[] keys = {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
        };

        private char[] values = {
            'r', '*', 'p', 'f', '»', 'O', '-', ',', '6', '[', 'X', '7', 'G', 'W', '1',
            'I', '9', '«', '~', 'F', 'i', 'Ø', '-', 'V', 't', '{', '+', 'x', 'ç', 'n',
            'Q', 'Æ', '^', 'B', '$', 'h', 'M', 'C', ']', '=', 'o', '0', '/', 'Ó', 'N',
            'S', 'Z', '2', 'Ð', 'K', '§', 'H', 'Î', 's', 'Y', 'E', 'Ê', '_', 'P', 'á',
            '5', 'b', 'U', '.', '€', 'D', 'z', 'g', 'c', '3', 'e', '%', 'J', ':', '#',
            'j', '@', '}', 'Â', 'A', 'ò', '?', 'y', 'Ç', '¿', 'u', 'd', 'm', 'R', 'l',
            'q', 'a', 'w', 'k', 'Á', '4', 'L', 'T', 'v', '8',
        };

        private Dictionary<string, char> keychar;
        private Dictionary<char, string> charkey;

        // Arrays for mapping keys and values
        public TextProcessing(string key)
        {
            if (key != "") this.key = key;
            else this.key = "123";

            keychar = new Dictionary<string, char>();
            charkey = new Dictionary<char, string>();

            //Populate the keychar and charkey dictionaries
            for (int i = 0; i < keys.Length; i++)
            {
                keychar[keys[i]] = values[i];
                charkey[values[i]] = keys[i];
            }
        }

        //Default constructor
        public TextProcessing()
        {
            key = "123";

            keychar = new Dictionary<string, char>();
            charkey = new Dictionary<char, string>();

            // Populate the keychar and charkey dictionaries
            for (int i = 0; i < keys.Length; i++)
            {
                keychar[keys[i]] = values[i];
                charkey[values[i]] = keys[i];
            }
        }

        // Encrypt method
        public string encrypt(string text)
        {
            string temp = new string("");
            string final = new string("");

            // Encrypt the input text
            foreach (char tc in text)
            {
                foreach (char kc in key)
                {
                    temp += (tc ^ kc).ToString().PadLeft(5, '0');
                }
            }

            // Combine the encrypted values
            for (int i = 1; i <= temp.Length; i += 2)
            {
                final += keychar[$"{temp[i - 1]}{temp[i]}"];
            }

            return final;
        }

        // Decrypt method
        public string decrypt(string text)
        {
            string f_stage = new string("");
            string final = new string("");

            // Check if the input text length is even
            for (int i = 0; i < text.Length; i++)
            {
                f_stage += charkey[text[i]];
            }

            string s_stage = new string("");
            List<int> numbers = new List<int>();

            for (int i = 0; i < f_stage.Length; i++)
            {
                s_stage += f_stage[i];

                if ((i + 1) % 5 == 0)
                {
                    numbers.Add(int.Parse(s_stage));
                    s_stage = "";
                }
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i % key.Length == 0)
                {
                    final += (char)((char)numbers[i] ^ key[0]);
                }
            }

            return final;
        }

        // Decrypt from a file method

        public string decrypt_trom_file(string path)
        {

            // Check if the file exists
            bool fileExists = File.Exists(path);

            if (fileExists)
            {
                string fileContent = File.ReadAllText(path);
                return decrypt(fileContent);
            }
            else
            {
                Console.WriteLine("\x1b[31mThe file does not exist.\x1b[0m");
            }

            return "";
        }
    }

    public class FileProcessing
    {
        private byte[] key;

        // Constructor with a key parameter

        public FileProcessing(string key)
        {
            this.key = Encoding.UTF8.GetBytes(key);
        }

        // XOR method to encrypt a file
        public void XOR(string srcFile, string newFile)
        {
            if (File.Exists(srcFile))
            {
                bool fileExists = File.Exists(newFile);

                if (fileExists)
                {
                    Console.WriteLine("\x1b[31mIt is not possible to create a file in the specified " +
                    $"directory because such a {newFile} file already exists!\x1b[0m");
                }
                else
                {
                    //Encrypt the file using XOR and the provided key
                    using (FileStream writer = new FileStream(newFile, FileMode.Create))
                    {
                        using (FileStream fs = new FileStream(srcFile, FileMode.Open, FileAccess.Read))
                        {
                            int byteRead;
                            int i = 0;

                            while ((byteRead = fs.ReadByte()) != -1)
                            {
                                if (i % key.Length == 0) { i = 0; }

                                writer.WriteByte((byte)(byteRead ^ key[i]));
                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}
