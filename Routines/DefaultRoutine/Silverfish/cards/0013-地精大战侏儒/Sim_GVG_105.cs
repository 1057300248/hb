using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 载人飞天魔像卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力6，生命值4
    // 卡牌效果：<b>亡语：</b>随机召唤一个法力值消耗为（4）的随从。
    class Sim_GVG_105 : SimTemplate //* 载人飞天魔像 Piloted Sky Golem
    // <b>Deathrattle:</b> Summon a random 4-Cost minion.
    // <b>亡语：</b>随机召唤一个法力值消耗为（4）的随从。 
    {
        // 定义要召唤的4费随从卡牌（这里以冰风雪人为例）
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_182);

        // 重写亡语效果方法，这是载人飞天魔像卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 在载人飞天魔像死亡的位置召唤一个4费随从
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(kid, m.zonepos - 1, m.own);
        }
    }
}