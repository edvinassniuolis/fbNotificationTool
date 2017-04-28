using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutePY
{
    public class MainMVC
    {
        private static CaseController xmlDocumentController;
        private static CustomFormController customFormController;

        public static void ManageCases(CaseXmlDocument xDocument, MainView mainBoxView)
        {
            xmlDocumentController = new CaseController(xDocument, mainBoxView);

            xmlDocumentController.SetLocation(xDocument.Location);
            xmlDocumentController.LoadXml();
            xmlDocumentController.ParseXml();
            xmlDocumentController.CreateCases();
            xmlDocumentController.CheckCases();

            xmlDocumentController.SetViewCases();
        }

        public static void ManageControls(CustomTextBox customTextBox, CustomFlowLayoutPanel customFlowLayoutPanel, MainView mainBoxView)
        {
            customFormController = new CustomFormController(customTextBox, customFlowLayoutPanel, mainBoxView);

            customFormController.SetTextBoxName(customTextBox.Name);
            customFormController.SetTextBoxText(customTextBox.Text);

            customFormController.SetSizeX(customTextBox.SizeX);
            customFormController.SetSizeY(customTextBox.SizeY);

            
            customFormController.CreateTextBox();
            customFormController.SetSize();
            customFormController.AddTextBoxToFlowLayoutPanel();
        }
    }
}
