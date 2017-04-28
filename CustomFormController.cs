using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutePY
{
    public class CustomFormController
    {
        private CustomTextBox customTextBox;
        private CustomFlowLayoutPanel customFlowLayoutPanel;
        private MainView mainView;

        public CustomFormController(CustomTextBox customTextBox, CustomFlowLayoutPanel customFlowLayoutPanel, MainView mainView)
        {
            this.customTextBox = customTextBox;
            this.customFlowLayoutPanel = customFlowLayoutPanel;
            this.mainView = mainView;
        }
        #region TextBox properties
        public void SetTextBoxName(string name)
        {
            customTextBox.Name = name;
        }
        public string GetTextBoxName()
        {
            return customTextBox.Name;
        }
        public void SetTextBoxText(string text)
        {
            customTextBox.Text = text;
        }
        public string GetTextBoxText()
        {
            return customTextBox.Text;
        }
        public void SetSizeX(int sizeX)
        {
            customTextBox.SizeX = sizeX;
        }
        public int GetSizeX()
        {
            return customTextBox.SizeX;
        }
        public void SetSizeY(int sizeY)
        {
            customTextBox.SizeY = sizeY;
        }
        public int GetSizeY()
        {
            return customTextBox.SizeY;
        }
        #endregion

        #region METHODS
        public void CreateTextBox()
        {
            mainView.CreateTextBox(GetTextBoxName(), GetTextBoxText(), GetSizeX(), GetSizeY());
        }
        public void SetSize()
        {
            customTextBox.Size = new Size(GetSizeX(), GetSizeY());
        }
        public void AddTextBoxToFlowLayoutPanel()
        {
            mainView.CreateFlowLayoutPanel(customFlowLayoutPanel);
        }
        #endregion
    }
}
