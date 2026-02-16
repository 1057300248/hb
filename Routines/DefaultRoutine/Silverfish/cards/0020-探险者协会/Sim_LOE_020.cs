using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 大漠沙驼（Desert Camel）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_020 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 从双方牌库中各召唤一个1费随从
            CardDB.Card oneCostMinion = CardDB.Instance.getCardData(CardDB.cardNameEN.unknown);
            p.callKid(oneCostMinion, p.ownMinions.Count, true);  // 己方召唤
            p.callKid(oneCostMinion, p.enemyMinions.Count, false); // 敌方召唤
        }
    }
}