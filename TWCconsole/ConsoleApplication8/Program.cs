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
 
        public void use(ref Player player,int deckNumber)
        {
            if (player.getLastEquispace() <= 2)
            {
                player.equi[player.getLastEquispace()] = deckNumber;
            }
            player.hunger -= this.cost;
        }
   }
    class World
    {
        public int meatdebuff , vegdebuff, threedebuff,twodebuff,onedebuff,grassdebuff,buff;
        public bool sp;
        public string name, info;
        public World()
        {
            this.meatdebuff = 0;
            this.vegdebuff = 0;
            this.threedebuff = 0;
            this.twodebuff = 0;
            this.onedebuff = 0;
            this.grassdebuff = 0;
            this.buff = 0;
            this.sp = false;
            this.name = "";
            this.info = "";
        }
    }
    class Player
      {
          public Player(int a, int b, int c, int d, int e)
          {
              int DNA = a;
              int number = b;
              int hunger = c;
              int power = d;
              int speed = e;
              int wanttoeat = 0;
              this.equi = new int[3]{0,0,0};
              this.deck = new int[30];
              bool canclimb = false;
              bool canfly = false;
              bool canswim = false;
              bool vege = false;
              bool meat = false;
              bool bug = false;
          }
          public int DNA, number, hunger, power, speed, wanttoeat;
          public int[] equi, deck;
          public bool canclimb, canfly, canswim,vege,meat,bug;
          //DNA = 玩家角色持有的DNA數量 number = 玩家角色的族群總數 hunger = 玩家角色的飽食度
          // info = 玩家資訊 equi = 玩家目前已裝備之裝備 speed = 玩家的速度值 power = 玩家的力量值
          public string info;
          public void changeNumber(int damage)
          {
              this.number += damage;
          }
          public void defense(bool AI,int atk)
          {
              switch(AI)
              {
                  case true:

                      if (this.hunger == 1)///不防禦
                      { 
                          Console.WriteLine("Your attack worked!");
                          this.changeNumber(-atk);
                      }
                      else ///防禦
                      {
                          Console.WriteLine("Your attack has missed!");
                          this.hunger -= 1;
                          this.changeNumber(1-atk);
                      }
                      break;
                  case false:

                      break;
              }
          }
          public int getLast()
          {
              int i;
              for (i = 0; i < this.deck.Length; i++)
              {
                  if (this.deck[i] == 0) break;
              }
              return i - 1;
          }
          public int getLastspace()
          {
              return this.getLast() + 1;
          }
          public int getLastEquispace()
          {
              int i;
              for (i = 0; i < this.equi.Length; i++)
              {
                  if (this.equi[i] == 0) break;
              }
              return i ;
          }
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
            this.meat = false;
            this.vege = false;
            this.bug = false;
        }
        public int needquant, needpower, needspeed;
        // needquant = 進食此食物所需擁有之族群數量 needpower = 進食此食物所需擁有之力量 needspeed = 進食此食物所需擁有之速度
        public bool needfly, needclimb, needswim,meat,vege,bug;
        // needfly = 進食此食物是否需要能夠飛行 needclimb = 進食此食物是否需要能夠攀爬 needswim = 進食此食物是否需要能夠游泳
        public string foodname;
        // 該種食物之名稱
    }
    class Deck
    {
        public int[] deck;
        public Deck(int quant)
        {
            this.deck = new int[quant];
        }
        public int getLast()
        {
            int i;
            for (i = 0; i < this.deck.Length; i++)
            {
                if (this.deck[i] == 0) break;
            }
            return i - 1;
        
        }
        public int getLastspace()
        {
            return this.getLast() + 1;
        }

    }
    
    class Program
    {
        static void Main()
        {
            
                System.IO.StreamReader file = new System.IO.StreamReader("../../card/card.txt");
                int i;
                int allcardquant = int.Parse(file.ReadLine());
                Card[] card = new Card[allcardquant];
                for (i = 0; i < allcardquant; ++i)
                {
                    card[i] = new Card("0", 0, 0, " 0", " 0", 0, 0, 0, false, false, false, 0, 0);
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
            
                ///讀食物
                System.IO.StreamReader file2 = new System.IO.StreamReader("../../food/food.txt");
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
                
                ///world
                World[] world = new World[9];
                for (i = 0; i < 9; i++)
                {
                    world[i] = new World();
                }

                int card_max = 0;
                for (i = 0; i < allcardquant; i++)
                {
                    card_max += card[i].quant;
                }
                Card[] deck = new Card[card_max+1];
                int card_print = 1;
                Deck starting = new Deck(card_max+1);
                for (i = 0; i <= card_max; i++)
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
                        starting.deck[card_print] = card_print;
                        card_print++;
                    }
                }
               //角色初始化
                Player[] player = new Player[4];
                for (i = 0; i < player.Length; i++)
                {
                    player[i] = new Player(4,3,4,3,3);///number DNA hunger power speed
                }
                //建立桌面
                shuffle(ref starting.deck);
                placeZero(ref starting.deck);
                shuffle(ref food);
                int deal = 1;//從startingdeck依序發牌給playerhads,shop,shopdeck
                for (i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        player[i].deck[j] = starting.deck[deal];
                        deal++;
                    }
                }
                Deck shop = new Deck(8);
                for (i = 0; i < shop.deck.Length; i++)
                {
                    shop.deck[i] = starting.deck[deal];
                    deal++;
                }
                Deck shopdeck = new Deck(starting.deck.Length);
                for (i = 0; i < shopdeck.deck.Length-29; i++)
                {
                    shopdeck.deck[i] = starting.deck[deal];
                    deal++;
                }
                Deck shopUsed = new Deck(starting.deck.Length);
                
                int[] currentfood = { 0, 1, 2 };
                int[] fooddebuff = {0,0,0,0 };
                int currentworld = 0;
                
                int currentPlayer = 0;

                ///Game Start
                do
                {
                    ///print player hand
                    Console.WriteLine("Your hand:");
                    for (i = 0; i < player[currentPlayer].getLastspace(); i++)
                    {
                        Console.WriteLine("{0}", deck[player[currentPlayer].deck[i]].name);
                    }
                    int act_choose = 0;

                    ///draw food
                    Random random = new Random();
                    Console.WriteLine("Current food:");
                    for(i=0;i<3;i++)
                    {
                        currentfood[i] = random.Next(foodquant);
                        Console.WriteLine("hunger+{0} {1}",3-i,food[currentfood[i]].foodname);
                    }
                    ///world effect
                    currentworld = random.Next(9);
                    Console.WriteLine("World card : {0}",world[currentworld].name);
                    Console.WriteLine(world[currentworld].info);
                    for(i = 0;i < 3 ; i++)
                    {
                        int totaldebuff = 0;
                        if (i == 0) totaldebuff = world[currentworld].threedebuff;
                        if (i == 1) totaldebuff = world[currentworld].twodebuff;
                        if (i == 2) totaldebuff = world[currentworld].onedebuff;
                        if (food[currentfood[i]].meat) totaldebuff += world[currentworld].meatdebuff;
                        if (food[currentfood[i]].vege) totaldebuff += world[currentworld].vegdebuff;
                        fooddebuff[i] += totaldebuff + world[currentworld].buff;
                    }


                    bool basicAttacked = false;
                    while (act_choose != 4)
                    {
                        Console.WriteLine("Choose your act:\n0 for use hand card\n1 for shopping\n2 for checking info(s)");
                        if(!basicAttacked)
                        Console.WriteLine("3 for basic attack\n4 for end turn");
                        else Console.WriteLine("3 for end turn");
                        act_choose = int.Parse(Console.ReadLine());
                        if (basicAttacked && act_choose == 3) act_choose++;
                        switch (act_choose)
                        {
                            case 0:
                                use(ref deck,ref player,ref shopUsed,currentPlayer);
                                break;
                            case 1:
                                if (shop.getLast() == -1)///shop沒牌啦
                                {
                                    Console.WriteLine("There's nothing in the shop");
                                }
                                else
                                {
                                    buy(ref deck, ref shopdeck, ref shop,ref player,ref shopUsed ,currentPlayer);
                                }
                                break;
                            case 2:
                                show(ref deck, ref shopdeck, ref shop, ref player,currentPlayer);
                                break;
                            case 3:
                                basic_attack(ref player,currentPlayer);
                                basicAttacked = true;
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("What do you want to eat?");
                    for (i = 0; i<currentfood.Length ; i++)
                    {
                        Console.WriteLine("{0} for {1}",i,food[currentfood[i]]);
                    }
                    player[currentPlayer].wanttoeat = int.Parse(Console.ReadLine());


                    Console.WriteLine("You ended your turn.");
                    Console.WriteLine("CPU1 did nothing");
                    Console.WriteLine("CPU2 did nothing");
                    Console.WriteLine("CPU3 did nothing");
                    eat(ref currentfood,food,ref player,ref card,fooddebuff);
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

        static void placeZero(ref int[] starting )
        {
            for (int i = 0; i < starting.Length; i++)
            {
                if (starting[i] == 0)
                {
                    starting[i] = starting[0];
                    starting[0] = 0;
                }
            }
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






        static void use(ref Card[] deck, ref Player[] player,ref Deck shopUsed, int currentPlayer)
        {
            Console.WriteLine("Which card do you want to use:");
            int i;
            for (i = 0; i < player[currentPlayer].getLastspace(); i++)
            {
                Console.WriteLine("{0} for {1}", i, deck[player[currentPlayer].deck[i]].name);
            }
            int choose = int.Parse(Console.ReadLine());

            if (player[currentPlayer].hunger > deck[player[currentPlayer].deck[choose]].cost)///is hunger enough?
            {
                player[currentPlayer].hunger -= deck[player[currentPlayer].deck[choose]].cost;///yes
            }
            else
            {
                Console.WriteLine("you don't have enough hunger");///no
                return;
            }

            if (deck[player[currentPlayer].deck[choose]].type == "def")
            {
                ///防禦
                Console.WriteLine("you can't use this card now");
            }
            else if (deck[player[currentPlayer].deck[choose]].type == "atk")
            {
                ///攻擊card_attack
                card_attack(ref player, currentPlayer, deck[player[currentPlayer].deck[choose]].atk, deck[player[currentPlayer].deck[choose]].cost);
                shopUsed.deck[shopUsed.getLastspace()] = player[currentPlayer].deck[choose]; ///棄牌
            }
            else 
            { 
                ///裝備
                deck[player[currentPlayer].deck[choose]].use(ref player[currentPlayer], player[currentPlayer].deck[choose]); 
            }

             player[currentPlayer].deck[choose] = player[currentPlayer].deck[player[currentPlayer].getLast()];
             player[currentPlayer].deck[player[currentPlayer].getLast()] = 0; ///將最後一張手牌放回空格
        }

        static void eat(ref int[] currentfood, Food[] food, ref Player[] player, ref Card[] card,int[] fooddebuff)
        {
            int i, j, k;
            int[] take = new int[4];
            int[] eatscore = new int[4];
            int[] tempplayer = new int[4];
            bool[] climballow = new bool[3];
            bool[] flyallow = new bool[3];
            bool[] swimallow = new bool[3];
            for (i = 0; i < player.Length; i++)
            {
                player[i].canclimb = card[player[i].equi[0]].climb || card[player[i].equi[1]].climb || card[player[i].equi[2]].climb;
                player[i].canfly = card[player[i].equi[0]].fly || card[player[i].equi[1]].fly || card[player[i].equi[2]].fly;
                player[i].canswim = card[player[i].equi[0]].swim || card[player[i].equi[1]].swim || card[player[i].equi[2]].swim;
            }
            for (i = 0; i < player.Length; i++)
            {
                if (player[i].wanttoeat == 3 && player[i].vege) player[i].hunger += 1 - fooddebuff[3];
            }
            for (i = 0; i < currentfood.Length; i++)
            {
                for (j = 0; j < tempplayer.Length; j++)
                {
                    tempplayer[j] = j;
                }
                for (j = 0; j < eatscore.Length; j++)
                {
                    eatscore[j] = (player[j].wanttoeat != i || (food[currentfood[i]].needclimb && !(player[j].canclimb)) && (food[currentfood[i]].needfly && !(player[j].canfly)) && (food[currentfood[i]].needswim && !(player[j].canswim)) ? 0 : 1) * (player[j].speed * 30 + player[j].power);
                }
                Array.Sort(eatscore, tempplayer);
                if (player[tempplayer[3]].wanttoeat != i) player[tempplayer[3]].hunger += 3 - i - fooddebuff[i];
            }
        }


        static void buy(ref Card[] deck, ref Deck shopdeck, ref Deck shop, ref Player[] player,ref Deck shopUsed,int currentPlayer)
        {
            int i;
            Console.WriteLine("What would you like to buy:");
            for (i = 0; i < shop.getLastspace(); i++)
            {
                Console.WriteLine("{0} for {1}", i, deck[shop.deck[i]].name);
            }
            int choose = int.Parse(Console.ReadLine());
            if (deck[shop.deck[choose]].cost <= player[0].DNA)
            {
                Console.WriteLine("It costs you {0} DNA!", deck[shop.deck[choose]].cost);
                player[0].DNA -= deck[shop.deck[choose]].cost;


               player[currentPlayer].deck[player[currentPlayer].getLastspace()] = shop.deck[choose];
   
                if (shopdeck.getLast() != -1) ///商店deck還有牌 從shopdeck補充牌進shop
                {
                    shop.deck[choose] = 0;
                    shop.deck[choose] = shopdeck.deck[shopdeck.getLast()];
                    shopdeck.deck[shopdeck.getLast()] = 0;
                }
                else if (shopUsed.getLast() != -1)///shopdeck用完了 棄牌庫有牌
                {
                    recycle(ref shopdeck,ref shopUsed);///重建shop.deck
                    shop.deck[choose] = 0;
                    shop.deck[choose] = shopdeck.deck[shopdeck.getLast()];
                    shopdeck.deck[shopdeck.getLast()] = 0;
                }
                else if (shop.getLast() != choose && choose != shopdeck.getLast())///棄牌庫沒牌
                {
                    shop.deck[choose] = shop.deck[shop.getLast()];
                    shop.deck[shop.getLast()] = 0;
                }
                else ///剩下一張
                {
                    shop.deck[shop.getLast()] = 0;
                }
            }
            else
            {
                Console.WriteLine("you don't have enough DNA!");
            }
        }


        static void show(ref Card[] deck, ref Deck shopdeck, ref Deck shop, ref Player[] player,int currentPlayer)
        {
            Console.WriteLine("What would you like to see?");
            Console.WriteLine("0 for your own hands\n1 for shop item list\n2 for player status");
            int choose = int.Parse(Console.ReadLine());
            int i;
            if (choose == 0)
            {
                Console.WriteLine("Your hand:");
                for (i = 0; i < player[currentPlayer].getLastspace(); i++)
                {
                    Console.WriteLine("{0}", deck[player[currentPlayer].deck[i]].name);
                }
            }
            else if (choose == 1)
            {
                Console.WriteLine("Items in shop:");
                for (i = 0; i < shop.getLast()+1; i++)
                {
                    Console.WriteLine("{0}", deck[shop.deck[i]].name);
                }
            }
            else
            {
                Console.WriteLine("Player status:");
                for (i = 0; i < 4; i++)
                {
                    Console.WriteLine("Player {0}", i);
                    Console.WriteLine("Quantity:{0}", player[i].number);
                    Console.WriteLine("Hunger:{0}", player[i].hunger);
                    Console.WriteLine("DNA:{0}", player[i].DNA);
                    Console.WriteLine("Equip:{0}", player[i].equi);
                    Console.WriteLine("Power:{0}", player[i].power);
                    Console.WriteLine("Speed:{0}", player[i].speed);
                    Console.ReadLine();
                }
            }
        }

        static void basic_attack(ref Player[] player,int currentPlayer)
          {
              Console.WriteLine("Which player would you want to attack");
              int j = 0;
              for (int i = 0; i < 4; i++)
              {
                  if (currentPlayer != i)
                  {
                      Console.WriteLine("{1} for player{0}", i, j);///印出非自己的腳色 並給予編號
                      j++;
                  }
              }
              int choise = int.Parse(Console.ReadLine());
              if (currentPlayer <= choise) choise += 1;
              player[currentPlayer].hunger -= 1;
              player[choise].defense(choise != 0,1);///進行防禦並判定是否叫出AI 

          }
        static void card_attack(ref Player[] player, int currentPlayer,int atk,int cost)
        {
            Console.WriteLine("Which player would you want to attack");
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                if (currentPlayer != i)
                {
                    Console.WriteLine("{1} for player{0}", i, j);///印出非自己的腳色 並給予編號
                    j++;
                }
            }
            int choise = int.Parse(Console.ReadLine());
            if (currentPlayer <= choise) choise += 1;
            player[choise].defense(choise != 0,atk);///進行防禦並判定是否叫出AI 

        }
        static void recycle(ref Deck shopdeck,ref Deck shopUsed)
        {
            Random rand = new Random();
            int[] temp = new int[shopUsed.deck.Length];
            for (int i = 0; i < shopUsed.deck.Length; i++)
            {
                if (i <= shopUsed.getLast()) temp[i] = rand.Next();
                else temp[i] = -1;
            }
            Array.Sort(temp, shopUsed.deck);
            Array.Reverse(shopUsed.deck);
            for (int i = 0; i < shopUsed.deck.Length; i++)
            {
                shopdeck.deck[i] = shopUsed.deck[i];
                shopUsed.deck[i] = 0;
            }
        }
    }
}
