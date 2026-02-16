using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_099 : SimTemplate //* 我找到了 Eureka!
	{
        //Summon a copy of_a_random minion from your hand.
        //随机召唤你手牌中的一张随从牌的一个复制。

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 找到手牌中法力值消耗最高的随从
            Handmanager.Handcard highestCostMinion = null;
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.type == CardDB.cardtype.MOB)
                {
                    highestCostMinion = hc;
                }
            }
            if (highestCostMinion != null)
            {
                int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(highestCostMinion.card, pos, ownplay); // 召唤该随从
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}
