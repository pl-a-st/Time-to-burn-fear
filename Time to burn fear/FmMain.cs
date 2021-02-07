using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Time_to_burn_fear
{   
    public partial class FmMain : Form
    {
        public FmMain()
        {
            Program.fmMain = this;
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void addChar_Click(object sender, EventArgs e)
        {
            AddChar addChar = new AddChar();
        
            addChar.ShowDialog();
            ClearLoadSelectCharToCombobox(cBxCharFirst);
        }
        private void ClearLoadSelectCharToCombobox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            LoadCharToComboBox(comboBox, ListChar);
            comboBox.Text = comboBox.Items[comboBox.Items.Count - 1].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private static ListChar listChar = new ListChar();
        /// <summary>
        /// Список персонажей
        /// </summary>
        public static ListChar ListChar { get => listChar; set => listChar = value; }
        public Hero HeroFirst
        { get; private set;}
       public void SetHeroFirst(Hero heroFirst)
        {
            HeroFirst = heroFirst;
        }
        private void FmMain_Load(object sender, EventArgs e)
        {
            ListChar.AddInChars(DAO.GetListStringsFromFile(Constants.CHARS_FILE_NAME));// to do
            LoadCharToComboBox(cBxCharFirst, ListChar);
            LoadCharToComboBox(cBxCharSecond, ListChar);
            LoadCutToAllDressCombobox(this);
            LoadAllThingToAllComboBox();
            ChooseFirstItemInCBx(this);
            AddListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            Headdress headdress = new Headdress(3,2, "Деревянный шлем");
            Human human = new Human("Дагоберт");
            //Hero hero = new Hero(human, headdress);
        }
        public void EquipЕheРero(GroupBox groupBox)
        {

            foreach(Control control in groupBox.Controls)
            {

            }
        }
        public void LoadCutToAllDressCombobox(Control control)
        {
            foreach (Control controlPart in control.Controls)
            {
                if (controlPart is ComboBox)
                {
                    ComboBox comboBox = controlPart as ComboBox;
                    if (!comboBox.Name.Contains("Char"))
                        comboBox.Items.Add("Cнято");
                }
                if (controlPart.Controls.Count > 0)
                    LoadCutToAllDressCombobox(controlPart);
            }
        }
        public void LoadAllThingToAllComboBox()
        {
            List<string> listDress = DAO.GetListStringsFromFile(Constants.THING_FILE_NAME);
            foreach (string dress in listDress)
            {
                LoadThingToComboboxInControl(dress, this);
            }
        }
        public void LoadThingToComboboxInControl(string dress, Control control)
        {
            foreach (Control controlPart in control.Controls)
            {
                if (controlPart is ComboBox)
                {
                    ComboBox comboBox = controlPart as ComboBox;
                    if (comboBox.Name.Contains(dress.Split('\t')[1]))
                        comboBox.Items.Add(dress.Split('\t')[0]);
                }
                if (controlPart.Controls.Count > 0)
                    LoadThingToComboboxInControl(dress, controlPart);
            }
        }
        private void ChooseFirstItemInCBx(Control control)
        {
            foreach(Control controlPart in control.Controls)
            {
                if (controlPart is ComboBox)
                {
                    ComboBox comboBox = controlPart as ComboBox;
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedItem = comboBox.Items[0].ToString();
                }
                if (controlPart.Controls.Count>0)
                    ChooseFirstItemInCBx(controlPart);
            }    
        }

        public void LoadCharToComboBox(ComboBox comboBox, ListChar chars)
        {
            foreach (Parameters character in chars.Chars)
            {
                LoadCharToComboBox(comboBox, character.Name);
            }
        }
        public void LoadCharToComboBox(ComboBox comboBox, string character)
        {
            comboBox.Items.Add(character.Split('\t')[0]);
        }

        private void addCharSecond_Click(object sender, EventArgs e)
        {
            AddChar addChar = new AddChar();
            addChar.ShowDialog();
            ClearLoadSelectCharToCombobox(cBxCharSecond);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void btnCreationobjectThing_Click(object sender, EventArgs e)
        {
            AddThing addThing = new AddThing();
            addThing.ShowDialog();
        }
        public List<Dress> ListDress
        { get; private set; } = new List<Dress>();
        public void AddListDress (string strDress)
        {
            Dress dress = Dress.CreateTypeDressFromString(strDress);
            ListDress.Add(dress as Dress);
        }
        public void AddListDress(List<string> lstDress)
        {
            foreach(string strDress in lstDress)
            {
                AddListDress(strDress);
            }
        }
        

    }
}
