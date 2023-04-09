using damn.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace damn
{
    public partial class FormClientMain : Form
    {

        public FormClientMain()
        {
            InitializeComponent(); refresh();
        }

        private void FormClientMain_Load(object sender, EventArgs e)
        {
            textBoxUserPhone.Text = Global.User.Phone;
            labelUserName.Text = Global.User.Name;

            comboBoxMasters.DataSource = User.Users.Where(x => x.RoleID == 2).ToList();
            comboBoxMasters.DisplayMember = "Name";
            comboBoxMasters.ValueMember = "Id";
            comboBoxMasters.SelectedIndex = 0;
        }

        private void FormClientMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void buttonUpdateUserPhone_Click(object sender, EventArgs e)
        {
            var usr = Global.User;
            Global.Update<User>(usr.Id, usr.Login, usr.Password, usr.Name, textBoxUserPhone.Text, usr.RoleID);
        }

        private void comboBoxMasters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                var pool = MasterService.Links.Where(x => x.MasterID == Convert.ToInt32(comboBoxMasters.SelectedValue)).ToList();
                comboBoxServices.DataSource = Service.Services.Where(x => pool.Exists(xx => xx.ServiceID == x.Id)).ToList();
                comboBoxServices.DisplayMember = "Name";
                comboBoxServices.ValueMember = "Id";
            }
            catch { }
        }

        private void comboBoxServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (comboBoxServices.SelectedItem as Service).Price.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBoxServices.SelectedItem == null) {
                MessageBox.Show("Заполните все поля!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try {
                Global.Add<Record>((comboBoxMasters.SelectedItem as User).Id, Global.User.Id, (comboBoxServices.SelectedItem as Service).Id, dateTimePicker1.Value.ToString("MM/dd/yyyy ") + comboBoxTime.Text + ":00");
                refresh();
            }
            catch {
                MessageBox.Show("Заполните все поля!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count <= 0) {
                MessageBox.Show("Выберите запись для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var ind = dataGridView1.SelectedCells[0].RowIndex;
            int id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
            Global.Delete<Record>($"Id = {id}");
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void refresh()
        {
            dataGridView1.Rows.Clear();
            var records = Record.Records.Where(x => x.ClientID == Global.User.Id);

            foreach(Record r in records) {
                var master = User.Users.First(x => x.Id == r.MasterID);
                var service = Service.Services.First(x => x.Id == r.ServiceID);
                dataGridView1.Rows.Add(r.Id, master.Name, master.Phone, service.Name, service.Price, r.Date);
            }
        }
    }
}

