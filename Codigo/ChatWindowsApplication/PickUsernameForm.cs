using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChatWindowsApplication;

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
            _form.Username = username.Text;
            var lang = language.SelectedItem.ToString();
            _form.Language = lang.Substring(lang.IndexOf('('), lang.IndexOf(')'));
        }

        private void PickUsernameForm_Load(object sender, EventArgs e)
        {
            themes.Items.AddRange(_form);
        }
    }
}
