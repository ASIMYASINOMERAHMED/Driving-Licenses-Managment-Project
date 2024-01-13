using Driving_Licenses_Managment.Properties;
using DVLDBussiness1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }


      



        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (button4.Focus())
            {
                if (CTextBoxPassword.PasswordChar == true)
                {

                    button3.BackgroundImage = Resources.eyeclose2;
                    CTextBoxPassword.PasswordChar = false;

                }
                else
                {
                    button3.BackgroundImage = null;
                    button3.BackgroundImage = Resources.eyePassword_removebg_preview;
                    CTextBoxPassword.PasswordChar = true;
                }
            }
        }
        string UserName, Password;
        private async void btnLogin_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            if (CTextBoxUserName.Texts == "UserName" || CTextBoxPassword.Texts == "Password")
            {
                lbError.Text = "Enter UserName And Password!";
                lbError.Visible = true;
                SystemSounds.Hand.Play();
                Cursor = Cursors.Default;
                await Task.Delay(1500);
                lbError.Visible = false;
                return;
            }
            UserName = CTextBoxUserName.Texts;
            Password = CTextBoxPassword.Texts;
            clsUser User = clsUser.FindByUserNameAndPassword(UserName.Trim(),clsUtil.ComputeHash(Password.Trim()));
            if( User != null )
            {

                if (ckbRememberme.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUserNameAndPasswordInRegistry(UserName.Trim(), Password.Trim());

                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUserNameAndPasswordInRegistry("", "");

                }





                if (!User.IsActive)
                {
                    CTextBoxUserName.Focus();
                    lbError.Text = "User is Not Active!";
                    lbError.Visible = true;
                    SystemSounds.Hand.Play();
                    Cursor = Cursors.Default;
                    await Task.Delay(1500);
                    lbError.Visible = false;
                    return;
                }
                clsGlobal.CurrentUser = User;
                this.Hide();
                Cursor = Cursors.Default;
                    Main frm = new Main(this);
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                
 
            }
            else
            {
                CTextBoxUserName.Focus();
                lbError.Text = "Invalid UserName/Password!";
                lbError.Visible = true;
                SystemSounds.Hand.Play();
                Cursor = Cursors.Default;
                await Task.Delay(1500);
                lbError.Visible = false;
                return;
            }


          
        }

        private void BttnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void CTextBoxPassword__TextChanged(object sender, EventArgs e)
        {
            if (CTextBoxPassword.Texts != "" && CTextBoxPassword.Texts != "Password")
                button3.Visible = true;
            else
                button3.Visible = false;
        }

        private void CTextBoxUserName_Enter(object sender, EventArgs e)
        {
            if (CTextBoxUserName.Texts == "UserName")
                CTextBoxUserName.Texts = "";
            CTextBoxUserName.ForeColor = Color.Black;
        }

        private void CTextBoxUserName_Leave(object sender, EventArgs e)
        {
            if (CTextBoxUserName.Texts == "")
                CTextBoxUserName.Texts = "UserName";
            CTextBoxUserName.ForeColor = Color.DarkGray;
        }

        private void CTextBoxPassword_Enter(object sender, EventArgs e)
        {

            if (CTextBoxPassword.Texts == "Password")
            {
                CTextBoxPassword.Texts = "";
                CTextBoxPassword.PasswordChar = true;
            }
            CTextBoxPassword.ForeColor = Color.Black;
        }

        private void CTextBoxPassword_Leave(object sender, EventArgs e)
        {
            if (CTextBoxPassword.Texts == "")
            {
                CTextBoxPassword.PasswordChar = false;
                CTextBoxPassword.Texts = "Password";
            }
            CTextBoxPassword.ForeColor = Color.DarkGray;
        }

        private void BttnClose_MouseEnter(object sender, EventArgs e)
        {
            BttnClose.BackColor = Color.Red;
        }

        private void BttnClose_MouseLeave(object sender, EventArgs e)
        {
            BttnClose.BackColor = Color.Transparent;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
                if (clsGlobal.GetStoredCredentialFromRegistery(ref UserName,ref Password))
                {

                    CTextBoxUserName.Texts =UserName;
                    CTextBoxPassword.Texts = Password;
                    ckbRememberme.Checked = true;
                }
                else
                {
                    CTextBoxUserName.Texts = "UserName";
                    CTextBoxPassword.Texts = "Password";
                    ckbRememberme.Checked = false;
                }
         
          
        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            if (button4.Focus())
            {
                if (CTextBoxPassword.PasswordChar == true)
                {

                    button3.BackgroundImage = Resources.eyeclose2;
                    CTextBoxPassword.PasswordChar = false;

                }
                else
                {
                    button3.BackgroundImage = null;
                    button3.BackgroundImage = Resources.eyePassword_removebg_preview;
                    CTextBoxPassword.PasswordChar = true;
                }
            }
        }
    }
}
