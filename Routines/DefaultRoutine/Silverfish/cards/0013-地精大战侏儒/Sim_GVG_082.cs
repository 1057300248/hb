using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 发条侏儒卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力2，生命值1
    // 卡牌效果：<b>亡语：</b>将一张<b>零件</b>牌置入你的手牌。
    class Sim_GVG_082 : SimTemplate //* 发条侏儒 Clockwork Gnome
    // <b>Deathrattle:</b> Add a <b>Spare Part</b> card to your hand.
    // <b>亡语：</b>将一张<b>零件</b>牌置入你的手牌。 
    {
        // 重写亡语效果方法，这是发条侏儒卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 为发条侏儒的拥有者添加一张随机零件牌到手牌
            // 参数说明：- CardDB.cardIDEnum.None：表示随机抽取一张卡牌
            // - m.own：指示添加到哪一方的手牌（true为己方，false为敌方）
            // - true：表示这是特殊效果抽卡（获得零件牌）
            p.drawACard(CardDB.cardIDEnum.None, m.own, true);
        }
    }
}