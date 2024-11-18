using Microsoft.Win32;
using System.Diagnostics.Eventing.Reader;


namespace FrmRegTest
{
    public partial class Form1 : Form
    {
        private const string RegistryPath = @"HKEY_CURRENT_USER\SOFTWARE\MyCompany\TestRegProgram";
        /*
        private const string OpenCountKey = "OpenCount";//value
        private const string ExpiredDateValue = "ExpiredDate";
        private static DateTime MaxdateTime = new DateTime(2024, 11, 15, 22, 20, 0);
        */

        private const string FormSizeKey = "FormSize";
        private const string FormLocationKey = "FormLocation";


        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            const int MaxOpenCount = 5;
            int? CurrOpenCount;

            CurrOpenCount = (int?)Registry.GetValue(RegistryPath, OpenCountKey, 0);

            if (CurrOpenCount >= MaxOpenCount)
            {
                MessageBox.Show("Превышен лимит открытия программы");
                Close();
            }
            CurrOpenCount++;
            Registry.SetValue(RegistryPath, OpenCountKey, CurrOpenCount);
            */

            /*
            string? strDate = (string?)Registry.GetValue(RegistryPath, "ExpiredDate", null);
            DateTime dateFromReg;

            if (strDate == null)
            {
                dateFromReg = DateTime.Now;
                strDate = dateFromReg.ToString();
            }
            else
                dateFromReg = DateTime.Parse(strDate);

            if (dateFromReg >= MaxdateTime)
            {
                MessageBox.Show("Превышен лимит открытия программы");
                Close();
            }
            Registry.SetValue(RegistryPath, "ExpiredDate", strDate);
            */

            LoadFormSettings();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormSettings();
        }

        private void LoadFormSettings()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null)
                {
                    string formSizeValue = (string)key.GetValue(FormSizeKey);
                    if (!string.IsNullOrEmpty(formSizeValue))
                    {
                        string[] sizeParts = formSizeValue.Split(',');

                        int width = int.Parse(sizeParts[0]);
                        int height = int.Parse(sizeParts[1]);
                        Size = new Size(width, height);
                        
                    }

                    string formLocationValue = (string)key.GetValue(FormLocationKey);
                    if (!string.IsNullOrEmpty(formLocationValue))
                    {
                        string[] locationParts = formLocationValue.Split(',');

                        int x = int.Parse(locationParts[0]);
                        int y = int.Parse(locationParts[1]);
                        Location = new Point(x, y);
                        
                    }
                }
            }
        }

        private void SaveFormSettings()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                key.SetValue(FormSizeKey, $"{Size.Width},{Size.Height}");
                key.SetValue(FormLocationKey, $"{Location.X},{Location.Y}");
            }
        }
    }
}
