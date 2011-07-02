using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using ChatWindowsApplication;
using ChatWindowsApplication.ChatService;

namespace ChatServiceProject
{
    public partial class PickUsernameForm : Form
    {
        private readonly ChatForm _form;

        public PickUsernameForm(ChatForm form)
        {
            _form = form;
            InitializeComponent();
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            username.Enabled = false;
            language.Enabled = false;
            themes.Enabled = false;
            _form.Username = username.Text;
            var lang = language.SelectedItem.ToString();
            int idx;
            _form.Language = lang.Substring(idx = (lang.IndexOf('(') + 1), lang.IndexOf(')') - idx);

            bool tryagain = false;
            do
            {
                try
                {
                    _form.Tracker.Subscribe(username.Text, themes.SelectedItem.ToString(), _form.Language);
                }
                catch (FaultException<UserFault> p)
                {
                    username.Text = p.Detail.Reason;
                    username.SelectAll();
                    username.Enabled = true;
                    language.Enabled = true;
                    themes.Enabled = true;
                }
                catch (CommunicationException)
                {
                    _form.CreateTracker();
                    tryagain = true;
                } 
            } while (tryagain);
            
            this.Close();
        }

        private void PickUsernameForm_Load(object sender, EventArgs e)
        {
            themes.Items.AddRange(_form.Tracker.GetThemes().Select(t => t.Name).ToArray());
        }
    }
}
