﻿using System;
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

        private void btnAddChar_Click(object sender, EventArgs e)
        {
            if (tBxName.Text == "")
            {
                MessageBox.Show("Не выбрано имя!");
                return;
            }
            Constitution newChar = Calculate.ChangeRaceAndCreate((Race)Enum.Parse(typeof(RaceInRussian), cBxRace.Text, true), tBxName.Text);
            foreach(Parameters constitution in FmMain.ListChar.Chars)
            {
                if (constitution.Name== newChar.Name)
                {
                    MessageBox.Show("Герой с таким именем уже в игре. Позовите другого героя!");
                    return;
                }
                   
            }
            FmMain.ListChar.AddInChars(newChar);
            DAO.AddStringToFile(newChar.Name + "\t" + newChar.Race, Constants.CHARS_FILE_NAME);
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
