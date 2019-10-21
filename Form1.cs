using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MySearchEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
            textBox2.Text = "*.txt";
            textBox3.Text = "";
            if (File.Exists(Application.StartupPath + "parametres.txt")) //загрузка старого поиска 
            {
                StreamReader opening = new StreamReader(Application.StartupPath + "parametres.txt");
                textBox1.Text = opening.ReadLine();
                textBox2.Text = opening.ReadLine();
                if (textBox2.Text == "")
                {
                    textBox2.Text = "*.txt";
                }
                textBox3.Text = opening.ReadLine();
                opening.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            StreamWriter draft = new StreamWriter(Application.StartupPath + "parametres.txt"); //сохранение поиска в файл
            draft.WriteLine(textBox1.Text);
            draft.WriteLine(textBox2.Text);
            draft.WriteLine(textBox3.Text);
            draft.Close();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            button1.Enabled = false;
            button2.Enabled = false;
            
            
            try
            {
                DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
                treeView1.Nodes.Clear();
                Tree(dir, treeView1.Nodes);
            }
            catch (UnauthorizedAccessException ex)
            {
                button4.Enabled = true;
                button2.Enabled = false;
                MessageBox.Show(ex.Message);
            }
            catch (OutOfMemoryException ex)
            {
                button4.Enabled = true;
                button2.Enabled = false;
                MessageBox.Show(ex.Message);
            }
            
        }

       

        private void Button4_Click(object sender, EventArgs e)
        {
            
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = false;
            label2.Text = "";
            treeView1.Nodes.Clear();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Tree(DirectoryInfo directoryinfo, TreeNodeCollection addInMe)
        {

            TreeNode curNode = addInMe.Add(directoryinfo.Name);
            

            foreach (DirectoryInfo subdir in directoryinfo.GetDirectories())
            {

                Tree(subdir, curNode.Nodes);
                
            }

            foreach (FileInfo file in directoryinfo.GetFiles(textBox2.Text))
            {
                label2.Text = file.FullName;
                

                if (File.ReadAllText(file.FullName).Contains(textBox3.Text))
                {
                    curNode.Nodes.Add(file.Name);
                    
                }

            }
            button2.Enabled = false;
            button4.Enabled = true;
        }
        
        
    }
}
