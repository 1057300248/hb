using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 虫群风暴卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为7点
    // 卡牌效果：对所有敌方随从造成3点伤害。为你的英雄恢复3点生命值。
    class Sim_NAX1_05 : SimTemplate //* 虫群风暴 Locust Swarm
    // Deal $3 damage to all enemy minions. Restore #3 Health to your hero.
    // 对所有敌方随从造成$3点伤害。为你的英雄恢复#3点生命值。
    {
        // 重写卡牌使用效果方法，这是虫群风暴卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算法术伤害和治疗量，考虑法强加成
            int damage = (ownplay) ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
            int heal = (ownplay) ? p.getSpellHeal(3) : p.getEnemySpellHeal(3);

            if (ownplay)
            {
                // 己方使用：治疗己方英雄，伤害敌方英雄和敌方所有随从
                p.minionGetDamageOrHeal(p.ownHero, -heal); // 治疗己方英雄（负数表示治疗）

                // 对敌方英雄造成伤害
                p.minionGetDamageOrHeal(p.enemyHero, damage);

                // 对所有敌方随从造成伤害
                foreach (Minion enemyMinion in p.enemyMinions)
                {
                    p.minionGetDamageOrHeal(enemyMinion, damage);
                }
            }
            else
            {
                // 敌方使用：治疗敌方英雄，伤害己方英雄和己方所有随从
                p.minionGetDamageOrHeal(p.enemyHero, -heal); // 治疗敌方英雄（负数表示治疗）

                // 对己方英雄造成伤害
                p.minionGetDamageOrHeal(p.ownHero, damage);

                // 对所有己方随从造成伤害
                foreach (Minion ownMinion in p.ownMinions)
                {
                    p.minionGetDamageOrHeal(ownMinion, damage);
                }
            }
        }
    }
}