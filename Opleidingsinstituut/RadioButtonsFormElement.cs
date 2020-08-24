using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glashandel
{
    public class RadioButtonsFormElement : FormElement
    {
        private const string exceptionNoOptionSelected = "no radio option selected";
        private const int radioButtonWidthOfsetDefault = 3;
        private const int radioButtonHeightOfsetDefault = -3;

        private int rowHeight;
        private int radioButtonWidthOfset;
        private int radioButtonHeightOfset;

        private GroupBox group;
        private RadioButton[] radioButtons;

        private string[] options;

        public RadioButtonsFormElement(Form form, string text, string[] options, int rowHeight) : base(form)
        {
            this.options = CopyStringArray(options);
            group = new GroupBox();
            group.Text = text;
            group.Height = rowHeight * (1 + options.Length);
            radioButtons = new RadioButton[options.Length];
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i] = new RadioButton();
                group.Controls.Add(radioButtons[i]);
                radioButtons[i].Text = this.options[i];
                form.Controls.Add(group);
            }

            this.rowHeight = rowHeight;
            radioButtonWidthOfset = radioButtonWidthOfsetDefault;
            radioButtonHeightOfset = radioButtonHeightOfsetDefault;
        }
        
        public string GetValue()
        {
            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked == true)
                {
                    return radioButton.Text;
                }
            }
            throw new Exception(exceptionNoOptionSelected);
        }
        public int GetAmountOfRows()
        {
            return 1+radioButtons.Length;
        }
        public void SetWidth(int width)
        {
            group.Width = width;
        }
        public void SetRowHeight(int height)
        {
            rowHeight = height;
        }
        public void SetRadioButtonWidthOfset(int ofset)
        {
            radioButtonWidthOfset = ofset;
        }
        public void SetRadioButtonHeightOfset(int ofset)
        {
            radioButtonHeightOfset = ofset;
        }
        public override void ChangePosition(int widthOfset, int heightOfset)
        {
            group.Location = new System.Drawing.Point(widthOfset, heightOfset);
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].Location = new System.Drawing.Point(radioButtonWidthOfset, radioButtonHeightOfset + rowHeight * (1 + i));
            }
        }
        public override void RemoveElement()
        {
            form.Controls.Remove(group);
            foreach (RadioButton radioButton in radioButtons)
            {
                group.Controls.Remove(radioButton);
                form.Controls.Remove(radioButton);
            }
        }
        private string[] CopyStringArray(string[] toCopy)
        {
            string[] toReturn = new string[toCopy.Length];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = toCopy[i];
            }
            return toReturn;
        }
    }
}
