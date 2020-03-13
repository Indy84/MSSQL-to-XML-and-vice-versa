using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt
{
    /// <summary>
    /// Helper class for logging in TextBox
    /// </summary>
    public class TextBoxAppender : AppenderSkeleton
    {
        /// <summary>
        /// Field indicating to logger which TextBox is destined for logs.
        /// </summary>
        private TextBox textBox;
        
        /// <summary>
        /// Field indicating to logger the name of the Form with desirable TextBox.
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Field containing the name of desirable TextBox.
        /// </summary>
        public string TextBoxName { get; set; }


        /// <summary>
        /// The method overrides the original one to check if the proper Form with desirable TextBox is loaded and then writes the line of the logs to it.
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (textBox == null)
            {
                if (String.IsNullOrEmpty(FormName) || String.IsNullOrEmpty(TextBoxName))
                {
                    return;
                }
                
                Form form = Application.OpenForms[FormName];

                if (form == null)
                {
                    return;
                }
                
                textBox = form.Controls[TextBoxName] as TextBox;
                
                if (textBox == null)
                {
                    return;
                }
                
                form.FormClosing += (s, e) => textBox = null;
            }

            textBox.AppendText(loggingEvent.RenderedMessage + Environment.NewLine);
        }
    }
}
