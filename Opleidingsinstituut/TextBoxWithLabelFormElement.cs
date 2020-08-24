using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glashandel
{
    public class TextBoxWithLabelFormElement : FormElement
    {
        private const int valueIfNoIntValueDefault = 0;
        private const int textBoxOfsetDefault = 100;

        private int valueIfNoValue;
        protected int textBoxOfset;

        protected Label label;
        protected TextBox textBox;

        public TextBoxWithLabelFormElement(Form form, string labelText) : base(form)
        {
            this.form = form;
            textBoxOfset = textBoxOfsetDefault;
            valueIfNoValue = valueIfNoIntValueDefault;

            label = new Label();
            label.Text = labelText;
            label.Width = textBoxOfset;
            form.Controls.Add(label);

            textBox = new TextBox();
            form.Controls.Add(textBox);
        }
        public string GetValue()
        {
            return textBox.Text;
        }
        public int GetValueAsInt()
        {
            try
            {
                return int.Parse(GetValue());
            }
            catch (FormatException exception)
            {
                return valueIfNoValue;
            }
        }
        public double GetValueAsDouble()
        {
            try
            {
                return double.Parse(GetValue());
            }
            catch (FormatException exception)
            {
                return valueIfNoValue;
            }
        }
        public void SetTextBoxOfset(int ofset)
        {
            textBoxOfset = ofset;
            label.Width = textBoxOfset;
        }
        public void SetValueIfNoValue(int value)
        {
            valueIfNoValue = value;
        }
        public override void ChangePosition(int widthOfset, int heightOfset)
        {
            label.Location = new System.Drawing.Point(widthOfset, heightOfset);
            textBox.Location = new System.Drawing.Point(widthOfset + textBoxOfset, heightOfset);
        }
        public override void RemoveElement()
        {
            form.Controls.Remove(label);
            form.Controls.Remove(textBox);
        }
    }
}
