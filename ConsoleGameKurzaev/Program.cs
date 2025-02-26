using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameKurzaev
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int health = 100;
            int gold = 50;
            int arrows = 20;
            List<string> inventory = new List<string>() { "Мечь", "Стрелы" }; 

            
            string[] dungeonMap = GenerateDungeonMap();

            Console.WriteLine("Добро пожаловать в Number Quest PLUS!");

            bool gameEnded = false; 

            
            for (int roomNumber = 0; roomNumber < dungeonMap.Length; roomNumber++)
            {
                Console.WriteLine($"\nВы входите в комнату {roomNumber + 1}...");
                string currentRoom = dungeonMap[roomNumber];

                switch (currentRoom)
                {
                    case "Monster":
                        (health, gold, arrows) = BattleMonster(health, gold, arrows);
                        break;
                    case "Trap":
                        health = EnterTrap(health);
                        break;
                    case "Chest":
                        (inventory, gold, arrows) = FindChest(inventory, gold, arrows);
                        break;
                    case "Trader":
                        gold = MeetTrader(gold, health, inventory); 
                        break;
                    case "Empty Room":
                        Console.WriteLine("Комната пуста. Здесь ничего не происходит.");
                        break;
                    case "Boss":
                        (health, gold, arrows) = BattleBoss(health, gold, arrows);
                        break;
                }

               
                if (health <= 0)
                {
                    Console.WriteLine("\nВы погибли в подземелье. Игра окончена.");
                    gameEnded = true;
                    break; 
                }

                if (roomNumber == dungeonMap.Length - 1 && currentRoom == "Boss")
                {
                    Console.WriteLine("\nПоздравляем! Вы победили Босса и прошли Number Quest PLUS!");
                    Console.WriteLine($"Ваши итоговые показатели: Здоровье: {health}, Золото: {gold}, Стрелы: {arrows}, Инвентарь: {string.Join(", ", inventory)}");
                    gameEnded = true;
                    break; 
                }

                Console.WriteLine($"Состояние: Здоровье: {health}, Золото: {gold}, Стрелы: {arrows}, Инвентарь: {string.Join(", ", inventory)}");

             
                if (inventory.Contains("Potion"))
                {
                    Console.WriteLine("У вас есть зелье. Хотите выпить? (y/n)");
                    string drink = Console.ReadLine();
                    if (drink.ToLower() == "y")
                    {
                        health = DrinkPotion(health, inventory);
                    }
                }
            }

            if (gameEnded)
            {
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey(); 
            }
        }

    
        static string[] GenerateDungeonMap()
        {
            string[] dungeonMap = new string[10];
            Random rnd = new Random();
            string[] events = { "Monster", "Trap", "Chest", "Trader", "Empty Room" };

            for (int i = 0; i < dungeonMap.Length - 1; i++)
            {
                dungeonMap[i] = events[rnd.Next(events.Length)];
            }

            dungeonMap[9] = "Boss"; 
            return dungeonMap;
        }

        
        static (int, int, int) BattleMonster(int health, int gold, int arrows)
        {
            Random rnd = new Random();
            int monsterHealth = rnd.Next(25, 61); 

            Console.WriteLine("Внезапно! Перед вами возник злобный монстр!");

            while (health > 0 && monsterHealth > 0)
            {
                Console.WriteLine($"Ваше здоровье: {health}, Здоровье монстра: {monsterHealth}, Стрелы: {arrows}");
                Console.WriteLine("Выберите действие: (1) Атаковать мечом, (2) Стрелять из лука");

                if (arrows <= 0)
                {
                    Console.WriteLine("Стрелы закончились! Только меч!");
                }

                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                int playerDamage = 0;

                switch (choice)
                {
                    case "1": 
                        playerDamage = rnd.Next(12, 23);
                        Console.WriteLine($"Вы рубите мечом и наносите {playerDamage} урона!");
                        break;
                    case "2": 
                        if (arrows > 0)
                        {
                            playerDamage = rnd.Next(7, 18);
                            arrows--;
                            Console.WriteLine($"Вы стреляете из лука, нанося {playerDamage} урона! Осталось {arrows} стрел.");
                        }
                        else
                        {
                            Console.WriteLine("Стрел нет! Бесполезно.");
                        }
                        break;
                    default:
                        Console.WriteLine("Непонятно что вы сделали, пропускаете ход.");
                        break;
                }

                monsterHealth -= playerDamage;

                if (monsterHealth <= 0)
                {
                    Console.WriteLine("Монстр повержен! Ура!");
                    int rewardGold = rnd.Next(15, 36);
                    gold += rewardGold;
                    Console.WriteLine($"Вы получаете {rewardGold} золота!");
                    return (health, gold, arrows);
                }

                int monsterDamage = rnd.Next(7, 18);
                health -= monsterDamage;
                Console.WriteLine($"Монстр огрызается, нанося {monsterDamage} урона!");

                if (health <= 0)
                {
                    Console.WriteLine("Монстр вас одолел...");
                    return (health, gold, arrows);
                }
            }

            return (health, gold, arrows);
        }

        static (int, int, int) BattleBoss(int health, int gold, int arrows)
        {
            Random rnd = new Random();
            int bossHealth = rnd.Next(70, 121);

            Console.WriteLine("Дрожите, путник! Перед вами сам БОСС!");

            while (health > 0 && bossHealth > 0)
            {
                Console.WriteLine($"Ваше здоровье: {health}, Здоровье Босса: {bossHealth}, Стрелы: {arrows}");
                Console.WriteLine("Выберите действие: (1) Атаковать мечом, (2) Стрелять из лука");

                if (arrows <= 0)
                {
                    Console.WriteLine("Стрелы? Забудьте. Только сталь!");
                }

                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                int playerDamage = 0;

                switch (choice)
                {
                    case "1": 
                        playerDamage = rnd.Next(18, 29);
                        Console.WriteLine($"Вы яростно рубите мечом, нанося {playerDamage} урона!");
                        break;
                    case "2": 
                        if (arrows > 0)
                        {
                            playerDamage = rnd.Next(13, 24);
                            arrows--;
                            Console.WriteLine($"Стрела летит точно в цель, нанося {playerDamage} урона! Осталось {arrows} стрел.");
                        }
                        else
                        {
                            Console.WriteLine("Зря на лук смотрите. Нет стрел.");
                        }
                        break;
                    default:
                        Console.WriteLine("Стоите как вкопанный, пропускаете ход.");
                        break;
                }

                bossHealth -= playerDamage;

                if (bossHealth <= 0)
                {
                    Console.WriteLine("Босс повержен! Слава герою!");
                    int rewardGold = rnd.Next(60, 121);
                    gold += rewardGold;
                    Console.WriteLine($"Вы срываете с поверженного врага {rewardGold} золота!");
                    return (health, gold, arrows);
                }

                int bossDamage = rnd.Next(13, 24);
                health -= bossDamage;
                Console.WriteLine($"Босс обрушивает на вас всю свою ярость, нанося {bossDamage} урона!");

                if (health <= 0)
                {
                    Console.WriteLine("Босс оказался сильнее...");
                    return (health, gold, arrows);
                }
            }

            return (health, gold, arrows);
        }

 
        static int EnterTrap(int health)
        {
            Random rnd = new Random();
            int trapDamage = rnd.Next(12, 23);

            Console.WriteLine("О нет! Вы угодили в хитроумную ловушку!");
            health -= trapDamage;
            Console.WriteLine($"Ловушка отнимает {trapDamage} здоровья. Осталось {health}");

            return health;
        }

       
        static (List<string>, int, int) FindChest(List<string> inventory, int gold, int arrows)
        {
            Console.WriteLine("Чудесно! Перед вами древний сундук. Решите задачу, чтобы его открыть:");
            Random rnd = new Random();
            int num1 = rnd.Next(2, 13);
            int num2 = rnd.Next(2, 13);
            int operation = rnd.Next(1, 4); 
            int correctAnswer = 0;
            string operationSymbol = "";

            switch (operation)
            {
                case 1:
                    operationSymbol = "+";
                    correctAnswer = num1 + num2;
                    break;
                case 2:
                    operationSymbol = "-";
                    correctAnswer = num1 - num2;
                    break;
                case 3:
                    operationSymbol = "*";
                    correctAnswer = num1 * num2;
                    break;
            }

            Console.Write($"Сколько будет: {num1} {operationSymbol} {num2} = ");
            string answer = Console.ReadLine();

            if (int.TryParse(answer, out int playerAnswer) && playerAnswer == correctAnswer)
            {
                Console.WriteLine("Браво! Замок открыт!");
                string[] rewards = { "Potion", "Gold", "Arrows" };
                string reward = rewards[rnd.Next(rewards.Length)];

                switch (reward)
                {
                    case "Potion":
                        if (inventory.Count < 5)
                        {
                            inventory.Add("Potion");
                            Console.WriteLine("Найдено зелье! Отправляется в ваш рюкзак.");
                        }
                        else
                        {
                            Console.WriteLine("Зелье есть, но ваш рюкзак полон.");
                        }
                        break;
                    case "Gold":
                        int rewardGold = rnd.Next(25, 46);
                        gold += rewardGold;
                        Console.WriteLine($"В сундуке оказалось {rewardGold} золотых монет!");
                        break;
                    case "Arrows":
                        int rewardArrows = rnd.Next(12, 24);
                        arrows += rewardArrows;
                        Console.WriteLine($"Получите {rewardArrows} стрел! Да пригодятся они вам...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Увы, неверно. Сундук заперт.");
            }

            return (inventory, gold, arrows);
        }

        
        static int MeetTrader(int gold, int health, List<string> inventory) 
        {
            Console.WriteLine("Приветствую! Я - торговец. Что желаете?");
            Console.WriteLine($"У вас {gold} золота. Зелье исцеления - 30 золотых. Купите? (y/n)");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "y")
            {
                if (gold >= 30)
                {
                    gold -= 30;
                    if (inventory.Count < 5)
                    {
                        inventory.Add("Potion");
                        Console.WriteLine("Вы купили зелье.");
                    }
                    else
                    {
                        Console.WriteLine("У вас нет места в инвентаре, чтобы купить зелье!");
                    }
                }
                else
                {
                    Console.WriteLine("Не хватает золота, путник!");
                }
            }
            else
            {
                Console.WriteLine("Ну как знаете... Заходите еще!");
            }

            return gold;
        }


        static int DrinkPotion(int health, List<string> inventory)
        {
            if (inventory.Contains("Potion"))
            {
                health += 35; 
                inventory.Remove("Potion"); 
                Console.WriteLine("Вы выпили зелье. Здоровье восстановлено!");
                return health;
            }
            else
            {
                Console.WriteLine("У вас нет зелья!");
                return health;
            }
        }
    }
}
