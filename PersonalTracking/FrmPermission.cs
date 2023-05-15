using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public PermissionDetailDTO detail = new PermissionDetailDTO();
        TimeSpan PermissionDay;
        public bool isUpdate = false;
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
            if(isUpdate)
            {
                dpStart.Value = detail.StartDate;
                dpFinish.Value = detail.FinishDate;
                txtDayAmount.Text = detail.PermissionDayAmount.ToString();
                txtExplanation.Text = detail.Explanation;
                txtUserNo.Text = detail.UserNo.ToString();

            }
        }

        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpFinish.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("PLease change the start date or end date");
            else if (Convert.ToInt32(txtDayAmount.Text) <= 0)
                MessageBox.Show("Permission Day must be bigger than 0");
            else if (txtExplanation.Text.Trim() == "")
                MessageBox.Show("Explanation is empty");
            else
            {
                PERMISSION permission = new PERMISSION();
                if (!isUpdate)
                {
                    permission.EmployeeID = UserStatic.EmployeeID;
                    permission.PermissionState = 1;
                    permission.PermissionStartDate = dpStart.Value.Date;
                    permission.PermissionEndDate = dpFinish.Value.Date;
                    permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                    permission.PermissionExplanation = txtExplanation.Text;
                    PermissionBLL.AddPermission(permission);
                    MessageBox.Show("Permission was added");
                    permission = new PERMISSION();
                    dpStart.Value = DateTime.Today;
                    dpFinish.Value = DateTime.Today;
                    txtDayAmount.Clear();
                    txtExplanation.Clear();
                }
                else if(isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are you sure","Warning", MessageBoxButtons.YesNo);
                    permission.ID = detail.PermissionID;
                    permission.PermissionExplanation = txtExplanation.Text;
                    permission.PermissionStartDate = dpStart.Value;
                    permission.PermissionEndDate = dpFinish.Value;
                    permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                    PermissionBLL.UpdatePermission(permission);
                    MessageBox.Show("Permission was updated");
                    this.Close();
                }
            }
        }
    }
}
