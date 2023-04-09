using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using damn.Classes;


namespace damn
{
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            User user = User.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user == default) {
                MessageBox.Show("Такого пользователя не существует!", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Global.User = user;
            switch(user.RoleID) {
                case 1:
                    new FormAdminMain().Show();
                    break;
                case 2:
                    new FormMasterMain().Show();
                    break;
                case 3:
                    new FormClientMain().Show();
                    break;
            }
        }

        private void FormAuth_Load(object sender, EventArgs e)
        {

        }

        private void FormAuth_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            new FormRegister().ShowDialog();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
