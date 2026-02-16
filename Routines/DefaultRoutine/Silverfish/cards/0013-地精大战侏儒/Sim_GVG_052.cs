using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 重碾卡牌的模拟实现类，继承自SimTemplate基类
    // 卡牌效果：消灭一个随从。如果你控制任何受伤的随从，该法术的法力值消耗减少（4）点。
    class Sim_GVG_052 : SimTemplate //* 重碾 Crush
    // Destroy a minion. If you have a damaged minion, this costs (4) less.
    // 消灭一个随从。如果你控制任何受伤的随从，该法术的法力值消耗减少（4）点。 
    {
        // 重写卡牌打出时的效果方法，这是重碾卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 消灭指定的目标随从
            p.minionGetDestroyed(target);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个随从作为消灭目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}