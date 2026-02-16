using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 猎人 费用：2
	//Amphibian's Spirit
	//两栖之灵
	//[x]Give a minion +2/+2and "<b>Deathrattle:</b> Give afriendly minion +2/+2and this <b>Deathrattle</b>."
	//使一个随从获得+2/+2和“<b>亡语：</b>使一个友方随从获得+2/+2以及此<b>亡语</b>。”
	class Sim_EDR_261 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{            
            p.minionGetBuffed(target, 2, 2);
            if (ownplay)
            {
                target.souloftheforest++;
            }
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
