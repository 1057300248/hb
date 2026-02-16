using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 雷德·黑手卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张战士职业随从卡牌，费用为5点，攻击力3，生命值6
    // 卡牌效果：<b>战吼：</b>如果你的手牌中有龙牌，则消灭一个<b>传说</b>随从。
    class Sim_BRM_029 : SimTemplate //* 雷德·黑手 Rend Blackhand
    // <b>Battlecry:</b> If you're holding a Dragon, destroy a <b>Legendary</b> minion.
    // <b>战吼：</b>如果你的手牌中有龙牌，则消灭一个<b>传说</b>随从。 
    {
        // 重写战吼效果方法，这是雷德·黑手卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查目标是否存在
            if (target != null)
            {
                // 消灭目标传说随从
                p.minionGetDestroyed(target);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含三个条件：
            // 1. REQ_LEGENDARY_TARGET - 目标必须是传说随从
            // 2. REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND - 如果有龙牌在手则必须指定目标
            // 3. REQ_MINION_TARGET - 目标必须是随从
            // 这意味着必须手上有龙牌且指定一个传说随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_LEGENDARY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}