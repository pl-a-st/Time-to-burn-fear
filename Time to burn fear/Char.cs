using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_to_burn_fear
{
    /// <summary>
    /// Раса
    /// </summary>
    public enum Race
    {
        Human,
        Elf,
        Gnome,
        Orc,
        Witcher,
        Elemental
    }
    class Char
    {
        /// <summary>
        /// Раса персонажа
        /// </summary>
        public Race Race
        { get; private set;}
        /// <summary>
        /// Выбрать расу
        /// </summary>
        /// <param name="race"></param>
        public void  SetRace(Race race)
        {
            Race = race;
        }
        /// <summary>
        /// Урон
        /// </summary>
        public int Damage 
        { get; private set; }
        /// <summary>
        /// Устаноть урон
        /// </summary>
        /// <param name="damage"></param>
        public void SetDamage(int damage)
        {
            Damage = damage;
        }
        public int Health
        { get; private set; }
        public void SetHelth(int helth)
        {
            Health = helth;
        }
        public int Speed
        { get; private set; }
        public void SetSpeed(int speed)
        {
            Speed = speed;
        }
        public int Luck
        { get; private set; }
        public void SetLuck(int luck)
        {
            Luck = luck;
        }
        public int Protection
        { get; private set; }
        public void SetProtection(int protection)
        {
            Protection = protection;
        }
        public string Name
        { get; private set; }
        public void SetName(string name)
        {
            Name = name;
        }
    }
}
