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

        public void use(ref Player player, int deckNumber, AI ai, int currentPlayer, ref Deck shopUsed)
        {
            player.equi[player.getLastEquispace()] = deckNumber;
            player.hunger -= this.cost;
        }
    }
    class World
    {
        public int meatdebuff, vegdebuff, threedebuff, twodebuff, onedebuff, grassdebuff, buff;
        public bool sp;
        public string name, info;
        public World(int a, int b, int c, int d, int e, int f, int g, bool h, string m, string n)
        {
            this.meatdebuff = a;
            this.vegdebuff = b;
            this.threedebuff = c;
            this.twodebuff = d;
            this.onedebuff = e;
            this.grassdebuff = f;
            this.buff = g;
            this.sp = h;
            this.name = m;
            this.info = n;
        }

    }
    class Player
    {
        public Player(int a, int b, int c, int d, int e)
        {
            this.DNA = a;
            this.number = b;
            this.hunger = c;
            this.power = d;
            this.speed = e;
            this.wanttoeat = 0;
            this.equi = new int[3] { 0, 0, 0 };
            this.deck = new int[30];
            this.canclimb = false;
            this.canfly = false;
            this.canswim = false;
            this.vege = false;
            this.meat = false;
            this.bug = false;
            this.isDead = false;
            this.defended = false;
        }
        public int DNA, number, hunger, power, speed, wanttoeat;
        public int[] equi, deck;
        public bool canclimb, canfly, canswim, vege, meat, bug, isDead, defended;
        //DNA = 玩家角色持有的DNA數量 number = 玩家角色的族群總數 hunger = 玩家角色的飽食度
        // info = 玩家資訊 equi = 玩家目前已裝備之裝備 speed = 玩家的速度值 power = 玩家的力量值
        public string info;
        public void changeNumber(int damage)
        {
            this.number += damage;
        }
        public void defense(bool AI, int atk)
        {
            switch (AI)
            {
                case true:

                    if (this.hunger == 1 || defended)///不防禦
                    {
                        Console.WriteLine("attack worked!");
                        this.changeNumber(-atk);
                    }
                    else ///防禦
                    {
                        Console.WriteLine("attack missed!");
                        this.hunger -= 1;
                        this.changeNumber(1 - atk);
                        this.defended = true;
                    }
                    break;
                case false:
                    if (defended)
                    {
                        Console.WriteLine("You're attacked!");
                        Console.WriteLine("You can't defend the attack! Attack worked!");
                        this.changeNumber(-atk);
                    }
                    else
                    {
                        Console.WriteLine("You're attacked!");
                        Console.WriteLine("Do you want to defend?/n0 for yes/n1 for no");
                        int choise = int.Parse(Console.ReadLine());
                        if (choise == 0)
                        {
                            this.defended = true;
                            Console.WriteLine("You didn't take any damage!");
                        }
                        else if (choise == 1)
                        {
                            Console.WriteLine("You didn't defend the attack! Attack worked!");
                            this.changeNumber(-atk);
                        }
                    }
                    break;
            }
            if (this.number <= 0 && !AI)
            {
                Console.WriteLine("You died");
                this.isDead = true;
            }
            else if (this.number <= 0)
            {
                Console.WriteLine("CPU died!");
                this.isDead = true;
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
            return i;
        }
        public bool NeedUnEquip()
        {
            if (getLastEquispace() < 2) return false;
            return true;
        }
        public void unequip(ref Deck shopUsed, int dropChoise)
        {
            shopUsed.deck[shopUsed.getLastspace()] = this.equi[dropChoise];
            this.equi[dropChoise] = 0;
            Array.Sort(this.equi);
            Array.Reverse(this.equi);
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
        public bool needfly, needclimb, needswim, meat, vege, bug;
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
    class AI
    {
        public bool GoShopping, GoAttacking, GoUsing;
        public int[] ShopAppeal, AtkAppeal, UseAppeal, choose;
        public int choise;
        public AI()
        {
            this.GoUsing = true;
            this.GoAttacking = true;
            this.GoShopping = true;
        }
        public void reset()
        {
            this.GoUsing = true;
            this.GoAttacking = true;
            this.GoShopping = true;
        }
        public int AIchoise(ref Player player)
        {
            if (this.GoShopping) return 1;
            if (this.GoAttacking && player.hunger >= 3) return 3;

            return 4;
        }
        public void Shopping(Card[] deck, Deck shop, Player[] player, int currentPlayer)
        {
            this.choose = new int[8];
            this.ShopAppeal = new int[8];
            bool AIreallyWantSomething = false;
            int j = 0;
            for (int i = 0; i < shop.getLastspace(); i++)
            {
                choose[i] = j;
                ShopAppeal[i] += deck[shop.deck[i]].power * 2;
                ShopAppeal[i] += deck[shop.deck[i]].speed * 3;
                ShopAppeal[i] += deck[shop.deck[i]].fly ? 2 : 0;
                ShopAppeal[i] += deck[shop.deck[i]].swim ? 1 : 0;
                ShopAppeal[i] += deck[shop.deck[i]].climb ? 2 : 0;
                ShopAppeal[i] += deck[shop.deck[i]].type == "atk" ? 2 : 0;
                ShopAppeal[i] += deck[shop.deck[i]].type == "def" ? 1 : 0;
                if (deck[shop.deck[choose[i]]].price > player[currentPlayer].DNA && ShopAppeal[i] >= 5) AIreallyWantSomething = true;
                else ShopAppeal[i] *= deck[shop.deck[choose[i]]].price > player[currentPlayer].DNA ? 0 : 1;
                j++;
            }
            Array.Sort(ShopAppeal, choose);
            if (AIreallyWantSomething) choise = 9;
            else if (ShopAppeal[shop.getLast()] == 0 | deck[shop.deck[choose[shop.getLast()]]].price > player[currentPlayer].DNA) choise = 9;
            else this.choise = choose[shop.getLast()];
        }
        public void Attacking(Player[] player, int currentPlayer)
        {
            this.choose = new int[4];
            this.AtkAppeal = new int[4];
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                choose[i] = i;
                AtkAppeal[choose[i]] = player[choose[i]].number * 10 + player[choose[i]].hunger * 9;
                AtkAppeal[choose[i]] *= player[choose[i]].isDead ? 0 : 1;
                AtkAppeal[choose[i]] *= currentPlayer == i ? 0 : 1;
            }
            Array.Sort(AtkAppeal, choose);
            this.choise = choose[3];
            this.GoAttacking = false;
        }
        public void Using(Card[] deck, Player[] player, int currentPlayer)
        {
            this.UseAppeal = new int[player[currentPlayer].getLastspace()];
            this.choose = new int[player[currentPlayer].getLast()];
            bool AIreallyWannaDoSomething = false;
            for (int i = 0; i < player[currentPlayer].getLastspace(); i++)
            {
                UseAppeal[i] += deck[player[currentPlayer].deck[i]].power * 2;
                UseAppeal[i] += deck[player[currentPlayer].deck[i]].speed * 3;
                UseAppeal[i] += deck[player[currentPlayer].deck[i]].fly ? 2 : 0;
                UseAppeal[i] += deck[player[currentPlayer].deck[i]].swim ? 1 : 0;
                UseAppeal[i] += deck[player[currentPlayer].deck[i]].climb ? 2 : 0;
                UseAppeal[i] += deck[player[currentPlayer].deck[i]].type == "atk" ? 3 : 0;
                UseAppeal[i] *= deck[player[currentPlayer].deck[i]].type == "def" ? 0 : 1;
                if (deck[choose[i]].cost > player[currentPlayer].hunger && UseAppeal[i] >= 5) AIreallyWannaDoSomething = true;
                else UseAppeal[i] *= deck[choose[i]].cost > player[currentPlayer].hunger ? 0 : 1;
            }
            Array.Sort(UseAppeal, choose);
            if (AIreallyWannaDoSomething) choise = 9;
            else if (UseAppeal[player[currentPlayer].getLast()] == 0) choise = 9;
            else this.choise = choose[player[currentPlayer].getLast()];
        }
        public int unequip()
        {
            Random ramdom = new Random();
            return ramdom.Next(0, 2);
        }
    }
    class Program
    {
        static void Main()
        {

            DirectoryInfo di = new DirectoryInfo(@"../../../../cardinfo/card");
            int i = 0;
            foreach (var fi in di.GetFiles())
            {
                i += 1;
            }
            //Console.WriteLine(i);
            int allcardquant = i;
            string[] filelist = new string[i];
            i = 0;
            foreach (var fi in di.GetFiles())
            {
                filelist[i] = fi.Name;
                i++;
            }

            Card[] card = new Card[allcardquant];
            for (i = 0; i < allcardquant; ++i)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"../../../../cardinfo/card/" + filelist[i]);

                /*int allcardquant = int.Parse(file.ReadLine());*/
                /*for (i = 0; i < allcardquant; ++i)*/
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
            }



            DirectoryInfo dy = new DirectoryInfo(@"../../../../cardinfo/food");
            int y = 0;
            foreach (var fi in dy.GetFiles())
            {
                y += 1;
            }
            //Console.WriteLine(y);
            int foodquant = y;
            string[] foodlist = new string[y];
            y = 0;
            foreach (var fi in dy.GetFiles())
            {
                foodlist[y] = fi.Name;
                y++;
            }

            /*int i;
            int foodquant = int.Parse(file.ReadLine());*/
            /*Food[] food = new Food[foodquant];*/
            Food[] food = new Food[foodquant];
            for (i = 0; i < foodquant; ++i)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"../../../../cardinfo/food/" + foodlist[i]);
                food[i] = new Food("0", 0, true, true, true, 0, 0);
                food[i].foodname = file.ReadLine();
                bool needquant = bool.Parse(file.ReadLine());
                food[i].needfly = bool.Parse(file.ReadLine());
                food[i].needclimb = bool.Parse(file.ReadLine());
                food[i].needswim = bool.Parse(file.ReadLine());
                food[i].meat = bool.Parse(file.ReadLine());
                food[i].vege = bool.Parse(file.ReadLine());
                food[i].needspeed = int.Parse(file.ReadLine());
                food[i].needpower = int.Parse(file.ReadLine());

            }

            DirectoryInfo ei = new DirectoryInfo(@"../../../../cardinfo/world");
            int x = 0;
            foreach (var fi in ei.GetFiles())
            {
                x += 1;
            }
            //Console.WriteLine(x);
            int worldquant = x;
            string[] worldlist = new string[x];
            x = 0;
            foreach (var fi in ei.GetFiles())
            {
                worldlist[x] = fi.Name;
                x++;
            }

            /*int i;
            int foodquant = int.Parse(file.ReadLine());*/
            /*Food[] food = new Food[foodquant];*/
            World[] world = new World[worldquant];
            for (i = 0; i < worldquant; ++i)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"../../../../cardinfo/world/" + worldlist[i]);
                    world[i] = new World(0, 0, 0, 0, 0, 0, 0, false, "0", "0");
                    world[i].vegdebuff = int.Parse(file.ReadLine());
                    world[i].threedebuff = int.Parse(file.ReadLine());
                    world[i].twodebuff = int.Parse(file.ReadLine());
                    world[i].onedebuff = int.Parse(file.ReadLine());
                    world[i].grassdebuff = int.Parse(file.ReadLine());
                    world[i].buff = int.Parse(file.ReadLine());
                    world[i].sp = bool.Parse(file.ReadLine());
                    world[i].name = file.ReadLine();
                    world[i].info = file.ReadLine();



            }

            int card_max = 0;
            for (i = 0; i < allcardquant; i++)
            {
                card_max += card[i].quant;
            }
            Card[] deck = new Card[card_max + 1];
            int card_print = 1;
            Deck starting = new Deck(card_max + 1);
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
                player[i] = new Player(4, 3, 4, 3, 3);///number DNA hunger power speed
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
            for (i = 0; i < shopdeck.deck.Length - 29; i++)
            {
                shopdeck.deck[i] = starting.deck[deal];
                deal++;
            }
            Deck shopUsed = new Deck(starting.deck.Length);

            ///建立AI
            AI[] ai = new AI[4];
            for (i = 0; i < 4; i++)
            {
                ai[i] = new AI();
            }

            int[] currentfood = { 0, 1, 2 };
            int[] fooddebuff = { 0, 0, 0, 0 };
            int currentworld = 0;
            int[] eatscore = new int[3];
            ///Game Start
            do
            {

                int currentPlayer = 0;
                ///draw food
                Random random = new Random();
                ///world effect
                currentworld = random.Next(9);
                Console.WriteLine("World card : {0}", world[currentworld].name);
                Console.WriteLine(world[currentworld].info);
                for (i = 0; i < 3; i++)
                {
                    int totaldebuff = 0;
                    if (i == 0) totaldebuff = world[currentworld].threedebuff;
                    if (i == 1) totaldebuff = world[currentworld].twodebuff;
                    if (i == 2) totaldebuff = world[currentworld].onedebuff;
                    if (food[currentfood[i]].meat) totaldebuff += world[currentworld].meatdebuff;
                    if (food[currentfood[i]].vege) totaldebuff += world[currentworld].vegdebuff;
                    fooddebuff[i] += totaldebuff + world[currentworld].buff;
                }
                Console.WriteLine("Current food:");
                for (i = 0; i < 3; i++)
                {
                    currentfood[i] = random.Next(foodquant);
                    Console.WriteLine("hunger+{0} {1}", 3 - i - fooddebuff[i], food[currentfood[i]].foodname);
                }

                //////////////////////////////
                while (currentPlayer < 4)
                {
                    if (player[currentPlayer].number <= 0)
                    {
                        if (notAI(currentPlayer))
                        {
                            Console.WriteLine("You died \nNext Player's turn");
                            currentPlayer += 1;
                            continue;
                        }
                        else
                        {
                            currentPlayer += 1;
                            continue;
                        }
                    }
                    int act_choose = 0;
                    bool basicAttacked = false;
                    player[currentPlayer].defended = false;
                    ai[currentPlayer].reset();
                    while (act_choose != 4)
                    {
                        if (currentPlayer == 0)
                        {
                            ///print player hand
                            Console.WriteLine("Your hand:");
                            for (i = 0; i < player[currentPlayer].getLastspace(); i++)
                            {
                                Console.WriteLine("{0}", deck[player[currentPlayer].deck[i]].name);
                            }
                            Console.WriteLine("Choose your act:\n0 for use hand card\n1 for shopping\n2 for checking info(s)");
                            if (!basicAttacked)
                                Console.WriteLine("3 for basic attack\n4 for end turn");
                            else Console.WriteLine("3 for end turn");
                            act_choose = int.Parse(Console.ReadLine());
                            if (basicAttacked && act_choose == 3) act_choose++;
                        }
                        else
                        {
                            if (shop.getLast() == -1) ai[currentPlayer].GoShopping = false;
                            act_choose = ai[currentPlayer].AIchoise(ref player[currentPlayer]);
                        }
                        if (notAI(currentPlayer)) Console.Clear();
                        else Console.Read();
                        switch (act_choose)
                        {
                            case 0:
                                use(ref deck, ref player, ref shopUsed, currentPlayer, ai[currentPlayer]);
                                break;
                            case 1:
                                if (shop.getLast() == -1)///shop沒牌啦
                                {
                                    Console.WriteLine("There's nothing in the shop");
                                }
                                else
                                {
                                    buy(ref deck, ref shopdeck, ref shop, ref player, ref shopUsed, currentPlayer, ref ai[currentPlayer]);
                                }
                                break;
                            case 2:
                                show(ref deck, ref shopdeck, ref shop, ref player, currentPlayer);
                                break;
                            case 3:
                                attack(ref player, currentPlayer, 1, 1, ai[currentPlayer]);
                                basicAttacked = true;
                                break;
                            default:
                                break;
                        }
                    }
                    if (currentPlayer == 0)
                    {
                        Console.WriteLine("What do you want to eat?");
                        for (i = 0; i < currentfood.Length; i++)
                        {
                            Console.WriteLine("{0} for {1}", i, food[currentfood[i]].foodname);
                        }
                        player[currentPlayer].wanttoeat = int.Parse(Console.ReadLine());
                        Console.WriteLine("You ended your turn.");
                    }
                    else
                    {
                        int[] eatChoise = { 0, 1, 2 };
                        for (i = 0; i < 3; i++)
                        {
                            eatscore[i] = ((food[currentfood[i]].needclimb && !(player[currentPlayer].canclimb)) && (food[currentfood[i]].needfly && !(player[currentPlayer].canfly)) && (food[currentfood[i]].needswim && !(player[currentPlayer].canswim)) && (food[currentfood[i]].meat && !(player[currentPlayer].meat)) ? 0 : 1) * (player[currentPlayer].speed * 30 + player[currentPlayer].power);
                        }
                        Array.Sort(eatscore, eatChoise);
                        if (eatscore[2] != 0) player[currentPlayer].wanttoeat = eatChoise[2];
                        else player[currentPlayer].wanttoeat = 3;
                        Console.WriteLine("CPU{0} choose to eat {1}", currentPlayer, food[currentfood[player[currentPlayer].wanttoeat]].foodname);
                    }
                    currentPlayer += 1;
                }
                /////////////////////

                eat(ref currentfood, food, ref player, ref deck, fooddebuff);
                for (i = 0; i < 4; i++)
                {
                    player[i].DNA += player[i].number;
                }
                Console.WriteLine("it's your turn again");
            }
            while (!leftOne(player));
            for (i = 0; i < 4; i++)
            {
                if (player[i].isDead && i == 0) Console.WriteLine("You win!");
                else if (!player[i].isDead) Console.WriteLine("CPU{0} wins");
            }
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

        static void placeZero(ref int[] starting)
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

        static void use(ref Card[] deck, ref Player[] player, ref Deck shopUsed, int currentPlayer, AI ai)
        {
            int choose = 0;
            if (notAI(currentPlayer))
            {
                Console.WriteLine("Which card do you want to use:");
                int i;
                for (i = 0; i < player[currentPlayer].getLastspace(); i++)
                {
                    Console.WriteLine("{0} for {1}", i, deck[player[currentPlayer].deck[i]].name);
                }
                choose = int.Parse(Console.ReadLine());
            }
            else
            {
                choose = ai.choise;
            }

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
                attack(ref player, currentPlayer, deck[player[currentPlayer].deck[choose]].atk, deck[player[currentPlayer].deck[choose]].cost, ai);
                shopUsed.deck[shopUsed.getLastspace()] = player[currentPlayer].deck[choose]; ///棄牌
            }
            else
            {
                ///裝備
                if (player[currentPlayer].NeedUnEquip())
                {
                    int dropChoise;
                    if (notAI(currentPlayer))
                    {
                        Console.WriteLine("your equipment place is full. please drop one.");
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine("{0} for {1}", i, deck[player[currentPlayer].equi[i]].name);
                        }
                        dropChoise = int.Parse(Console.ReadLine());
                        Console.WriteLine("you dropped {0}", deck[player[currentPlayer].equi[dropChoise]].name);
                    }
                    else
                    {
                        Console.WriteLine("CPU{0}'s equipment place is full.", currentPlayer);
                        dropChoise = ai.unequip();
                        Console.WriteLine("CPU dropped {0}", deck[player[currentPlayer].equi[dropChoise]].name);
                    }
                    player[currentPlayer].unequip(ref shopUsed, dropChoise);
                }
                if(notAI(currentPlayer))Console.WriteLine("your equiped {0}", deck[player[currentPlayer].deck[choose]].name);
                else Console.WriteLine("CPU equiped {0}", deck[player[currentPlayer].deck[choose]].name);
                deck[player[currentPlayer].deck[choose]].use(ref player[currentPlayer], player[currentPlayer].deck[choose], ai, currentPlayer, ref shopUsed);
            }

            player[currentPlayer].deck[choose] = player[currentPlayer].deck[player[currentPlayer].getLast()];
            player[currentPlayer].deck[player[currentPlayer].getLast()] = 0; ///將最後一張手牌放回空格
        }

        static void eat(ref int[] currentfood, Food[] food, ref Player[] player, ref Card[] card, int[] fooddebuff)
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
                if (player[tempplayer[3]].wanttoeat != i)
                {
                    player[tempplayer[3]].hunger += 3 - i - fooddebuff[i];
                    Console.WriteLine("player "+tempplayer[3]+"has eaten" +food[currentfood[i]].foodname);
                    if (eatscore[2] == eatscore[3]) player[tempplayer[2]].hunger += 3 - i - fooddebuff[i];
                    if (eatscore[1] == eatscore[2]) player[tempplayer[1]].hunger += 3 - i - fooddebuff[i];
                    if (eatscore[0] == eatscore[1]) player[tempplayer[0]].hunger += 3 - i - fooddebuff[i];
                }
            }
        }

        static void buy(ref Card[] deck, ref Deck shopdeck, ref Deck shop, ref Player[] player, ref Deck shopUsed, int currentPlayer, ref AI ai)
        {
            int choose = 0;
            int i;
            if (notAI(currentPlayer))
            {
                Console.WriteLine("What would you like to buy:");
                for (i = 0; i < shop.getLastspace(); i++)
                {
                    Console.WriteLine("{0} for {1}", i, deck[shop.deck[i]].name);
                }
                Console.WriteLine("9 for leave");
                choose = int.Parse(Console.ReadLine());
            }
            else ///AI
            {
                ai.Shopping(deck, shop, player, currentPlayer);
                choose = ai.choise;
            }  ///AI
            if (choose == 9)
            {
                if (!notAI(currentPlayer)) { Console.WriteLine("CPU{0} leaved the shop. ", currentPlayer); ai.GoShopping = false; }
                return;
            }
            if (deck[shop.deck[choose]].cost <= player[currentPlayer].DNA)
            {
                if (notAI(currentPlayer)) Console.WriteLine("It costs you {0} DNA!", deck[shop.deck[choose]].cost);
                else Console.WriteLine("CPU{0} choose to buy {1} and that cost it {2} DNA", currentPlayer, deck[shop.deck[choose]].name, deck[shop.deck[choose]].cost);
                player[currentPlayer].DNA -= deck[shop.deck[choose]].cost;


                player[currentPlayer].deck[player[currentPlayer].getLastspace()] = shop.deck[choose];

                if (shopdeck.getLast() != -1) ///商店deck還有牌 從shopdeck補充牌進shop
                {
                    shop.deck[choose] = 0;
                    shop.deck[choose] = shopdeck.deck[shopdeck.getLast()];
                    shopdeck.deck[shopdeck.getLast()] = 0;
                }
                else if (shopUsed.getLast() != -1)///shopdeck用完了 棄牌庫有牌
                {
                    recycle(ref shopdeck, ref shopUsed);///重建shop.deck
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

        static void show(ref Card[] deck, ref Deck shopdeck, ref Deck shop, ref Player[] player, int currentPlayer)
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
                for (i = 0; i < shop.getLast() + 1; i++)
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
                    for (int j = 0; j < player[currentPlayer].getLastEquispace(); j++)
                    {
                        Console.WriteLine("Equip:{0}", deck[player[i].equi[j]].name);
                    }
                    Console.WriteLine("Power:{0}", player[i].power);
                    Console.WriteLine("Speed:{0}", player[i].speed);
                    Console.ReadLine();
                }
            }
        }

        static void attack(ref Player[] player, int currentPlayer, int atk, int cost, AI ai)
        {
            int choise;
            if (notAI(currentPlayer))
            {
                Console.WriteLine("Which player would you want to attack");
                int j = 0;
                int deathcount = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (currentPlayer != i && !player[i].isDead)
                    {
                        Console.WriteLine("{1} for player{0}", i, j);///印出非自己的腳色 並給予編號
                        j++;
                    }
                    if (player[i].isDead)
                    {
                        deathcount++;
                    }
                }
                Console.WriteLine("4 for cancel");
                choise = int.Parse(Console.ReadLine());
                if (currentPlayer <= choise) choise += 1 + deathcount;
            }
            else
            {
                ai.Attacking(player, currentPlayer);
                choise = ai.choise;
            }
            if (choise != 4)
            {
                if (!notAI(currentPlayer))
                {
                    Console.WriteLine("CPU{0} is attacked by CPU{1}", choise, currentPlayer);
                }
                player[choise].defense(choise != 0, atk);///進行防禦並判定是否叫出AI 
            }

        }

        static void recycle(ref Deck shopdeck, ref Deck shopUsed)
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

        static bool notAI(int currentPlayer)
        {
            return currentPlayer == 0;
        }

        static bool leftOne(Player[] player)
        {
            int deathCount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (player[0].isDead) deathCount++;
            }
            if (deathCount == 3) return true;
            return false;
        }
    }
}