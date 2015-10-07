using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Networking_client
{
    public partial class ClientForm : Form
    {
        public ClientFormController myController { get; set; }
        public ClientForm(ClientFormController myController)
        {
            this.myController = myController;
            InitializeComponent();
        }

        private void listBoxChatt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            myController.Send(richTextBoxMessage.Text);
        }
    }
}
