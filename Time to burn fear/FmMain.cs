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
        { get; private set; }
        public void SetHeroFirst(Hero heroFirst)
        {
            HeroFirst = heroFirst;
        }
        public Hero HeroSecond
        { get; private set; }
        public void SetHeroSecond(Hero heroSecond)
        {
            HeroSecond = heroSecond;
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
            Headdress headdress = new Headdress(3, 2, "Деревянный шлем");
            Human human = new Human("Дагоберт");
            //Hero hero = new Hero(human, headdress);
        }
        public void EquipЕheРero(GroupBox groupBox)
        {

            foreach (Control control in groupBox.Controls)
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
            foreach (Control controlPart in control.Controls)
            {
                if (controlPart is ComboBox)
                {
                    ComboBox comboBox = controlPart as ComboBox;
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedItem = comboBox.Items[0].ToString();
                }
                if (controlPart.Controls.Count > 0)
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
        public void AddListDress(string strDress)
        {
            Dress dress = Dress.CreateTypeDressFromString(strDress);
            ListDress.Add(dress as Dress);
        }
        public void AddListDress(List<string> lstDress)
        {
            foreach (string strDress in lstDress)
            {
                AddListDress(strDress);
            }
        }

        private void cBxHeaddressFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Constitution human;
            try
            {
                human = new Human(SetStringComboboxFile
                (cBxCharFirst, Constants.CHARS_FILE_NAME).Split('\t')[0]) as Constitution;
                //SetHeroFirst(new Hero());
            }
            catch (Exception ex)
            {
                DAO.WriteLog(ex.Message);
                DAO.WriteLog(ex.StackTrace);
            }
        }
        public Constitution CreateHeroFromGroupBoxe(GroupBox groupBox, out List<Dress> allDress)
        {
            Constitution constitution = new Constitution();
            allDress = new List<Dress>();
           foreach (Control control in groupBox.Controls)
            {
                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    if (comboBox.Name.Contains("Char"))
                    {
                        constitution = CreateConstitutionFromCombobox(comboBox);
                    }
                    else
                    {
                        allDress.Add(CreateDressFromCombobox(comboBox));
                    }
                }
            }
            return constitution;


        }
        public Constitution CreateConstitutionFromCombobox(ComboBox comboBox)
        {
            Constitution constitution = new Constitution();
            string constitutionFromFile = SetStringComboboxFile(comboBox, Constants.CHARS_FILE_NAME);
            if ((Race)Enum.Parse(typeof(Race), constitutionFromFile.Split('\t')[1], true) == Race.Human)
                return new Human(constitutionFromFile.Split('\t')[0]) as Constitution;
            if ((Race)Enum.Parse(typeof(Race), constitutionFromFile.Split('\t')[1], true) == Race.Elemental)
                return new Elemental(constitutionFromFile.Split('\t')[0]) as Constitution;
            if ((Race)Enum.Parse(typeof(Race), constitutionFromFile.Split('\t')[1], true) == Race.Elf)
                return new Elf(constitutionFromFile.Split('\t')[0]) as Constitution;
            if ((Race)Enum.Parse(typeof(Race), constitutionFromFile.Split('\t')[1], true) == Race.Gnome)
                return new Gnome(constitutionFromFile.Split('\t')[0]) as Constitution;
            if ((Race)Enum.Parse(typeof(Race), constitutionFromFile.Split('\t')[1], true) == Race.Orc)
                return new Orc(constitutionFromFile.Split('\t')[0]) as Constitution;
            if ((Race)Enum.Parse(typeof(Race), constitutionFromFile.Split('\t')[1], true) == Race.Witcher)
                return new Witcher(constitutionFromFile.Split('\t')[0]) as Constitution;
            return constitution;
        }
        public Dress CreateDressFromCombobox(ComboBox comboBox)// to do заменить индексы на константы
        {
            string dressFromFile = SetStringComboboxFile(comboBox, Constants.THING_FILE_NAME);
            string[] dressArry = dressFromFile.Split('\t');
            Dress dress =new Dress();
            if (!dressFromFile.Contains("\t"))
                return dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.BodyArmor)
                return new BodyArmor(int.Parse(dressArry[2]), int.Parse(dressArry[3]), dressArry[0]) as Dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.Boots)
                return new Boots(int.Parse(dressArry[2]), dressArry[0]) as Dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.Gloves)
                return new Gloves(int.Parse(dressArry[2]), int.Parse(dressArry[3]), dressArry[0]) as Dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.Headdress)
                return new Headdress(int.Parse(dressArry[2]), int.Parse(dressArry[3]), dressArry[0]) as Dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.Leggings)
                return new Leggings(int.Parse(dressArry[2]), int.Parse(dressArry[3]), dressArry[0]) as Dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.Ring)
                return new Ring(int.Parse(dressArry[2]), int.Parse(dressArry[3]), dressArry[0]) as Dress;
            if ((TypeDress)Enum.Parse(typeof(TypeDress), dressFromFile.Split('\t')[1], true) == TypeDress.Weapon)
                return new Weapon(int.Parse(dressArry[2]), dressArry[0]) as Dress;
            return dress;
        }
        private string SetStringComboboxFile(ComboBox comboBox,string fileName)
        {
            int numberString = comboBox.SelectedIndex;
            return DAO.GetStringsFromFile(fileName, numberString);
        }
    }
}
