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
    public enum TypeDress
    {
        Weapon,
        Ring,
        Gloves,
        Headdress,
        Leggings,
        BodyArmor,
        Boots
    }
    public enum TypeDressInRussian
    {
        Оружие = TypeDress.Weapon,
        Кольцо =  TypeDress.Ring,
        Перчатки = TypeDress.Gloves,
        Шлем = TypeDress.Headdress,
        Поножи = TypeDress.Leggings,
        Нагрудник = TypeDress.BodyArmor,
        Обувь = TypeDress.Boots
    }
    public class Hero : Constitution
    {
        public Hero(Constitution constitution, Dress weapon, Dress headdress, Dress boots, Dress ring1, Dress ring2, Dress gloves, Dress leggings,
            Dress bodyArmor)
        {
            Parameters = new List<Parameters> {constitution, weapon, headdress, boots, ring1, ring2, gloves, leggings, bodyArmor};
            SetParameters();
        }
        public List<Parameters> Parameters
        { get; private set; }
        public void SetParameters()
        {
            foreach (Parameters parameters in Parameters)
            {
                SetHealth(this.Health + parameters.Health);
                SetLuck(this.Luck + parameters.Luck);
                SetProtection(this.Protection + parameters.Protection);
                SetSpeed(this.Speed + parameters.Speed);
                SetDamage(new[] { this.Damage[0] + parameters.Damage[0], this.Damage[1] + parameters.Damage[1] });
                if (parameters is Constitution)
                    SetName(parameters.Name);
            }
        }
        public Random rnd
        { get; private set; } = new Random(DateTime.Now.Millisecond);
        public int TakeDamageDone(int opponentsProtection)
        {
            const int MIN_POSITION_IN_DAMAGE = 0;
            const int MAX_POSITION_IN_DAMAGE = 1;
            const int POINT_MIN_PROBABILITY = 1;
            const int POINT_MAX_PROBABILITY = 101;
            int luckRate = 1;
            int rndToLuck = rnd.Next(POINT_MIN_PROBABILITY, POINT_MAX_PROBABILITY);

            if (rndToLuck <= Luck)
                luckRate = 2;
            int rndToDamage = rnd.Next(Damage[MIN_POSITION_IN_DAMAGE], Damage[MAX_POSITION_IN_DAMAGE] + 1);
            if ((luckRate * rndToDamage - opponentsProtection) < 0)
                return 0;
            return luckRate* rndToDamage - opponentsProtection;
        }
    }
    public class Weapon : Dress
    {
        public Weapon(int damage, string name)
        {
            SetDamage(new[] { damage, damage });
            SetName(name);
        }
    }
    public class Ring : Dress
    {
        public Ring(int damage, int luck, string name)
        {
            SetLuck(luck);
            SetDamage(new[] { damage,damage});
            SetName(name);
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
        public Boots(int speed, int protection, string name)
        {
            SetSpeed(speed);
            SetProtection(protection);
            SetName(name);
        }
    }
    
    public class Dress : Parameters 
    {
        static public Dress CreateTypeDressFromString(string strDress)
        {
            string[] dressParts = strDress.Split('\t');
            if (dressParts.Length==1)
                return new Dress();
            if (dressParts[1] == "Weapon")
                return new Weapon(int.Parse(dressParts[2]), dressParts[0]) as Dress;
            if (dressParts[1] == "Ring")
                return new Ring(int.Parse(dressParts[2]), int.Parse(dressParts[3]), dressParts[0]);
            if (dressParts[1] == "Gloves")
                return new Gloves(int.Parse(dressParts[2]), int.Parse(dressParts[3]), dressParts[0]);
            if (dressParts[1] == "Headdress")
                return new Headdress(int.Parse(dressParts[2]), int.Parse(dressParts[3]), dressParts[0]);
            if (dressParts[1] == "Leggings")
                return new Leggings(int.Parse(dressParts[2]), int.Parse(dressParts[3]), dressParts[0]);
            if (dressParts[1] == "BodyArmor")
                return new BodyArmor(int.Parse(dressParts[2]), int.Parse(dressParts[23]), dressParts[0]);
            if (dressParts[1] == "Boots")
                return new Boots(int.Parse(dressParts[2]), int.Parse(dressParts[3]),dressParts[0]);
            return new Dress();
        }
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
            SetRace(Race.Human);
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
            SetRace(Race.Elf);
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
            SetRace(Race.Gnome);
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
            SetRace(Race.Orc);
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
            SetRace(Race.Witcher);
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
            SetRace(Race.Elemental);
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
