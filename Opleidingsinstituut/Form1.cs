using Glashandel;
using OuderbijdrageSchool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opleidingsinstituut
{
    public partial class Form1 : Form
    {
        private const string classToFollowString = "type of class:";
        private const string addClassButtonString = "add a class to follow";
        private const string studentBirthDateString = "birth date of student";
        private const string currentDateString = "current date";
        private const string employmentAgencyMediatedString = "employment agency mediates classes";
        private const string showCostButtonString = "calculate the cost of the classes";
        private const string messageBoxShowCostString = "the cost of following these classes is ";

        private const int widthMargin = 10;
        private const int heightMargin = 10;
        private const int rowHeight = 25;
        private const int textLength = 140;
        private const int employmentAgencyMediatedTextLength = 225;
        private const int showCostButtonTextLength = 180;
        private const int radioButtonWidth = 120;

        private List<FormElementRemovable<RadioButtonsFormElement>> classesToFollow;
        private ButtonFormElement addClassButton;
        private DateFormElement studentBirthDate;
        private DateFormElement currentDate;
        private CheckBoxFormElement employmentAgencyMediated;
        private ButtonFormElement showCostButton;

        public Form1()
        {
            InitializeComponent();
            InitializeElements();
            ResetPosition();
        }
        private void InitializeElements()
        {
            classesToFollow = new List<FormElementRemovable<RadioButtonsFormElement>>();
            addClassButton = new ButtonFormElement(this, addClassButtonString, AddClassButtonFunction);
            studentBirthDate = new DateFormElement(this, studentBirthDateString);
            studentBirthDate.SetTextBoxOfset(textLength);
            currentDate = new DateFormElement(this, currentDateString);
            currentDate.SetTextBoxOfset(textLength);
            employmentAgencyMediated = new CheckBoxFormElement(this, employmentAgencyMediatedString);
            employmentAgencyMediated.SetWidth(employmentAgencyMediatedTextLength);
            showCostButton = new ButtonFormElement(this, showCostButtonString, ShowCostButtonFunction);
            showCostButton.SetWidth(showCostButtonTextLength);
        }
        private void ResetPosition()
        {
            int row = 0;
            foreach(FormElementRemovable<RadioButtonsFormElement> element in classesToFollow)
            {
                element.ChangePosition(widthMargin, heightMargin + row * rowHeight);
                row += element.GetElement().GetAmountOfRows();
            }
            addClassButton.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            studentBirthDate.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            currentDate.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            employmentAgencyMediated.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            showCostButton.ChangePosition(widthMargin, heightMargin + row * rowHeight);
        }

        private void AddsClassToFollow()
        {
            RadioButtonsFormElement internalElement = new RadioButtonsFormElement(this, classToFollowString, EducationInstitutePrice.GetClassTypes(), rowHeight);
            internalElement.SetWidth(radioButtonWidth);
            FormElementRemovable<RadioButtonsFormElement> externalElement = new FormElementRemovable<RadioButtonsFormElement>(this, internalElement, RemoveClassToFollow);
            externalElement.SetRemoveButtonOfset(radioButtonWidth);
            classesToFollow.Add(externalElement);
            ResetPosition();
        }
        private void RemoveClassToFollow(object element)
        {
            classesToFollow.Remove((FormElementRemovable<RadioButtonsFormElement>) element);
            ResetPosition();
        }
        private void ShowCost()
        {
            string[] classesToFollow = new string[this.classesToFollow.Count];
            for(int i = 0; i < classesToFollow.Length; i++)
            {
                classesToFollow[i] = this.classesToFollow[i].GetElement().GetValue();
            }
            int studentBirthDateDay = studentBirthDate.GetDateDay();
            int studentBirthDateMonth = studentBirthDate.GetDateMonth();
            int studentBirthDateYear = studentBirthDate.GetDateYear();
            int currentDateDay = currentDate.GetDateDay();
            int currentDateMonth = currentDate.GetDateMonth();
            int currentDateYear = currentDate.GetDateYear();
            bool employmentAgencyMediated = this.employmentAgencyMediated.GetValue();
            double cost = EducationInstitutePrice.GetPrice(classesToFollow, studentBirthDateDay, studentBirthDateMonth, studentBirthDateYear, currentDateDay, currentDateMonth, currentDateYear, employmentAgencyMediated);
            MessageBox.Show(messageBoxShowCostString + cost);
    }

        private void AddClassButtonFunction(object sender, EventArgs e)
        {
            AddsClassToFollow();
        }
        private void ShowCostButtonFunction(object sender, EventArgs e)
        {
            ShowCost();
        }
    }

    
}
