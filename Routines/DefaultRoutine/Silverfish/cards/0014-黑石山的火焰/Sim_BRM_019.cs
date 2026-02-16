using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 恐怖的奴隶主卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张战士职业随从卡牌，费用为5点，攻击力3，生命值3
    // 卡牌效果：在该随从受到伤害并没有死亡后，召唤另一个恐怖的奴隶主。
    class Sim_BRM_019 : SimTemplate //* 恐怖的奴隶主 Grim Patron
    // After this minion survives damage, summon another Grim Patron.
    // 在该随从受到伤害并没有死亡后，召唤另一个恐怖的奴隶主。 
    {
        // 定义要召唤的恐怖的奴隶主卡牌
        CardDB.Card patron = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_019);

        // 重写随从受伤触发方法，这是恐怖的奴隶主卡牌效果的核心实现
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查恐怖的奴隶主是否受到伤害并且仍然存活
            if (m.anzGotDmg > 0 && m.Hp > 0)
            {
                // 获取恐怖的奴隶主受到伤害的次数
                int damageInstances = m.anzGotDmg;
                m.anzGotDmg = 0; // 重置伤害计数器

                // 根据受到伤害的次数，召唤相应数量的恐怖的奴隶主
                for (int i = 0; i < damageInstances; i++)
                {
                    // 在原随从的位置召唤一个新的恐怖的奴隶主
                    // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤
                    p.callKid(patron, m.zonepos, m.own);
                }
            }
        }
    }
}