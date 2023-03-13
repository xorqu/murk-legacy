using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{

    public enum EnemyType
    {
        Devourer,
        Tvar,
        Dogniv,
        Istlev
    }

    public enum TownType
    {
        Rocksberg,
        Denberg,
        Qorberg
    }

    public enum LocationType
    {
        Swamp,
        Cave,
        Forest,
        Depth,
        Pepel
    }


    public class Enemy
    {
        public Enemy(EnemyType enemyType, int level)
        {
            this.level = level;

            if (enemyType == EnemyType.Devourer)
            {

                damageDispersion = rnd.Next(5, 20);
                name = "Пожиратель";
                damage = (rnd.Next(25, 25+damageDispersion)) + this.level * 4;
                healthpoints = (rnd.Next(300, 300)) + this.level * 5;
                gold = rnd.Next(110, 130) + this.level * 20;
            }  
            
            if (enemyType == EnemyType.Tvar)
            {
                damageDispersion = rnd.Next(5, 20);
                name = "Тварь";
                damage = (rnd.Next(25, 25 + damageDispersion)) + this.level * 4;
                healthpoints = (rnd.Next(300, 300)) + this.level * 5;
                poisDamage = rnd.Next(2, 7);
                gold = rnd.Next(15, 20) + this.level * 20;
            } 

            if (enemyType == EnemyType.Dogniv)
            {
                damageDispersion = rnd.Next(5, 20);
                name = "Догнивающий";
                damage = (rnd.Next(25, 25 + damageDispersion)) + this.level * 4;
                healthpoints = (rnd.Next(300, 300)) + this.level * 5;
                poisDamage = rnd.Next(2, 7);
                gold = rnd.Next(55, 70) + this.level * 20;
            }

            if (enemyType == EnemyType.Istlev)
            {
                damageDispersion = rnd.Next(5, 20);
                name = "Истлевший";
                damage = (rnd.Next(25, 25 + damageDispersion)) + this.level * 4;
                healthpoints = (rnd.Next(300, 300)) + this.level * 5;
                gold = rnd.Next(55, 70) + this.level * 20;
            }

        }

        static Random rnd = new Random();

        private string name;
        private int poisDamage;
        private int healthpoints;
        private int damage;
        private int damageDispersion;
        private int hitProbability = 95;
        private int level;
        private int gold;

        public int GetSetHP
        {
            get { return healthpoints; }
            set { healthpoints = value; }
        }

        public int GetSetDamage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int Attack()
        {
            if (hitProbability > rnd.Next(100))
            {
                int res = damage - rnd.Next(damageDispersion);
                Console.WriteLine("  " + name + " наносит " + res + " урона");
                return res;
            }
            else
            {

                Console.WriteLine("  " + name + " промахивается");
                return 0;
            }
            
        }

        public void DamageTaking(int heroDamage)
        {
            healthpoints -= heroDamage;
        }

        public void PrintInfo()
        {
            Console.WriteLine("\n  DMG [" + (damage - damageDispersion) + "-" + damage + "] HP [" + healthpoints + "] " + name);
        }

        public void DeathAndDrop(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("\n  "+name + " погибает");
            Console.WriteLine("  Вы получили " + gold + " золота");
            hero.GetSetGold += gold;
            // сделать дроп предметов
            Console.ReadLine();
        }

    }

    public class Hero
    {
        public Hero(string name)
        {
            this.name = name;
            damage = 50;
            healthpoints = 3000;
        }

        static Random rnd = new Random();

        private string name = "Hero";
        private int damage;
        private int damageDispersion = 3;
        private int healthpoints;
        private int hitProbability = 95;
        private int regen = 10;
        private int gold = 3000;
        public int GetSetHP
        {
            get { return healthpoints; }
            set { healthpoints = value; }
        }

        public int GetSetGold
        {
            get { return gold; }
            set { gold = value; }
        }


        public int GetSetRegen
        {
            get { return regen; }
            set { regen = value; }
        }

        public int GetSetDamage
        {
            get { return damage; }
            set { damage = value; }
        }

        public int Attack()
        {
            if (hitProbability > rnd.Next(100))
            {
                int res = damage - rnd.Next(damageDispersion);
                Console.WriteLine("  "+name + " наносит " + res + " урона");
                return res;
            }
            else
            {

                Console.WriteLine("  "+name + " промахивается");
                return 0;
            }

        }

        public void DamageTaking(int damage)
        {
            healthpoints -= damage;
        }

        public void PrintInfo()
        {
            Console.WriteLine("\n  DMG ["+ (damage - damageDispersion) + "-"+ damage + "] HP [" + healthpoints + "] " + "REG [" + regen + "] "+ "GLD [" + gold + "] " + name + "\n");

        }

        public void Death(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("\n  "+name + " погибает");
            Console.ReadLine();
            hero.GetSetHP = 300;
            hero.GetSetRegen = 10;
            Program.Menu(hero);
        }

        public void UseRegen()
        {
            if (regen > 0)
            {
                healthpoints += 50;
                regen--;
            }
            
        }

    }

    public class Tutorial
    {

        public static void Start(Hero hero)
        {
            Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
            do
            {
                Console.Clear();
                hero.PrintInfo();
                Console.WriteLine("  Пробираясь через болото, вы наткнулись на тварь, она выглядит враждебно");
                Console.WriteLine();
                Console.Write("\n{0} Атаковать\n{1} Принять судьбу", Program.cursorPosition[0], Program.cursorPosition[1]);
                Program.code = Program.Cursor(2);
            } while (Program.code == 0);
            if (Program.code == 1)
                Tutorial.Battle(hero, EnemyType.Tvar);
            else if (Program.code == 2)
            {
                Console.Clear();
                hero.Death(hero);
            }

            Tutorial.Tma(hero);
        }

        public static void Tma(Hero hero)
        {
            Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
            do
            {
                Console.Clear();
                hero.PrintInfo();
                Console.WriteLine();
                Console.WriteLine("\n  Вдалеке виднеется город, но уже вечереет");
                Console.WriteLine();
                Console.Write("\n{0} Идти ночью\n{1} Найти укрытие и дождаться утра", Program.cursorPosition[0], Program.cursorPosition[1]);
                Program.code = Program.Cursor(2);
            } while (Program.code == 0);
            if (Program.code == 1)
            {
                Tutorial.Battle(hero, EnemyType.Dogniv);

                Console.Clear();
                Console.WriteLine("\n  Вы стоите перед воротами Роксберга, тут начнётся ваш путь\n\n> Войти");
                Console.ReadLine();
                Town town = new Town(TownType.Rocksberg);
                Town.ChoiceInTown(hero, town);
            }
            else if (Program.code == 2)
            {
                Console.Clear();
                hero.Death(hero);
            }


        }


        static public void Battle(Hero hero, EnemyType enemyType)
        {
            Enemy enemy = new Enemy(enemyType, 1);

            while ((enemy.GetSetHP > 0) && (hero.GetSetHP > 0))
            {
                Console.Clear();

                

                

                Console.Beep(200, 300);

                Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
                do
                {
                    Console.Clear();
                    enemy.PrintInfo();
                    hero.PrintInfo();
                    Console.WriteLine();
                   
                    Console.Write("\n{0} Атаковать\n{1} Использовать зелье регенерации", Program.cursorPosition[0], Program.cursorPosition[1]);
                    Program.code = Program.Cursor(2);
                } while (Program.code == 0);
                if (Program.code == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    enemy.DamageTaking(hero.Attack());
                    hero.DamageTaking(enemy.Attack());
                    Console.ReadLine();
                }
                else if (Program.code == 2)
                {
                    Console.Clear();
                    hero.UseRegen();
                }



            }
            if (hero.GetSetHP <= 0)
            {
                hero.Death(hero);
            }
            enemy.DeathAndDrop(hero);

            

        }

    }

    public class Location
    {
        public Location(LocationType locationType)
        {
            if (locationType == LocationType.Swamp)
            {
                locationName = "Болото";
                locationEnemes = new List<EnemyType>{ EnemyType.Dogniv, EnemyType.Tvar };
            }
            if (locationType == LocationType.Forest)
            {
                locationName = "Лес";
                locationEnemes = new List<EnemyType> { EnemyType.Devourer, EnemyType.Tvar };

            }
            if (locationType == LocationType.Pepel)
            {
                locationName = "Пепелище";
                locationEnemes = new List<EnemyType> { EnemyType.Devourer, EnemyType.Istlev };
            }
        }

        static Random rnd = new Random();
        List<EnemyType> locationEnemes = new List<EnemyType>();
        private string locationName;

        public void Battle(Hero hero, Town town, Location location)
        {
            Enemy enemy = new Enemy(locationEnemes[rnd.Next(2)], 1);

            while ((enemy.GetSetHP > 0) && (hero.GetSetHP > 0))
            {
                Console.Clear();
                Console.Beep(200, 300);

                Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n  " + locationName);
                    enemy.PrintInfo();
                    hero.PrintInfo();
                    Console.WriteLine();

                    Console.Write("\n{0} Атаковать\n{1} Использовать зелье регенерации", Program.cursorPosition[0], Program.cursorPosition[1]);
                    Program.code = Program.Cursor(2);
                } while (Program.code == 0);
                if (Program.code == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    enemy.DamageTaking(hero.Attack());
                    hero.DamageTaking(enemy.Attack());
                    Console.ReadLine();
                }
                else if (Program.code == 2)
                {
                    Console.Clear();
                    hero.UseRegen();
                }



            }
            if (hero.GetSetHP <= 0)
            {
                hero.Death(hero);
            }
            enemy.DeathAndDrop(hero);

            Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
            do
            {
                Console.Clear();

                Console.Write("\n{0} Продолжить\n{1} Вернуться в город", Program.cursorPosition[0], Program.cursorPosition[1]);
                Program.code = Program.Cursor(2);
            } while (Program.code == 0);
            if (Program.code == 1)
            {
                
                location.Battle(hero, town, location);
            }
                
            else if (Program.code == 2)
            {
                Console.Clear();
                Town.ChoiceInTown(hero, town);
            }


        }
    }

    public class Town
    {


        public Town(TownType townType)
        {
            if (townType == TownType.Rocksberg)
            {
                townName = "Роксберг";
            }
            if (townType == TownType.Denberg)
            {
                townName = "Денберг";
            }
            if (townType == TownType.Qorberg)
            {
                townName = "Корберг";
            }

        }

        static Random rnd = new Random();
        private string townName;

        public string GetTownName
        {
            get { return townName; }
            set { townName = value; }
        }

        
        static public void ChoiceInTown(Hero hero, Town town)
        {
            

            Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
            do
            {
                Console.Clear();
                hero.PrintInfo();
                Console.WriteLine("\n  Вы находитесь в городе " + town.GetTownName);
                Console.Write("\n{0} Торговец\n{1} Библиотека\n{2} Выйти на охоту\n{3} Отправиться в другой город", Program.cursorPosition[0], Program.cursorPosition[1], Program.cursorPosition[2], Program.cursorPosition[3]);
                Program.code = Program.Cursor(4);
            } while (Program.code == 0);
            if (Program.code == 1)
            {
                Shop.PrintShop(hero, town);
            }
            else if (Program.code == 2)
            {
                Console.Clear();
                Console.WriteLine("\n  Библиотека");
                Console.WriteLine("\n\n  Тварь DMG [ ] HP [ ] GLD [ ]\n  Можно встретить на болоте и в лесу");
                Console.WriteLine("  Способность: ");
                Console.WriteLine("============================================================");
                Console.WriteLine("\n  Догнивающий DMG [ ] HP [ ] GLD [ ]\n  Втречаются в лесу и на пепелище");
                Console.WriteLine("  Мерзкое существо с горящими глазами, из пасти стекает черная субстанция");
                Console.WriteLine("  Способность: Отравление");
                Console.WriteLine("============================================================");
                Console.WriteLine("\n  Истлевший DMG [ ] HP [ ] GLD [ ]\n  Обитает на пепелище");
                Console.WriteLine("  Способность: Воспламенение");
                Console.WriteLine("============================================================");
                Console.WriteLine("\n  Пожиратель DMG [ ] HP [ ] GLD [ ]\n  Втречаются в лесу и на пепелище");
                Console.WriteLine("  Способности: Плевок, ");
                Console.WriteLine("============================================================");
                
                Console.ReadLine();
                Town.ChoiceInTown(hero, town);
            }
            else if (Program.code == 3)
            {
                Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
                do
                {
                    Console.Clear();
                    hero.PrintInfo();
                    Console.WriteLine("\n  Выберете локацию:");
                    Console.Write("\n{0} Болото\n{1} Пепелище\n{2} Лес\n{3} Назад", Program.cursorPosition[0], Program.cursorPosition[1], Program.cursorPosition[2], Program.cursorPosition[3]);
                    Program.code = Program.Cursor(4);
                } while (Program.code == 0);
                if (Program.code == 1)
                {
                    Location location = new Location(LocationType.Swamp);
                    location.Battle(hero, town, location);
                }
                else if (Program.code == 2)
                {
                    Location location = new Location(LocationType.Pepel);
                    location.Battle(hero, town, location);
                }
                else if (Program.code == 3)
                {
                    Location location = new Location(LocationType.Forest);
                    location.Battle(hero, town, location);
                }
                else if (Program.code == 4)
                {
                    Town.ChoiceInTown(hero, town);
                }
            }
            else if (Program.code == 4)
            {
                Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n  Отправиться в\n");
                    Console.Write("\n{0} Денберг\n{1} Корберг", Program.cursorPosition[0], Program.cursorPosition[1]);
                    Program.code = Program.Cursor(2);
                } while (Program.code == 0);
                if (Program.code == 1)
                {
                    Town town1 = new Town(TownType.Denberg);
                    Town.ChoiceInTown(hero, town1);
                }
                else if (Program.code == 2)
                {
                    Town town1 = new Town(TownType.Qorberg);
                    Town.ChoiceInTown(hero, town1);
                }
                
            }

        }

    }

    public class Shop
    {
        static public void PrintShop(Hero hero, Town town)
        {
            Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
            do
            {
                Console.Clear();
                hero.PrintInfo();
                Console.WriteLine("\n  Торговец");
                Console.Write("\n{0} Купить зелье регенерации [30] Золота\n{1} Назад", Program.cursorPosition[0], Program.cursorPosition[1]);
                Program.code = Program.Cursor(2);
            } while (Program.code == 0);
            if (Program.code == 1)
            {

                hero.GetSetRegen += 1;
                hero.GetSetGold -= 30;
                Shop.PrintShop(hero, town);
            }
            else if (Program.code == 2)
            {
                Town.ChoiceInTown(hero, town);
            }
        }
    }

    class Program
    {
        static public int code, menuItem;
        static public char[] cursorPosition = new char[255];

        static public int Cursor(int maxi)
        {
            ConsoleKey btn = Console.ReadKey().Key;
            if (btn == ConsoleKey.Enter)
            {
                cursorPosition[menuItem - 1] = ' ';
                return menuItem;
            }
            if (btn == ConsoleKey.W || btn == ConsoleKey.UpArrow)
            {
                if (menuItem == 1)
                {
                    cursorPosition[0] = ' ';
                    menuItem = maxi;
                    cursorPosition[maxi - 1] = '>';
                    return 0;
                }
                else
                {
                    cursorPosition[menuItem - 1] = ' ';
                    cursorPosition[menuItem - 2] = '>';
                    menuItem--;
                    return 0;
                }
            }
            else if (btn == ConsoleKey.S || btn == ConsoleKey.DownArrow)
            {
                if (menuItem == maxi)
                {
                    cursorPosition[maxi - 1] = ' ';
                    menuItem = 1;
                    cursorPosition[0] = '>';
                    return 0;
                }
                else
                {
                    cursorPosition[menuItem - 1] = ' ';
                    cursorPosition[menuItem] = '>';
                    menuItem++;
                    return 0;
                }
            }
            else return 0;
        }

        static void TitlePrint()
        {
            Console.Clear();
            Console.WriteLine("\n  Предвечный Морок v0.0.3");
            Console.ReadLine();
        }

        public static void Menu(Hero hero)
        {
            Program.code = 0; Program.menuItem = 1; Program.cursorPosition[0] = '>';
            do
            {
                Console.Clear();

                Console.Write("\n{0} Новая игра\n{1} Выход", Program.cursorPosition[0], Program.cursorPosition[1]);
                Program.code = Program.Cursor(2);
            } while (Program.code == 0);
            if (Program.code == 1)
                Tutorial.Start(hero);
            else if (Program.code == 2)
            {
                Console.Clear();
                Environment.Exit(0);
            }
        }
        
        static public void ConsoleSetup()
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        static void Main(string[] args)
        {
            Program.ConsoleSetup();
            Program.TitlePrint();
            Console.Clear();
            Console.WriteLine("\n  Введите имя:");
            Console.CursorLeft = 2;
            string name = Console.ReadLine();


            Hero hero = new Hero(name);

            Town town = new Town(TownType.Rocksberg);
            Town.ChoiceInTown(hero, town);
           // Program.Menu(hero);






            Console.ReadLine();
        }
    }
}
