using System;
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

        private void FmMain_Load(object sender, EventArgs e)
        {
            ListChar.AddInChars(DAO.GetListStringsFromFile(Constants.CHARS_FILE_NAME));
            LoadCharToComboBox(cBxCharFirst, ListChar);
            
            LoadCharToComboBox(cBxCharSecond, ListChar);
            ChooseFirstItemInCBx(this);
            Headdress headdress = new Headdress(3,2, "Деревянный шлем");
            Human human = new Human("Дагоберт");
            //Hero hero = new Hero(human, headdress);

        }
        private void ChooseFirstItemInCBx(Control control)
        {
            foreach(Control controlPart in control.Controls)
            {
                if (controlPart is ComboBox)
                {
                    ComboBox comboBox = controlPart as ComboBox;
                    if (comboBox.Items.Count > 0)
                        comboBox.Text = comboBox.Items[0].ToString();
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
    }
}
