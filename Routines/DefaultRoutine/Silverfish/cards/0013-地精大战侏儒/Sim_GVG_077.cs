using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 心能魔像卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力9，生命值10
    // 卡牌效果：在每个回合结束时，如果该随从是你唯一的随从，则消灭该随从。
    class Sim_GVG_077 : SimTemplate //* 心能魔像 Anima Golem
    // At the end of each turn, destroy this minion if it's your only one.
    // 在每个回合结束时，如果该随从是你唯一的随从，则消灭该随从。 
    {
        // 重写回合结束触发方法，这是心能魔像卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查心能魔像是否属于己方
            if (triggerEffectMinion.own)
            {
                // 如果己方只有一个随从（即心能魔像自己），则消灭该随从
                if (p.ownMinions.Count == 1)
                {
                    p.minionGetDestroyed(triggerEffectMinion);
                }
            }
            else
            {
                // 如果敌方只有一个随从（即心能魔像自己），则消灭该随从
                if (p.enemyMinions.Count == 1)
                {
                    p.minionGetDestroyed(triggerEffectMinion);
                }
            }
        }
    }
}