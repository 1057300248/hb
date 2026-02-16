using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 卡牌赠礼卡牌的模拟实现类，继承自SimTemplate基类
    // 这是林地树妖的抉择选项之一，法师职业法术卡牌，费用为0点
    // 卡牌效果：每个玩家抽一张牌。
    class Sim_GVG_032b : SimTemplate //* 卡牌赠礼 Gift of Cards
    // Each player draws a card.
    // 每个玩家抽一张牌。 
    {
        // 重写战吼效果方法，这是卡牌赠礼卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 己方玩家抽一张牌
            // 参数说明：- CardDB.cardIDEnum.None表示随机抽牌，- true表示为己方抽牌
            p.drawACard(CardDB.cardIDEnum.None, true);

            // 敌方玩家抽一张牌
            // 参数说明：- CardDB.cardIDEnum.None表示随机抽牌，- false表示为敌方抽牌
            p.drawACard(CardDB.cardIDEnum.None, false);
        }
    }
}