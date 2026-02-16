using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 饥饿的巨龙卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为4点，攻击力5，生命值6
    // 卡牌效果：<b>战吼：</b>为你的对手随机召唤一个法力值消耗为（1）的随从。
    class Sim_BRM_026 : SimTemplate //* 饥饿的巨龙 Hungry Dragon
    // <b>Battlecry:</b> Summon a random 1-Cost minion for_your opponent.
    // <b>战吼：</b>为你的对手随机召唤一个法力值消耗为（1）的随从。 
    {
        // 定义要召唤的1费随从卡牌（这里使用了默认值，实际应为随机1费随从）
        CardDB.Card oneCostMinion = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_614t);

        // 重写战吼效果方法，这是饥饿的巨龙卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 确定召唤位置（在对手的随从队列末尾）
            int summonPosition = (m.own) ? p.enemyMinions.Count : p.ownMinions.Count;

            // 为对手召唤一个1费随从
            // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤（这里是对手，所以是!m.own）
            p.callKid(oneCostMinion, summonPosition, !m.own);
        }
    }
}