using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 恶魔之怒卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为3点
    // 卡牌效果：对所有非恶魔随从造成2点伤害。
    class Sim_BRM_005 : SimTemplate //* 恶魔之怒 Demonwrath
    // [x]Deal $2 damage to all minions except Demons.
    // 对所有非恶魔随从造成$2点伤害。 
    {
        // 重写卡牌使用效果方法，这是恶魔之怒卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算伤害值，考虑法术伤害加成
            int damage = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);

            // 对己方所有非恶魔随从造成伤害
            foreach (Minion ownMinion in p.ownMinions)
            {
                // 检查随从是否不是恶魔种族
                if ((TAG_RACE)ownMinion.handcard.card.race != TAG_RACE.DEMON)
                {
                    p.minionGetDamageOrHeal(ownMinion, damage);
                }
            }

            // 对敌方所有非恶魔随从造成伤害
            foreach (Minion enemyMinion in p.enemyMinions)
            {
                // 检查随从是否不是恶魔种族
                if ((TAG_RACE)enemyMinion.handcard.card.race != TAG_RACE.DEMON)
                {
                    p.minionGetDamageOrHeal(enemyMinion, damage);
                }
            }
        }
    }
}