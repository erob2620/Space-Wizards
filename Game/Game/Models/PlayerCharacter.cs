using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Items;
using System.ComponentModel;
using spacewizards.Abilities;

namespace spacewizards.Models
{
    public enum CharClass
    {
        Fighter, Wizard, Rogue
    }
    public class PlayerCharacter : Character
    {
        public Status CurrentStatus { get; set; }
        public int keys = 0;
        public int treasuresOpened = 0;

        private int money = 0;
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

        private int tempDef = 0;
        public int TempDef
        {
            get
            {
                return tempDef;
            }
            set
            {
                tempDef = value;
            }
        }
        public int mana = 15;
        private int currentMana;
        public int CurrentMana
        {
            get { return currentMana; }
            set
            {
                currentMana = value;
            }
        }
        public int exp = 0;
       
        private int next = 100;
        public int EXPToNext
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }
        public static Random r = new Random();
        private CharClass PlayerClass { get; set; }
        public List<Equipment> EquipList = new List<Equipment>(10);
        public List<Consumable> ConsumablesList = new List<Consumable>(10);
        public Dictionary<Placement, Equipment> Equipped = new Dictionary<Placement, Equipment>();
        public double EncounterChance = 0;

        public PlayerCharacter()
        {
            PlayerClass = CharClass.Fighter;

            while (ConsumablesList.Count < 10)
            {
                ConsumablesList.Add(null);
            }
            while (EquipList.Count < 10)
            {
                EquipList.Add(null);
            }
            currentMana = mana;
        }
        public void Equip(Equipment e)
        {
            Placement p = e.WhereWorn;
            Equipment temp;
            if (!Equipped.ContainsKey(p))
            {
                Equipped.Add(e.WhereWorn, e);
            }
            else
            {
                if (Equipped.TryGetValue(p, out temp))
                {
                    temp.Equipped = false;
                }
                Equipped.Remove(p);
                e.Equipped = true;
                Equipped.Add(e.WhereWorn, e);
            }
        }
        public void incrementEncounter()
        {
            EncounterChance += .01;
        }
        public void levelUp()
        {
            if (PlayerClass == CharClass.Fighter)
            {
                hp = (int)(hp * 1.2);
                speed = (int)(speed * 1.4);
                atk = (int)(atk * 1.4);
                defense = (int)(defense * 1.4);
            }
            if (PlayerClass == CharClass.Wizard)
            {
                HP = (int)(HP * 1.2);
                Speed = (int)(Speed * 1.2);
                Atk = (int)(Atk * 1.2);
                Defense = (int)(Defense * 1.2);
            }
            if (PlayerClass == CharClass.Rogue)
            {
                HP = (int)(HP * 1.2);
                Speed = (int)(Speed * 1.2);
                Atk = (int)(Atk * 1.2);
                Defense = (int)(Defense * 1.2);
            }
            mana = (int)(mana * 1.3);
            exp -= EXPToNext;
            EXPToNext = (int)(EXPToNext * 1.4);
            level++;
            EncounterChance = 0;
            CurrentHP = HP;
            CurrentMana = mana;
        }
        public void AcquireItem(Equipment e)
        {
            EquipList.Insert(EquipList.IndexOf(null), e);
        }
        public void AcquireItem(Consumable c)
        {
            ConsumablesList.Insert(ConsumablesList.IndexOf(null), c);
        }
        public void GainHP(int toHeal)
        {
            if (CurrentHP + toHeal > HP)
            {
                CurrentHP = HP;
            }
            else
            {
                CurrentHP += toHeal;
            }
        }
        public int getAttacked(int dmg)
        {
            int eqDef = 0;
            foreach (KeyValuePair<Placement, Equipment> e in Equipped)
            {
                eqDef += e.Value.AddDef;
            }
            if (dmg - eqDef - Defense < 4)
            {
                dmg = r.Next(2, 4);
                CurrentHP -= dmg;
            }
            else
            {
                dmg -= eqDef + Defense;
                CurrentHP -= (dmg);
            }
            return dmg;
        }
        public int getDamage()
        {
            int eqAtk = 0;
            foreach (KeyValuePair<Placement, Equipment> e in Equipped)
            {
                eqAtk += e.Value.AddAtk;
            }

            return Atk + eqAtk;
        }
        internal void DecrementEnounter()
        {
            EncounterChance -= 0.01;
            
        }
        public void AddItems(List<Item> items)
        {
            foreach (Item i in items)
            {
                if (i is Consumable)
                {
                    AcquireItem((Consumable)i);
                }
            }
        }
        public Ability GetAbility()
        {
            Ability returnAbility;
            Ability[] abilities = new Ability[] 
            {
                new AttackMove(),
                new DefenseMove(),
                new SpeedMove()
            };

            returnAbility = abilities[r.Next(0, 3)];
            return new AttackMove();
        }
        public void GainKey()
        {
            keys++;
        }
        public void GainMana(int ManaGained)
        {
            if ((CurrentMana + ManaGained) > mana)
            {
                CurrentMana = mana;
            }
            else
            {
                CurrentMana += ManaGained;
            }
        }

    }
}
