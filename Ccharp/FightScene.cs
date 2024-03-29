using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

public class FightScene
{
    List<Chakimon> playerTeam = new List<Chakimon>();
    List<Attack> allAttacks = new List<Attack>();
    List<TypeTable> types = new List<TypeTable>();

    Chakimon chakimonAlly;
    Chakimon chakimonEnnemy;

    Ascii m_ascii = new Ascii();

    bool isFinish = false;

    public FightScene(Data data, Chakimon chakimon)
    {
        playerTeam = data.GetTeamList().team;
        allAttacks = data.GetAttackList();        
        types = data.GetTypeTableList();

        chakimonAlly = playerTeam.First();
        chakimonEnnemy = chakimon;
    }

    public void FightSceneGameLoop()
    {
        bool playerTurn = true;
        while (isFinish == false)
        {
            Console.SetCursorPosition(0, 0);
            var bg = m_ascii.LoadImg(Program.imgToLoad[2]);
            var res = m_ascii.Adding(bg, Program.imgToLoad[3], -25, -19, 50f, 50f);
            res = m_ascii.Adding(res, Program.imgToLoad[4], 25, -19, 50f, 50f);
            res = m_ascii.Adding(m_ascii.GetEmptyImage(), res, 0, 0, 100f, 100f);
            Console.Write(res);

            Console.WriteLine(playerTurn);
            //PlayerTurn(playerTurn);
            //Console.WriteLine(chakimonEnnemy.pv);
            playerTurn = !playerTurn;
        }
        //Change de scene
    }

    public void PlayerTurn(bool isOwnTurn)
    {
        bool isPlayerTurnFinish = false;
        while (isPlayerTurnFinish == false)
        {
            Console.Clear();
            Console.WriteLine($"Ennemy stats : \n name : {chakimonEnnemy.Name} lvl. {chakimonEnnemy.Level}\n pv : {chakimonEnnemy.pv}\n");
            Console.WriteLine($"Ally stats : \n name : {chakimonAlly.Name} lvl. {chakimonEnnemy.Level}\n pv : {chakimonAlly.pv}\n");


            if(isOwnTurn)
            {
                Attack attack = ChooseAttack(chakimonAlly);
                //Console.WriteLine($"Attack :{attack.name}");
                if(attack != null)
                {
                    Attack(chakimonAlly, chakimonEnnemy, attack);
                    isFinish = IsWin(chakimonEnnemy);
                    Console.Clear();
                    Console.WriteLine("You win");
                    chakimonAlly.Level += 1;
                    isPlayerTurnFinish = true;
                }
            }
            else
            {
                IA();
                isFinish = IsWin(chakimonAlly);
                Console.Clear();
                Console.WriteLine("You lose...");
                isPlayerTurnFinish = true;
                
            }
        }
    }

    public Attack ChooseAttack(Chakimon chakimon)
    {
        Console.Write($"your turn : \n 1. {chakimon.Attacks.Keys.ElementAt(0)}\n 2. {chakimon.Attacks.Keys.ElementAt(1)}\n 3. {chakimon.Attacks.Keys.ElementAt(2)}\n 4. {chakimon.Attacks.Keys.ElementAt(3)}\n\nYour choice : ");
        string temp = Console.ReadLine();
        int number_of_attack = Int32.Parse(temp);

        foreach (Attack attack in allAttacks)
        { 
            if (attack.name == chakimon.Attacks.Keys.ElementAt(number_of_attack - 1))
            {
                return attack;                    
            }
        }
        return allAttacks.FirstOrDefault();
    }
     
    public void Attack(Chakimon chakimonThatAttacks, Chakimon chakimonWhichIsAttacked, Attack attack)
    {
        type typeChakimonThatAttacks = chakimonThatAttacks.Type;
        type typeChakimonWhichIsAttacked = chakimonWhichIsAttacked.Type;
        foreach (TypeTable typeTable in types)
        {
            if (typeTable._type == attack._type)
            {
                weakness weakness = typeTable.weakness[typeChakimonWhichIsAttacked];
                float damage = (((chakimonThatAttacks.Level * 0.4f + 2) * chakimonThatAttacks.Stats.Attaque * attack.damage) / (chakimonWhichIsAttacked.Stats.Defense * 50.0f)) + 2;

                if (weakness == weakness.RESISTANT)
                {
                    damage *= 0.5f;
                    if (attack._type == typeChakimonThatAttacks)
                    {
                        damage *= 1.5f;
                    }
                    chakimonWhichIsAttacked.TakeDamage(damage);
                }
                else if (weakness == weakness.NEUTRAL)
                {
                    if (attack._type == typeChakimonThatAttacks)
                    {
                        damage *= 1.5f;
                    }
                    chakimonWhichIsAttacked.TakeDamage(damage);
                }
                else if (weakness == weakness.WEAK)
                {
                    damage *= 2;
                    if (attack._type == typeChakimonThatAttacks)
                    {
                        damage *= 1.5f;
                    }
                    chakimonWhichIsAttacked.TakeDamage(damage);
                }
                //Console.WriteLine(damage);
                break;
            }
        }
    }

    public void IA()
    {
        Random rnd = new Random();
        string attackChoose = chakimonEnnemy.Attacks.Keys.ElementAt(rnd.Next() % chakimonEnnemy.Attacks.Count);

        Console.Write($"Ennemy's turn : \n 1. {chakimonEnnemy.Attacks.Keys.ElementAt(0)}\n 2. {chakimonEnnemy.Attacks.Keys.ElementAt(1)}\n 3. {chakimonEnnemy.Attacks.Keys.ElementAt(2)}\n 4. {chakimonEnnemy.Attacks.Keys.ElementAt(3)}\n\nHe's choice : {rnd.Next() % chakimonEnnemy.Attacks.Count + 1}");
        Thread.Sleep(2000);

        foreach (Attack attack in allAttacks)
        {
            if (attack.name == chakimonEnnemy.Attacks.FirstOrDefault().Key)
            {

                Attack(chakimonEnnemy, chakimonAlly, attack);
            }
        }
    }

    public bool IsWin(Chakimon chakimonEnnemy)
    {
        if (chakimonEnnemy.pv == 0)
        {
            return true;
        }
        return false;
    }
}

