using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 不稳定的传送门卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师法术卡牌，费用为2点
    // 卡牌效果：随机将一张随从牌置入你的手牌。该牌的法力值消耗减少（3）点。
    class Sim_GVG_003 : SimTemplate //* 不稳定的传送门 Unstable Portal
    //Add a random minion to your hand. It costs (3) less.
    //随机将一张随从牌置入你的手牌。该牌的法力值消耗减少（3）点。 
    {
        // 重写卡牌打出时的效果方法，这是卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 调用游戏场地的抽卡方法，添加一张随机随从到手牌
            // 参数说明：
            // - CardDB.cardIDEnum.None: 表示随机抽取一张卡牌（不是指定特定卡牌）
            // - ownplay: 布尔值，指示是否是己方打出此卡牌
            // - true: 第三个参数为true，表示这是一个发现/随机获取机制，会触发相应的卡牌效果处理
            // 在实际实现中，drawACard方法内部应该会处理"法力值消耗减少3点"的效果
            p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
        }
    }
}