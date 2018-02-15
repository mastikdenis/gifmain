using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ImageScaling
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string nameImgOk;    // путь к изображению
        public Image myImg;    // 
        public Bitmap myBitmap;    //создаем объет под хранение изображения
        public Graphics myGraphics;
        
        private void button1_Click(object sender, EventArgs e) // увеличение маштаба
        {
            try
            {
                if ((myBitmap.Width + myImg.Width * 0.1) <= (myImg.Width * 10) && (myBitmap.Height + myImg.Height * 0.1) <= (myImg.Height * 10))
                {
                    myBitmap = new Bitmap(myImg, (int)(myBitmap.Width + myImg.Width * 0.1), (int)(myBitmap.Height + myImg.Height * 0.1));    // маштобирование
                    Pauselook();
                    pictureBox1.Image = (Image)myBitmap;       // вывод в форму
                    textBox1.Text = (myBitmap.Width * 100 / myImg.Width).ToString() + " %";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((myBitmap.Width - myImg.Width * 0.1) >= (myImg.Width * 0.1) && (myBitmap.Height - myImg.Height * 0.1) >= (myImg.Height * 0.1))
                {
                    myBitmap = new Bitmap(myImg, (int)(myBitmap.Width - myImg.Width * 0.1), (int)(myBitmap.Height - myImg.Height * 0.1));    // маштобирование
                    Pauselook();
                    pictureBox1.Image = (Image)myBitmap;       // вывод в форму
                    textBox1.Text = (myBitmap.Width * 100 / myImg.Width).ToString() + " %";                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                myBitmap = new Bitmap(myImg, myImg.Width, myImg.Height);    // маштобирование
                pictureBox1.Image = (Image)myBitmap;       // вывод в форму
                textBox1.Text = "100 %";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog(); //создаем объет типа диалоговое окно
            
            openFileDialog1.InitialDirectory = "c:\\";  // исходная директория
            openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*"; // задаем фильтр типов файлов
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK) // проверяем успешно ли создано диалоговое окно
            {
                try
                {
                    if ((openFileDialog1.OpenFile()) != null)   // обработка исключения на закрытие диалогового окна
                    {
                        nameImgOk = openFileDialog1.FileName;   // сохраняем путь к изображению
                        myImg = Image.FromFile(openFileDialog1.FileName);    // загружаем изображение в переменную типа Image
                        myBitmap = new Bitmap(myImg, pictureBox1.Width, pictureBox1.Height);    // маштобирование
                        pictureBox1.Image = (Image)myBitmap;       // вывод в форму
                        textBox1.Text = (pictureBox1.Width * 100 / myImg.Width).ToString() + " %";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    myStream.Close();
                }
            }
        }

        private void Pauselook()
        {
            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = 100;
            progressBar1.Value = 1;
            progressBar1.Step = 1;
            Timer timer = new Timer();
            timer.Interval = (myBitmap.Height + myBitmap.Width) * 100;          
            for (int x = 1; x < 100; x++)
            {
                timer.Start();
                progressBar1.Value++;              
            }
            progressBar1.Visible = false;
        }
    }
}
