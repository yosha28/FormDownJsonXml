using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Newtonsoft.Json;
//using System.Json;



namespace WindowsFormsApp2
{
    class DataTMC
    {
        public string ID;
        public string NAME;
        public string EDIZM_SNAME;
        public string PRICE;
    }
    class DataTMCGroup
    {
       // public string ID;
        public string NAME;
        public string TMC_GROUP_ID;
    }
    class ConfigLoader
    {
        private String FFileName;
        public ConfigLoader()
        {
        }
        protected virtual void DoAfterLoad(String Settings)
        {

        }
        protected virtual String GetSettings()
        {
            String Result = "";
            return Result;
        }
        public bool LoadFromFile()
        {
            bool result = false;
            String LFilePath = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData
            );
            LFilePath = Path.Combine(LFilePath, @"ITStep");
            LFilePath = Path.Combine(LFilePath, @"Appl");
            LFilePath = Path.Combine(LFilePath, FFileName);
            if (!File.Exists(LFilePath))
            {
                SaveToFile();
            }
            String LFileContent = File.ReadAllText(LFilePath);
            DoAfterLoad(LFileContent);
            result = true;
            return result;
        }
        public bool SaveToFile()
        {
            bool result = false;
            String LFilePath = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData
            );
            LFilePath = Path.Combine(LFilePath, @"ITStep");
            if (!Directory.Exists(LFilePath))
            {
                Directory.CreateDirectory(LFilePath);
            }
            LFilePath = Path.Combine(LFilePath, @"Appl");
            if (!Directory.Exists(LFilePath))
            {
                Directory.CreateDirectory(LFilePath);
            }
            LFilePath = Path.Combine(LFilePath, FFileName);
            File.WriteAllText(LFilePath, GetSettings());
            result = true;
            return result;
        }
        public String FileName
        {
            get => FFileName;
            set { FFileName = value; }
        }
    }
    class ConfigLoaderAppJSON : ConfigLoader
    {
        //вложенный класс
        public class AppSett
        {
            public String InitialDir;
            public class Wind
            {
                public int Left;
                public int Top;
                public int Width;
                public int Height;
                public FormWindowState State;
            }
            public Wind Windows;
            public class Edit
            {
                public Color ForegroundColor;
                public Color BackgroundColor;
                public Font EditorFont;
                public Edit()
                {
                   // EditorFont = new Font(null, FontStyle.Regular);
                }

            }
            public Edit Editor;
            public AppSett()
            {
                Windows = new Wind();
                Editor = new Edit();
            }

        }
        public AppSett AppSettings;


        public ConfigLoaderAppJSON()
        {
            AppSettings = new AppSett();
            AppSettings.InitialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            AppSettings.Windows.Left = Screen.PrimaryScreen.WorkingArea.Left;
            AppSettings.Windows.Top = Screen.PrimaryScreen.WorkingArea.Top;
            AppSettings.Windows.Height = Screen.PrimaryScreen.WorkingArea.Height;
            AppSettings.Windows.Width = Screen.PrimaryScreen.WorkingArea.Width;
            AppSettings.Windows.State = FormWindowState.Normal;
            AppSettings.Editor.BackgroundColor = Color.White;
            AppSettings.Editor.ForegroundColor = Color.Black;
            //  AppSettings.Editor.EditorFont=Font.

        }
        protected override void DoAfterLoad(String Settings)
        {
            if (Settings.Length > 0)
            {
                AppSettings = JsonConvert.DeserializeObject<AppSett>(Settings);
            }
        }
        protected override String GetSettings()
        {
            return JsonConvert.SerializeObject(AppSettings,Newtonsoft.Json. Formatting.Indented);
        }


    }
    class ConfigLoaderApp : ConfigLoader
    {
        private int FWindowX;
        private int FWindowY;
        private int FWindowWidth;
        private int FWindowHeight;
        private int FWindowState;
        private String FInitDir;
        private List<string> FRecent;
        private Font FFont;
        private Color FColorBack;
        private FontConverter FFont1 = new FontConverter();
        private ColorConverter FColor1 = new ColorConverter();

        public ConfigLoaderApp()
        {
            Screen LScreen = Screen.PrimaryScreen;
            FWindowX = LScreen.WorkingArea.Left;
            FWindowY = LScreen.WorkingArea.Top;
            FWindowWidth = LScreen.WorkingArea.Width;
            FWindowHeight = LScreen.WorkingArea.Height;
            FWindowState = (int)FormWindowState.Normal;
            FInitDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FRecent = new List<string>();
            FFont = Form.DefaultFont;
            FColorBack = Form.DefaultBackColor;

        }
        protected override void DoAfterLoad(String Settings)
        {
            String[] LValues = Settings.Split(new Char[] { '\n' });
            if (LValues.Length > 0)
            {
                FWindowState = int.Parse(LValues[0]);
            }
            if (LValues.Length > 1)
            {
                FWindowX = int.Parse(LValues[1]);
            }
            if (LValues.Length > 2)
            {
                FWindowY = int.Parse(LValues[2]);
            }
            if (LValues.Length > 3)
            {
                FWindowWidth = int.Parse(LValues[3]);
            }
            if (LValues.Length > 4)
            {
                FWindowHeight = int.Parse(LValues[4]);
            }
            if (LValues.Length > 5)
            {
                FInitDir = LValues[5];
            }
            if (LValues.Length > 6)
            {
                string[] LFilesArray = LValues[6].Split(new char[] { '|' });
                for (int j = 0; j < LFilesArray.Length; j++)
                {
                    FRecent.Insert(j, LFilesArray[j]);
                }

            }
            if (LValues.Length > 7)
            {
                FFont = FFont1.ConvertFromString(LValues[7]) as Font;

            }
            if (LValues.Length > 8)
            {

                FColorBack = (Color)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(LValues[8]);
            }

        }
        protected override String GetSettings()
        {
            string LRecentFilesStr = String.Join("|", this.FRecent.ToArray());
            string LFontForm = FFont1.ConvertToString(this.FFont);
            string LColor = FColor1.ConvertToString(this.FColorBack);
            String Result = String.Join("\n", new String[]{
                FWindowState.ToString(),
                FWindowX.ToString(),
                FWindowY.ToString(),
                FWindowWidth.ToString(),
                FWindowHeight.ToString(),
               FInitDir,
               LRecentFilesStr,
               LFontForm,
               LColor


            });
            return Result;
        }
        protected virtual void setInitDir(String value)
        {
            FInitDir = value;
            if (!(FInitDir.Length > 0))
            {
                FInitDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }

        //private string GetColorVal(Color LColorVal)
        //{
        //    string ColorVal = "";

        //    if (!LColorVal.IsNamedColor)
        //    {
        //        ColorVal = LColorVal.ToArgb().ToString();
        //    }
        //    else
        //    {
        //        ColorVal = LColorVal.Name.ToString();
        //    }

        //    return ColorVal;
        //}
        public Color ColorBack
        {
            get => FColorBack;
            set { FColorBack = value; }
        }
        public Font FontText
        {
            get => FFont;
            set { FFont = value; }
        }

        public List<string> Recent
        {
            get => FRecent;
            set { FRecent = value; }
        }
        public String InitDir
        {
            get => FInitDir;
            set { setInitDir(value); }
        }
        public int WindowX
        {
            get => FWindowX;
            set { FWindowX = value; }
        }
        public int WindowY
        {
            get => FWindowY;
            set { FWindowY = value; }
        }
        public int WindowWidth
        {
            get => FWindowWidth;
            set { FWindowWidth = value; }
        }
        public int WindowHeight
        {
            get => FWindowHeight;
            set { FWindowHeight = value; }
        }
        public int WindowState
        {
            get => FWindowState;
            set { FWindowState = value; }
        }
    }
    class ConfigLoaderLng : ConfigLoader
    {
        private String[] FMessgs;
        private String FMenuMainFile;
      private String FMenuMainEdit;
        public ConfigLoaderLng()
        {
            FMenuMainFile = "File";
            FMenuMainEdit = "Edit";
        }
        protected override void DoAfterLoad(String Settings)
        {

            Settings = Settings.Replace('\r', '=');
            FMessgs = Settings.Split('\n');
            //String[] LMsgs = Settings.Split();
            //foreach (String LMsg in LMsgs)
            //{
            //    if (LMsg.ToLower().StartsWith("msgmenufilemain="))
            //    {
            //        String[] LValues = LMsg.Split(new char[] { '=' });
            //        FMenuMainFile = LValues[1];
            //    }
            //    if (LMsg.ToLower().StartsWith("msgmenueditmain="))
            //    {
            //        String[] LValues = LMsg.Split(new char[] { '=' });
            //        FMenuMainEdit = LValues[1];
            //    }
            //}
        }
        public String GetMessage(String MsgName)
        {
            String LResult = "";

            foreach (String LeMag in FMessgs)
            {
                if (LeMag.ToLower().StartsWith(MsgName.ToLower()))
                {
                    String[] LValues = LeMag.Split(new Char[] { '=' });
                    LResult = LValues[1];
                    break;
                }

            }

            return LResult;
        }

      //  public String MenuMainFile { get => FMenuMainFile; }
     //   public String MenuMainEdit { get => FMenuMainEdit; }
    }
    class ConfigLoaderLngJSON : ConfigLoader
    {
        public class LngSett
        {
           // public String InitialDir;
            public String stripmenuitemFile;
            public String stripmenuitemEdit;
         
            //Dictionary<string, string> Settings = new Dictionary<string, string>
            //{
            //{"msgmenumenufile","File" },
            //{"msgmenumainedit","Edit" }
            //    };
            //protected override void DoAfterLoad(string Settings)
            //{
            //    Dictionary<string, string> FMessage = new Dictionary<string, string>
            //{
            //{"msgmenufilemain","File" },
            //{"msgmenueditmain","Edit" }
            //    };

            //    if (Settings.Length > 0)
            //    {
            //        //  Settings = JsonConvert.DeserializeObject<ConfigLoaderLngJSON>(settings);
            //    }
            //}

            //public virtual String GetMessage(String MessageName, String DefaultValue)
            //{

            //    return DefaultValue;
            //}
        }
        public LngSett LngSettings;

        public ConfigLoaderLngJSON()
        {
            LngSettings = new LngSett();
          //  LngSettings.InitialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LngSettings.stripmenuitemFile = "File";
            LngSettings.stripmenuitemEdit = "Edit";

        }
        protected override void DoAfterLoad(String Settings)
        {
            if (Settings.Length > 0)
            {
                LngSettings = JsonConvert.DeserializeObject<LngSett>(Settings);
            }
        }
        protected override  String GetSettings()
        {
            return JsonConvert.SerializeObject(LngSettings, Formatting.Indented);
        }

    }

    class Product
    {
       public float ID;
        public String NAME;
        public float EDIZM_SNAME;
        public float PRICE;

        public Product()
        {


        }
        public Product[] ProductList;



    }
}
