using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment
{
    public partial class frmPersonDetails : Form
    {
     
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrPersonInfo1.LoadPersonInfo(PersonID);
        }
        public frmPersonDetails(string NationalNo)
        {
            InitializeComponent();
            ctrPersonInfo1.LoadPersonInfo(NationalNo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        private void frmPersonDetails_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void frmPersonDetails_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void frmPersonDetails_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
    }
}
