using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 活力图腾卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力0，生命值3
    // 卡牌效果：在你的回合结束时，为你的英雄恢复#4点生命值。
    class Sim_GVG_039 : SimTemplate //* 活力图腾 Vitality Totem
    // At the end of your turn, restore #4 Health to your hero.
    // 在你的回合结束时，为你的英雄恢复#4点生命值。 
    {
        // 重写回合结束触发方法，这是活力图腾卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查是否是活力图腾拥有者的回合结束
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                // 检查活力图腾是否属于己方
                if (triggerEffectMinion.own)
                {
                    // 计算己方的治疗量（考虑治疗加成）
                    int heal = p.getMinionHeal(4);

                    // 为己方英雄恢复生命值
                    // 传递负的伤害值来实现治疗效果
                    // 第三个参数true表示这是治疗操作
                    p.minionGetDamageOrHeal(p.ownHero, -heal, true);
                }
                else
                {
                    // 计算敌方的治疗量（考虑治疗加成）
                    int heal = p.getEnemyMinionHeal(4);

                    // 为敌方英雄恢复生命值
                    p.minionGetDamageOrHeal(p.enemyHero, -heal, true);
                }
            }
        }
    }
}