using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 战士 费用：1
	//Siphoning Growth
	//虹吸生长
	//Destroy a friendlyminion to gain 8 Armor.
	//消灭一个友方随从以获得8点护甲值。
	class Sim_EDR_531 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (target != null && target.own)
            {
                p.minionGetDestroyed(target); // 消灭目标友方随从
            }

			if (ownplay)
            {
                p.minionGetArmor(p.ownHero, 8);
            }
            else
            {
                p.minionGetArmor(p.enemyHero, 8);
            }
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
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
