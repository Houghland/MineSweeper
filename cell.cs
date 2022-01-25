using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HoughlandLaurenA3
{
    public partial class Cell : UserControl
    {
        public event EventHandler OnCellClick;
        Panel myPanel;
        Button myButton;
        int size = 25;
        int x;
        int y;
        Label myLabel = new Label();
        
        public Cell()
        {
            InitializeComponent();
            this.Padding = new Padding(0);
            this.Size = new Size(size, size);
            SetButton();
            SetLabel();
            SetPanel();
            
        }

        private List<Cell> Surrounding { get; set; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Button MyButton { get => myButton; }

        public Color MyColor { get => myPanel.BackColor; set => myPanel.BackColor = value; }

        public Label MyLabel { get => myLabel; }


        private void SetButton()
        {
            myButton = new Button();
            myButton.Size = new Size(size, size);
            myButton.Location = this.Location;
            myButton.BackColor = DefaultBackColor;
            myButton.Click += OnButtonClick;
            this.Controls.Add(myButton);
            
        }
        private void SetLabel()
        {
            myLabel = new Label();
            myLabel.Size = new Size(size, size);
            myLabel.Location = this.Location;
            myLabel.Text = " ";
            myLabel.BackColor = Color.DarkSeaGreen;
            this.Controls.Add(myLabel);
        }
        private void SetPanel()
        {
            myPanel = new Panel();
            myPanel.Size = new Size(size, size);
            myPanel.Location = this.Location;
            myPanel.BackColor = Color.DarkSeaGreen;
            this.Controls.Add(myPanel);        }
        
        public void OnButtonClick(object sender, EventArgs e)
        {
            ((Button)sender).Visible = false;

            if(OnCellClick != null)
            {
                OnCellClick(this, EventArgs.Empty);
            }
        }
        
    }
}
