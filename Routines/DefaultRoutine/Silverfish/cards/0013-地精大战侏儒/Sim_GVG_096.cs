using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 载人收割机卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力4，生命值3
    // 卡牌效果：<b>亡语：</b>随机召唤一个法力值消耗为（2）的随从。
    class Sim_GVG_096 : SimTemplate //* 载人收割机 Piloted Shredder
    // <b>Deathrattle:</b> Summon a random 2-Cost minion.
    // <b>亡语：</b>随机召唤一个法力值消耗为（2）的随从。 
    {
        // 重写亡语效果方法，这是载人收割机卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 定义2费随从的卡牌ID列表
            List<CardDB.cardIDEnum> twoCostMinions = new List<CardDB.cardIDEnum>
            {
                CardDB.cardIDEnum.CS2_172, // 石牙野猪
                CardDB.cardIDEnum.CS2_121, // 铁炉堡火枪手
                CardDB.cardIDEnum.CS2_147, // 暗鳞先知
                CardDB.cardIDEnum.CS2_169, // 年轻的龙息者
                CardDB.cardIDEnum.CS2_181, // 火舌图腾
                // 可以继续添加其他2费随从...
            };

            // 随机选择一个2费随从
            CardDB.cardIDEnum randomMinionID = twoCostMinions[rand.Next(twoCostMinions.Count)];
            CardDB.Card kid = CardDB.Instance.getCardDataFromID(randomMinionID);

            // 在载人收割机死亡的位置召唤随机2费随从
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(kid, m.zonepos - 1, m.own);
        }
    }
}