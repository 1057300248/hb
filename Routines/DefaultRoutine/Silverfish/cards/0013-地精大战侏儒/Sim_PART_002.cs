using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 时间回溯装置卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业零件法术卡牌，费用为1点
    // 卡牌效果：将一个友方随从移回你的手牌。
    class Sim_PART_002 : SimTemplate //* 时间回溯装置 Time Rewinder
    // Return a friendly minion to your hand.
    // 将一个友方随从移回你的手牌。 
    {
        // 重写卡牌使用效果方法，这是时间回溯装置卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将目标友方随从移回手牌
            // 参数说明：- 目标随从，- 随从归属（true为己方），- 0表示立即返回手牌
            p.minionReturnToHand(target, target.own, 0);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含三个条件：
            // 1. REQ_TARGET_TO_PLAY - 必须指定一个目标
            // 2. REQ_FRIENDLY_TARGET - 目标必须是友方角色
            // 3. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个友方随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}