using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintProgram
{
    public partial class Form1 : Form
    {
        private Color selectedColor = Color.Black;
        private bool isDrawing = true;
        private int brushSize = 2;
        private bool isMouseDown = false;
        private Point lastPoint = Point.Empty;
        private Bitmap canvas;
        private Bitmap resizedCanvas; 

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            canvas = new Bitmap(this.Width, this.Height);
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            canvas = new Bitmap(this.Width, this.Height);
            this.Resize += Form1_Resize;
            canvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            canvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            resizedCanvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(resizedCanvas))
            {
                g.DrawImage(canvas, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }

            this.Invalidate();


        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isMouseDown = true;
                lastPoint = e.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && isDrawing)
            {
              
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    using (Pen pen = new Pen(selectedColor, brushSize))
                    {
                        g.DrawLine(pen, lastPoint, e.Location);
                    }
                }

                
                this.Invalidate();
                lastPoint = e.Location;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            selectedColor = Color.Red;
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            selectedColor = Color.Green;
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            selectedColor = Color.Blue;
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            selectedColor = Color.Black;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            selectedColor = Color.Yellow;
        }

        private void btnPurple_Click(object sender, EventArgs e)
        {
            selectedColor = Color.Purple;
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            brushSize = 2;
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            brushSize = 5;
        }

        private void btnLarge_Click(object sender, EventArgs e)
        {
            brushSize = 10;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);  
            }
            this.Invalidate();
        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            selectedColor = Color.White;
        }

        
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    
                    canvas.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

       
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.DrawImage(canvas, 0, 0);
        }

        private void btnBucket_Click(object sender, EventArgs e)
        {            
            Color selectedColorForBucket = selectedColor;            
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(selectedColorForBucket); 
            }           
            this.Invalidate();
        }

    }
}
