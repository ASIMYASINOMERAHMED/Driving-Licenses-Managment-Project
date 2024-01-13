using DVLDBussiness1;
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
    public partial class CtrUserCard : UserControl
    {
        public CtrUserCard()
        {
            InitializeComponent();

        }
        public string CurrentPassword="";

        public void PrintUserCard(int UserID)
        {
            clsUser User = clsUser.FindByUserID(UserID);
            if (User != null)
            {
                CurrentPassword = User.Password;
                ctrPersonInfo1.LoadPersonInfo (User.PersonID);
                lblUserID.Text = UserID.ToString();
                lblUserName.Text = User.UserName;
                if (User.IsActive)
                    lblIsActive.Text = "Yes";
                else
                    lblIsActive.Text = "No";

            }
            else

                MessageBox.Show($"There's No User With ID {UserID}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
