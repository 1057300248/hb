using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 潜行者 费用：2 攻击力：4 生命值：4
	//Envoy of Prosperity
	//暴富特使
	//<b>Battlecry:</b> Put the highest Cost card in your hand on top of your deck.
	//<b>战吼：</b>将你手牌中法力值消耗最高的牌置于你的牌库顶。
	class Sim_WORK_031 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            List<Handmanager.Handcard> handcards = own.own ? p.owncards : p.enemyHand;

            // 寻找手牌中法力值消耗最高的牌
            Handmanager.Handcard highestCostCard = null;
            foreach (Handmanager.Handcard hc in handcards)
            {
                if (highestCostCard == null || hc.manacost > highestCostCard.manacost)
                {
                    highestCostCard = hc; // 找到当前法力值消耗最高的牌
                }
            }

            if (highestCostCard != null)
            {
                // 将法力值消耗最高的牌从手牌移除并放置到牌库顶
                p.removeCard(highestCostCard); // 从手牌移除这张牌
                if (own.own)
                {
                    p.AddToDeck(highestCostCard.card); // 将这张牌放入己方牌库顶
                }
            }
        }
    }
}
