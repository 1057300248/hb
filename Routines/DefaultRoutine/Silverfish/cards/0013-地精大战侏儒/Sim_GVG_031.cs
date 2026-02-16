using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 回收卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为6点
    // 卡牌效果：将一个敌方随从洗入你对手的牌库。
    class Sim_GVG_031 : SimTemplate //* 回收 Recycle
    // Shuffle an enemy minion into your opponent's deck.
    // 将一个敌方随从洗入你对手的牌库。 
    {
        // 重写卡牌打出时的效果方法，这是回收卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将目标随从洗入对手的牌库
            // 参数说明：- 目标随从，- !ownplay表示将随从洗入对手的牌库（ownplay为false时是对手）
            p.minionReturnToDeck(target, !ownplay);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含三个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 2. REQ_ENEMY_TARGET - 目标必须是敌方角色
            // 3. REQ_MINION_TARGET - 目标必须是一个随从
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}