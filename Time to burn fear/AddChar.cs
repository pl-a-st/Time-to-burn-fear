using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DALs;

namespace Time_to_burn_fear
{
    public partial class AddChar : Form
    {
        public AddChar()
        {
            InitializeComponent();
        }
        private void btnAddChar_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (tBxName.Text == "")
            {
                MessageBox.Show("Не выбрано имя!");
                return;
            }
            Constitution newChar = Calculate.ChangeRaceAndCreate((Race)Enum.Parse(typeof(RaceInRussian), cBxRace.Text, true), tBxName.Text);
            foreach (string dress in db.GetListNamesFromBase(TablesName.constitution, ConstitutionColumnName.name.ToString()))
            {
                if (dress == tBxName.Text)
                {
                    MessageBox.Show("Герой с таким именем уже в игре. Позовите другого героя!");
                    return;
                }
            }
            FmMain.ListChar.AddInChars(newChar);
            db.InsertDataToDB(TablesName.constitution, new List<string> {"'" + newChar.Name + "'", "'" + newChar.Race.ToString() + "'" });
           Close();
        }

        private void AddChar_Load(object sender, EventArgs e)
        {
            foreach (RaceInRussian race in Enum.GetValues(typeof(Race)))
            {
                cBxRace.Items.Add(race );
            }
            cBxRace.Text = cBxRace.Items[0].ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
} 
