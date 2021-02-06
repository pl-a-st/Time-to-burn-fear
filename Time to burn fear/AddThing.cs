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
                    if (!(control is ListBox||control is Button))
                        control.Enabled = false;
                }
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
                        control.Enabled = true;
                }
            }
        }

        private void AddThing_Load(object sender, EventArgs e)
        {
            foreach (int element in Enum.GetValues(typeof(TypeDressInRussian)))
            {
                cBxType.Items.Add((TypeDressInRussian)element);
            }

            foreach (string strDress in DAO.GetListStringsFromFile(Constants.THING_FILE_NAME))
            {
                lBxDress.Items.Add(strDress.Split('\t')[0]);
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
                    (int)nUDFirstParametr.Value + (int)nUDSecondParametr.Value,Constants.THING_FILE_NAME);
                MessageBox.Show("Успех. Новый артефакт в вашем распоряжении.");
                return;
            }
        }
    }
}
