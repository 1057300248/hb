using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 战士 费用：0
	//Through Fel and Flames
	//突破邪火
	//Give a minion <b>Rush</b>. <b>Finale:</b> And +1/+1.
	//使一个随从获得<b>突袭</b>。<b>压轴：</b>以及+1/+1。
	class Sim_JAM_017 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			target.rush = 1;
			if(p.mana == 0)
			{
				target.Hp += 1;
				target.Angr += 1;
			}
		}
		
		public override PlayReq[] GetPlayReqs()
        {
			return new PlayReq[] {
				new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
				new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
				new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
			};
		}
}
}
