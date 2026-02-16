using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 集合石（Summoning Stone）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_086 : SimTemplate
    {
        /// <summary>
        /// 当卡牌即将被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="hc">即将使用的卡牌。</param>
        /// <param name="wasOwnCard">是否为当前玩家使用此卡牌。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查是否为法术卡牌，并且是己方使用
            if (hc.card.type == CardDB.cardtype.SPELL && triggerEffectMinion.own == wasOwnCard)
            {
                // 获取随机同费用随从
                CardDB.Card randomMinion = getRandomMinionWithCost(hc.manacost);
                if (randomMinion != null)
                {
                    int pos = wasOwnCard ? p.ownMinions.Count : p.enemyMinions.Count;
                    p.callKid(randomMinion, pos, wasOwnCard);
                }
            }
        }

        /// <summary>
        /// 获取指定费用的随机随从。
        /// </summary>
        /// <param name="cost">目标费用。</param>
        /// <returns>随机随从卡牌，如果未找到则返回null。</returns>
        private CardDB.Card getRandomMinionWithCost(int cost)
        {
            List<CardDB.Card> minionsWithCost = new List<CardDB.Card>();

            // 遍历所有卡牌ID，查找指定费用的随从
            foreach (CardDB.cardIDEnum cardId in Enum.GetValues(typeof(CardDB.cardIDEnum)))
            {
                CardDB.Card card = CardDB.Instance.getCardDataFromID(cardId);
                if (card != null && card.type == CardDB.cardtype.MOB && card.cost == cost)
                {
                    minionsWithCost.Add(card);
                }
            }

            // 如果找到符合条件的随从，则随机选择一张
            if (minionsWithCost.Count > 0)
            {
                Random random = new Random();
                return minionsWithCost[random.Next(minionsWithCost.Count)];
            }

            // 未找到符合条件的随从，返回null
            return null;
        }
    }
}