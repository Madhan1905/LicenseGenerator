using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt_Password
{
    class Decryptor
    {
        static void Main1(string[] args)
        {
            //The key and IV must be the same values that were used
            //to encrypt the stream.
            byte[] key = ASCIIEncoding.ASCII.GetBytes("KamehamehaX4".PadLeft(32));
            byte[] iv = ASCIIEncoding.ASCII.GetBytes("KamehamehaX4".PadLeft(16));
            try
            {
                //Create a file stream.
                FileStream myStream = new FileStream("C://Users//hp//Downloads//license.lic", FileMode.Open);

                //Create a new instance of the default Aes implementation class
                Aes aes = Aes.Create();

                //Create a CryptoStream, pass it the file stream, and decrypt
                //it with the Aes class using the key and IV.
                CryptoStream cryptStream = new CryptoStream(
                   myStream,
                   aes.CreateDecryptor(key, iv),
                   CryptoStreamMode.Read);

                //Read the stream.
                StreamReader sReader = new StreamReader(cryptStream);

                //Display the message.
                Console.WriteLine("The decrypted original message: {0}", sReader.ReadToEnd());
                Console.ReadKey();
                //Close the streams.
                sReader.Close();
                myStream.Close();
            }
            //Catch any exceptions.
            catch
            {
                Console.WriteLine("The decryption failed.");
                Console.ReadKey();
                throw;
            }
        }
    }
}
