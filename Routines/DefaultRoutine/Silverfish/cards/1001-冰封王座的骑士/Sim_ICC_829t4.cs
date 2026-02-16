using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 圣骑士 费用：2 攻击力：2 生命值：2
	//Inquisitor Whitemane
	//检察官怀特迈恩
	//
	//
	class Sim_ICC_829t4 : SimTemplate
	{
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            if (turnEndOfOwner == triggerEffectMinion.own)
            {
                p.minionGetDamageOrHeal(p.enemyHero, 7);
            }
        }

    }
}
