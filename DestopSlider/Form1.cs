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

namespace DestopSlider
{
    public partial class Form1 : Form
    {
        public static string title = "Slider 1.0";


        public Form1()
        {
            InitializeComponent();
            doStuffAndThingsOnProgramLoad();

            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            
            using (Bitmap bmpScreenCapture = new Bitmap(screenSize.Width,
                                            screenSize.Height))
            {
                Console.WriteLine("Bounds width: {0}", screenSize.Width);
                Console.WriteLine("height: {0}", screenSize.Height);
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     screenSize,
                                     CopyPixelOperation.SourceCopy);
                    
                }

                bmpScreenCapture.Save(Game.dir);
            }
        

          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DialogResult result;
            DialogResult quit;
            


            string message = "Oops, looks like somebody doesn't like you very much! You have to finish this sliding tile puzzle before you can continue with whatever it is you're doing! Use the mouse to move the pieces (black piece is the empty one).";
            
 
            result = MessageBox.Show(message, title, MessageBoxButtons.OK);
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                new Game().Show();

               Hide();
            }



           
           

        }

        private Size GetDpiSafeResolution()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                  , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);
            }

          
        }

        public void doStuffAndThingsOnProgramLoad()
        {
            //do stuff and things here
            //like disable task manager :D

        }
    }
}
