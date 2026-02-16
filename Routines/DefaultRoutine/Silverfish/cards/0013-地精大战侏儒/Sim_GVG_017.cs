using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 召唤宠物卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为2点
    // 卡牌效果：抽一张牌。如果该牌是野兽牌，则其法力值消耗减少（4）点。
    class Sim_GVG_017 : SimTemplate //* 召唤宠物 Call Pet
    // Draw a card.If it's a Beast, it costs (4) less.
    // 抽一张牌。如果该牌是野兽牌，则其法力值消耗减少（4）点。 
    {
        // 重写卡牌打出时的效果方法，这是召唤宠物卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 调用游戏场地的抽卡方法，随机抽取一张卡牌到手牌
            // 参数说明：
            // - CardDB.cardIDEnum.None: 表示随机抽取一张卡牌（不是指定特定卡牌）
            // - ownplay: 布尔值，指示将卡牌添加到哪一方的手牌中（true为己方，false为敌方）
            // 在实际实现中，drawACard方法内部应该会处理"如果是野兽牌则费用减少4点"的效果
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
        }
    }
}