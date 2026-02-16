using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 龙蛋卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为1点，攻击力0，生命值2
    // 卡牌效果：每当该随从受到伤害，便召唤一条2/1的雏龙。
    class Sim_BRM_022 : SimTemplate //* 龙蛋 Dragon Egg
    // Whenever this minion takes damage, summon a 2/1 Whelp.
    // 每当该随从受到伤害，便召唤一条2/1的雏龙。 
    {
        // 定义要召唤的雏龙卡牌
        CardDB.Card whelp = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_022t);

        // 重写随从受伤触发方法，这是龙蛋卡牌效果的核心实现
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查龙蛋是否受到了伤害
            if (m.anzGotDmg > 0)
            {
                // 获取龙蛋受到伤害的次数
                int damageInstances = m.anzGotDmg;
                m.anzGotDmg = 0; // 重置伤害计数器

                // 根据受到伤害的次数，召唤相应数量的2/1雏龙
                for (int i = 0; i < damageInstances; i++)
                {
                    // 在龙蛋的位置召唤2/1雏龙
                    // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤
                    int summonPosition = m.zonepos;
                    p.callKid(whelp, summonPosition, m.own);
                }
            }
        }
    }
}