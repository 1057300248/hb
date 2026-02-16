using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 机械野兽卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力7，生命值6
    // 卡牌效果：每当该随从受到伤害，将一张<b>零件</b>牌置入你的手牌。
    class Sim_GVG_034 : SimTemplate //* 机械野兽 Mech-Bear-Cat
    // Whenever this minion takes damage, add a <b>Spare Part</b> card to your hand.
    // 每当该随从受到伤害，将一张<b>零件</b>牌置入你的手牌。 
    {
        // 重写随从受到伤害时的触发方法，当随从受到伤害时调用
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查该随从是否受到了伤害（anzGotDmg记录受伤害次数）
            if (m.anzGotDmg > 0)
            {
                // 保存受伤害次数，然后重置计数器
                int tmp = m.anzGotDmg;
                m.anzGotDmg = 0;

                // 根据受伤害次数，为拥有者添加相应数量的零件牌到手牌
                for (int i = 0; i < tmp; i++)
                {
                    // 添加装甲镀层零件牌到手牌
                    // 参数说明：- CardDB.cardNameEN.armorplating：零件牌名称
                    // - m.own：指示添加到哪一方的手牌（true为己方，false为敌方）
                    // - true：表示这是特殊效果抽卡
                    p.drawACard(CardDB.cardNameEN.armorplating, m.own, true);
                }
            }
        }
    }
}