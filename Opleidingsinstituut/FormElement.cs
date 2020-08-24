using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glashandel
{
    public abstract class FormElement
    {
        protected Form form;

        public FormElement(Form form)
        {
            this.form = form;
        }
        public abstract void ChangePosition(int widthOfset, int heightOfset);
        public abstract void RemoveElement();
    }

    public class FormElementRemovable<T> : FormElement where T : FormElement
    {
        private T element;

        private const int removeButtonOfsetDefault = 250;
        private const string removeButtonText = "remove";

        private int removeButtonOfset;

        private Button removeButton;
        private Action<object> removeButtonLamba;


        public FormElementRemovable(Form form, T element, Action<object> removeButtonLamba):base(form)
        {
            this.element = element;

            removeButton = new Button();
            removeButton.Text = removeButtonText;
            removeButton.Click += new EventHandler(RemoveButtonFunction);
            form.Controls.Add(removeButton);

            this.removeButtonLamba = removeButtonLamba;

            removeButtonOfset = removeButtonOfsetDefault;
        }
        public T GetElement()
        {
            return element;
        }
        public void SetRemoveButtonOfset(int ofset)
        {
            removeButtonOfset = ofset;
        }
        public override void ChangePosition(int widthOfset, int heightOfset)
        {

            element.ChangePosition(widthOfset, heightOfset);
            removeButton.Location = new System.Drawing.Point(widthOfset + removeButtonOfset, heightOfset);
        }
        public override void RemoveElement()
        {
            element.RemoveElement();
            form.Controls.Remove(removeButton);
            if (removeButtonLamba != null)
            {
                removeButtonLamba.Invoke(this);
            }
        }
        private void RemoveButtonFunction(object sender, EventArgs e)
        {
            RemoveElement();
        }
    }
}
