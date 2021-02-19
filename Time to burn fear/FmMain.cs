using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Time_to_burn_fear
{
    public partial class FmMain : Form
    {
        /// <summary>
        /// Главное меню. Обращаться можно через Program.fmMain
        /// </summary>
        public FmMain()
        {
            Program.fmMain = this;
            InitializeComponent();
        }
        /// <summary>
        /// Перечисление для организации состояния главной формы
        /// </summary>
        public enum fmMainStatus
        {
            Load,
            Working
        }
        /// <summary>
        /// состояние главной формы: загружаются элементы - Load (по умолчанию), рабочий - Working
        /// </summary>
        public fmMainStatus FmMainStatus
        { get; private set; } = fmMainStatus.Load;
        /// <summary>
        /// Открывает окно создания персонажа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addChar_Click(object sender, EventArgs e)
        {
            AddChar addChar = new AddChar();
            addChar.ShowDialog();
            ClearLoadSelectCharToCombobox(cBxCharFirst);
        }
        /// <summary>
        /// Загружает в comboBox список персонажей 
        /// </summary>
        /// <param name="comboBox"></param>
        private void ClearLoadSelectCharToCombobox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            LoadCharToComboBox(comboBox, ListChar);
            comboBox.SelectedIndex = comboBox.Items.Count - 1;
        }
        /// <summary>
        /// Список персонажей
        /// </summary>
        private static ListChar listChar = new ListChar();
        /// <summary>
        /// Список персонажей
        /// </summary>
        public static ListChar ListChar 
        { get => listChar; set => listChar = value; }
        /// <summary>
        /// Герой из клана сжигающих страх
        /// </summary>
       
        public Hero HeroFirst
        { get; private set; }
        /// <summary>
        /// Назначить героя адептом клана сигающих страх
        /// </summary>
        /// <param name="heroFirst"></param>
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
        public List<string> ListDress
        { get; private set; } = new List<string>();
        public void SetListDress(List<string> listDress)
        {
            ListDress =  listDress;
        }
        public void SetListDress()
        {
            ListDress = new List<string>();
        }
        public List<(string strDress, bool free)> ListDressTuple
        { get; private set; } = new List<(string strDress, bool free)>();
        private void FmMain_Load(object sender, EventArgs e)
        {
            DAO.LoadCutToFile();
            DAO.LoadCharToFileFirstStart();
            ListChar.AddInChars(DAO.GetListStringsFromFile(Constants.CHARS_FILE_NAME));// to do
            LoadCharToComboBox(cBxCharFirst, ListChar);
            LoadCharToComboBox(cBxCharSecond, ListChar);
            LoadAllDressToListTuple();
            LoadAllThingToAllComboBox();
            ChooseFirstItemInCBx(this);
            FmMainStatus = fmMainStatus.Working;
            CreateHeroFirstHeroSecondHero();
        }
        private void LoadAllDressToListTuple()
        {
            ListDressTuple.Clear();
            foreach (string strDress in DAO.GetListStringsFromFile(Constants.THING_FILE_NAME))
            {
                ListDressTuple.Add((strDress, true));
            }
        }
        
        private void DeleteChangedAddReturnedDressComBox(ComboBox chagedComboBox)
        {
            if (FmMainStatus == fmMainStatus.Load)
                return;
            (string strDress, bool free) typleDress = ("", false);
            if (chagedComboBox.SelectedItem.ToString()!=Constants.CUT_DRESS_NAME)
            {
                // в отдельный метод установка False для выбранно элемента одежды
               
                for(int i=0; i<ListDressTuple.Count;i++)
                {
                    if (ListDressTuple[i].strDress.Split('\t')[0]== chagedComboBox.SelectedItem.ToString()
                        && chagedComboBox.Name.Contains(ListDressTuple[i].strDress.Split('\t')[1]))
                    {
                        ListDressTuple[i] = (ListDressTuple[i].strDress, false);
                        typleDress = ListDressTuple[i];
                        break;
                    }
                }
                
               
            }

            // в отдельный метод установка true для освободившегося элемента одежды
            (string strDress, bool free) targetTuple = ("", false);
            for (int i = 0; i < ListDressTuple.Count; i++)
            {
                if (typleDress != ListDressTuple[i] && !typleDress.free)
                {
                    for (int j = 0; j < chagedComboBox.Items.Count; j++)
                    {
                        if (ListDressTuple[i].free==false&&
                            ListDressTuple[i].strDress != Constants.CUT_DRESS_NAME
                            && chagedComboBox.Name.Contains(ListDressTuple[i].strDress.Split('\t')[1]) &&
                                ListDressTuple[i].strDress.Split('\t')[0] != chagedComboBox.SelectedItem.ToString() &&
                            ListDressTuple[i].strDress.Split('\t')[0] == chagedComboBox.Items[j].ToString())
                        {
                            ListDressTuple[i] = (ListDressTuple[i].strDress, true);
                            targetTuple = ListDressTuple[i];
                        }
                    }
                }
            }
            DeletItem(chagedComboBox, this); 
            //в отдельный метод добавить item если true
           
                if (targetTuple.free == true && targetTuple.strDress!=Constants.CUT_DRESS_NAME)
                {
                    AddIthem(chagedComboBox, this, targetTuple);
                }
          
        }
        public void AddIthem(ComboBox chagedComboBox, Control control,(string strDress,bool free) dress)
        {
            foreach (Control thisControl in control.Controls)
            {
                if (thisControl is ComboBox)
                {
                    ComboBox comboBox = thisControl as ComboBox;
                    if (comboBox != chagedComboBox && dress.strDress != Constants.CUT_DRESS_NAME&&
                        comboBox.Name.Contains(dress.strDress.Split('\t')[1]))
                    {
                        comboBox.Items.Add(dress.strDress.Split('\t')[0]);
                    }
                }
                if (thisControl.Controls.Count > 1)
                    AddIthem(chagedComboBox, thisControl, dress);
            }
        }
        public void DeletItem(ComboBox chagedComboBox, Control control)
        {
            foreach (Control thisControl in control.Controls)
            {
                if (thisControl is ComboBox)
                {
                    ComboBox comboBox = thisControl as ComboBox;
                    if (comboBox != chagedComboBox)
                    {
                        for (int i = 0; i < ListDressTuple.Count; i++)
                        {
                            if (ListDressTuple[i].strDress!=Constants.CUT_DRESS_NAME&&
                                comboBox.Name.Contains(ListDressTuple[i].strDress.Split('\t')[1]) &&
                                ListDressTuple[i].free == false&& ListDressTuple[i].strDress!=Constants.CUT_DRESS_NAME)
                            {
                                for (int j = 0; j < comboBox.Items.Count; j++)
                                {
                                    if (ListDressTuple[i].strDress.Split('\t')[0] == comboBox.Items[j].ToString()&& 
                                        comboBox.Items[j]!=comboBox.SelectedItem)
                                        comboBox.Items.Remove(comboBox.Items[j]);
                                }
                            }

                        }
                    }

                }
                if (thisControl.Controls.Count>1)
                {
                    DeletItem(chagedComboBox,thisControl);
                }

            }
        }
        public void LoadAllThingToAllComboBox()
        {
            foreach (string dress in DAO.GetListStringsFromFile(Constants.THING_FILE_NAME))
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
                    if (dress == Constants.CUT_DRESS_NAME && !comboBox.Name.Contains("Char"))
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
            FmMainStatus = fmMainStatus.Load;
            AddThing addThing = new AddThing();
            addThing.ShowDialog();
            ClearDressComboBox(this);
            SetListDress();
            LoadAllDressToListTuple();
            LoadAllThingToAllComboBox();
            ChooseFirstItemInCBx(this);
            FmMainStatus = fmMainStatus.Working;
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
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }
        private void CreateHeroFirstHeroSecondHero()
        {
            try
            {
                Hero hero = HeroFirst;
                Hero hero2 = HeroSecond;
                SetHeroFirst(CreateHeroFromGroupBox(this.gBxHeroFirst));
                SetHeroSecond(CreateHeroFromGroupBox(this.gBxHeroSecond));
                LoadToLBxParameters(HeroFirst, this.lBxCharParametersFirst);
                LoadToLBxParameters(HeroSecond, this.lBxCharParametersSecond);

            }
            catch (Exception ex)
            {
                DAO.WriteLog(ex.Message);
                DAO.WriteLog(ex.StackTrace);
            }
        }
        private void LoadToLBxParameters(Hero hero,ListBox listBox)
        {
            listBox.Items.Clear();
            Constitution constitution= new Constitution();
            if (hero.Parameters[0] is Constitution)
                constitution = hero.Parameters[0] as Constitution;
            try
            {
                listBox.Items.Insert(0, "Герой: " + constitution.Name+" ("+(RaceInRussian)constitution.Race + ")");
                listBox.Items.Insert(1, "Здоровье: "+'\t' + hero.Health);
                listBox.Items.Insert(2, "Защита: " + '\t' + '\t' + hero.Protection);
                listBox.Items.Insert(3, "Атака: " + '\t' + '\t' + hero.Damage[0]+" - "+ hero.Damage[1]);
                listBox.Items.Insert(4, "Скорость: " + '\t' + hero.Speed);
                listBox.Items.Insert(5, "Удача: " + '\t' + '\t' + hero.Luck);
                this.btnFait.Enabled = true;
                if (hero.Health <= 0)
                    this.btnFait.Enabled = false;
            }
            catch (Exception ex)
            {
                DAO.WriteLog(ex.Message);
                DAO.WriteLog(ex.StackTrace);
            }
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
                return new Boots(int.Parse(dressArry[2]), int.Parse(dressArry[3]), dressArry[0]) as Dress;
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
                    return str;
               
            }
            return "";
            
        }

        private void cBxWeaponFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
           
        }

        private void cBxWeaponSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxRingAFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxRingASecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxHeaddressSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxArmorSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxArmorFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxLeggingsFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxLeggingsSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxBootsFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxBootsSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxGlovesFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxGlovesSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxRingBFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxRingBSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteChangedAddReturnedDressComBox(sender as ComboBox);
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxCharFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateHeroFirstHeroSecondHero();
        }

        private void cBxCharSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateHeroFirstHeroSecondHero();
        }
        private void EnabledFalseAllCombobox(Control thisControl)
        {
            foreach (Control control in thisControl.Controls)
            {
                if(control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    comboBox.Enabled=false; 
                }
                if (control.Controls.Count > 1)
                    EnabledFalseAllCombobox(control);
            }
        }
        private void EnabledTrueAllCombobox(Control thisControl)
        {
            foreach (Control control in thisControl.Controls)
            {
                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    comboBox.Enabled = true;
                }
                if (control.Controls.Count > 1)
                    EnabledTrueAllCombobox(control);
            }
        }
        private void btnFait_Click(object sender, EventArgs e)
        {
            EnabledFalseAllCombobox(this);
            TimerСounter = 0;
            SpeedCounter = 0;
            lBxArena.Items.Clear();
            timer.Start();
   
        }
        private int TimerСounter;
        private double SpeedCounter=0;
       private void timer_Tick(object sender, EventArgs e)
        {
            TimerСounter++;
            if (TimerСounter == 1)
                lBxArena.Items.Add(HeroFirst.Name + ": Твоя душа познает муки в плаемени моей ярости!");
            if (TimerСounter == 2)
                lBxArena.Items.Add(HeroSecond.Name + ": Твоя душа сгорит в огне моего гнева!");
            if (TimerСounter>2)
            {
                lBxArena.Items.Add("|");
                double quotientSpeeds = Convert.ToDouble(HeroFirst.Speed) / HeroSecond.Speed;
                const int QUOTIENT_SPEEDS_BOUNDARY = 1;
                Hero firstAttackHero= HeroFirst;
                Hero secondAttackHero=HeroSecond;
                if (quotientSpeeds< QUOTIENT_SPEEDS_BOUNDARY)
                {
                    secondAttackHero = HeroFirst;
                    firstAttackHero = HeroSecond;
                    quotientSpeeds = Convert.ToDouble(HeroSecond.Speed)/ HeroFirst.Speed;
                }
                SpeedCounter = SpeedCounter + quotientSpeeds;
                int rndToLuck;
                int damageDoneHeroFirst = firstAttackHero.TakeDamageDone(secondAttackHero.Protection, out rndToLuck);
                int damageDoneHeroSecond = secondAttackHero.TakeDamageDone(firstAttackHero.Protection, out rndToLuck);
                do
                {
                    secondAttackHero.SetHealth(secondAttackHero.Health - damageDoneHeroFirst);
                    lBxArena.Items.Add(firstAttackHero.Name + " наносит противнику" + damageDoneHeroFirst + " урона." );
                    if (secondAttackHero.Health<=0)
                    {
                        KillHero(secondAttackHero);
                        ReplaceHeroAndLoadListBoxes(firstAttackHero, secondAttackHero, QUOTIENT_SPEEDS_BOUNDARY);
                        EnabledTrueAllCombobox(this);
                        return;
                    }
                    SpeedCounter--;
                }
                while (SpeedCounter >= 1);
                firstAttackHero.SetHealth(firstAttackHero.Health - damageDoneHeroSecond);
                lBxArena.Items.Add(secondAttackHero.Name + " наносит противнику" + damageDoneHeroSecond + " урона.");
                if (firstAttackHero.Health <= 0)
                {
                    KillHero(firstAttackHero);
                    ReplaceHeroAndLoadListBoxes(firstAttackHero, secondAttackHero, QUOTIENT_SPEEDS_BOUNDARY);
                    EnabledTrueAllCombobox(this);
                    return;
                }
                ReplaceHeroAndLoadListBoxes(firstAttackHero, secondAttackHero, QUOTIENT_SPEEDS_BOUNDARY);
                
            }
        }
        private void KillHero(Hero hero)
        {
            lBxArena.Items.Add(hero.Name + " утер немеющей рукой кровь со лба.");
            lBxArena.Items.Add("Осознание конца застыло в стекле его глаз.");
            lBxArena.Items.Add(hero.Name + " упал бездыханно в грязь.");
            timer.Stop();
        }
        private void ReplaceHeroAndLoadListBoxes(Hero firstAttackHero,Hero SecondAttackHero, int quotientSpeedsBoundary)
        {
            int quotientSpeeds = HeroFirst.Speed / HeroSecond.Speed;
            if (quotientSpeeds < quotientSpeedsBoundary)
            {
                HeroFirst = SecondAttackHero;
                HeroSecond = firstAttackHero;
            }
           
            LoadToLBxParameters(HeroFirst, this.lBxCharParametersFirst);
            LoadToLBxParameters(HeroSecond, this.lBxCharParametersSecond);
            lBxArena.SelectedIndex = lBxArena.Items.Count - 1;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public enum saveOrCancel
        {
            Save,
            Cancel
        }
        public saveOrCancel SaveOrCancel
        { get; private set; } = saveOrCancel.Cancel;
        private void SaveOnClick(object sender, EventArgs eventArgs)
        {
            SaveOrCancel = saveOrCancel.Save;
            Button button = sender as Button;
            Form form = button.Parent as Form;
            form.Close();
        }
        private void SaveHeroFirstAndDress(GroupBox groupBox)
        {
            SaveOrCancel = saveOrCancel.Cancel;
            AddChar addChar = new AddChar();
            addChar.btnAddChar.Visible = false;
            addChar.cBxRace.Visible = false;
            addChar.label1.Text = "Название комплекта";
            addChar.label2.Visible = false;
            Button save = new Button();
            save.Text = "Сохранить";
            save.Location = addChar.btnAddChar.Location;
            save.Click += SaveOnClick;
            addChar.Controls.Add(save);
            addChar.ShowDialog();
            if (SaveOrCancel == saveOrCancel.Save)
            {
                if (addChar.tBxName.Text == string.Empty)
                {
                    MessageBox.Show("Не введено название комплекта!");
                    SaveOrCancel = saveOrCancel.Cancel;
                    SaveHeroFirstAndDress(groupBox);
                }
            }
            if (SaveOrCancel==saveOrCancel.Save)
            {
                foreach (string str in DAO.GetListStringsFromFile(Constants.HERO_FILE_NAME))
                {
                    if (str.Split('\t')[0] == addChar.tBxName.Text)
                    {
                        MessageBox.Show("Комплек с таким названием уже существует, придумайте другой");
                        SaveOrCancel = saveOrCancel.Cancel;
                        SaveHeroFirstAndDress(groupBox);
                        break;
                    }
                }
                if (SaveOrCancel == saveOrCancel.Save)
                    DAO.AddStringToFile(addChar.tBxName.Text + "\t" + HeroAndDressToList(groupBox as Control), Constants.HERO_FILE_NAME);
            } 
            SaveOrCancel = saveOrCancel.Cancel;
        }
        private void btnSaveHeroFirstAndDress_Click(object sender, EventArgs e)
        {
            SaveHeroFirstAndDress(this.gBxHeroFirst);
        }
        private string HeroAndDressToList(Control control)
        {
            List<string> listHeroWithDress = new List<string>();
            string strHeroWithDress=string.Empty;
            foreach (Control thisControl in control.Controls)
            {
                if (thisControl is ComboBox)
                {
                    ComboBox comboBox = thisControl as ComboBox;
                    listHeroWithDress.Add(comboBox.Name + "\t" + comboBox.SelectedItem.ToString());
                }
                if (thisControl.Controls.Count > 1)
                    HeroAndDressToList(thisControl);
            }
            
            foreach (string str in listHeroWithDress)
            {
                if (strHeroWithDress != string.Empty)
                    strHeroWithDress = strHeroWithDress + "\t";
                strHeroWithDress = strHeroWithDress + str;
            }
            return strHeroWithDress;
        }

        private void btnLoadFirstHeroAndDress_Click(object sender, EventArgs e)
        {
            LoadHeroAndDress(this.gBxHeroFirst);
        }
        private void LoadHeroAndDress(GroupBox groupBox)
        {
            Form form = new Form();
            form.Text = "Гардероб";
            form.Width = 300;
            form.Height = 300;
            form.StartPosition = FormStartPosition.CenterScreen;
            ListBox listBox = new ListBox();
            listBox.Location = new System.Drawing.Point(15, 15);
            listBox.Width = form.ClientSize.Width - listBox.Location.X * 2;
            listBox.Height = form.ClientSize.Height - listBox.Location.Y * 2;
            Button btnLoad = new Button();
            Button btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnLoad.Text = "Одеть костюм";
            btnLoad.Tag = groupBox.Name;
            btnLoad.Width = listBox.Width / 2 - 15 / 2;
            btnCancel.Width = btnLoad.Width;
            btnLoad.Height = 35;
            btnCancel.Height = 35;
            listBox.Height = listBox.Height - btnLoad.Height - 15;
            btnLoad.Location = new System.Drawing.Point(listBox.Location.X, listBox.Location.Y + listBox.Height + 15);
            btnCancel.Location = new System.Drawing.Point(listBox.Location.X + btnLoad.Width + 15, listBox.Location.Y + listBox.Height + 15);
            btnLoad.Click += btnLoad_Click;
            btnCancel.Click += btnCancel_Click;
            form.Controls.Add(listBox);
            form.Controls.Add(btnLoad);
            form.Controls.Add(btnCancel);
            foreach (string heroAndDress in DAO.GetListStringsFromFile(Constants.HERO_FILE_NAME))
            {
                listBox.Items.Add(heroAndDress.Split('\t')[0]);
            }
            form.ShowDialog();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Form form = button.Parent as Form;
            foreach(Control controlForm in form.Controls)
            {
                if (controlForm is ListBox)
                {
                    ListBox listBox = controlForm as ListBox;
                    if (listBox.SelectedIndex > -1)
                    {
                        foreach (Control control in this.Controls)
                        {
                            if (control is GroupBox && control.Name == button.Tag.ToString())
                            {
                                foreach (Control thisControl in control.Controls)
                                {
                                    if (thisControl is ComboBox)
                                    {
                                        ComboBox comboBox = thisControl as ComboBox;
                                        string heroAndDress = DAO.GetListStringsFromFile(Constants.HERO_FILE_NAME)[listBox.SelectedIndex];
                                        for (int i = 0; i < heroAndDress.Split('\t').Length; i++)
                                        {
                                            string str = comboBox.Name.Replace("First", "").Replace("Second", "");
                                            if (comboBox.Name.ToString().Replace("First","").Replace("Second","") == heroAndDress.Split('\t')[i].Replace("First", "").Replace("Second", ""))
                                            {
                                                try
                                                {
                                                    comboBox.SelectedItem = heroAndDress.Split('\t')[i + 1];
                                                    if (comboBox.SelectedItem.ToString() != heroAndDress.Split('\t')[i + 1])
                                                        MessageBox.Show("Вы не можете найти в своем гардеробе " + heroAndDress.Split('\t')[i + 1] +
                                                        ". Сорее всего этот проходимец, с которым предстоит дуэль, пошарился по гардеробу");
                                                }
                                                catch
                                                {
                                                    MessageBox.Show("Вы не можете найти в своем гардеробе " + heroAndDress.Split('\t')[i + 1] +
                                                        ". Сорее всего этот проходимец, с которым предстоит дуэль, пошарился по гардеробу");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
               
            
            form.Close();
        } 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Form form = button.Parent as Form;
            form.Close();
        }

        private void btnSaveHeroSecondAndDress_Click(object sender, EventArgs e)
        {
            SaveHeroFirstAndDress(this.gBxHeroSecond);
        }

        private void btnLoadSecondHeroAndDress_Click(object sender, EventArgs e)
        {
            LoadHeroAndDress(this.gBxHeroSecond);
        }
    }
}
