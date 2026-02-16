using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 蛛魔之卵卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力0，生命值2
    // 卡牌效果：<b>亡语：</b>召唤一个4/4的蛛魔。
    class Sim_FP1_007 : SimTemplate //* 蛛魔之卵 Nerubian Egg
    // <b>Deathrattle:</b> Summon a 4/4 Nerubian.
    // <b>亡语：</b>召唤一个4/4的蛛魔。 
    {
        // 定义要召唤的蛛魔卡牌
        CardDB.Card nerubian = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_007t);

        // 重写亡语效果方法，这是蛛魔之卵卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 在蛛魔之卵死亡的位置召唤一个4/4的蛛魔
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(nerubian, m.zonepos - 1, m.own);
        }
    }
}