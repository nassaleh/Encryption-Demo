using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption_Demo
{
    class Program
    {
        public static void Main()
        {
            // generate a key
            var key = new byte[32];
            RandomNumberGenerator.Fill(key);

            using var aes = new AesGcm(key);
            string plaintext = "Vincent is the coolest boss in the world. Please give me a raise";

            Console.WriteLine($"Plaintext: {plaintext}");

            (byte[] ciphertext, byte[] nonce, byte[] tag) values = AesGcmEncryption.EncryptWithNet(plaintext, key);

            Console.WriteLine($"Ciphertext: {Convert.ToHexString(values.ciphertext)}");
            Console.WriteLine($"Ciphertext64: {Convert.ToBase64String(values.ciphertext)}");
            Console.WriteLine($"Nonce: {Convert.ToHexString(values.nonce)}");
            Console.WriteLine($"Nonce64: {Convert.ToBase64String(values.nonce)}");
            Console.WriteLine($"Tag: {Convert.ToHexString(values.tag)}");
            Console.WriteLine($"Tag64: {Convert.ToBase64String(values.tag)}");
            //TODO: Base64 text encoding

            var decrypted = AesGcmEncryption.DecryptWithNet(values.ciphertext, values.nonce, values.tag, key);

            Console.WriteLine($"Decrypted: {decrypted}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            //AESEncryption_Run();
        }

        public static void AESEncryption_Run()
        {
            string original = "Here is some data to encrypt!";

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {
                // Encrypt the string to an array of bytes.
                byte[] encrypted = AESEncryption.EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                // Decrypt the bytes to a string.
                string roundtrip = AESEncryption.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                //Display the original data and the decrypted data.
                Console.WriteLine("Original:   {0}", original);
                Console.WriteLine("Round Trip: {0}", roundtrip);
            }
        }

        public void EncodeDecode()
        {
            // Encoding
            string passw = "tes123";
            var plainTextBytes = Encoding.UTF8.GetBytes(passw);
            string pass = Convert.ToBase64String(plainTextBytes);

            // Normal
            var encodedTextBytes = Convert.FromBase64String(pass);
            string plainText = Encoding.UTF8.GetString(encodedTextBytes);
        }
    }
}

