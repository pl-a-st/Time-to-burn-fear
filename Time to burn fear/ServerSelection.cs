using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Threading;

namespace Time_to_burn_fear
{
    public partial class ServerSelection : Form
    {
        public ServerSelection()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ServerSelection_Load(object sender, EventArgs e)
        {

        }

        private void ServerSelection_Shown(object sender, EventArgs e)
        {


        }

        private void cBxServers_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            cBxServers.Text = "Идет формирование списка серверов";
            pBLoad.MarqueeAnimationSpeed = 30;
            pBLoad.Style = ProgressBarStyle.Marquee;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            ObjectForLoad objectForLoad = new ObjectForLoad();
            objectForLoad.ProgressBar = pBLoad;
            objectForLoad.ComboBox = cBxServers;
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread myThread = new Thread(new ParameterizedThreadStart(loadServers));
            myThread.Start(objectForLoad); // запускаем поток
        }
        public  class ObjectForLoad
        {
            public ComboBox ComboBox;
            public ProgressBar ProgressBar;
        }
        private  void loadServers(object objectForLoad)
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable table = instance.GetDataSources();
            ObjectForLoad thisObjectForLoad = objectForLoad as ObjectForLoad;
            foreach (DataRow row in table.Rows)
            {
                if (row["InstanceName"].ToString() == "")
                    thisObjectForLoad.ComboBox.Items.Add(row["ServerName"]);
                else
                    thisObjectForLoad.ComboBox.Items.Add(row["ServerName"] + "\\" + row["InstanceName"]);
            }
            thisObjectForLoad.ComboBox.Text = "Выберите сервер";
            thisObjectForLoad.ComboBox.Enabled = true;
            thisObjectForLoad.ProgressBar.MarqueeAnimationSpeed = 0;
            thisObjectForLoad.ProgressBar.Style = ProgressBarStyle.Continuous;
            thisObjectForLoad.ProgressBar.Visible = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cBxServers.SelectedIndex<0)
            {
                MessageBox.Show("Сервер не выбран");
                return;
            }
            Constants.serverName = cBxServers.SelectedItem.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.serverName);
        }
    }
}
