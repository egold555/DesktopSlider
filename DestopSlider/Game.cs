using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DestopSlider
{
    public partial class Game : Form
    {
        string title = Form1.title;
        public static String dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\DestopSlider.png";

        Bitmap screenshot;

        PictureBox[] allPictures;
        int tileWidth, tileHeight;

        public Game()
        {
            InitializeComponent();

        }

        private void Game_Load(object sender, EventArgs e)
        {
            
            allPictures = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4,
                pictureBox5, pictureBox6, pictureBox7, pictureBox8,
                pictureBox9, pictureBox10, pictureBox11, pictureBox12};

            screenshot = (Bitmap) Image.FromFile(dir);
            int width = screenshot.Width;
            int height = screenshot.Height;
            tileWidth = width / 4;
            tileHeight = height / 3;

            pictureBox1.Image = CopyScreenshotPart(0, 0, tileWidth, tileHeight);
            pictureBox2.Image = CopyScreenshotPart(tileWidth, 0, tileWidth, tileHeight);
            pictureBox3.Image = CopyScreenshotPart(tileWidth * 2, 0, tileWidth, tileHeight);
            pictureBox4.Image = CopyScreenshotPart(tileWidth * 3, 0, tileWidth, tileHeight);
            pictureBox5.Image = CopyScreenshotPart(0, tileHeight, tileWidth, tileHeight);
            pictureBox6.Image = CopyScreenshotPart(tileWidth, tileHeight, tileWidth, tileHeight);
            pictureBox7.Image = CopyScreenshotPart(tileWidth * 2, tileHeight, tileWidth, tileHeight);
            pictureBox8.Image = CopyScreenshotPart(tileWidth * 3, tileHeight, tileWidth, tileHeight);
            pictureBox9.Image = CopyScreenshotPart(0, tileHeight * 2, tileWidth, tileHeight);
            pictureBox10.Image = CopyScreenshotPart(tileWidth * 1, tileHeight * 2, tileWidth, tileHeight);
            pictureBox11.Image = CopyScreenshotPart(tileWidth * 2, tileHeight * 2, tileWidth, tileHeight);

            pictureBox12.BackColor = Color.Black;

            RandomizePictures();

            
            
            
        }

        void RandomizePictures()
        {
            Random rand = new Random();
            int count = 0;
            
            for (int i = 0; i < 10000; ++i)
            {
                int index1 = rand.Next(12);
                int p1Row = tableLayout.GetRow(allPictures[index1]), p1Col = tableLayout.GetColumn(allPictures[index1]);
                int p2Row = tableLayout.GetRow(pictureBox12), p2Col = tableLayout.GetColumn(pictureBox12);

                if ((p1Row == p2Row && Math.Abs(p1Col - p2Col) == 1) ||
                    (p1Col == p2Col && Math.Abs(p1Row - p2Row) == 1))
                {
                    SwapPictures(allPictures[index1], pictureBox12);
                    count += 1;
                    if (count == 100)
                        return;
                }
            }
        }

        void SwapPictures(PictureBox pict1, PictureBox pict2)
        {
            int p1Row = tableLayout.GetRow(pict1), p1Col = tableLayout.GetColumn(pict1);
            int p2row = tableLayout.GetRow(pict2), p2Col = tableLayout.GetColumn(pict2);

            tableLayout.SetRow(pict1, p2row);
            tableLayout.SetColumn(pict1, p2Col);
            tableLayout.SetRow(pict2, p1Row);
            tableLayout.SetColumn(pict2, p1Col);
        }

        Bitmap CopyScreenshotPart(int x, int y, int tileWidth, int tileHeight)
        {
            Bitmap bm = new Bitmap(tileWidth, tileHeight);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.DrawImage(screenshot, 
                    new Rectangle(0, 0, tileWidth, tileHeight),
                    new Rectangle(x, y, tileWidth, tileHeight), 
                    GraphicsUnit.Pixel);
            }

            return bm;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedBox = (PictureBox)sender;

            int p1Row = tableLayout.GetRow(clickedBox), p1Col = tableLayout.GetColumn(clickedBox);
            int p2Row = tableLayout.GetRow(pictureBox12), p2Col = tableLayout.GetColumn(pictureBox12);

            if ((p1Row == p2Row && Math.Abs(p1Col - p2Col) == 1) ||
                (p1Col == p2Col && Math.Abs(p1Row - p2Row) == 1))
            {
                SwapPictures(clickedBox, pictureBox12);
            }

            if (IsSolved())
            {
                Solved();
            }
        }

        void Solved()
        {
            String message = "Congrads! You beat the puzzle! Now you can get on with what ever you were doing before. Have a nice day. ~Eric (http://eric.golde.org)";
            pictureBox12.Image = CopyScreenshotPart(tileWidth * 3, tileHeight * 2, tileWidth, tileHeight);
            MessageBox.Show(message, Form1.title, MessageBoxButtons.OK);
            eric();
            Environment.Exit(0);
            
        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            DialogResult quit;
            if ((e.KeyChar = (char)Keys.Escape) != 0)
            {
                quit = MessageBox.Show("Give up?", title, MessageBoxButtons.YesNo);
                
                if (quit == System.Windows.Forms.DialogResult.Yes)
                {
                    MessageBox.Show("Wow.", title, MessageBoxButtons.OK);
                    MessageBox.Show("Just wow.", title, MessageBoxButtons.OK);
                    MessageBox.Show("You give up? Just that easily?", title, MessageBoxButtons.OK);
                    MessageBox.Show("I would expect more from you.", title, MessageBoxButtons.OK);
                    MessageBox.Show("But whatevs.", title, MessageBoxButtons.OK);
                    MessageBox.Show("You said you quit so i guess you really quit.", title, MessageBoxButtons.OK);
                    MessageBox.Show("I guess thats it then.", title, MessageBoxButtons.OK);
                    MessageBox.Show("Better luck next time i guess.", title, MessageBoxButtons.OK);
                    MessageBox.Show("GoodBye.", title, MessageBoxButtons.OK);
                    eric();
                    Environment.Exit(0);
                }

            }
        }

        private void eric()
        {
            MessageBox.Show("This program was made by Eric Golde (http://eric.golde.org) This program was intended to be used just as a fun joke to play on friends. Source code can be found here: https://github.com/egold555/DesktopSlider/", title, MessageBoxButtons.OK);
        }

        bool IsSolved()
        {
            for (int i = 0; i < 12; ++i)
            {
                if (tableLayout.GetRow(allPictures[i]) != i / 4)
                    return false;
                if (tableLayout.GetColumn(allPictures[i]) != i % 4)
                    return false;
            }

            return true;
        }
    }
}
