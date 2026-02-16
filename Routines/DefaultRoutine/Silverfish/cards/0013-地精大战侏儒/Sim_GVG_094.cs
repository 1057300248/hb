using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 基维斯卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力1，生命值4
    // 卡牌效果：在每个玩家的回合结束时，该玩家抽若干牌，直至其手牌数量达到3张。
    class Sim_GVG_094 : SimTemplate //* 基维斯 Jeeves
    // At the end of each player's turn, that player draws until they have 3 cards.
    // 在每个玩家的回合结束时，该玩家抽若干牌，直至其手牌数量达到3张。 
    {
        // 重写回合结束触发方法，这是基维斯卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 处理己方玩家抽牌
            int 计数器 = 0;
            if (p.owncards.Count <= 2)
            {
                // 计算需要抽的牌数，使手牌达到3张
                计数器 = 3 - p.owncards.Count;
            }

            // 为己方玩家抽牌
            for (int i = 0; i < 计数器; i++)
            {
                p.drawACard(CardDB.cardIDEnum.None, true);
            }

            // 重置计数器
            计数器 = 0;

            // 处理敌方玩家抽牌
            if (p.enemyAnzCards <= 2)
            {
                // 计算需要抽的牌数，使手牌达到3张
                计数器 = 3 - p.enemyAnzCards;
            }

            // 为敌方玩家抽牌
            for (int i = 0; i < 计数器; i++)
            {
                p.drawACard(CardDB.cardIDEnum.None, false);
            }
        }
    }
}