using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 火妖卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为3点，攻击力2，生命值4
    // 卡牌效果：在你施放一个法术后，造成2点伤害，随机分配到所有敌人身上。
    class Sim_BRM_002 : SimTemplate //* 火妖 Flamewaker
    // After you cast a spell, deal 2 damage randomly split among all enemies.
    // 在你施放一个法术后，造成2点伤害，随机分配到所有敌人身上。 
    {
        // 重写卡牌即将被使用触发方法，这是火妖卡牌效果的核心实现
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool ownplay, Minion m)
        {
            // 检查触发火妖效果的条件：
            // 1. 使用卡牌的玩家与火妖属于同一方
            // 2. 使用的卡牌是法术类型
            if (m.own == ownplay && hc.card.type == CardDB.cardtype.SPELL)
            {
                // 创建所有敌人的列表（包括敌方英雄和敌方所有随从）
                List<Minion> allEnemies = new List<Minion>();

                if (ownplay)
                {
                    // 如果是己方使用法术，则敌人是敌方英雄和敌方随从
                    allEnemies.Add(p.enemyHero);
                    allEnemies.AddRange(p.enemyMinions);
                }
                else
                {
                    // 如果是敌方使用法术，则敌人是己方英雄和己方随从
                    allEnemies.Add(p.ownHero);
                    allEnemies.AddRange(p.ownMinions);
                }

                // 创建随机数生成器
                Random rand = new Random();

                // 造成2点伤害，随机分配到敌人身上
                // 第一次分配1点伤害
                if (allEnemies.Count > 0)
                {
                    Minion firstTarget = allEnemies[rand.Next(allEnemies.Count)];
                    p.minionGetDamageOrHeal(firstTarget, 1);
                }

                // 第二次分配1点伤害
                if (allEnemies.Count > 0)
                {
                    Minion secondTarget = allEnemies[rand.Next(allEnemies.Count)];
                    p.minionGetDamageOrHeal(secondTarget, 1);
                }
            }
        }
    }
}