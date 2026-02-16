using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 潜行者 费用：1
	//Deafen
	//致聋术
	//<b>Silence</b> a minion. <b>Combo:</b> Also deal $2 damage to it.
	//<b>沉默</b>一个随从。<b>连击：</b>并对其造成$2点伤害。
	class Sim_JAM_022 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			int dmg = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);
			p.minionGetSilenced(target);
			if(ownplay && p.cardsPlayedThisTurn >= 1)
			{
				p.minionGetDamageOrHeal(target, dmg);
			}
		}
		
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}
