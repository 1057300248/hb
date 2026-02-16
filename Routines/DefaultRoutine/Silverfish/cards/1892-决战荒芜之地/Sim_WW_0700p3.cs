using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//英雄技能 中立 费用：2
	//Fire Bullet
	//火焰枪弹
	//Deal $2 damage,then deal $1 damage toall enemy minions.Swaps each turn.
	//造成$2点伤害，再对所有敌方随从造成$1点伤害。每回合切换。
	class Sim_WW_0700p3 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);
            int dmg1 = (ownplay) ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);

            List<Minion> temp = (ownplay) ? p.enemyMinions : p.ownMinions;
            p.minionGetDamageOrHeal(target, dmg);
            foreach (Minion m in temp)
            {
                if (m.entitiyID != target.entitiyID)
                {
                    m.getDamageOrHeal(dmg1, p, true, false); // isMinionAttack=true because it is extra damage (we calc clear lostDamage)
                }
            }
            if (ownplay)
            {
                if (p.enemyHero.entitiyID != target.entitiyID)
                {
                    p.minionGetDamageOrHeal(p.enemyHero, dmg1);
                }
                if (Hrtprozis.Instance.enemyMinions.Count > p.enemyMinions.Count) p.lostDamage += dmg1;
            }
            else
            {
                if (p.ownHero.entitiyID != target.entitiyID)
                {
                    p.minionGetDamageOrHeal(p.ownHero, dmg1);
                }
                if (Hrtprozis.Instance.ownMinions.Count > p.ownMinions.Count) p.lostDamage += dmg1;
            }
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
