using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApplication8
{
      class Card
    {
        public Card(string a, int b, int c, string d, string e, int f, int g, int h, bool i, bool j, bool k, int l, int m)
        {
            string name = a;
            int price = b;
            int cost = c;
            string type = d;
            string info = e;
            int quant = f;
            int atk = g;
            int def = h;
            bool fly = i;
            bool climb = j;
            bool swim = k;
            int speed = l;
            int power = m;
        }
        public string name, info, type; //name = 卡片名稱 info = 卡片資訊 type = 卡片類型
        public int price, cost, quant, atk, def, speed, power; //price = 購買此卡需要的DNA數量 cost = 卡片需消耗飽食度
        //atk = 攻擊力 def = 防禦力 speed = 速度 power = 力量
        public bool fly, climb, swim;
        //fly = 是否能飛 climb = 是否能攀爬 swim = 是否能游泳
        
    }
      class Player
      {
          public Player(int a, int b, int c, int d, int e, int f)
          {
              int DNA = a;
              int number = b;
              int hunger = c;
              int power = d;
              int speed = e;
              int equi = f;
          }
          public int DNA, number, hunger, power, speed, equi;
          //DNA = 玩家角色持有的DNA數量 number = 玩家角色的族群總數 hunger = 玩家角色的飽食度
          // info = 玩家資訊 equi = 玩家目前已裝備之裝備 speed = 玩家的速度值 power = 玩家的力量值
          public string info;
      }
    class Food
    {
        public Food(string a, int b, bool c, bool d, bool e, int f, int g)
        {
            string foodname = a;
            int needquant = b;
            bool needfly = c;
            bool needclimb = d;
            bool needswim = e;
            int needspeed = f;
            int needpower = g;
        }
        public int needquant, needpower, needspeed;
        // needquant = 進食此食物所需擁有之族群數量 needpower = 進食此食物所需擁有之力量 needspeed = 進食此食物所需擁有之速度
        public bool needfly, needclimb, needswim;
        // needfly = 進食此食物是否需要能夠飛行 needclimb = 進食此食物是否需要能夠攀爬 needswim = 進食此食物是否需要能夠游泳
        public string foodname;
        // 該種食物之名稱
    }

    class Program
    {
        static void Main()
        {
            
                System.IO.StreamReader file = new System.IO.StreamReader("Card.txt");
                int i;
                int allcardquant = int.Parse(file.ReadLine());
                Card[] card = new Card[allcardquant];
                for (i = 0; i < allcardquant; ++i)
                {
                    card[i] = new Card("0", 0, 0, " 0", " 0", 0, 0, 0, true, true, true, 0, 0);
                    /*card[i] = new Card(file.ReadLine(), int.Parse(file.ReadLine()), int.Parse(file.ReadLine()), file.ReadLine(),
                        file.ReadLine(), int.Parse(file.ReadLine()), int.Parse(file.ReadLine()), int.Parse(file.ReadLine()),
                        bool.Parse(file.ReadLine()), bool.Parse(file.ReadLine()), bool.Parse(file.ReadLine()), int.Parse(file.ReadLine())
                        , int.Parse(file.ReadLine()));*/


                    card[i].name = file.ReadLine();
                    card[i].price = int.Parse(file.ReadLine());
                    card[i].cost = int.Parse(file.ReadLine());
                    card[i].type = file.ReadLine();
                    card[i].info = file.ReadLine();
                    card[i].quant = int.Parse(file.ReadLine());
                    card[i].atk = int.Parse(file.ReadLine());
                    card[i].def = int.Parse(file.ReadLine());
                    card[i].fly = bool.Parse(file.ReadLine());
                    card[i].climb = bool.Parse(file.ReadLine());
                    card[i].swim = bool.Parse(file.ReadLine());
                    card[i].speed = int.Parse(file.ReadLine());
                    card[i].power = int.Parse(file.ReadLine());
                }
            
            
                System.IO.StreamReader file2 = new System.IO.StreamReader("Food.txt");
                int foodquant = int.Parse(file2.ReadLine());
                Food[] food = new Food[foodquant];
                for (i = 0; i < foodquant; ++i)
                {
                    food[i] = new Food("0", 0, true, true, true, 0, 0);
                    food[i].foodname = file2.ReadLine();
                    food[i].needquant = int.Parse(file2.ReadLine());
                    food[i].needfly = bool.Parse(file2.ReadLine());
                    food[i].needclimb = bool.Parse(file2.ReadLine());
                    food[i].needswim = bool.Parse(file2.ReadLine());
                    food[i].needspeed = int.Parse(file2.ReadLine());
                    food[i].needpower = int.Parse(file2.ReadLine());
                
                }

                int card_max = 0;
                for (i = 0; i < allcardquant; i++)
                {
                    card_max += card[i].quant;
                }
                Card[] deck = new Card[card_max];
                int card_print = 0;
                int[] startingdeck = new int[card_max];
                for (i = 0; i < card_max; i++)
                {
                    deck[i] = new Card("0", 0, 0, " 0", " 0", 0, 0, 0, true, true, true, 0, 0);
                }
                for (i = 0; i < allcardquant; i++)
                {
                    for (int k = 0; k < card[i].quant; k++)
                    {
                        deck[card_print].name = card[i].name;
                        deck[card_print].price = card[i].price;
                        deck[card_print].cost = card[i].cost;
                        deck[card_print].type = card[i].type;
                        deck[card_print].info = card[i].info;
                        deck[card_print].quant = 1;
                        deck[card_print].atk = card[i].atk;
                        deck[card_print].def = card[i].def;
                        deck[card_print].fly = card[i].fly;
                        deck[card_print].climb = card[i].climb;
                        deck[card_print].swim = card[i].swim;
                        deck[card_print].speed = card[i].speed;
                        deck[card_print].power = card[i].power;
                        startingdeck[card_print] = card_print;
                        card_print++;
                    }
                }
                //建立桌面
                shuffle(ref startingdeck);
                shuffle(ref food);
                int[,] playercards = new int[4, 30];
                int[,] playerused = new int[4, 30];
                int[,] playerhands = new int[4, 30];
                int deal = 0;//從startingdeck依序發牌給playerhads,shop,shopdeck
                for (i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        playercards[i, j] = startingdeck[deal];
                        playerused[i, j] = 255;
                        playerhands[i, j] = 255;
                        deal++;
                    }
                    for (int j = 5; j < 30; j++)
                    {
                        playercards[i, j] = 255;
                        playerused[i, j] = 255;
                        playerhands[i, j] = 255;
                    }
                }
                int[] shop = new int[8];
                for (i = 0; i < shop.Length; i++)
                {
                    shop[i] = startingdeck[deal];
                    deal++;
                }
                int[] shopdeck = new int[startingdeck.Length - 28];
                for (i = 0; i < shopdeck.Length; i++)
                {
                    shopdeck[i] = startingdeck[deal];
                    deal++;
                }
                //食物
                //角色初始化
                Player[] player = new Player[4];
                for (i = 0; i < player.Length; i++)
                {
                    player[i] = new Player(0,0,0,0,0,0);
                    player[i].number = 3;
                    player[i].DNA = 4;
                    player[i].equi = 0;
                    player[i].power = 3;
                    player[i].speed = 3;
                }


                
                Random playerrand = new Random(); ///玩家洗牌用
                do
                {
                    Console.WriteLine("It's your turn. Draw!");
                    ///Draw!
                    for (i = 0; i < 2; i++)
                    {
                        if (last(playercards, 0) != -1 && last(playerhands, 0) != 6)
                        {
                            playerhands[0, last(playerhands, 0) + 1] = playercards[0, last(playercards, 0)];
                            playercards[0, last(playercards, 0)] = 255;
                        }
                        else if (last(playercards, 0) == -1 && last(playerhands, 0) != 6 && last(playerused, 0) != -1)
                        {
                            Console.WriteLine("No cards in deck.Rebuilding your deck.");
                            while (last(playerused, 0) != -1)
                            {
                                int recycle = playerrand.Next() % (last(playerused, 0) + 1);
                                playercards[0, last(playercards, 0)+1] = playerused[0, recycle];
                                if (recycle != last(playerused, 0)) playerused[0, recycle] = playerused[0, last(playerused, 0)];
                                playerused[0, last(playerused, 0)] = 255;
                                ///隨機將棄牌堆剩下的任一張牌放到玩家牌堆頂 並將棄牌的最後一張補進空格 直到棄牌區沒牌
                            }
                            playerhands[0, last(playerhands, 0)] = playercards[0, last(playercards, 0)];
                            playercards[0, last(playercards, 0)] = 255;
                        }
                        else if (last(playerused, 0) != -1) Console.WriteLine("No card to draw");
                        else
                        {
                            Console.WriteLine("Your hand is full. Can't draw more cards!");
                            break;
                        }
                    }
                    ///Draw end!
                    ///print player hand
                    Console.WriteLine("Your hand:");
                    for (i = 0; i < last(playerhands, 0)+1; i++)
                    {
                        Console.WriteLine("{0}", deck[playerhands[0, i]].name);
                    }
                    int act_choose = 0;
                    while (act_choose != 3)
                    {
                        Console.WriteLine("Choose your act:\n0 for use hand card\n1 for shopping\n2 for checking info(s)\n3 for end turn");
                        act_choose = int.Parse(Console.ReadLine());
                        switch (act_choose)
                        {
                            case 0:
                                use(ref playerhands, ref deck, ref player);
                                break;
                            case 1:
                                if (last(shop) == -1)///shop沒牌啦
                                {
                                    Console.WriteLine("There's nothing in the shop");
                                }
                                else
                                {
                                    buy(ref playerused, ref deck, ref shopdeck, ref shop, ref player);
                                }
                                break;
                            case 2:
                                show(ref playerhands, ref deck, ref shopdeck, ref shop, ref player);
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("You ended your turn.");
                    Console.WriteLine("CPU1 did nothing");
                    Console.WriteLine("CPU2 did nothing");
                    Console.WriteLine("CPU3 did nothing");
                    for (i = 0; i < 4; i++)
                    {
                        player[i].DNA += player[i].number;
                    }
                    Console.WriteLine("it's your turn again");
                }
                while (true);  
        }
        /// Main over
        static void shuffle(ref int[] cards)
        {
            Random rand = new Random();
            int[] temp = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                temp[i] = rand.Next();
            }
            Array.Sort(temp, cards);
        }
        static void shuffle(ref Food[] cards)
        {
            Random rand = new Random();
            int[] temp = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                temp[i] = rand.Next();
            }
            Array.Sort(temp, cards);
        }






        static void use(ref int[,] playerhands, ref Card[] deck, ref Player[] player)
        {
            Console.WriteLine("Which card do you want to use:");
            int i;
            for (i = 0; i < last(playerhands, 0)+1; i++)
            {
                Console.WriteLine("{0} for {1}", i, deck[playerhands[0, i]].name);
            }
            int choose = int.Parse(Console.ReadLine());
            switch (deck[playerhands[0, choose]].type)
            {
                case "atk":


                    break;
                case "def":


                    break;
                case "equi":


                    break;
            }
        }


        static void buy(ref int[,] playerused, ref Card[] deck, ref int[] shopdeck, ref int[] shop, ref Player[] player)
        {
            int i;
            Console.WriteLine("What would you like to buy:");
            for (i = 0; i < last(shop)+1; i++)
            {
                Console.WriteLine("{0} for {1}", i, deck[shop[i]].name);
            }
            int choose = int.Parse(Console.ReadLine());
            if (deck[shop[choose]].cost <= player[0].DNA)
            {
                Console.WriteLine("It costs you {0} DNA!", deck[shop[choose]].cost);
                player[0].DNA -= deck[shop[choose]].cost;


                playerused[0, last(playerused, 0)+1] = shop[choose];
   
                if (last(shopdeck) != -1) ///從shopdeck補充牌進shop
                {
                    shop[choose] = 255;
                    shop[choose] = shopdeck[last(shopdeck)];
                    shopdeck[last(shopdeck)] = 255;
                }
                else if (last(shop) != choose && choose != last(shopdeck))///shopdeck用完了 且買的牌不是最後面一張
                {
                    shop[choose] = shop[last(shop)];
                    shop[last(shop)] = 255;
                }
                else
                {
                    shop[choose] = 255;
                }
            }
            else
            {
                Console.WriteLine("you don't have enough DNA!");
            }
        }


        static void show(ref int[,] playerhands, ref Card[] deck, ref int[] shopdeck, ref int[] shop, ref Player[] player)
        {
            Console.WriteLine("What would you like to see?");
            Console.WriteLine("0 for your own hands\n1 for shop item list\n2 for player status");
            int choose = int.Parse(Console.ReadLine());
            int i;
            if (choose == 0)
            {
                Console.WriteLine("Your hand:");
                for (i = 0; i < last(playerhands, 0)+1; i++)
                {
                    Console.WriteLine("{0}", deck[playerhands[0, i]].name);
                }
            }
            else if (choose == 1)
            {
                Console.WriteLine("Items in shop:");
                for (i = 0; i < last(shop)+1; i++)
                {
                    Console.WriteLine("{0}", deck[shop[i]].name);
                }
            }
            else
            {
                Console.WriteLine("Player status:");
                for (i = 0; i < 4; i++)
                {
                    Console.WriteLine("Player {0}", i);
                    Console.WriteLine("Quantity:{0}", player[i].number);
                    Console.WriteLine("DNA:{0}", player[i].DNA);
                    Console.WriteLine("Equip:{0}", player[i].equi);
                    Console.WriteLine("Power:{0}", player[i].power);
                    Console.WriteLine("Speed:{0}", player[i].speed);
                }
            }
        }


        static int last(int[] obj)
        {
            int i;
            for (i = 0; i < obj.Length; i++)
            {
                if (obj[i] == 255) break;
            }
            return i - 1;
        }
        static int last(int[,] obj, int pl)
        {
            int i;
            for (i = 0; i < obj.Length; i++)
            {
                if (obj[pl, i] == 255) break;
            }
            return i - 1;
        }

    }
}
