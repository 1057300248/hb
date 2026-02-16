using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 岩肤石像鬼卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力1，生命值4
    // 卡牌效果：在你的回合开始时，为该随从恢复所有生命值。
    class Sim_FP1_027 : SimTemplate //* 岩肤石像鬼 Stoneskin Gargoyle
    // At the start of your turn, restore this minion to full Health.
    // 在你的回合开始时，为该随从恢复所有生命值。 
    {
        // 重写回合开始触发方法，这是岩肤石像鬼卡牌效果的核心实现
        public override void onTurnStartTrigger(Playfield p, Minion triggerEffectMinion, bool turnStartOfOwner)
        {
            // 检查是否是岩肤石像鬼拥有者的回合开始
            if (triggerEffectMinion.own == turnStartOfOwner)
            {
                // 计算需要恢复的生命值（最大生命值 - 当前生命值）
                int healAmount = triggerEffectMinion.maxHp - triggerEffectMinion.Hp;

                // 如果需要恢复生命值
                if (healAmount > 0)
                {
                    // 考虑治疗加成计算实际治疗量
                    int actualHeal = (triggerEffectMinion.own) ?
                        p.getMinionHeal(healAmount) :
                        p.getEnemyMinionHeal(healAmount);

                    // 为岩肤石像鬼恢复生命值
                    // 参数说明：- 目标随从，- 负的伤害值（表示治疗）
                    p.minionGetDamageOrHeal(triggerEffectMinion, -actualHeal);
                }
            }
        }
    }
}