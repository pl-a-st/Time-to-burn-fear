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
   
    public partial class AddThing : Form
    {
        public enum createChangeСhoice
        {
            Create,
            Change,
            Choice
        }
        public createChangeСhoice CreateChangeСhoice
        { get;private set;}
        public void SetCreateChangeСhoice(createChangeСhoice createOrChange)
        {
            CreateChangeСhoice = createOrChange;
        }
        public AddThing()
        {
            InitializeComponent();
        }
        public void SetFormDisplay()
        {
            if(CreateChangeСhoice==createChangeСhoice.Choice)
            {
                btnAdd_Save.Text = "Создать";
                btnChange_Cancel.Text = "Изменить";
                foreach (Control control in this.Controls)
                {
                    control.Enabled = true;
                    if (!(control is ListBox||control is Button|| control is Label))
                        control.Enabled = false;
                }
                lBxFromFileSelectFirst(lBxDress, Constants.THING_FILE_NAME);
            }
            if (CreateChangeСhoice == createChangeСhoice.Create)
            {
                btnAdd_Save.Text = "Сохранить";
                btnChange_Cancel.Text = "Отмена";
                foreach (Control control in this.Controls)
                {
                    if (!(control is Button))
                        control.Enabled = true;
                    if (control is ListBox)
                        control.Enabled = false;
                }
                cBxType.Text = cBxType.Items[0].ToString();
                tBxName.Text = "";
                nUDFirstParametr.Value = 0;
                nUDSecondParametr.Value = 0;
            }
            if (CreateChangeСhoice == createChangeСhoice.Change)
            {
                btnAdd_Save.Text = "Сохранить";
                btnChange_Cancel.Text = "Отмена";
                foreach (Control control in this.Controls)
                {
                    if (!(control is Button))
                        control.Enabled = true;
                    if (control is ListBox)
                        control.Enabled = false;
                }
            }
            

        }
        public void lBxFromFileSelectFirst(ListBox listBox, string filename)
        {
            lBxDress.Items.Clear();
            foreach (string strDress in DAO.GetListStringsFromFile(filename))
            {
                if (strDress!=Constants.CUT_DRESS_NAME)
                lBxDress.Items.Add(strDress.Split('\t')[0]);
            }
            if (lBxDress.Items.Count>0)
            lBxDress.SetSelected(0, true);
        }

        private void AddThing_Load(object sender, EventArgs e)
        {
            foreach (int element in Enum.GetValues(typeof(TypeDressInRussian)))
            {
                cBxType.Items.Add((TypeDressInRussian)element);
            }
            
            SetCreateChangeСhoice(createChangeСhoice.Choice);
            SetFormDisplay();
            
        }

        private void btnAdd_Save_Click(object sender, EventArgs e)
        {
            if(CreateChangeСhoice == createChangeСhoice.Choice)
            {
                SetCreateChangeСhoice(createChangeСhoice.Create);
                SetFormDisplay();
                return;
            }
            if (CreateChangeСhoice == createChangeСhoice.Create)
            {
                if(tBxName.Text==""||cBxType.Text=="")
                {
                    MessageBox.Show("Громкий хлопок, дым заполнил лабораторию. Вы забыли добавить необходимые ингредиенты. Предмет не создан!");
                    return;
                }
                foreach(string dress in DAO.GetListStringsFromFile(Constants.THING_FILE_NAME))
                {
                    if (dress.Split('\t')[0]==tBxName.Text && dress.Split('\t')[1] == Convert.ToString((TypeDress)Enum.Parse(typeof(TypeDressInRussian), cBxType.Text, true)))
                    {
                        MessageBox.Show("Вы чувствуете как сгущается воздух вокруг. Пространство не может позволить одинаковые артифакты. " +
                            "Хлопок, артифакт рассыпается у вас в руках!");
                        return;
                    }
                }
                DAO.AddStringToFile(tBxName.Text + '\t' + (TypeDress)Enum.Parse(typeof(TypeDressInRussian), cBxType.Text, true) + '\t' +
                    (int)nUDFirstParametr.Value + '\t' + (int)nUDSecondParametr.Value,Constants.THING_FILE_NAME);
                MessageBox.Show("Успех. Новый артифакт в вашем распоряжении.");
                SetCreateChangeСhoice(createChangeСhoice.Choice);
                SetFormDisplay();
                return;
            }
            if(CreateChangeСhoice == createChangeСhoice.Change)
            {
                if (tBxName.Text == "" || cBxType.Text == "")
                {
                    MessageBox.Show("Громкий хлопок, дым заполнил лабораторию. Вы забыли добавить необходимые ингредиенты. Предмет не изменен!");
                    return;
                }
                List<string> listThing =DAO.GetListStringsFromFile(Constants.THING_FILE_NAME);
                string modifyingString = tBxName.Text + '\t' + (TypeDress)Enum.Parse(typeof(TypeDressInRussian), cBxType.Text, true) + '\t' +
                    (int)nUDFirstParametr.Value + '\t' + (int)nUDSecondParametr.Value;
                listThing[lBxDress.SelectedIndex+1] = modifyingString;
                int i = 0;
                foreach (string dress in listThing)
                {
                    if(dress.Split('\t')[0] == tBxName.Text && dress.Split('\t')[1] == Convert.ToString((TypeDress)Enum.Parse(typeof(TypeDressInRussian), cBxType.Text, true)))
                    {
                        i++;
                        if (i>1)
                        {
                            MessageBox.Show("Вы чувствуете как сгущается воздух вокруг. Вы понимаете, что пространство не может позволить одинаковые артифакты. " +
                                "Хлопок, артифакт остался прежним!");
                            return;
                        }
                    }
                    
                }
                DAO.AddListToFile(listThing, Constants.THING_FILE_NAME);
                MessageBox.Show("Вы вытираете пол с лица: артифакт успешно изменен!");
                SetCreateChangeСhoice(createChangeСhoice.Choice);
                SetFormDisplay();
            }
        }

        private void cBxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                if (control is NumericUpDown)
                {
                    NumericUpDown numericUpDown = control as NumericUpDown;
                    numericUpDown.Value = 0;  
                }
            }
            if(cBxType.Text==string.Concat(TypeDressInRussian.Оружие))
            {
                lblFirstParametr.Text = "Урон";
                nUDSecondParametr.Visible = false;
                lblSecondParamer.Visible = false;
                return;
            }
            if (cBxType.Text == string.Concat(TypeDressInRussian.Кольцо))
            {
                lblFirstParametr.Text = "Урон";
                lblSecondParamer.Text = "Удача";
                return;
            }
            if (cBxType.Text == string.Concat(TypeDressInRussian.Перчатки))
            {
                lblFirstParametr.Text = "Скорость";
                lblSecondParamer.Text = "Защита";
                return;
            }
            if (cBxType.Text == string.Concat(TypeDressInRussian.Шлем))
            {
                lblFirstParametr.Text = "Защита";
                lblSecondParamer.Text = "Здоровье";
                return;
            }
            if (cBxType.Text == string.Concat(TypeDressInRussian.Поножи))
            {
                lblFirstParametr.Text = "Защита";
                lblSecondParamer.Text = "Скорость";
                return;
            }
            if (cBxType.Text == string.Concat(TypeDressInRussian.Нагрудник))
            {
                lblFirstParametr.Text = "Защита";
                lblSecondParamer.Text = "Здоровье";
                return;
            }
            if (cBxType.Text == string.Concat(TypeDressInRussian.Обувь))
            {
                lblFirstParametr.Text = "Скорость";
                lblSecondParamer.Text = "Защита";
                return;
            }
        }

        private void btnChange_Cancel_Click(object sender, EventArgs e)
        {
            if (CreateChangeСhoice==createChangeСhoice.Choice)
            {
                SetCreateChangeСhoice(createChangeСhoice.Change);
                SetFormDisplay();
                return;
            }
            SetCreateChangeСhoice(createChangeСhoice.Choice);
            SetFormDisplay();
        }

        private void lBxDress_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lBxDress.SelectedIndex < 0)
                return;
            if (lBxDress.SelectedItem.ToString() == Constants.CUT_DRESS_NAME)
                return;
            string stringDress = DAO.GetStringsFromFile(Constants.THING_FILE_NAME, lBxDress.SelectedIndex+1);
            string[] allDressPararmetrs = stringDress.Split('\t');
            const int NAME_IN_STRING = 0;
            const int TYPE_IN_STRING = 1;
            const int PARM1_IN_STRING = 2;
            const int PARAM2_IN_STRING = 3;
            tBxName.Text = allDressPararmetrs[NAME_IN_STRING];
            cBxType.SelectedItem=(TypeDressInRussian)Enum.Parse(typeof(TypeDress), allDressPararmetrs[TYPE_IN_STRING], true);
            nUDFirstParametr.Value = int.Parse(allDressPararmetrs[PARM1_IN_STRING]);
            nUDSecondParametr.Value = int.Parse(allDressPararmetrs[PARAM2_IN_STRING]);
        }
    }
}
