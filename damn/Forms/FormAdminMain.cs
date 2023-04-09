using damn.Classes;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace damn
{
    public partial class FormAdminMain : Form
    {
        public FormAdminMain()
        {
            InitializeComponent();
            refresh();
        }

        private void FormAdminMain_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text)) {
                MessageBox.Show("Заполните все поля!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Global.Add<Service>(textBox1.Text, numericUpDown1.Value);
            refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedCells.Count <= 0) {
                MessageBox.Show("Выберите запись для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var ind = dataGridView3.SelectedCells[0].RowIndex;
            int id = Convert.ToInt32(dataGridView3.Rows[ind].Cells[0].Value);
            Global.Delete<Service>($"Id = {id}");
            refresh();
        }


        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var ind = dataGridView3.SelectedCells[0].RowIndex;
            int id = Convert.ToInt32(dataGridView3.Rows[ind].Cells[0].Value);
            var serv = Service.Services.First(x => x.Id == id);
            textBox1.Text = serv.Name;
            numericUpDown1.Value = (decimal)Math.Min((decimal)Math.Max((decimal)100.0, serv.Price), (decimal)5000.0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ind = dataGridView3.SelectedCells[0].RowIndex;
            int id = Convert.ToInt32(dataGridView3.Rows[ind].Cells[0].Value);
            var serv = Service.Services.First(x => x.Id == id);

            Global.Update<Service>(id, textBox1.Text, numericUpDown1.Value);
            refresh();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refresh()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();

            foreach (var r in Record.Records) {
                var master = User.Users.First(x => x.Id == r.MasterID);
                var client = User.Users.First(x => x.Id == r.ClientID);
                var service = Service.Services.First(x => x.Id == r.ServiceID);
                dataGridView4.Rows.Add(r.Id, master.Name, master.Phone, client.Name, client.Phone, service.Name, service.Price, r.Date);

            }

            foreach(var usr in User.Users.Where(x => x.RoleID != 1)) {
                if(usr.RoleID == 2) {
                    dataGridView2.Rows.Add(usr.Login, usr.Password, usr.Name, usr.Phone);
                }
                else {
                    dataGridView1.Rows.Add(usr.Login, usr.Password, usr.Name, usr.Phone);
                }
            }

            foreach(var service in Service.Services) {
                dataGridView3.Rows.Add(service.Id, service.Name, service.Price);
            }
        }
    }
}
