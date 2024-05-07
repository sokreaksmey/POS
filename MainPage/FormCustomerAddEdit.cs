using MainPage.Data_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainPage
{
    public partial class FormCustomerAddEdit : Form
    {
        Customer _customer;
        bool newcustomer;
        public FormCustomerAddEdit(Customer customer)
        {
            InitializeComponent();
            if(customer == null )
            {
                this._customer = new Customer();
                lblTitle.Text = "New Customer";
                this.newcustomer = true;
                txtCustomerName.Focus();
            }
            else
            {
                this._customer = customer;
                this.newcustomer = false;
                lblTitle.Text = "Edit Customer";
                InitializeData();
                txtCustomerName.Focus();

            }
        }
        void InitializeData()
        {
            txtCustomerName.Text = _customer.CustomerName;
            txtCompanyName.Text = _customer.CompanyName;
            txtPhone.Text = _customer.Phone;
            txtEmail.Text = _customer.Email;
            txtAddress.Text = _customer.Address;
        }


        private void FormCustomerAddEdit_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DoValidation())
            {
                _customer.CustomerName = txtCustomerName.Text.Trim();
                _customer.CompanyName = txtCompanyName.Text.Trim();
                _customer.Phone = txtPhone.Text.Trim();
                _customer.Email = txtEmail.Text.Trim();
                _customer.Address = txtAddress.Text.Trim();
                if (newcustomer)
                {
                    Customers.Add(_customer);
                }
                else
                {
                    Customers.Update(_customer);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private bool DoValidation()
        {
            bool result = true;
            if (txtCustomerName.Text.Trim() == "")
            {
                epCustomerName.SetError(txtCustomerName, "Please enter Customer Name");
                result = false;
            }
            return result;
        }
    }
}
