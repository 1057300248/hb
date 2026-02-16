using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 掷斧者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张战士职业随从卡牌，费用为2点，攻击力2，生命值2
    // 卡牌效果：每当该随从受到伤害，对敌方英雄造成2点伤害。
    class Sim_BRM_016 : SimTemplate //* 掷斧者 Axe Flinger
    // Whenever this minion takes damage, deal 2 damage to the enemy hero.
    // 每当该随从受到伤害，对敌方英雄造成2点伤害。 
    {
        // 重写随从受伤触发方法，这是掷斧者卡牌效果的核心实现
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查掷斧者是否受到了伤害
            if (m.anzGotDmg > 0)
            {
                // 获取掷斧者受到伤害的次数
                int damageInstances = m.anzGotDmg;
                m.anzGotDmg = 0; // 重置伤害计数器

                // 根据受到伤害的次数，对敌方英雄造成相应次数的2点伤害
                for (int i = 0; i < damageInstances; i++)
                {
                    // 根据掷斧者的归属确定敌方英雄
                    // 如果是己方掷斧者，则伤害敌方英雄
                    // 如果是敌方掷斧者，则伤害己方英雄
                    p.minionGetDamageOrHeal(m.own ? p.enemyHero : p.ownHero, 2);
                }
            }
        }
    }
}