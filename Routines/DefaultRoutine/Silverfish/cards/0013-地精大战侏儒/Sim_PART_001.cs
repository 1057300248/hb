using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 重型护甲卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业零件法术卡牌，费用为1点
    // 卡牌效果：使一个随从获得+1生命值。
    class Sim_PART_001 : SimTemplate //* 重型护甲 Armor Plating
    // Give a minion +1 Health.
    // 使一个随从获得+1生命值。 
    {
        // 重写卡牌使用效果方法，这是重型护甲卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 给目标随从增加+1生命值
            // 参数说明：- 目标随从，- 0攻击力变化，- +1生命值变化
            p.minionGetBuffed(target, 0, 1);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 必须指定一个目标
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}