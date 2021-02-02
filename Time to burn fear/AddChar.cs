using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_to_burn_fear
{
    public partial class AddChar : Form
    {
        public AddChar()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddFullNamePart_Click(object sender, EventArgs e)
        {
            try
            {
                Char newChar = Calculate.ChangeRaceAndCreate((Race)Enum.Parse(typeof(Race), cBxRace.Text, true), textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Некорректно указана раса");
            }
          
        }
    }
} 
