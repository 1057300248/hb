using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：1
	//Snake Oil
	//蛇油
	//<b>Tradeable</b>Deal $0 damage.<i>This seems useless...</i>
	//<b>可交易</b>造成$0点伤害。<i>好像没什么用……</i>
	class Sim_WW_331t : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			int dmg = ownplay ? p.getSpellDamageDamage(0) : p.getEnemySpellDamageDamage(0);   
			p.minionGetDamageOrHeal(target, dmg);
		}

		public override PlayReq[] GetPlayReqs()
		{
			return new PlayReq[] {
				new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
				new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
			};
		}
	}
}
