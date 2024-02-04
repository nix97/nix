using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;



namespace AES
{
    public partial class Form1 : Form
    {
        //private byte[] key;
        //private byte[] iv;

        public Form1()
        {
            InitializeComponent();
        }


       
        private void butRun_Click(object sender, EventArgs e)
        {
            
            try
            {

                Aes aes = Aes.Create();

                if (rb128.Checked)
                    aes.KeySize = 128;
                else if (rb192.Checked)
                    aes.KeySize = 192;
                else //256
                    aes.KeySize = 256;

                aes.Mode = CipherMode.CBC; //(CBC:CipherMode Block Chain)
                aes.GenerateKey();
                aes.GenerateIV();  //IV:Initialization Vector

                string plaintext;

                plaintext = tbPlain.Text;

                byte[] ciphertext;


                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                    ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                }



               

                string keyBase64 = Convert.ToBase64String(aes.Key);
            
                tbKey.Text = keyBase64;

                tbCipher.Text = Convert.ToBase64String(ciphertext);

                string iv = Convert.ToBase64String(aes.IV);
                tbIV.Text = iv;

            }
           catch (Exception)
            {

                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }
        }

        private void btRunDec_Click(object sender, EventArgs e)
        {
            try
            {

                Aes aes = Aes.Create();

                aes.Mode = CipherMode.CBC;

                string decryptedText;

                byte[] ciphertext;

                aes.Key = Convert.FromBase64String(tbKeyDec.Text);
                aes.IV = Convert.FromBase64String(tbIVDec.Text);
                ciphertext = Convert.FromBase64String(tbCipherDec.Text);


                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                    decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                }

                tbPlainDec.Text = decryptedText;
            }
            catch (Exception)
            {

                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }


        }

        private void butAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("|======== Cryptography using AES ========|" + Environment.NewLine + Environment.NewLine +
                "(Cryptography using Advanced Encryption Standard)" + Environment.NewLine + Environment.NewLine +

                "Version 1.0 - build oct 1, 2023." + Environment.NewLine +
                       "Created by Lukas Setiawan." + Environment.NewLine +
                       "Copyright (c) 2023. All Rights Reserved." + Environment.NewLine +
                       "Visit www.metodenumeriku.blogspot.com." + Environment.NewLine +
                       "FB search: Metode Numerik-Plus Programnya." + Environment.NewLine +
                       "e-mail: lukassetiawan@yahoo.com." + Environment.NewLine + Environment.NewLine +
                       "My other works :" + Environment.NewLine +
                       "https://bitbucket.org/nixz97/nix/downloads/" + Environment.NewLine + Environment.NewLine +
                       "Accept donations for software development."
           );
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            tbKey.Clear();
            tbIV.Clear();
            tbPlain.Clear();
            tbCipher.Clear();

        }

        private void butClearDec_Click(object sender, EventArgs e)
        {
            tbKeyDec.Clear();
            tbIVDec.Clear();
            tbPlainDec.Clear();
            tbCipherDec.Clear();
        }
    }
}



    



    


