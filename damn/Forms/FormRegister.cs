using damn.Classes;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace damn
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            comboBoxRole.DataSource = Role.Roles.Where(x => x.Id > 1).ToList();
            comboBoxRole.ValueMember = "Id";
            comboBoxRole.DisplayMember = "Name";
            comboBoxRole.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;
            string phone = textBoxPhone.Text;
            int roleId = (comboBoxRole.Items[comboBoxRole.SelectedIndex] as Role).Id;

            if(string.IsNullOrWhiteSpace(login)) {
                MessageBox.Show("Для регистрации, заполните ВСЕ поля!", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(User.Users.Any(x => x.Login == login)) {
                MessageBox.Show("Такой пользователь уже существует!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Global.Add<User>(login, pass, name, phone, roleId);
            MessageBox.Show("Регистрация завершена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
