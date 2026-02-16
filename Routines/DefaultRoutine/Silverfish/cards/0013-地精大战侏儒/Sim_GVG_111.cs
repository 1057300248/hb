using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 米米尔隆的头部卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力4，生命值5
    // 卡牌效果：在你的回合开始时，如果你控制至少三个机械，则消灭这些机械，并将其组合成V-07-TR-0N。
    class Sim_GVG_111 : SimTemplate //* 米米尔隆的头部 Mimiron's Head
    // At the start of your turn, if you have at least 3 Mechs, destroy them all and form V-07-TR-0N.
    // 在你的回合开始时，如果你控制至少三个机械，则消灭这些机械，并将其组合成V-07-TR-0N。 
    {
        // 定义要召唤的V-07-TR-0N卡牌
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_111t);

        // 重写回合开始触发方法，这是米米尔隆的头部卡牌效果的核心实现
        public override void onTurnStartTrigger(Playfield p, Minion triggerEffectMinion, bool turnStartOfOwner)
        {
            // 检查是否是米米尔隆的头部拥有者的回合开始
            if (turnStartOfOwner != triggerEffectMinion.own) return;

            // 根据回合开始方确定要检查的随从列表
            List<Minion> minionsToCheck = (turnStartOfOwner) ? p.ownMinions : p.enemyMinions;

            // 计算存活的机械随从数量
            int mechCount = 0;
            foreach (Minion m in minionsToCheck)
            {
                // 检查随从是否为机械种族且存活
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL && m.Hp >= 1)
                {
                    mechCount++;
                }
            }

            // 如果机械随从数量大于等于3
            if (mechCount >= 3)
            {
                // 重置计数器
                mechCount = 0;

                // 遍历并消灭前3个机械随从
                foreach (Minion m in minionsToCheck)
                {
                    if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL)
                    {
                        // 消灭机械随从
                        p.minionGetDestroyed(m);
                        mechCount++;

                        // 只消灭前3个
                        if (mechCount == 3) break;
                    }
                }

                // 在战场末尾召唤V-07-TR-0N
                // 参数说明：- 要召唤的卡牌，- 召唤位置（战场末尾），- 是否为己方召唤，- false表示不是衍生物，- true表示立即召唤
                int summonPosition = (triggerEffectMinion.own) ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(kid, summonPosition, triggerEffectMinion.own, false, true);
            }
        }
    }
}