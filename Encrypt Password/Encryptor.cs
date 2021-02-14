using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Encrypt_Password
{
    class Encryptor
    {
        static void Main(string[] args)
        {
            try
            {
                //Create a file stream
                FileStream myStream = new FileStream("license.lic", FileMode.OpenOrCreate);

                //Create a new instance of the default Aes implementation class  
                // and encrypt the stream.  
                Aes aes = Aes.Create();

                byte[] key = ASCIIEncoding.ASCII.GetBytes("KamehamehaX4".PadLeft(32));
                byte[] iv = ASCIIEncoding.ASCII.GetBytes("KamehamehaX4".PadLeft(16));  

                //Create a CryptoStream, pass it the FileStream, and encrypt
                //it with the Aes class.  
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);


                CryptoStream cryptStream = new CryptoStream(
                    myStream,
                    aes.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);

                //Create a StreamWriter for easy writing to the
                //file stream.  
                StreamWriter sWriter = new StreamWriter(cryptStream);

                //Write to the stream.  
                var licenseDetails = new Dictionary<string, string>();
                licenseDetails.Add("macAddress", "2C6FC9012F77");
                licenseDetails.Add("expirationDate", "01/02/2022 23:59:59");
                string jsonString = JsonSerializer.Serialize(licenseDetails);
                sWriter.WriteLine(jsonString);

                //Close all the connections.  
                sWriter.Close();
                cryptStream.Close();
                myStream.Close();

                //Inform the user that the message was written  
                //to the stream.  
                Console.WriteLine("The file was encrypted.");
                Console.ReadKey();
            }
            catch
            {
                //Inform the user that an exception was raised.  
                Console.WriteLine("The encryption failed.");
                Console.ReadKey();
                throw;
            }

        }
    }
}
