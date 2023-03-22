using TaskMe.Migrations;
using TaskMe.Model;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Status = TaskMe.Model.Status;
using Microsoft.EntityFrameworkCore;

namespace TaskMe
{
    public partial class Form1 : Form
    {
        private tubmDBContext tmContext;
        public Form1()
        {
            InitializeComponent();

            tmContext= new tubmDBContext();

            var statuses = tmContext.Statuses.ToList();

            foreach(var stat in statuses) 
            { 
               cboStatus.Items.Add(stat);
            }
            reFreshData();
        }


        private void reFreshData()
        {
            BindingSource bi = new BindingSource();
            var query = from t in tmContext.Tasks
                        orderby t.DueDate
                        select new { t.Id, Taskname = t.Name, StatusName = t.Status.Name, t.DueDate };
            bi.DataSource = query.ToList();
            dataGridView1.DataSource = bi;
            dataGridView1.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reFreshData();

        }

      

        private void cmdCreateTask_Click(object sender, EventArgs e)
        {
            if(cboStatus.SelectedItem != null && txtTask.Text != String.Empty)
            {
                var newTask = new Model.Task
                {
                   Name= txtTask.Text,
                   StatusId =(cboStatus.SelectedItem as Model.Status).Id,
                   DueDate = dateTimePicker1.Value
                };
                tmContext.Tasks.Add(newTask);
                tmContext.SaveChanges();
                txtTask.Clear();
                reFreshData();
            }
            else  
            {
                MessageBox.Show("Please make sure all data has been entered");
            }
        }

       

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            var t = tmContext.Tasks.Find((int)dataGridView1.SelectedCells[0].Value);
            tmContext.Tasks.Remove(t);
            tmContext.SaveChanges(true);

            reFreshData();
        }

        

        private void cmdUpdateTask_Click(object sender, EventArgs e)
        {
            

                if (cmdUpdateTask.Text == "Update")
                {

                     txtTask.Text = dataGridView1.SelectedCells[1].Value.ToString();
                     dateTimePicker1.Value = (DateTime)dataGridView1.SelectedCells[3].Value;

                foreach (Status t in cboStatus.Items)
                {
                    if (t.Name == dataGridView1.SelectedCells[2].Value.ToString())
                    {
                        cboStatus.SelectedItem = t;
                    }
                }
                     cmdUpdateTask.Text = "Save";
                }
               else if (cmdUpdateTask.Text == "Save")
                {   
                    var t = tmContext.Tasks.Find((int)dataGridView1.SelectedCells[0].Value);
                    t.Name = txtTask.Text;
                    t.StatusId = (cboStatus.SelectedItem as Status).Id;
                    t.DueDate = dateTimePicker1.Value;
                    tmContext.SaveChanges();
                    reFreshData();

                    cmdUpdateTask.Text = "Update";
                    txtTask.Text = String.Empty;
                    dateTimePicker1.Value = DateTime.Now;
                    cboStatus.Text = "Please Select....";
                }
           


        }

       

        private void button1_Click(object sender, EventArgs e)
        {

            

             
            if (string.IsNullOrEmpty(txtSearchBox.Text))
            {
                    MessageBox.Show("Please input your search parameter.");
            }
            if (int.TryParse(txtSearchBox.Text, out int num))
            {
                BindingSource bi = new BindingSource();
                var query = from t in tmContext.Tasks
                orderby t.DueDate
                where t.Id == num
                select new { t.Id, Taskname = t.Name, StatusName = t.Status.Name, t.DueDate };
                
                
                
                bi.DataSource = query.ToList();
                dataGridView1.DataSource = bi;


                if (dataGridView1.SelectedCells.Count == 0)
                {
                    bi.Clear();
                    MessageBox.Show("Record not found");

                }
                else
                dataGridView1.Refresh();


            }

            else
            {
                BindingSource bi2 = new BindingSource();
                var query = from t in tmContext.Tasks
                orderby t.DueDate
                where t.Name.StartsWith(txtSearchBox.Text) || t.Name.Contains(txtSearchBox.Text)
                select new { t.Id, Taskname = t.Name, StatusName = t.Status.Name, t.DueDate };

               
                
                bi2.DataSource = query.ToList();
                dataGridView1.DataSource = bi2;

                if (dataGridView1.SelectedCells.Count == 0)
                {
                    bi2.Clear();
                    MessageBox.Show("Record not found");

                }
                else
                dataGridView1.Refresh();
                

                

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            reFreshData();
        }

        private void cmdDeleteAllTask_Click(object sender, EventArgs e)
        {

            var allTask = tmContext.Tasks.ToList();
             tmContext.Tasks.RemoveRange(allTask);
             tmContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Tasks', RESEED, 0)");
             tmContext.SaveChanges();
             reFreshData();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, EventArgs e)
        {
        }
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }

}
