using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 干杯！英雄技能卡牌的模拟实现类，继承自SimTemplate基类
    // 这是黑石山冒险模式中的英雄技能，费用为2点
    // 卡牌效果：<b>英雄技能</b>从双方的牌库中各将一个随从置入战场。
    class Sim_BRMA01_2 : SimTemplate //* 干杯！ Pile On!
    // <b>Hero Power</b>Put a minion from each deck into the battlefield.
    // <b>英雄技能</b>从双方的牌库中各将一个随从置入战场。 
    {
        // 定义要召唤的随从卡牌（这里使用的是蛛魔之卵的衍生随从，但在实际使用中应该是随机随从）
        CardDB.Card randomMinion = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_007t);

        // 重写卡牌使用效果方法，这是干杯！英雄技能的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查己方牌库是否有牌
            if (p.ownDeckSize > 0)
            {
                // 从己方牌库中取出一个随从并召唤到战场上
                // 参数说明：- 要召唤的卡牌，- 召唤位置（在己方随从队列末尾），- true表示为己方召唤，- false表示不是衍生物
                p.callKid(randomMinion, p.ownMinions.Count, true, false);

                // 减少己方牌库数量
                p.ownDeckSize--;
            }

            // 检查敌方牌库是否有牌
            if (p.enemyDeckSize > 0)
            {
                // 从敌方牌库中取出一个随从并召唤到战场上
                // 参数说明：- 要召唤的卡牌，- 召唤位置（在敌方随从队列末尾），- false表示为敌方召唤，- false表示不是衍生物
                p.callKid(randomMinion, p.enemyMinions.Count, false, false);

                // 减少敌方牌库数量
                p.enemyDeckSize--;
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // 1. REQ_NUM_MINION_SLOTS - 至少需要一个随从位置
            // 这意味着战场上必须有至少一个随从位置才能使用这个英雄技能
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
    }
}