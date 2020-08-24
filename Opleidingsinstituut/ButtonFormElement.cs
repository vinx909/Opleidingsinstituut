using System;
using System.Windows.Forms;

namespace Glashandel
{
    public class ButtonFormElement : FormElement
    {
        private Button button;

        public ButtonFormElement(Form form, string buttonText, Action<object, EventArgs> buttonFunction) : base(form)
        {
            button = new Button();
            button.Text = buttonText;
            button.Click += new EventHandler(buttonFunction);
            form.Controls.Add(button);
        }
        public void SetWidth(int width)
        {
            button.Width = width;
        }
        public override void ChangePosition(int widthOfset, int heightOfset)
        {
            button.Location = new System.Drawing.Point(widthOfset, heightOfset);
        }
        public override void RemoveElement()
        {
            form.Controls.Remove(button);
        }
    }
}