using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExecutePY
{
    public partial class MainView : Form
    {
        public User User { get; set; }
        public Querry Querry { get; set; }
        public TextBox textBox;
        public FlowLayoutPanel flp;

        private MainSettingView mainBoxSettings;
        private List<FlowLayoutPanel> flpList = new List<FlowLayoutPanel>();
        private List<FbCase> fbCases;

        private void Execute()
        {
            Test();
            
            //ListManager.RemoveList();
            //Parse();

            //FileEditor.Expand(StringBuilder.GetExportPath());
            //XmlParser.Parse(StringBuilder.GetExportPath());

         //   GenerateTextBox(flowLayoutPanel1, fbCases);
            //GenerateTextBox(flowLayoutPanel2, ListManager.GetLongTermList());
            //GenerateTextBox(flowLayoutPanel3, ListManager.GetInProgressList());
            //GenerateTextBox(flowLayoutPanel4, ListManager.GetFeedbackList());
        }

        #region Random events
        public MainView(User User)
        {
            InitializeComponent();
            this.User = User;
        }

        private void MainBox_Load(object sender, EventArgs e)
        {
            Execute();
        }
        private void casesButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wut");
            Execute();
        }



        private void CreateTextBoxInstances(string name, string text, int sizeX, int sizeY, FlowLayoutPanel flp)
        {

            textBox.Click += new EventHandler(ClickedTextBox);

            flp.Controls.Add(textBox);
        }

        private void Parse()
        {
            if (Querry == null)
            {
                Querry querry = new Querry(QuerryColumns.OpenedBy);
                CaseWorker.Parse(User, querry);
            }
            else
            {
                Querry querry = new Querry(Querry.QuerryColumn);
                CaseWorker.Parse(User, querry);
            }
        }
        private void ClickedTextBox(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "caseId")
            {
                System.Diagnostics.Process.Start("https://dtnet.fogbugz.com/f/cases/" + textBox.Text);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }


        
        private void exitImg_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void minImg_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Notify(string caseText, string priorityText, string titleText)
        {
            NotificationView notification;

            //for (int i = 0; i < ListManager.GetTypeList().Count; i++)
            //{
                //MessageBox.Show(priorityList.ElementAt(i));
                switch (priorityText)
                {
                    case "1":
                        {
                            Color color = Color.DarkRed;
                            notification = new NotificationView(caseText, priorityText, titleText, color);
                            notification.Show();
                            break;
                        }
                    case "2":
                        {
                            Color color = Color.Red;
                            notification = new NotificationView(caseText, priorityText, titleText, color);
                            notification.Show();
                            break;
                        }
                    case "3":
                        {
                            Color color = Color.OrangeRed;
                            notification = new NotificationView(caseText, priorityText, titleText, color);
                            notification.Show();
                            break;
                        }
                    case "4":
                        {
                            Color color = Color.Orange;
                            notification = new NotificationView(caseText, priorityText, titleText, color);
                            notification.Show();
                            break;
                        }
                    case "5":
                        {
                            Color color = Color.Green;
                            notification = new NotificationView(caseText, priorityText, titleText, color);
                            notification.Show();
                            break;
                        }
            }
           // }
        }

        
        private void logoutButton_Click(object sender, EventArgs e)
        {
            StartupView startupView = new StartupView("");

            this.Hide();
            startupView.Show();

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            mainBoxSettings = new MainSettingView(this);
            mainBoxSettings.Show();


        }

        private void casesButton_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();

            Execute();
        }
        #endregion

        /*TEST ENVIRONMENT*/
        public void Test()
        {

            CaseXmlDocument xDocument = new CaseXmlDocument(@"C:\Users\esniuolis\Documents\Visual Studio 2015\Projects\fbNotificationTool-Home\bin\Debug\DO_NOT_CHANGE\Parameters\crawler.xml");
            MainMVC.ManageCases(xDocument, this);

            for (int i = 0; i < fbCases.Count; i++)
            {
                CustomFlowLayoutPanel flp = new CustomFlowLayoutPanel();
                flp.Size = new Size(660, 24);

                CustomTextBox customTextBox = new CustomTextBox("caseId", fbCases.ElementAt(i).ID, 60, 18);
                
                //CustomTextBox customTextBox2 = new CustomTextBox("caseId", fbCases.ElementAt(i).ID, 60, 18);
                /*CreateTextBoxInstances("caseId", fbCases.ElementAt(i).ID, 60, 18, flp);
               CreateTextBoxInstances("title", fbCases.ElementAt(i).Title, 350, 18, flp);
               CreateTextBoxInstances("priority", fbCases.ElementAt(i).Priority, 30, 18, flp);
               CreateTextBoxInstances("date", fbCases.ElementAt(i).DateOpened, 130, 18, flp);*/

                MainMVC.ManageControls(customTextBox, flp, this);

                flpList.Add(flp);
                flowLayoutPanel1.Controls.Add(flpList.ElementAt(i));
            }

            ModifyFlowLayoutPanel(flowLayoutPanel1);
        }

        public void SetFbCases(List<FbCase> fbCases)
        {
            this.fbCases = fbCases;
        }

        public void CreateTextBox(string name, string text, int sizeX, int sizeY)
        {
            textBox = new TextBox();

            textBox.Name = name;
            textBox.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            textBox.ReadOnly = true;
            textBox.BorderStyle = 0;
            textBox.BackColor = this.BackColor;
            textBox.TabStop = false;
            textBox.Text = text;
        }
        public void CreateFlowLayoutPanel(CustomFlowLayoutPanel flp)
        {
           // this.flp = flp;
            flp.Controls.Add(textBox);
        }
        public void ModifyFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.HorizontalScroll.Maximum = 0;
            flowLayoutPanel.AutoScroll = false;
            flowLayoutPanel.VerticalScroll.Visible = false;
            flowLayoutPanel.AutoScroll = true;
        }
    }
}
