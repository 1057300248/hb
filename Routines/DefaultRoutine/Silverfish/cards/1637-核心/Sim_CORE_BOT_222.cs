using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 术士 费用：1
	//Spirit Bomb
	//灵魂炸弹
	//Deal $4 damage to a minion and your hero.
	//对一个随从和你的英雄各造成$4点伤害。
	class Sim_CORE_BOT_222 : SimTemplate
	{
		//Deal $4 damage to a minion and your hero.
        //对一个随从和你的英雄各造成$4点伤害。
       public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = p.getSpellDamageDamage(4);

            // 对己方英雄造成伤害
            p.minionGetDamageOrHeal(p.ownHero, dmg);
			//对随从造成伤害
			p.minionGetDamageOrHeal(target, dmg);
        }
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
		
	}
}
