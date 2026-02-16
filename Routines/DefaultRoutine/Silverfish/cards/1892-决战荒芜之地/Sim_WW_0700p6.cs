using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//英雄技能 中立 费用：2
	//Shadow Bullet
	//暗影枪弹
	//Deal $2 damage.Summon a random3-Cost minion.Swaps each turn.
	//造成$2点伤害。随机召唤一个法力值消耗为（3）的随从。每回合切换。
	class Sim_WW_0700p6 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			// 造成$2点伤害
			int dmg = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);
			p.minionGetDamageOrHeal(target, dmg);
			int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
			p.callKid(p.getRandomCardForManaMinion(3), pos, ownplay);
		}
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
		
	}
}
