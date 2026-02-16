using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 术士 费用：2
	//Deadline
	//最后期限
	//<b>Tradeable</b>, <b>Temporary</b>Destroy a minion.
	//<b>可交易</b>，<b>临时</b>消灭一个随从。
	class Sim_WORK_007 : SimTemplate
	{
		 public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 如果有目标，消灭该目标随从
            if (target != null)
            {
                p.minionGetDestroyed(target);
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET)    // 目标必须是随从
            };
        }
		
	}
}
