using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 夜幕奇袭卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为2点
    // 卡牌效果：选择一个随从。将该随从的三张复制洗入你的牌库。
    class Sim_BRM_007 : SimTemplate //* 夜幕奇袭 Gang Up
    // Choose a minion. Shuffle 3 copies of it into your deck.
    // 选择一个随从。将该随从的三张复制洗入你的牌库。 
    {
        // 重写卡牌使用效果方法，这是夜幕奇袭卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查使用方
            if (ownplay)
            {
                // 为己方牌库增加3张复制
                p.ownDeckSize += 3;
            }
            else
            {
                // 为敌方牌库增加3张复制
                p.enemyDeckSize += 3;
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要指定一个目标
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}