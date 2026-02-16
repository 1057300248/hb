using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 工匠镇技师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力1，生命值2
    // 卡牌效果：<b>战吼：</b>如果你控制一个机械，便获得+1/+1并将一张<b>零件</b>牌置入你的手牌。
    class Sim_GVG_102 : SimTemplate //* 工匠镇技师 Tinkertown Technician
    // <b>Battlecry:</b> If you have a Mech, gain +1/+1 and add a <b>Spare Part</b> to your hand.
    // <b>战吼：</b>如果你控制一个机械，便获得+1/+1并将一张<b>零件</b>牌置入你的手牌。 
    {
        // 重写战吼效果方法，这是工匠镇技师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据工匠镇技师的归属确定要检查的随从列表
            List<Minion> minionsToCheck = (own.own) ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，检查是否存在机械
            foreach (Minion m in minionsToCheck)
            {
                // 检查随从种族是否为机械
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL)
                {
                    // 给工匠镇技师增加+1/+1
                    p.minionGetBuffed(own, 1, 1);

                    // 为工匠镇技师的拥有者添加一张零件牌到手牌
                    // 参数说明：- CardDB.cardNameEN.armorplating：零件牌名称（装甲镀层）
                    // - own.own：指示为哪一方添加手牌
                    // - true：表示这是特殊效果抽卡
                    p.drawACard(CardDB.cardNameEN.armorplating, own.own, true);

                    // 找到一个机械就够了，退出循环
                    return;
                }
            }
        }
    }
}