using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 机械雪人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力4，生命值5
    // 卡牌效果：<b>亡语：</b>使每个玩家获得一张<b>零件</b>牌。
    class Sim_GVG_078 : SimTemplate //* 机械雪人 Mechanical Yeti
    // <b>Deathrattle:</b> Give each player a <b>Spare Part.</b>
    // <b>亡语：</b>使每个玩家获得一张<b>零件</b>牌。 
    {
        // 重写亡语效果方法，这是机械雪人卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 给敌方玩家添加一张零件牌（装甲镀层）
            // 参数说明：- CardDB.cardNameEN.armorplating：零件牌名称
            // - false：表示给敌方玩家添加
            // - true：表示这是特殊效果抽卡
            p.drawACard(CardDB.cardNameEN.armorplating, false, true);

            // 给己方玩家添加一张随机零件牌
            // 参数说明：- CardDB.cardIDEnum.None：表示随机抽取一张卡牌
            // - true：表示给己方玩家添加
            // - true：表示这是特殊效果抽卡
            p.drawACard(CardDB.cardIDEnum.None, true, true);
        }
    }
}