using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 牧师 费用：1
	//Tar Slick
	//焦油飞溅
	//Minions take double damage this turn.Deal $1 damage.
	//在本回合中，随从受到的伤害翻倍。造成$1点伤害。
	class Sim_TTN_726 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			p.tarslick++;
			int dmg = (ownplay) ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
            p.minionGetDamageOrHeal(target, dmg);
		}
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
				new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET), // 需要敌方目标
			};
		}
	}
}
