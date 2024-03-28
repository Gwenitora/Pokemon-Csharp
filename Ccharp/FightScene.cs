using System.ComponentModel.DataAnnotations;
using System.Net.Quic;

public class FightScene
{
    List<Chakimon> playerTeam = new List<Chakimon>();
    List<Attack> allAttacks = new List<Attack>();
    List<TypeTable> types = new List<TypeTable>();

    Chakimon chakimonAlly;
    Chakimon chakimonEnnemy;

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
            PlayerTurn(playerTurn);
            Console.WriteLine(chakimonAlly.pv);
            playerTurn = !playerTurn;
        }
        //Change de scene
    }

    public void PlayerTurn(bool isOwnTurn)
    {
        bool isPlayerTurnFinish = false;
        while (isPlayerTurnFinish == false)
        {
            if(isOwnTurn)
            {
                Attack attack = ChooseAttack(chakimonAlly);
                if(attack != null)
                {
                    Attack(chakimonAlly, chakimonEnnemy, attack);
                    if (chakimonEnnemy.pv == 0)
                    {
                        isFinish = true;
                        //xpppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp
                        
                    }
                    isPlayerTurnFinish = true;
                }
            }
            else
            {
                // IA
            }
        }
    }

    public Attack ChooseAttack(Chakimon chakimon)
    {
        while (true)
        {
            foreach (Attack attack in allAttacks)
            { 
                if (attack.name == chakimon.Attacks.FirstOrDefault().Key)
                {
                    return attack;                    
                }
            }
        }
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
                float damage = (float)(((chakimonThatAttacks.Level * 0.4f + 2) * chakimonThatAttacks.Stats.Attaque * attack.damage) / chakimonWhichIsAttacked.Stats.Defense * 50.0f) + 2;

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
                break;
            }
        }
    }
}

