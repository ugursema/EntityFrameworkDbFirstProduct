using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDbFirstProduct
{
    public partial class FrmCategory : Form
    {
        DBEntityProjectEntities db = new DBEntityProjectEntities();
        public FrmCategory()
        {
            InitializeComponent();
        }
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            CategoryList();
        }

        void CategoryList()
        {
            //var values = db.Tbl_Category.ToList();
            dataGridView1.DataSource = db.Tbl_Category.ToList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tbl_Category tbl_Category = new Tbl_Category();
            tbl_Category.CategoryName = txtCategoryName.Text;
            db.Tbl_Category.Add(tbl_Category);
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CategoryList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var value = db.Tbl_Category.Find(id);
            db.Tbl_Category.Remove(value);
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            CategoryList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var value = db.Tbl_Category.Find(id);
            value.CategoryName=txtCategoryName.Text;
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            CategoryList();
        }
    }
}
