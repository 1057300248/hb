using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 微型战斗机甲卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1，生命值2
    // 卡牌效果：在每个回合开始时，获得+1攻击力。
    class Sim_GVG_103 : SimTemplate //* 微型战斗机甲 Micro Machine
    // At the start of each turn, gain +1 Attack.
    // 在每个回合开始时，获得+1攻击力。 
    {
        // 重写回合开始触发方法，这是微型战斗机甲卡牌效果的核心实现
        public override void onTurnStartTrigger(Playfield p, Minion triggerEffectMinion, bool turnStartOfOwner)
        {
            // 给触发效果的微型战斗机甲增加+1攻击力
            // 参数说明：- 目标随从，- +1攻击力，- 0生命值变化
            p.minionGetBuffed(triggerEffectMinion, 1, 0);
        }
    }
}