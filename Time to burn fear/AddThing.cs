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
        /// <summary>
        /// Присваивает свойству CreateChangeСhoice значение передаваемого enum
        /// </summary>
        /// <param name="createOrChange"></param>
        public void SetCreateChangeСhoice(createChangeСhoice createOrChange)
        {
            CreateChangeСhoice = createOrChange;
        }
        public AddThing()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Формируем внешний вид формы в зависимости от режима
        /// </summary>
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
                lBxFromBaseSelectFirst(lBxDress, TablesName.dress, DressColumnName.name.ToString());
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
        /// <summary>
        /// Заполняет ListBox строками значений из указанной колонки указанной таблицы
        /// </summary>
        /// <param name="listBox">заполняемый ListBox</param>
        /// <param  name="filename">имя файла</param>
         public void lBxFromBaseSelectFirst(ListBox listBox, TablesName tablesName, string columnName)
        {
            DB db = new DB();
            listBox.Items.Clear();
            foreach (string strDress in db.GetListNamesFromBase(tablesName, columnName))
            {
                if (strDress!=Constants.CUT_DRESS_NAME)
                    listBox.Items.Add(strDress);
            }
            if (lBxDress.Items.Count>0)
                listBox.SetSelected(0, true);
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
            DB db = new DB();
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
                foreach(string dress in db.GetListNamesFromBase(TablesName.dress, DressColumnName.name.ToString()))
                {
                    if (dress==tBxName.Text)
                    {
                        MessageBox.Show("Вы чувствуете как сгущается воздух вокруг. Пространство не может позволить одинаковые артифакты. " +
                            "Хлопок, артифакт рассыпается у вас в руках!");
                        return;
                    }
                }
                string[] string1 = {tBxName.Text, ((TypeDress)Enum.Parse(typeof(TypeDressInRussian),cBxType.Text, true)).ToString(), 
                    nUDFirstParametr.Value.ToString(), nUDSecondParametr.Value.ToString() };
                Dress dressForBase = Dress.CreateTypeDressFromArryString(string1);
                List<string> listParaametersForDb= dressForBase.ParametersToListForDB();
                db.InsertDataToDB(TablesName.dress, listParaametersForDb);
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
                string[] string1 = {tBxName.Text, ((TypeDress)Enum.Parse(typeof(TypeDressInRussian),cBxType.Text, true)).ToString(),
                    nUDFirstParametr.Value.ToString(), nUDSecondParametr.Value.ToString() };
                Dress dressForBase = Dress.CreateTypeDressFromArryString(string1);
                List<string> listParaametersForDb = dressForBase.ParametersToListForDB();
                List<string> listDresesName = db.GetListNamesFromBase(TablesName.dress, DressColumnName.name.ToString());
                listDresesName.Remove(lBxDress.SelectedItem.ToString());
                if (listDresesName.Contains(dressForBase.Name))
                {
                    MessageBox.Show("Вы чувствуете как сгущается воздух вокруг. Вы понимаете, что пространство не может позволить одинаковые артифакты. " +
                               "Хлопок, артифакт остался прежним!");
                    return;
                }
                db.ChangeDataInDB(TablesName.dress, listParaametersForDb,DressColumnName.name.ToString(), lBxDress.SelectedItem.ToString());
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
            if (lBxDress.SelectedItem.ToString() == Constants.CUT_DRESS_NAME|| lBxDress.SelectedItem.ToString() =="")
                return;

            Dress dress = new Dress(lBxDress.SelectedItem.ToString());
            //string stringDress = DAO.GetStringsByNumberFromBase(Constants.NAME_BASE,Constants.NAME_TABLE_DRESS,
            //    Constants.DressColumnName, lBxDress.SelectedIndex);
            //string[] allDressPararmetrs = stringDress.Split('\t');
            //const int NAME_IN_STRING = 0;
            //const int TYPE_IN_STRING = 1;
            //const int PARM1_IN_STRING = 2;
            //const int PARAM2_IN_STRING = 3;
            tBxName.Text = dress.Name;
            cBxType.SelectedItem=(TypeDressInRussian)Enum.Parse(typeof(TypeDress), dress.TypeDressEnum.ToString(), true);// to do
            nUDFirstParametr.Value = dress.GetFirstSecondParameters()[0];
            nUDSecondParametr.Value = dress.GetFirstSecondParameters()[1];
            
        }

        private void lBxDress_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
