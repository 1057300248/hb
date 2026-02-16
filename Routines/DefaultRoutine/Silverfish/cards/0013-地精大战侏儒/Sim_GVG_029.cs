using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 先祖召唤卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为4点
    // 卡牌效果：每个玩家从手牌中随机将一个随从置入战场。
    class Sim_GVG_029 : SimTemplate //* 先祖召唤
    {
        // 重写卡牌打出时的效果方法，这是先祖召唤卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 处理己方手牌中的随从
            List<Handmanager.Handcard> ownMinions = new List<Handmanager.Handcard>();
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.type == CardDB.cardtype.MOB)
                {
                    ownMinions.Add(hc);
                }
            }

            // 如果己方有随从，则随机选择一个召唤到战场
            if (ownMinions.Count > 0)
            {
                Handmanager.Handcard randomOwnMinion = ownMinions[rand.Next(ownMinions.Count)];
                int pos = p.ownMinions.Count;
                p.callKid(randomOwnMinion.card, pos, true); // 召唤该随从到己方战场
                p.removeCard(randomOwnMinion); // 从手牌中移除该随从
            }

            // 处理敌方手牌中的随从
            // 注意：在AI模拟中，敌方手牌通常是未知的，但为了完整实现效果，我们假设可以访问
            List<Handmanager.Handcard> enemyMinions = new List<Handmanager.Handcard>();
            foreach (Handmanager.Handcard hc in p.enemyHand)
            {
                if (hc.card.type == CardDB.cardtype.MOB)
                {
                    enemyMinions.Add(hc);
                }
            }

            // 如果敌方有随从，则随机选择一个召唤到战场
            if (enemyMinions.Count > 0)
            {
                Handmanager.Handcard randomEnemyMinion = enemyMinions[rand.Next(enemyMinions.Count)];
                int pos = p.enemyMinions.Count;
                p.callKid(randomEnemyMinion.card, pos, false); // 召唤该随从到敌方战场
                p.RemoveFromEnemyHand(randomEnemyMinion); // 从敌方手牌中移除该随从
            }
        }
    }
}