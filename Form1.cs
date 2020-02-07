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
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using System.Xml;


namespace WindowsFormsApp2
{
    public partial class FormMain : Form
    {
        public const String CDefaultFileName = "noname.txt";
        private String FFileName;
        private String FInitDir;
        //  private ConfigLoaderApp FConf;
        //  private ConfigLoaderLng FConfLng;
        private List<string> FRecentFiles = new List<string>();
        private Font FFontText;
        private Color FColor;
        private FormFind FFormFind;
        private ConfigLoaderAppJSON FCnf;
        private ConfigLoaderLngJSON FCnfLng;
        private Product[] FListPro;

        public FormMain()
        {
            InitializeComponent();
            FCnf = new ConfigLoaderAppJSON();

            FCnf.FileName = @"main.json";

            if (!(FCnf.LoadFromFile()))
            {
                ///
            }
            else
            {
                this.Left = FCnf.AppSettings.Windows.Left;
                this.Top = FCnf.AppSettings.Windows.Top;
                this.Width = FCnf.AppSettings.Windows.Width;
                this.Height = FCnf.AppSettings.Windows.Height;
                this.WindowState = FCnf.AppSettings.Windows.State;
                this.FInitDir = FCnf.AppSettings.InitialDir;
            }
            //  this.FRecentFiles = FConf.Recent;
            //  this.FFontText = FConf.FontText;
            //   Editor.Font = FConf.FontText;
            //   Editor.BackColor = FConf.ColorBack;
            //  FConfLng = new ConfigLoaderLng();

            FCnfLng = new ConfigLoaderLngJSON();
            FCnfLng.FileName = @"ru.json";
            if (!(FCnfLng.LoadFromFile()))
            {

            }
            else
            {
                //  this.FInitDir = FCnfLng.LngSettings.InitialDir;
                this.fileToolStripMenuItem.Text = FCnfLng.LngSettings.stripmenuitemFile;
                this.editToolStripMenuItem.Text = FCnfLng.LngSettings.stripmenuitemEdit;
            }
            //var LTypeLang = this.GetType();  //Отражаем структуру формы

            //foreach (var lngField in LTypeLang.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)) //проходим  по полям отражения ( не экземпляра!)
            //{
            //    string[] optArr; //

            //    if (lngField.Name.ToString().ToLower().Contains("toolstripmenuitem")) //если содержит имя параметра меню...
            //    {

            //        //проверка на принадлежность к классу???
            //        //var obj = lngField.Module;
            //        try
            //        {
            //            //svar LMethod = typeof(ToolStripMenuItem).GetProperty("Text",BindingFlags.Instance|BindingFlags.CreateInstance); 
            //            //How to avoid "Possible System.NullReferenceException"?

            //            optArr = lngField.Name.ToString().Split(new Char[] { 'T' }); // парсим имя параметра  - слово до "..Tool..." - ключевое в файле локализации
            //            var LInstanceOfMenu = lngField.GetValue(this); //возвращаем имеющийся экземпляр пункта меню 
            //            var LMethod = LInstanceOfMenu.GetType().GetProperty("Text"); //получаем описание (PropertyInfo Class) свойства "Text"

            //            LMethod.SetMethod.Invoke(LInstanceOfMenu, new Object[] { FConfLng.GetMessage(optArr[0]) }); // применяем (Invoke) метод к текущему полученному экз-ру ...ToolStripMenuItem 

            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //            throw;
            //        }

            //    }
            // }

            //this.fileToolStripMenuItem.Text = FConfLng.GetMessage("msgmenufilemain");


            //this.fileToolStripMenuItem.Text = FCnfLng.MenuMainFile;
            //    this.editToolStripMenuItem.Text = FCnfLng.MenuMainEdit;
            //}
            FileName = CDefaultFileName;
            FFormFind = null;
            timer.Enabled = true;

        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FCnf.AppSettings.Windows.Left = this.Left;
            FCnf.AppSettings.Windows.Top = this.Top;
            FCnf.AppSettings.Windows.Width = this.Width;
            FCnf.AppSettings.Windows.Height = this.Height;
            FCnf.AppSettings.Windows.State = this.WindowState;
            FCnf.AppSettings.InitialDir = this.FInitDir;
            //  FCnf.Recent = this.FRecentFiles;
            //  FCnf.FontText = Editor.Font;
            //   FCnf.ColorBack = Editor.BackColor;

            // FConf.FontText = this.FFontText;
            FCnf.SaveToFile();

            // FCnfLng.LngSettings.InitialDir = this.FInitDir;
            FCnfLng.SaveToFile();
        }
        private void DoFileOpen()
        {
            openFileDialog.Title = "Укажите имя файла...";
            openFileDialog.InitialDirectory = this.FInitDir;
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Editor.Text = File.ReadAllText(openFileDialog.FileName, Encoding.Default);
                Editor.Modified = false;
                this.FileName = openFileDialog.FileName;
                this.FInitDir = Path.GetFullPath(openFileDialog.FileName);

                // if (FConf.Recent.Contains(this.FileName)) { 

                //     FRecentFiles.Remove(this.FileName);
                // }

                // this.FRecentFiles.Insert(0, this.FileName);
                //if (FRecentFiles.Count > 10) { FRecentFiles.RemoveAt(10); }


            }
        }

        private void DoFileSave()
        {
            saveFileDialog.Title = "Куда сохранить файл?...";
            saveFileDialog.InitialDirectory = this.FInitDir;
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter =
                "Текстовые файлы UTF-8|*.txt|" +
                "Текстовые файлы ANSI|*.txt|" +
                "Все файлы|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = this.FileName;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (0 == saveFileDialog.FilterIndex)
                {
                    File.WriteAllText(saveFileDialog.FileName, Editor.Text, Encoding.UTF8);
                }
                else
                {
                    File.WriteAllText(saveFileDialog.FileName, Editor.Text, Encoding.Default);
                }
                Editor.Modified = false;
                this.FileName = saveFileDialog.FileName;
                this.FInitDir = Path.GetFullPath(saveFileDialog.FileName);
                if (FRecentFiles.Count > 10) { FRecentFiles.RemoveAt(10); }

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StatusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            StatusBar.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void WordWrapToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Editor.WordWrap = wordWrapToolStripMenuItem.Checked;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.Modified)
            {
                DialogResult ask = MessageBox.Show(
                    String.Format("Save changes in file \"{0}\" ?", FileName)
                    , "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (ask)
                {
                    case DialogResult.Yes:
                        {
                            DoFileSave();
                            break;
                        }
                    case DialogResult.No:
                        {
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            return;
                        }
                }
            }
            DoFileOpen();
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFileSave();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Undo();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout F = new FormAbout();
            F.ShowDialog();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Paste();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Clear();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.SelectAll();
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        protected virtual void SetFileName(String value)
        {
            FFileName = value;
            this.Text = value;
        }

        public String FileName
        {
            get => FFileName;
            set { SetFileName(value); }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == FFormFind)
            {
                FFormFind = new FormFind();
            }
            if (FFormFind.ShowDialog() != DialogResult.OK) return;
            String LText = Editor.Text;
            String LSearchText = FFormFind.SearchText;
            int LIndx = 0;
            if (!FFormFind.CaseSensetive)//без учета регистра
            {
                LText = LText.ToUpper();
                LSearchText = LSearchText.ToUpper();
            }

            if (FFormFind.WholeWordOnly)//только слово целиком
            {
                LIndx = Regex.Match(LText, @"\b(" + LSearchText + @")\b").Index;

            }
            else if (!FFormFind.WholeWordOnly)
            {

                LIndx = LText.IndexOf(LSearchText);
            }

            if ((0 == LIndx) || (-1 == LIndx))
            {
                MessageBox.Show(String.Format("Searching Text\"{0}\" was  not founded in file.", FFormFind.SearchText,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information));
                return;
            }
            Editor.SelectionStart = LIndx;
            Editor.SelectionLength = LSearchText.Length;
            Editor.ScrollToCaret();
        }

        private void WordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.WordWrap = ((ToolStripMenuItem)(sender)).Checked;
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDlg.Color = Editor.ForeColor;
            fontDlg.Font = Editor.Font;

            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                Editor.ForeColor = fontDlg.Color;
                Editor.Font = fontDlg.Font;

            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CDefaultFileName != FileName)
            {
                File.WriteAllText(FFileName, Editor.Text, Encoding.UTF8);
            }
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Editor.ForeColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Editor.BackColor = colorDialog.Color;

            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            String LURLGroup = @"http://77.121.253.160:8080/rastipuzzo/entrs_r_tmc_groupshare.php";

            String LFileNameGr = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            LFileNameGr = Path.Combine(LFileNameGr, "myfilegroup.json");

            WebClient WCGr = new WebClient();
            WCGr.DownloadFile(LURLGroup, LFileNameGr);
            String LJSON = "";

            LJSON = DoRemove(LFileNameGr);

            List<DataTMCGroup> LLst = JsonConvert.DeserializeObject<List<DataTMCGroup>>(LJSON);
            Editor.Text = "";
            String LItemStr = "";
            String LCount = "";

            foreach (DataTMCGroup LTMC in LLst)
            {
                LCount = "myGroup" + LTMC.TMC_GROUP_ID;
                String LURL = @"http://77.121.253.160:8080/rastipuzzo/entrs_r_tmc.php?TMC_GROUP_ID=" + LTMC.TMC_GROUP_ID;
                String LFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                LFileName = Path.Combine(LFileName, LCount);
                WebClient WC = new WebClient();
                WC.DownloadFile(LURL, LFileName);
                LJSON = DoRemove(LFileName);
                List<DataTMC> LLst1 = JsonConvert.DeserializeObject<List<DataTMC>>(LJSON);
                LItemStr += Environment.NewLine +
                    LTMC.TMC_GROUP_ID.PadRight(12) +
                    LTMC.NAME.PadLeft(12) +
                    Environment.NewLine + Environment.NewLine;
                foreach (DataTMC LTMC1 in LLst1)
                {
                    LItemStr += " " +
                     LTMC1.ID + ". " +
                     LTMC1.NAME.PadRight(52) +
                     LTMC1.EDIZM_SNAME.PadLeft(5) +
                    LTMC1.PRICE.PadLeft(12) + Environment.NewLine;
                }
            }
            Editor.Text = LItemStr;

        }
        public String DoRemove(string LFile)
        {
            String LJSON = File.ReadAllText(LFile, Encoding.UTF8);
            String LPatch = "\"DATA\":";
            LJSON = LJSON.Substring(LJSON.IndexOf(LPatch) + LPatch.Length);
            int i = LJSON.Length - 1;
            while ((i > 0) && (!LJSON[i].Equals(']')))
            {
                i--;
            }
            i++;
            LJSON = LJSON.Remove(i);

            return LJSON;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            String LURLGroup = @"http://resources.finance.ua/ru/public/currency-cash.xml";

            String LFileNameGr = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            LFileNameGr = Path.Combine(LFileNameGr, "mycurses.xml");

            if (!File.Exists(LFileNameGr))
            {
                WebClient WCGr = new WebClient();
                WCGr.DownloadFile(LURLGroup, LFileNameGr);
            }
            //  WebClient WCGr = new WebClient();
            //   WCGr.DownloadFile(LURLGroup, LFileNameGr);

            XmlDocument LXMLDoc = new XmlDocument();
            LXMLDoc.Load(LFileNameGr);

            XmlElement LRootNode = LXMLDoc.DocumentElement;
            string LResult = "";
            foreach (XmlNode LNode in LRootNode)
            {
                if (LNode.Name.ToLower().Equals("organizations"))
                {
                    foreach (XmlNode LNodeOrganiz in LNode.ChildNodes)
                    {
                        if (LNodeOrganiz.Name.ToLower().Equals("organization"))
                        {
                            foreach (XmlNode LAttrOrg in LNodeOrganiz.ChildNodes)
                            {
                                if (LAttrOrg.Name.ToLower().Equals("title"))
                                {
                                    foreach (XmlNode LNodeTitle in LAttrOrg.Attributes)
                                    {
                                      LResult += LNodeTitle.Value;

                                    }
                                }
                                if (LAttrOrg.Name.ToLower().Equals("currencies"))
                                {
                                    foreach (XmlNode LNodeCur in LAttrOrg.ChildNodes)
                                    {
                                        if (LNodeCur.Name.ToLower().Equals("id"))
                                        {
                                            LResult += " "+ LNodeCur.Value + Environment.NewLine;

                                        }
                                    }
                                }






                            }
                            //foreach (XmlNode LNode in LRootNode)
                            //{
                            //    if (LNode.Name.ToLower().Equals("currencies"))
                            //    {
                            //        foreach (XmlNode LNodeCities in LNode.ChildNodes)
                            //        {
                            //            foreach (XmlNode LAttrCity in LNodeCities.Attributes)
                            //            {
                            //                if (LAttrCity.Name.ToLower().Equals("id"))
                            //                {
                            //                    LResult += LAttrCity.Value + " =";
                            //                }
                            //            }
                            //            foreach (XmlNode LAttrCity1 in LNodeCities.Attributes)
                            //            {
                            //                if (LAttrCity1.Name.ToLower().Equals("title"))
                            //                {
                            //                    LResult += " " + LAttrCity1.Value + Environment.NewLine;
                            //                }

                            //            }

                            //        }
                            //    }
                            //}
                            // Editor.Text = LResult;
                        }
                    }
                }
            }
            Editor.Text = LResult;
        }

    }
}