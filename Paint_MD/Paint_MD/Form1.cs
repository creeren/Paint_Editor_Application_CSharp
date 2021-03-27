using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace Paint_MD
{
   
    public partial class Form1 : Form
    {

        Graphics g;
        Bitmap b;
        Graphics graph;
        Point start = new Point(0, 0);
        Point end = new Point(0, 0);
        Pen myPen = new Pen(Color.FromArgb(0, 0, 0));
        SolidBrush myBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        int x, y, xc, yc; //coordinates 
        bool pencil = false;
        bool eraser = false;
        bool line = false;
        bool rectangle = false;
        bool ellipse = false;
        bool mouseDown = false;
      
     
        public Form1()
        {
            InitializeComponent();

            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(b);
            pictureBox1.BackgroundImage = b;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            g = pictureBox1.CreateGraphics();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            tabControl1.SelectedTab = Home;
        }

        /// <summary>
        /// Mouse movements.
        /// </summary>
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {           
            x = e.X;
            y = e.Y;
            label1.Text = "x: " + x.ToString();
            label2.Text = "y: " + y.ToString();

            if (mouseDown == true)
            {
                end = e.Location;
       
                if (pencil == true)
                {
                    g.DrawLine(myPen, start, end);
                    graph.DrawLine(myPen, start, end);               
                }
                if (eraser == true)
                {
                    g.FillEllipse(myBrush, e.X + 10, e.Y + 10, 6, 6);
                    graph.FillEllipse(myBrush, e.X + 10, e.Y + 10, 6, 6);
                }
            start = end;
            }
        }

        /// <summary>
        /// Mouse click.
        /// </summary>
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int xd=0, yd=0;

            if (mouseDown == true)
            {
                //drawing rectangle or ellipse from top to bottom
                if ((rectangle == true && e.X > xc && e.Y > yc) || (ellipse == true && e.X > xc && e.Y > yc))
                {
                    xd = e.X - xc; //width
                    yd = e.Y - yc; //height
                }
                //drawing rectangle or ellipse from top to bottom
                if ((rectangle == true && e.X < xc && e.Y < yc) || (ellipse == true && e.X < xc && e.Y < yc))
                {
                    xd = xc - e.X; //width
                    xc = e.X;
                    yd = yc - e.Y; //height
                    yc = e.Y;
                }
                if (line == true)
                {                
                    g.DrawLine(myPen, xc, yc, e.X, e.Y);
                    graph.DrawLine(myPen, xc, yc, e.X, e.Y);
                }
                if (rectangle == true)
                {
                    g.DrawRectangle(myPen, xc, yc, xd, yd);
                    graph.DrawRectangle(myPen, xc, yc, xd, yd);

                }
                if (ellipse == true)
                {
                    g.DrawEllipse(myPen, xc, yc, xd, yd);
                    graph.DrawEllipse(myPen, xc, yc, xd, yd);
                }
            }
        }

        /// <summary>
        /// Mouse up.
        /// </summary>
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        /// <summary>
        /// Mouse tool down.
        /// </summary>
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
            }
            start = e.Location; //start location
            xc = e.X;
            yc = e.Y;
        }

        /// <summary>
        /// Pencil tool click.
        /// </summary>
        private void Pencil_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Cursor = new Cursor(Application.StartupPath + "\\Cursor2.cur"); // new cursor for pencil
            pencil = true;
            rectangle = false;
            line = false;
            eraser = false;
            ellipse = false;

        }

        /// <summary>
        /// Ereaser tool click.
        /// </summary>
        private void Ereaser_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Cursor = new Cursor(Application.StartupPath + "\\Cursor1.cur"); // new cursor for ereaser
            eraser = true;
            pencil = false;
            rectangle = false;
            line = false;
            ellipse = false;
        }

        /// <summary>
        /// Line segment tool click.
        /// </summary>
        private void LineSegment_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Cursor = Cursors.Cross;
            line = true;
            rectangle = false;
            pencil = false;
            eraser = false;
            ellipse = false;
        }

        /// <summary>
        /// Rectangle tool click.
        /// </summary>
        private void Rectangle_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Cursor = Cursors.Cross;
            rectangle = true;
            pencil = false;
            line = false;
            eraser = false;
            ellipse = false;
        }

        /// <summary>
        /// Ellipse tool click.
        /// </summary>
        private void Ellipse_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Cursor = Cursors.Cross;
            ellipse = true;
            rectangle = false;
            pencil = false;
            line = false;
            eraser = false;
        }

        /// <summary>
        /// Color choice pick.
        /// </summary>
        private void ColorChoice_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            DialogResult colorResult = colorDialog1.ShowDialog();
            if (colorResult == DialogResult.OK)
            {
                myPen.Color = colorDialog1.Color;
            }
        }

        /// <summary>
        /// New and reset click.
        /// </summary>
        private void NewReset_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play(); //sound
            pictureBox1.Image = null;
   
            b = new Bitmap(pictureBox1.Width = 500, pictureBox1.Height=420);
            graph = Graphics.FromImage(b);
            pictureBox1.BackgroundImage = b;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            g = pictureBox1.CreateGraphics();
        }

        /// <summary>
        /// Open file click.
        /// </summary>
        private void OpenFile_Click(object sender, EventArgs e)
        {        
            System.Media.SystemSounds.Beep.Play(); //sound         
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
 
            if (fd.ShowDialog() == DialogResult.OK)              
            {       
                b = new Bitmap(Image.FromFile(fd.FileName));
                graph = Graphics.FromImage(b);
                pictureBox1.Image = b;
            }           
        }
        
        /// <summary>
        /// Save file click.
        /// </summary>
        private void Save_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play(); //sound
            pictureBox1.DrawToBitmap(b, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
    
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string fName = sf.FileName;
                try
                {
                    b.Save(fName, ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Print Click.
        /// </summary>
        private void Print_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play(); //sound
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
        
            doc.PrintPage += PrintDocument_PrintPage;
            pd.Document = doc;

            if (pd.ShowDialog() == DialogResult.OK)
            {
               doc.Print();
            }
        }

        /// <summary>
        /// Print page.
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
           
            g = e.Graphics;
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(b, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            
            e.Graphics.DrawImage(b, 0, 0);
            b.Dispose();
        }

        /// <summary>
        /// Print preview click.
        /// </summary>
        private void PrintPreview_Click(object sender, EventArgs e)
        {       
            System.Media.SystemSounds.Beep.Play(); //sound

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        /// <summary>
        /// Clear labels with coordinates when mouse is on the form.
        /// </summary>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "x: ";
            label2.Text = "y: ";
        }
    }
}
