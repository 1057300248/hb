using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 牧师 费用：2
	//Web of Deception
	//欺诈之网
	//Return a friendly minion to your hand to summon a 4/4 Spider with <b>Stealth</b>.
	//将一个友方随从移回你的手牌以召唤一只4/4并具有<b>潜行</b>的蜘蛛。
	class Sim_EDR_523 : SimTemplate
	{
		CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EDR_523t);
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            // 将目标随从移回手牌
             if(target != null) p.minionReturnToHand(target, target.own, 0);
		}
		
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),    // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET)   // 目标必须是友方随从
            };
        }
	}
}
