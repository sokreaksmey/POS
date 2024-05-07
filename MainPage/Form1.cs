using MainPage.Data_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainPage
{
    public partial class Form1 : Form
    {
        DataTable dtCustomer;
        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            dtCustomer = Customers.GetAll();
            dgCustomers.DataSource = dtCustomer;
            dgCustomers.Columns[0].Visible = false;

            dgCustomers.Columns[1].HeaderText = "Customer Name";
            dgCustomers.Columns[1].Width = 200;
            dgCustomers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgCustomers.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgCustomers.Columns[1].Visible = true;
            dgCustomers.Columns[2].Visible = false;
            dgCustomers.Columns[3].Visible = false;
            dgCustomers.Columns[4].Visible = false;
            dgCustomers.Columns[5].Visible = false;
        }
        private void dgCustomers_CellPainting(object sender,DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                e.Handled = true;
                using (Brush b = new SolidBrush(dgCustomers.DefaultCellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(b, e.CellBounds);
                }
                using (Pen p = new Pen(Brushes.Black))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    p.Color = Color.FromArgb(33, 37, 41);
                    e.Graphics.DrawLine(p, new Point(0, e.CellBounds.Bottom - 1), new
                    Point(e.CellBounds.Right, e.CellBounds.Bottom - 1));
                    e.Graphics.DrawLine(p, new Point(0, 0), new
                   Point(e.CellBounds.Right, 0));
                }
                e.PaintContent(e.ClipBounds);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormCustomerAddEdit formAddEdit = new FormCustomerAddEdit(null);
            if (formAddEdit.ShowDialog() == DialogResult.OK)
            {
                InitializeData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgCustomers.SelectedRows.Count > 0)
            {
                int customerid =
               Convert.ToInt32(dgCustomers.SelectedRows[0].Cells["CustomerId"].Value.ToString());
                Customer customer = Customers.Get(customerid);
                if (customer == null)
                {
                    MessageBox.Show("Cannot find customer");
                }
                else
                {
                    FormCustomerAddEdit formAddEdit = new
                   FormCustomerAddEdit(customer);
                    if (formAddEdit.ShowDialog() == DialogResult.OK)
                    {
                        InitializeData();
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(this, "Confirmation!\nDo you really want to delete this record ? ","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                int customerid = Convert.ToInt32(dgCustomers.SelectedRows[0].Cells["CustomerId"].Value.ToString());
                Customers.Delete(customerid);

                MessageBox.Show("Customer had deleted successfully.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
               
                InitializeData();
            }

        }

        private void dgCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCustomers.SelectedRows.Count > 0)
            {
                int customerid = Convert.ToInt32(dgCustomers.SelectedRows[0].Cells["CustomerId"].Value.ToString());
                Customer customer = Customers.Get(customerid);
                if (customer != null)
                {
                    txtCustomerName.Text = customer.CustomerName;
                    txtCompanyName.Text = customer.CompanyName;
                    txtPhone.Text = customer.Phone;
                    txtEmail.Text = customer.Email;
                    txtAddress.Text = customer.Address;
                }
            }

        }
    }
}
