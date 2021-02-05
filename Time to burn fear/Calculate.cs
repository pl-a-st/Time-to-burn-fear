using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_to_burn_fear
{
    static class Calculate
    {
        //public static Enum Translation(Enum firstEnum, Enum secondEnum, string strFforTranslation)
        //{
        //   return (firstEnum.)Enum.Parse(typeof(secondEnum), strFforTranslation, true)
        //}
        public static Parameters ChangeRaceAndCreate(Race race, string name)
        {
            Parameters newChar = new Parameters();
            switch (race)
            {
                case Race.Human:
                    Human human = new Human(name);
                    newChar = human; 
                    break;
                case Race.Elf:
                    Elf elf = new Elf(name);
                    newChar = elf;
                    break;
                case Race.Gnome:
                    Gnome gnome = new Gnome(name);
                    newChar = gnome;
                    break;
                case Race.Orc:
                    Orc orc = new Orc(name);
                    newChar = orc;
                    break;
                case Race.Witcher:
                    Witcher witcher = new Witcher(name);
                    newChar = witcher;
                    break;
                case Race.Elemental:
                    Elemental elemental = new Elemental(name);
                    newChar = elemental;
                    break;
                default:
                    MessageBox.Show("Некорректно указана раса.");
                    break;
            }
            return newChar;
        }
    }
}