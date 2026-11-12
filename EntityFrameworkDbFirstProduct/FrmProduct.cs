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

namespace EntityFrameworkDbFirstProduct
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }
        DBEntityProjectEntities db = new DBEntityProjectEntities();


        private void FrmProduct_Load(object sender, EventArgs e)
        {
            var values = db.Tbl_Category.ToList();
            cmbProductCategory.DisplayMember = "CategoryName";
            cmbProductCategory.ValueMember = "CategoryId";
            cmbProductCategory.DataSource = values; ;
            ProductList();
           
        }
        void ProductList()
        {
            dataGridView1.DataSource = db.Tbl_Product.ToList();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            Tbl_Product tbl_Product = new Tbl_Product();
            tbl_Product.ProductName = txtProductName.Text;
            tbl_Product.ProductPrice = decimal.Parse(txtProductPrice.Text);
            tbl_Product.ProductStock = int.Parse(txtProductStock.Text);
            tbl_Product.CategoryId = int.Parse(cmbProductCategory.SelectedValue.ToString());
            db.Tbl_Product.Add(tbl_Product); 
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProductList();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtProductId.Text);
            var value = db.Tbl_Product.Find(id);
            db.Tbl_Product.Remove(value);
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ProductList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtProductId.Text);
            var value = db.Tbl_Product.Find(id);
            value.ProductName = txtProductName.Text;
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ProductList();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values= db.Tbl_Product.Where(x=>x.ProductName ==txtProductName.Text).ToList();
            dataGridView1.DataSource = values;
        }

        //kategori ile listeleme
        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            var values=db.Tbl_Product.Join(db.Tbl_Category,product=>product.CategoryId,
                category => category.CategoryId,
                (product,category)=> new
                {
         //sütun isimleri
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductStock = product.ProductStock,
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName   
            }).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
