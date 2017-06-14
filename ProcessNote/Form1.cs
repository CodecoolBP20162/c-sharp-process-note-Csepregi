using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.Dynamic;


namespace ProcessNote
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            Process process = new Process();
            process.StartInfo.FileName = text;
            process.Start();
            loadProcessList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadProcessList();
        }

        private void loadProcessList()
        {

            listView1.Items.Clear();
            listView1.FullRowSelect = true;
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {

                ListViewItem item = new ListViewItem(process.ProcessName);
                item.SubItems.Add(process.Id.ToString());

                item.Tag = process;
                listView1.Items.Add(item);
            }
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];

            Process process = (Process)item.Tag;
            process.Kill();
            loadProcessList();
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            listView2.Items.Clear();
            string item_Id = listView1.SelectedItems[0].SubItems[1].Text;      
            int int_item = Int32.Parse(item_Id);
            Process process = Process.GetProcessById(int_item);                      
            
            //listView2.Items.Clear();
            //listView2.FullRowSelect = true;                                  
                ListViewItem item = new ListViewItem(process.PagedMemorySize64.ToString());

                //item.SubItems.Add(process.PagedMemorySize64.ToString());
                item.SubItems.Add((DateTime.UtcNow - process.StartTime.ToUniversalTime()).ToString());


                item.SubItems.Add(process.StartTime.ToString());


                item.Tag = process;
                listView2.Items.Add(item);
                //listView2.Items.Add((process.PagedMemorySize64.ToString()));
                //listView2.Items.Add((DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString());           
        }

        
    }
}
 
    

