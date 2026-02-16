using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 小鬼首领卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张术士职业随从卡牌，费用为2点，攻击力2，生命值4
    // 卡牌效果：每当本随从受到伤害，召唤一个1/1的小鬼。
    class Sim_BG_BRM_006 : SimTemplate //* 小鬼首领 Imp Gang Boss
    // Whenever this minion takes damage, summon a 1/1 Imp.
    // 每当本随从受到伤害，召唤一个1/1的小鬼。
    {
        // 定义要召唤的小鬼卡牌
        CardDB.Card imp = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_006t);

        // 重写随从受伤触发方法，这是小鬼首领卡牌效果的核心实现
        public override void onMinionGotDmgTrigger(Playfield p, Minion triggerEffectMinion, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查小鬼首领是否受到了伤害
            if (triggerEffectMinion.anzGotDmg > 0)
            {
                // 获取受到伤害的次数
                int damageInstances = triggerEffectMinion.anzGotDmg;
                triggerEffectMinion.anzGotDmg = 0; // 重置伤害计数器

                // 根据受到伤害的次数召唤相应数量的小鬼
                for (int i = 0; i < damageInstances; i++)
                {
                    // 在小鬼首领的位置召唤一个1/1小鬼
                    // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤
                    int summonPosition = triggerEffectMinion.zonepos;
                    p.callKid(imp, summonPosition, triggerEffectMinion.own);
                }
            }
        }
    }
}