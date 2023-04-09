using damn.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace damn
{
    public partial class FormMasterMain : Form
    {
        public FormMasterMain()
        {
            InitializeComponent();
            textBoxUserPhone.Text = Global.User.Phone;
            labelUserName.Text = Global.User.Name;
        }

        private void FormMasterMain_Load(object sender, EventArgs e)
        {
            List<MasterService> links = MasterService.Links.Where(x => x.MasterID == Global.User.Id).ToList();

            List<Service> allServices = Service.Services;
            List<Service> myServices = allServices.Where(x => links.Exists(xx => xx.ServiceID == x.Id)).ToList();

            checkedListBox1.Items.Clear();
            foreach (Service s in allServices) {
                checkedListBox1.Items.Add(new MasterServiceItem(s.Id, s.Name));
            }

            foreach(var item in myServices) {
                checkedListBox1.SetItemChecked(item.Id - 1, true);
            }

            List<Record> records = Record.Records.Where(x => x.MasterID == Global.User.Id).ToList();
            var clients = User.Users.Where(x => x.RoleID == 3).ToList();

            foreach(Record rec in records) {
                var client = clients.First(x => x.Id == rec.ClientID);
                var serv = allServices.First(x => x.Id == rec.ServiceID);
                dataGridView1.Rows.Add(rec.Id, client.Name, client.Phone, serv.Name, serv.Price, rec.Date);
            }
        }

        private void buttonUpdateUserPhone_Click(object sender, EventArgs e)
        {
            var usr = Global.User;
            Global.Update<User>(usr.Id, usr.Login, usr.Password, usr.Name, textBoxUserPhone.Text, usr.RoleID);
        }

        private void FormMasterMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void update_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = (checkedListBox1.Items[e.Index] as MasterServiceItem);
            if (e.NewValue == CheckState.Checked) {
                Global.Add<MasterService>(Global.User.Id, item.Value);
            }
            else {
                Global.Delete<MasterService>($"ServiceID = '{item.Value}' AND MasterID = {Global.User.Id}");
            }
        }
    }
}
