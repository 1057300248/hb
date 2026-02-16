using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 猎人 费用：2 攻击力：2 生命值：3
	//Terrorscale Stalker
	//恐鳞追猎者
	//<b>Battlecry:</b> Trigger a friendly minion's <b>Deathrattle</b>.
	//<b>战吼：</b>触发一个友方随从的<b>亡语</b>。
	class Sim_CORE_UNG_800 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null) p.doDeathrattles(new List<Minion>() { target });
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_DEATHRATTLE),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
		
	}
}
