using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Threading;
using DALs;

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
            if (File.Exists(Constants.SERVER_NAME))
            {
                StreamReader streamReader = new StreamReader(Constants.SERVER_NAME);
                while (!streamReader.EndOfStream)
                {
                    cBxServers.Items.Add(streamReader.ReadLine());
                    cBxServers.SelectedIndex=0;
                }
                streamReader.Close();
                cBxServers.Enabled = true;
            }
        }

        private void ServerSelection_Shown(object sender, EventArgs e)
        {


        }

        private void cBxServers_Click(object sender, EventArgs e)
        {


        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            cBxServers.Enabled = false;
            cBxServers.Items.Clear();
            cBxServers.Text = "Идет формирование списка серверов";
            pBLoad.Visible = true;
            pBLoad.MarqueeAnimationSpeed = 30;
            pBLoad.Style = ProgressBarStyle.Marquee;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnLoadServers.Enabled = false;
            Thread myThread = new Thread(new ThreadStart(loadServers));
            myThread.Start(); // запускаем поток
        }
        private  void loadServers()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable table = instance.GetDataSources();
            
            foreach (DataRow row in table.Rows)
            {
                if (row["InstanceName"].ToString() == "")
                    cBxServers.Items.Add(row["ServerName"]);
                else
                    cBxServers.Items.Add(row["ServerName"] + "\\" + row["InstanceName"]);
            }
            cBxServers.DropDownStyle = ComboBoxStyle.DropDown;
            cBxServers.Text = "Выберите сервер";
            cBxServers.Enabled = true;
            pBLoad.MarqueeAnimationSpeed = 0;
            pBLoad.Style = ProgressBarStyle.Continuous;
            pBLoad.Visible = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnLoadServers.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cBxServers.SelectedIndex<0)
            {
                MessageBox.Show("Сервер не выбран");
                return;
            }
            DB.ServerName = cBxServers.SelectedItem.ToString();
            StreamWriter streamWriter = new StreamWriter(Constants.SERVER_NAME, false);

            streamWriter.WriteLine(cBxServers.SelectedItem.ToString());
            streamWriter.Close();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cBxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cBxServers.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
