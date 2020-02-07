using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            labelAbout.Text =
                "First line \n" +
                "Second line \n" +
                "Third line \n" +
                "Fouth line \n" +
                "First line \n" +
                "Second line \n" +
                "Last line of text";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.microsoft.com");
        }
    }
}
