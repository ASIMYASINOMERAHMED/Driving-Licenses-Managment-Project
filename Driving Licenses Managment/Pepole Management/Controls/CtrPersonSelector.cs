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
    public partial class CtrPersonSelector : UserControl
    {
        public CtrPersonSelector()
        {
            InitializeComponent();

        }

        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

     


        private int _PersonID = -1;

        public int PersonID
        {
            get { return ctrPersonInfo1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrPersonInfo1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilterBy.SelectedIndex = 1;
            textBoxfilter.Text = PersonID.ToString();
            FindNow();

        }

        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID.":
                    ctrPersonInfo1.LoadPersonInfo(int.Parse(textBoxfilter.Text));

                    break;

                case "National No.":
                    ctrPersonInfo1.LoadPersonInfo(textBoxfilter.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(ctrPersonInfo1.PersonID);
        }

    


        private void textBoxfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnSearch.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }
        public void FilterFocus()
        {
            textBoxfilter.Focus();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxfilter.Text = "";
            textBoxfilter.Focus();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEdit = new frmAddEditPerson();
            frmAddEdit.StartPosition = FormStartPosition.CenterScreen;
            frmAddEdit.DataBack += DataBackEvent;
            frmAddEdit.ShowDialog();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            textBoxfilter.Text = PersonID.ToString();
            ctrPersonInfo1.LoadPersonInfo(PersonID);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();
        }

        private void CtrPersonSelector_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            textBoxfilter.Focus();
        }

        private void textBoxfilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxfilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxfilter, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(textBoxfilter, null);
            }
        }

   
    }
}
