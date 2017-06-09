using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    public partial class Form1 : Form
    {  

      
        public Form1()
        {
            InitializeComponent();
        }
        CourseDataContext db = new CourseDataContext();

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowData();
            Fillgroupname();
        }

        private void ShowData()
        {
            var myquery = from stu in db.Students join
                          grp in db.Groups on
                          stu.group_id equals grp.id
                          select new
                          {   
                              stu.id,
                              stu.fullname,
                              stu.age,
                              grp.name
                          };
            dataGridView1.DataSource = myquery;
        }

        void Fillgroupname()
        {
            var cmbquery = from grp in db.Groups
                           select grp;
            cboxGroupName.DataSource = cmbquery;
            cboxGroupName.DisplayMember = "name";
            cboxGroupName.ValueMember = "id";
                           
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.fullname = txtFull.Text;
            stu.age =Convert.ToInt32(txtAge.Text);
            stu.group_id = (int)cboxGroupName.SelectedValue;
            db.Students.InsertOnSubmit(stu);
            db.SubmitChanges();
            ShowData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id =int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Student stu = db.Students.First(a => a.id == id);
            db.Students.DeleteOnSubmit(stu);
            db.SubmitChanges();
            ShowData();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            txtFull.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAge.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cboxGroupName.Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Student stu = db.Students.First(b => b.id == id);
            stu.fullname = txtFull.Text;
            stu.age = int.Parse(txtAge.Text);
            stu.group_id = (int)cboxGroupName.SelectedValue;
            db.SubmitChanges();
            ShowData();

        }
    }
}
