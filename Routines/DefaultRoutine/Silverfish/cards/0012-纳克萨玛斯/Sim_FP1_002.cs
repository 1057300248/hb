using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 鬼灵爬行者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1，生命值2
    // 卡牌效果：<b>亡语：</b>召唤两只1/1的鬼灵蜘蛛。
    class Sim_FP1_002 : SimTemplate //* 鬼灵爬行者 Haunted Creeper
    // <b>Deathrattle:</b> Summon two 1/1 Spectral Spiders.
    // <b>亡语：</b>召唤两只1/1的鬼灵蜘蛛。 
    {
        // 定义要召唤的鬼灵蜘蛛卡牌
        CardDB.Card spider = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_002t);

        // 重写亡语效果方法，这是鬼灵爬行者卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 在鬼灵爬行者死亡的位置召唤第一只鬼灵蜘蛛
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(spider, m.zonepos - 1, m.own);

            // 在鬼灵爬行者死亡的位置召唤第二只鬼灵蜘蛛
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(spider, m.zonepos - 1, m.own);
        }
    }
}