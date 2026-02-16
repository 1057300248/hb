using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 干杯！英雄模式卡牌的模拟实现类，继承自SimTemplate基类
    // 这是黑石山冒险模式中的一张英雄技能卡牌，费用为2点
    // 卡牌效果：<b>英雄技能</b>从你的牌库中将两个随从置入战场；对手将一个随从置入战场。
    class Sim_BRMA01_2H : SimTemplate //* 干杯！ Pile On!
    // <b>Hero Power</b>Put two minions from your deck and one from your opponent's into the battlefield.
    // <b>英雄技能</b>从你的牌库中将两个随从置入战场；对手将一个随从置入战场。 
    {
        // 定义要召唤的随从卡牌（这里使用的是蛛魔随从）
        CardDB.Card minion = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_007t);

        // 重写卡牌使用效果方法，这是干杯！英雄模式技能的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 首先从己方牌库召唤一个随从（双方都会执行）
            if (p.ownDeckSize > 0)
            {
                // 在己方战场末尾召唤随从
                p.callKid(minion, p.ownMinions.Count, true, false);
                p.ownDeckSize--; // 减少己方牌库数量
            }

            // 然后从敌方牌库召唤一个随从（双方都会执行）
            if (p.enemyDeckSize > 0)
            {
                // 在敌方战场末尾召唤随从
                p.callKid(minion, p.enemyMinions.Count, false, false);
                p.enemyDeckSize--; // 减少敌方牌库数量
            }

            // 根据使用技能的玩家身份召唤额外的随从
            if (ownplay) // 如果是己方使用技能
            {
                // 己方再召唤一个随从（总共召唤两个己方随从）
                if (p.ownDeckSize > 0)
                {
                    p.callKid(minion, p.ownMinions.Count, true, false);
                    p.ownDeckSize--; // 再次减少己方牌库数量
                }
            }
            else // 如果是敌方使用技能
            {
                // 敌方再召唤一个随从（总共召唤两个敌方随从）
                if (p.enemyDeckSize > 0)
                {
                    p.callKid(minion, p.enemyMinions.Count, false, false);
                    p.enemyDeckSize--; // 再次减少敌方牌库数量
                }
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // 1. REQ_NUM_MINION_SLOTS - 至少需要一个随从位置
            // 这意味着场上必须有至少一个随从位置才能使用这个英雄技能
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
    }
}