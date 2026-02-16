using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 骑乘迅猛龙（Mounted Raptor）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_050 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 随机获取一张1费随从卡牌
            CardDB.Card oneCostMinion = getRandomOneCostMinion();

            // 召唤随机1费随从
            if (oneCostMinion != null)
            {
                p.callKid(oneCostMinion, m.zonepos - 1, m.own);
            }
        }

        /// <summary>
        /// 获取一张随机的1费随从卡牌。
        /// </summary>
        /// <returns>随机的1费随从卡牌，如果未找到则返回null。</returns>
        private CardDB.Card getRandomOneCostMinion()
        {
            List<CardDB.Card> oneCostMinions = new List<CardDB.Card>();

            // 遍历所有卡牌ID，查找1费随从
            foreach (CardDB.cardIDEnum cardId in Enum.GetValues(typeof(CardDB.cardIDEnum)))
            {
                CardDB.Card card = CardDB.Instance.getCardDataFromID(cardId);
                if (card != null && card.type == CardDB.cardtype.MOB && card.cost == 1)
                {
                    oneCostMinions.Add(card);
                }
            }

            // 如果找到1费随从，则随机选择一张
            if (oneCostMinions.Count > 0)
            {
                Random random = new Random();
                return oneCostMinions[random.Next(oneCostMinions.Count)];
            }

            // 未找到1费随从，返回null
            return null;
        }
    }
}