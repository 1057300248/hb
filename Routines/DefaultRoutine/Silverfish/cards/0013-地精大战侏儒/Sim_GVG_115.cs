using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 托什雷卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力5，生命值7
    // 卡牌效果：<b>战吼，亡语：</b>将一张<b>零件</b>牌置入你的手牌。
    class Sim_GVG_115 : SimTemplate //* 托什雷 Toshley
    // <b>Battlecry and Deathrattle:</b> Add a <b>Spare Part</b> card to your hand.
    // <b>战吼，亡语：</b>将一张<b>零件</b>牌置入你的手牌。 
    {
        // 重写战吼效果方法，这是托什雷战吼效果的实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 为托什雷的拥有者添加一张零件牌到手牌
            // 参数说明：- CardDB.cardNameEN.armorplating：零件牌名称（装甲镀层）
            // - own.own：指示为哪一方添加手牌
            // - true：表示这是特殊效果抽卡
            p.drawACard(CardDB.cardNameEN.armorplating, own.own, true);
        }

        // 重写亡语效果方法，这是托什雷亡语效果的实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 为托什雷的拥有者添加一张零件牌到手牌
            // 参数说明：- CardDB.cardNameEN.armorplating：零件牌名称（装甲镀层）
            // - m.own：指示为哪一方添加手牌
            // - true：表示这是特殊效果抽卡
            p.drawACard(CardDB.cardNameEN.armorplating, m.own, true);
        }
    }
}