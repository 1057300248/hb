using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 严正警戒卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为5点
    // 卡牌效果：抽两张牌。在本回合中每有一个随从死亡，本牌的法力值消耗便减少（1）点。
    class Sim_BRM_001 : SimTemplate //* 严正警戒 Solemn Vigil
    {
        // 重写卡牌使用效果方法，这是严正警戒卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 抽两张牌
            p.drawACard(CardDB.cardIDEnum.None, ownplay); // 抽第一张牌
            p.drawACard(CardDB.cardIDEnum.None, ownplay); // 抽第二张牌
        }
    }
}