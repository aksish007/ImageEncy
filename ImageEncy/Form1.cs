using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEncy
{
    public partial class Form1 : Form
    {
        string encryptedData;
        Bitmap image1 = null;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void openFDialog()
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open_dialog.FileName);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            openFDialog();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            EncryptHelper encryptor = new EncryptHelper();
            //string key1 = "aaabbbb-"+textBox2.Text.Trim() + "-xxxyyy";
            if (textBox2.Text.Equals("") || textBox1.Text.Equals(""))
            {
                MessageBox.Show("Please Provide a Password or a Text");
            }
            else
            {
                string key1 = textBox2.Text.Trim();
                this.encryptedData = encryptor.EncryptIt(textBox1.Text, key1);
                //textBox1.Text = encryptedData;
                MessageBox.Show("SuccessFully Encrypted.");
            }
        }
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            EncryptHelper encryptor = new EncryptHelper();
            //string key1 = "aaabbbb-"+textBox2.Text.Trim() + "-xxxyyy";
            string key1 = textBox2.Text.Trim();
            this.image1 = (Bitmap)pictureBox1.Image;
            string outText = "";
            try
            {
                outText = HideHelper.extractText(image1);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Password Incorrect Or Image Doesn't contain any Steganographic Data.");
            }
            this.encryptedData = encryptor.DecryptIt(outText, key1);
            textBox1.Text = this.encryptedData;
            MessageBox.Show("SuccessFully Decrypted.");
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.image1 = (Bitmap)pictureBox1.Image;
            if (this.encryptedData.Equals(""))
            {
                MessageBox.Show("The text you want to hide can't be empty", "Warning");

                return;
            }
            this.image1 = HideHelper.embedText(this.encryptedData, image1);
            MessageBox.Show("SuccesFully Hidden! Don't Forget to Save!");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:
                        {
                            this.image1.Save(save_dialog.FileName, ImageFormat.Png);
                        }
                        break;
                    case 1:
                        {
                            this.image1.Save(save_dialog.FileName, ImageFormat.Bmp);
                        }
                        break;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By- Subhadip Halder and Surya Narayana Nunna");
        }
    }
}
