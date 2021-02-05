﻿using System;
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
    public class Hero : Constitution
    {
        public Hero(Constitution constitution, Headdress headdress, Boots boots)
        {
            Parameters = new List<Parameters> { constitution, headdress, boots };
            SetHealth();
        }
        public List<Parameters> Parameters
        { get; private set; }
        public void SetHealth()
        {
            foreach (Parameters parameters in Parameters)
            {
                SetHealth(this.Health + parameters.Health);
            }
        }
    }
    public class Gloves :Dress
    {
        public Gloves(int speed, int protection, string name)
        {
            SetProtection(protection);
            SetSpeed(speed);
            SetName(name);
        }
    }
    public class Headdress : Dress
    {
        public Headdress(int health, int protection, string name)
        {
            SetHealth(health);
            SetProtection(protection);
            SetName(name);
        }
    }
    public class Leggings : Dress
    {
        public Leggings(int speed, int protection,  string name)
        {
            SetProtection(protection);
            SetSpeed(speed);
            SetName(name);
        }
    }
    public class BodyArmor : Dress
    {
        public BodyArmor(int health, int protection, string name)
        {
            SetProtection(protection);
            SetHealth(health);
            SetName(name);
        }
    }
    public class Boots : Dress
    {
        public Boots(int speed,  string name)
        {
            SetSpeed(speed);
            SetName(name);
        }
    }
    
    public class Dress : Parameters
    {
    }
    public class Human : Constitution
    {
        public Human(string name)
        {
            SetName(name);
            SetHealth(100);
            SetDamage(new[] { 5, 6 });
            SetLuck(10);
            SetSpeed(5);
        }
    }
    public class Elf : Constitution
    {
        public Elf(string name)
        {
            SetName(name);
            SetHealth(70);
            SetDamage(new[] { 2, 4 });
            SetLuck(30);
            SetSpeed(9);
        }
    }
    public class Gnome : Constitution
    {
        public Gnome(string name)
        {
            SetName(name);
            SetHealth(150);
            SetDamage(new[] { 6, 7 });
            SetLuck(10);
            SetSpeed(3);
        }
    }
    public class Orc : Constitution
    {
        public Orc(string name)
        {
            SetName(name);
            SetHealth(180);
            SetDamage(new[] { 7, 10 });
            SetLuck(5);
            SetSpeed(2);
        }
    }
    public class Witcher : Constitution
    {
        public Witcher(string name)
        {
            SetName(name);
            SetHealth(120);
            SetDamage(new[] { 7,7 });
            SetLuck(5);
            SetSpeed(10);
        }
    }
    public class Elemental : Constitution
    {
        public Elemental(string name)
        {
            SetName(name);
            SetHealth(200);
            SetDamage(new[] { 10, 10 });
            SetLuck(0);
            SetSpeed(5);
        }
    }
    public class Constitution: Parameters
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
    }
    public class Parameters
    {
        public int[] Damage
        { get; private set; } = new[] { 0, 0 };
        /// <summary>
        /// Устаноть урон
        /// </summary>
        /// <param name="damage"></param>
        public void SetDamage(int[] damage)
        {
            Damage = damage;
        }
        public int Health
        { get; private set; } = 0;
        public void SetHealth(int helth)
        {
            Health = helth;
        }
        public int Speed
        { get; private set; } = 0;
        public void SetSpeed(int speed)
        {
            Speed = speed;
        }
        public int Luck
        { get; private set; } = 0;
        public void SetLuck(int luck)
        {
            Luck = luck;
        }
        public int Protection
        { get; private set; } = 0;
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