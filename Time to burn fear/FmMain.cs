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
        public List<string> ListDress
        { get; private set; } = new List<string>();
        public void SetListDress(List<string> listDress)
        {
            ListDress =  listDress;
        }
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
            DAO.LoadCutToFile();
            ListChar.AddInChars(DAO.GetListStringsFromFile(Constants.CHARS_FILE_NAME));// to do
            LoadCharToComboBox(cBxCharFirst, ListChar);
            LoadCharToComboBox(cBxCharSecond, ListChar);
            SetListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            AddListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            LoadAllThingToAllComboBox();
            ChooseFirstItemInCBx(this);
            
          
        }
        public void EquipЕheРero(GroupBox groupBox)
        {

            foreach (Control control in groupBox.Controls)
            {

            }
        }
        private void DeleteChangedDressComBox()
        {
            foreach(Control control in this.Controls)
            {
                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    foreach (string dress in ListDress)
                    {
                        if (dress!= Constants.CUT_DRESS_NAME)
                        {
                            if (comboBox.SelectedItem.ToString() == dress.Split('\t')[0] && comboBox.Name.Contains(dress.Split('\t')[1]))
                            {
                                ListDress.Remove(dress);
                            }
                        }
                            
                    }
                }
                
            }
            
        }
        public void LoadAllThingToAllComboBox()
        {
            
            List<string> listDress =ListDress;
            foreach (string dress in listDress)
            {
                LoadThingToComboboxInControl(dress, this);
            }
        }
        public void ClearDressComboBox(Control control)
        {
            foreach (Control newControl in control.Controls)
            {
                if (newControl is ComboBox)
                {
                    ComboBox comboBox = newControl as ComboBox;
                    if (!comboBox.Name.Contains("Char"))
                        comboBox.Items.Clear();
                }
                if (newControl.Controls.Count > 0)
                    ClearDressComboBox(newControl);
            }
        }
        public void LoadThingToComboboxInControl(string dress, Control control)
        {
            foreach (Control controlPart in control.Controls)
            {
                if (controlPart is ComboBox)
                {
                    ComboBox comboBox = controlPart as ComboBox;
                    if (dress == Constants.CUT_DRESS_NAME)
                    {
                        comboBox.Items.Add(Constants.CUT_DRESS_NAME);
                    }
                    if (dress.Split('\t').Length>1)
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
            ClearDressComboBox(this);
            SetListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            AddListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            LoadAllThingToAllComboBox();
            ChooseFirstItemInCBx(this);
        }
       
        public void AddListDress(List<string> listDress)
        {
            foreach (string strDress in listDress)
            {
                ListDress.Add(strDress);
            }
        }

        private void cBxHeaddressFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateHeroFirstHeroSecondHero();
            SetListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            DeleteChangedDressComBox();
            LoadAllThingToAllComboBox();
        }
        private void CreateHeroFirstHeroSecondHero()
        {
            //try
            //{
                Hero hero = HeroFirst;
                Hero hero2 = HeroSecond;
                SetHeroFirst(CreateHeroFromGroupBox(this.gBxHeroFirst));
                SetHeroSecond(CreateHeroFromGroupBox(this.gBxHeroSecond));
            //}
            //catch (Exception ex)
            //{
            //    DAO.WriteLog(ex.Message);
            //    DAO.WriteLog(ex.StackTrace);
            //}
        }
        public Hero CreateHeroFromGroupBox(GroupBox groupBox)
        {
            List<Dress> allDress = new List<Dress>();
            Constitution constitution = CreateConstitutionListDressFromGroupBoxe(groupBox, out allDress);
            return new Hero(constitution, allDress[0], allDress[1], allDress[2], allDress[3], allDress[4], allDress[5], allDress[6], allDress[7]);
        }
        public Constitution CreateConstitutionListDressFromGroupBoxe(GroupBox groupBox, out List<Dress> allDress)
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
            if (constitutionFromFile==string.Empty)
                return constitution;
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
            string dressFromFile = SetStringItemFile(comboBox, Constants.THING_FILE_NAME);
            Dress dress = new Dress();
            if (dressFromFile==string.Empty||!dressFromFile.Contains("\t"))
                return dress;
            string[] dressArry = dressFromFile.Split('\t');
           
            
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
        private string SetStringItemFile(ComboBox comboBox, string fileName)
        {
            List<string> listString = DAO.GetListStringsFromFile(fileName);
            List<string> listStringAfter = new List<string>();
            foreach (string str in listString)
            {
                if (comboBox.SelectedIndex>-1)    
                if (str.Split('\t').Length>1)
                if (comboBox.Name.Contains(str.Split('\t')[1]) && str.Split('\t')[0] == comboBox.SelectedItem.ToString())
                    listStringAfter.Add(str);
               
            }
            if (listStringAfter.Count < 1)
                return "";
            return listStringAfter[comboBox.SelectedIndex-1];
        }

        private void cBxWeaponFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateHeroFirstHeroSecondHero();
            SetListDress(DAO.GetListStringsFromFile(Constants.THING_FILE_NAME));
            DeleteChangedDressComBox();
            LoadAllThingToAllComboBox();
        }
    }
}
