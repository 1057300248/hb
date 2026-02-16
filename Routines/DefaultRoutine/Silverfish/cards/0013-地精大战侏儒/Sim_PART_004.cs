using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 隐秘力场卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业零件法术卡牌，费用为1点
    // 卡牌效果：直到你的下个回合，使一个友方随从获得<b>潜行</b>。
    class Sim_PART_004 : SimTemplate //* 隐秘力场 Finicky Cloakfield
    // Give a friendly minion <b>Stealth</b> until your next turn.
    // 直到你的下个回合，使一个友方随从获得<b>潜行</b>。 
    {
        // 重写卡牌使用效果方法，这是隐秘力场卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 给目标友方随从添加潜行效果
            target.stealth = true;

            // 设置潜行持续到下个回合的标志
            target.conceal = true;
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