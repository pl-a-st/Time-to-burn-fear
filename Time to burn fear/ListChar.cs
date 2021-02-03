using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_to_burn_fear
{
    public class ListChar
    {
        public List<Char> Chars = new List<Char>();
        
        /// <summary>
        /// Создает и записвыает в лист персонажа по имени и расе
        /// </summary>
        /// <param name="race">Раса</param>
        /// <param name="name">Имя</param>
        public void AddInChars(string race, string name)
        {
            Chars.Add(Calculate.ChangeRaceAndCreate((Race)Enum.Parse(typeof(Race),race,true), name));
        }
        /// <summary>
        /// Записвыает в лист созданого персонажа
        /// </summary>
        /// <param name="newChar"> созданный персонаж </param>
        public void AddInChars(Char newChar)
        {
            Chars.Add(newChar);
        }
        /// <summary>
        /// Создает из листа строк персонажей и записвыает в лист 
        /// </summary>
        /// <param name="chars">Лист строк персонажей в формате Name \t Race</param>
        public void AddInChars(List<string> chars)
        {
            foreach (string partChar in chars)
            {
                AddInChars(partChar.Split('\t')[1], partChar.Split('\t')[0]);
            }
        }
    }
}
