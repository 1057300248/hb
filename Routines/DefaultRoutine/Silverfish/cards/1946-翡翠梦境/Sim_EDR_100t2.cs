using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0 种族：无效的 卡牌稀有度：无效的
	//Short Claws
	//短小利爪
	//Costs (2) less,but has -2 Attack.
	//法力值消耗减少（2）点，但拥有-2攻击力。
	class Sim_EDR_100t2 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            if (ownplay)
            {
                p.playedPreparation = true;
				p.minionGetTempBuff(target, -2, 0);
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
