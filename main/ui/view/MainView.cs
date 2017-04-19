﻿using System;
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

        private MainSettingView mainBoxSettings;

        private TextBox textBox;
        private PictureBox pictureBox;

        private List<FlowLayoutPanel> flpList = new List<FlowLayoutPanel>();
        private List<PictureBox> pictureList = new List<PictureBox>();

        private List<FbCase> fbCases;

        public MainView(User User)
        {
            InitializeComponent();
            this.User = User;
        }

        private void MainBox_Load(object sender, EventArgs e)
        {
            Execute();
        }

        private void Execute()
        {
            Test();
            
            //ListManager.RemoveList();
            //Parse();

            //FileEditor.Expand(StringBuilder.GetExportPath());
            //XmlParser.Parse(StringBuilder.GetExportPath());

            GenerateTextBox(flowLayoutPanel1, fbCases);
            //GenerateTextBox(flowLayoutPanel2, ListManager.GetLongTermList());
            //GenerateTextBox(flowLayoutPanel3, ListManager.GetInProgressList());
            //GenerateTextBox(flowLayoutPanel4, ListManager.GetFeedbackList());
        }

        private void casesButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wut");
            Execute();
        }



        private void CreateTextBoxInstances(string name, string text, int sizeX, int sizeY, int position, FlowLayoutPanel flp)
        {
            textBox = new TextBox();

            textBox.Name = name;
            textBox.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            textBox.ReadOnly = true;
            textBox.BorderStyle = 0;
            textBox.BackColor = this.BackColor;
            textBox.TabStop = false;

            textBox.Text = text;

            textBox.Click += new EventHandler(ClickedTextBox);

            textBox.Size = new Size(sizeX, sizeY);

            flp.Controls.Add(textBox);
        }

        private void CreatePictureBoxInstances(string name, string text, int sizeX, int sizeY, int position, FlowLayoutPanel flp)
        {
            pictureBox = new PictureBox();

            pictureBox.Size = new Size();

            Image image = Properties.Resources.sauktukas;

            pictureBox.Image = image;
            pictureBox.Height = image.Height;
            pictureBox.Width = image.Width;
            pictureBox.Hide();

            pictureList.Add(pictureBox);
            flp.Controls.Add(pictureBox);
        }

        //private void GenerateTextBox(FlowLayoutPanel flowLay, List<string> list)
        //{
        //    int j = 1;
        //    for (int i = 0; i < list.Count / 5; i++)
        //    {
        //        FlowLayoutPanel flp = new FlowLayoutPanel();
        //        flp.Size = new Size(660, 24);

        //        /* CreateTextBoxInstances("caseId", list.ElementAt(j + 0), 60, 18, i, flp);
        //         CreateTextBoxInstances("title", list.ElementAt(j + 1), 350, 18, i, flp);
        //         CreateTextBoxInstances("priority", list.ElementAt(j + 2), 30, 18, i, flp);
        //         CreateTextBoxInstances("date", list.ElementAt(j + 3), 130, 18, i, flp);
        //         CreatePictureBoxInstances("1", list.ElementAt(j + 3), 30, 10, i, flp);*/

        //        CreateTextBoxInstances("caseId", fbCases.ElementAt(i).ID, 60, 18, i, flp);
        //        CreateTextBoxInstances("title", fbCases.ElementAt(i).Title, 350, 18, i, flp);
        //        CreateTextBoxInstances("priority", fbCases.ElementAt(i).Priority, 30, 18, i, flp);
        //        CreateTextBoxInstances("date", fbCases.ElementAt(i).DateOpened, 130, 18, i, flp);

        //        Notify(list.ElementAt(j + 0), list.ElementAt(j + 2), list.ElementAt(j + 1));

        //        flpList.Add(flp);
        //        flowLay.Controls.Add(flpList.ElementAt(i));

        //        if ((list.Count - 4) != j)
        //            j += 5;
        //    }
        //    flowLay.HorizontalScroll.Maximum = 0;
        //    flowLay.AutoScroll = false;
        //    flowLay.VerticalScroll.Visible = false;
        //    flowLay.AutoScroll = true;
        //    flpList.Clear();
        //}

        private void GenerateTextBox(FlowLayoutPanel flowLay, List<FbCase> fbCases)
        {
            for (int i = 0; i < fbCases.Count; i++)
            {
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Size = new Size(660, 24);

                CreateTextBoxInstances("caseId", fbCases.ElementAt(i).ID, 60, 18, i, flp);
                CreateTextBoxInstances("title", fbCases.ElementAt(i).Title, 350, 18, i, flp);
                CreateTextBoxInstances("priority", fbCases.ElementAt(i).Priority, 30, 18, i, flp);
                CreateTextBoxInstances("date", fbCases.ElementAt(i).DateOpened, 130, 18, i, flp);

                flpList.Add(flp);
                flowLay.Controls.Add(flpList.ElementAt(i));
            }
            flowLay.HorizontalScroll.Maximum = 0;
            flowLay.AutoScroll = false;
            flowLay.VerticalScroll.Visible = false;
            flowLay.AutoScroll = true;
            flpList.Clear();
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

        #region Random events
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

            CaseXmlDocument xDocument = new CaseXmlDocument(@"C:\Users\DELL\Documents\Visual Studio 2015\MCSD\ExecutePY\ExecutePY\bin\Debug\DO_NOT_CHANGE\Parameters\crawler.xml");

            MainMVC.ParseXml(xDocument, this);

            foreach (var item in fbCases)
            {
                Console.WriteLine(item.ID);
            }
        }

        public void SetFbCases(List<FbCase> fbCases)
        {
            this.fbCases = fbCases;
        }
    }
}
