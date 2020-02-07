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
    public partial class FormFind : Form
    {
        public FormFind()
        {
            InitializeComponent();
        }

        private void FormFind_Load(object sender, EventArgs e)
        {
            

        }

        private void FormFind_Shown(object sender, EventArgs e)
        {
           textBox1.Focus();
        }
        private String getSearchText()
        {
            return textBox1.Text;
        }
        private void setSearchText(String val)
        {
            textBox1.Text = val;
        }
        public String SearchText
        {
            get => getSearchText();
            set { setSearchText(value); }
        }
        private bool getCaseSensetive()
        {
            return checkBoxCaseSensetive.Checked;
        }
        private void setCaseSensetive(bool val)
        {
            checkBoxCaseSensetive.Checked = val;
        }
        public bool CaseSensetive
        {
            get => getCaseSensetive();
            set { setCaseSensetive(value); }
        }

        private void checkBoxCaseSensetive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private bool GetWholeWordOnly()
        {
            return checkBoxWholeWordsOnly.Checked;
        }
        private void SetWholeWordOnly(bool val)
        {
            checkBoxWholeWordsOnly.Checked = val;
        }
        public bool WholeWordOnly
        {
            get => GetWholeWordOnly();
            set { SetWholeWordOnly(value); }
        }

        private void checkBoxWholeWordsOnly_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
