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
        Elemental,
    }
    public enum RaceInRussian
    {
        Человек = Race.Human,
        Эльф = Race.Elf,
        Гном = Race.Gnome,
        Орк = Race.Orc,
        Ведьмак = Race.Witcher,
        Элементаль = Race.Elemental
    }
    public class Dress : Char
    {
        public new int Health
        { get; private set; } = 0;
        /// <summary>
        /// Раса персонажа
        /// </summary>
        private new Race Race;
        /// <summary>
        /// Урон
        /// </summary>
        public new int[] Damage
        { get; private set; } = { 0, 0 };
        /// <summary>
        /// Устаноть урон
        /// </summary>
        /// <param name="damage"></param>
        public new int Speed
        { get; private set; } = 0;

        public new int Luck 
        { get; private set; } = 0;
        public new int Protection
        { get; private set; } = 0;
    }
    public class Human : Char
    {
        public Human(string name)
        {
            SetName(name);
            SetHelth(100);
            SetDamage(new[] { 5, 6 });
            SetLuck(10);
            SetSpeed(5);
        }
    }
    public class Elf : Char
    {
        public Elf(string name)
        {
            SetName(name);
            SetHelth(70);
            SetDamage(new[] { 2, 4 });
            SetLuck(30);
            SetSpeed(9);
        }
    }
    public class Gnome : Char
    {
        public Gnome(string name)
        {
            SetName(name);
            SetHelth(150);
            SetDamage(new[] { 6, 7 });
            SetLuck(10);
            SetSpeed(3);
        }
    }
    public class Orc : Char
    {
        public Orc(string name)
        {
            SetName(name);
            SetHelth(180);
            SetDamage(new[] { 7, 10 });
            SetLuck(5);
            SetSpeed(2);
        }
    }
    public class Witcher : Char
    {
        public Witcher(string name)
        {
            SetName(name);
            SetHelth(120);
            SetDamage(new[] { 7,7 });
            SetLuck(5);
            SetSpeed(10);
        }
    }
    public class Elemental : Char
    {
        public Elemental(string name)
        {
            SetName(name);
            SetHelth(200);
            SetDamage(new[] { 10, 10 });
            SetLuck(0);
            SetSpeed(5);
        }
    }
    public class Char
    {
        /// <summary>
        /// Раса персонажа
        /// </summary>
        public Race Race
        { get; private set; }
        /// <summary>
        /// Выбрать расу
        /// </summary>
        /// <param name="race"></param>
        public void SetRace(Race race)
        {
            Race = race;
        }
        /// <summary>
        /// Урон
        /// </summary>
        public int[] Damage
        { get; private set; }
        /// <summary>
        /// Устаноть урон
        /// </summary>
        /// <param name="damage"></param>
        public void SetDamage(int[] damage)
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
