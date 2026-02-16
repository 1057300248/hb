using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 费尔根卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力4，生命值7
    // 卡牌效果：<b>亡语：</b>如果斯塔拉格也在本局对战中死亡，召唤塔迪乌斯。
    class Sim_FP1_015 : SimTemplate //* 费尔根 Feugen
    // <b>Deathrattle:</b> If Stalagg also died this game, summon Thaddius.
    // <b>亡语：</b>如果斯塔拉格也在本局对战中死亡，召唤塔迪乌斯。 
    {
        // 定义要召唤的塔迪乌斯卡牌
        CardDB.Card thaddius = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_014t);

        // 重写亡语效果方法，这是费尔根卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 检查斯塔拉格是否已经死亡
            if (p.斯塔拉格死亡)
            {
                // 在费尔根死亡的位置召唤塔迪乌斯
                // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
                p.callKid(thaddius, m.zonepos - 1, m.own);
            }
        }
    }
}