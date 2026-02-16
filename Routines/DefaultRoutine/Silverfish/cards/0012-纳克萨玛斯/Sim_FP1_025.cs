using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 转生卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为2点
    // 卡牌效果：消灭一个随从，然后将其复活，并具有所有生命值。
    class Sim_FP1_025 : SimTemplate //* 转生 Reincarnate
    // Destroy a minion, then return it to life with full Health.
    // 消灭一个随从，然后将其复活，并具有所有生命值。
    {
        // 重写卡牌使用效果方法，这是转生卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查目标是否有效
            if (target != null)
            {
                // 获取目标随从的原始卡牌数据
                CardDB.Card originalCard = target.handcard.card;

                // 记录目标随从的位置
                int summonPosition = target.zonepos;

                // 消灭目标随从
                p.minionGetDestroyed(target);

                // 在原位置召唤该随从的全新复制（满生命值）
                // 参数说明：- 原始卡牌数据，- 召唤位置，- 是否为己方召唤
                p.callKid(originalCard, summonPosition, ownplay);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含三个条件：
            // 1. REQ_TARGET_TO_PLAY - 必须指定一个目标
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 3. REQ_FRIENDLY_TARGET - 目标必须是友方角色
            // 这意味着必须指定一个友方随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),     // 需要指定目标               
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),      // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),    // 目标必须是友方随从
            };
        }
    }
}