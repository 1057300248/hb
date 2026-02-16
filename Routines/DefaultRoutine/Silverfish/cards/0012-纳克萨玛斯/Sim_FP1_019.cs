using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 剧毒之种卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为4点
    // 卡牌效果：消灭所有随从，并召唤等量的2/2树人代替他们。
    class Sim_FP1_019 : SimTemplate //* 剧毒之种 Poison Seeds
    // Destroy all minions and summon 2/2 Treants to replace them.
    // 消灭所有随从，并召唤等量的2/2树人代替他们。 
    {
        // 定义要召唤的树人卡牌
        CardDB.Card treant = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_158t);

        // 重写卡牌使用效果方法，这是剧毒之种卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 记录当前双方随从数量
            int ownMinionCount = p.ownMinions.Count;
            int enemyMinionCount = p.enemyMinions.Count;

            // 消灭所有随从
            p.allMinionsGetDestroyed();

            // 为己方召唤等量的2/2树人
            for (int i = 0; i < ownMinionCount; i++)
            {
                // 在位置1召唤树人（己方）
                // 参数说明：- 要召唤的卡牌，- 召唤位置，- true表示为己方召唤
                p.callKid(treant, 1, true);
            }

            // 为敌方召唤等量的2/2树人
            for (int i = 0; i < enemyMinionCount; i++)
            {
                // 在位置1召唤树人（敌方）
                // 参数说明：- 要召唤的卡牌，- 召唤位置，- false表示为敌方召唤
                p.callKid(treant, 1, false);
            }
        }
    }
}