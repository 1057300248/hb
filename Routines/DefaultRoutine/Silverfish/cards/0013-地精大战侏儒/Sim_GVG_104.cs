using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 大胖卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值3
    // 卡牌效果：每当你使用一张攻击力为1的随从牌，便使该牌所召唤的随从获得+2/+2。
    class Sim_GVG_104 : SimTemplate //* 大胖 Hobgoblin
    // Whenever you play a 1-Attack minion, give it +2/+2.
    // 每当你使用一张攻击力为1的随从牌，便使该牌所召唤的随从获得+2/+2。 
    {
        // 重写随从被召唤触发方法，这是大胖卡牌效果的核心实现
        public override void onMinionWasSummoned(Playfield p, Minion m, Minion summonedMinion)
        {
            // 检查条件：召唤的随从是从手牌打出的、攻击力为1、与大胖同属一方、且不是大胖自己
            if (summonedMinion.playedFromHand &&
                summonedMinion.Angr == 1 &&
                m.own == summonedMinion.own &&
                m.entitiyID != summonedMinion.entitiyID)
            {
                // 给符合条件的召唤随从增加+2/+2
                p.minionGetBuffed(summonedMinion, 2, 2);
            }
        }
    }
}