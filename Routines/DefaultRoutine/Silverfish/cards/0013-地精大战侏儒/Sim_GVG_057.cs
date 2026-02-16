using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 光明圣印卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为2点
    // 卡牌效果：为你的英雄恢复#4点生命值，并在本回合中获得+2攻击力。
    class Sim_GVG_057 : SimTemplate //* 光明圣印 Seal of Light
    // Restore #4 Health to your hero and gain +2 Attack this turn.
    // 为你的英雄恢复#4点生命值，并在本回合中获得+2攻击力。 
    {
        // 重写卡牌打出时的效果方法，这是光明圣印卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查是否是己方打出此卡牌
            if (ownplay)
            {
                // 计算己方的实际治疗量（考虑法术治疗加成）
                int heal = p.getSpellHeal(4);

                // 为己方英雄恢复生命值
                // 传递负的伤害值来实现治疗效果
                p.minionGetDamageOrHeal(p.ownHero, -heal);

                // 给己方英雄临时增加+2攻击力（仅本回合有效）
                p.minionGetTempBuff(p.ownHero, 2, 0);
            }
            else
            {
                // 计算敌方的实际治疗量（考虑法术治疗加成）
                int heal = p.getEnemySpellHeal(4);

                // 为敌方英雄恢复生命值
                p.minionGetDamageOrHeal(p.enemyHero, -heal);

                // 给敌方英雄临时增加+2攻击力（仅本回合有效）
                p.minionGetTempBuff(p.enemyHero, 2, 0);
            }
        }
    }
}