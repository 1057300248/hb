using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 火鹰形态卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张德鲁伊职业的抉择法术卡牌，费用为0点
    // 卡牌效果：将目标随从变形成为2/5的火鹰
    class Sim_BRM_010b : SimTemplate //* 火鹰形态 Fire Hawk Form
    // Transform a minion into a 2/5 Fire Hawk.
    // 将目标随从变形成为2/5的火鹰。 
    {
        // 定义变形后的火鹰卡牌
        CardDB.Card firehawk = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_010t2);

        // 重写卡牌使用效果方法，这是火鹰形态卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将目标随从变形成为2/5的火鹰
            // 参数说明：- 要变形的目标随从，- 变形后的卡牌
            p.minionTransform(target, firehawk);
        }
    }
}