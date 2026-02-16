using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 生命之树卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为9点
    // 卡牌效果：为所有角色恢复所有生命值。
    class Sim_GVG_033 : SimTemplate //* 生命之树 Tree of Life
    // Restore all characters to full Health.
    // 为所有角色恢复所有生命值。 
    {
        // 重写卡牌打出时的效果方法，这是生命之树卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 设置一个足够大的治疗值，确保能完全恢复所有角色的生命值
            int heal = 1000;

            // 为所有己方随从恢复生命值
            foreach (Minion m in p.ownMinions)
            {
                // 通过传递负的伤害值来实现治疗效果
                // p.minionGetDamageOrHeal方法中，负值表示治疗，正值表示伤害
                p.minionGetDamageOrHeal(m, -heal);
            }

            // 为所有敌方随从恢复生命值
            foreach (Minion m in p.enemyMinions)
            {
                // 通过传递负的伤害值来实现治疗效果
                p.minionGetDamageOrHeal(m, -heal);
            }

            // 为敌方英雄恢复生命值
            p.minionGetDamageOrHeal(p.enemyHero, -heal);

            // 为己方英雄恢复生命值
            p.minionGetDamageOrHeal(p.ownHero, -heal);
        }
    }
}