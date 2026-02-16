using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 恶魔猎手 费用：4 攻击力：2 生命值：6
	//Lurker
	//潜伏者
	//[x]After a friendly minionattacks, deal 1 damage to arandom enemy <i>(or 2 if yourminion is a Zerg)</i>.
	//在一个友方随从攻击后，随机对一个敌人造成1点伤害<i>（如果你的随从为异虫则为2点）</i>。
	class Sim_SC_009 : SimTemplate
	{
        public override void onMinionAttacks(Playfield p, Minion triggerEffectMinion, Minion attackingMinion)
        {
            if (triggerEffectMinion.own == attackingMinion.own)
            {
                Minion target = null;

                if (triggerEffectMinion.own)
                {
                    target = p.getEnemyCharTargetForRandomSingleDamage(1);
                    p.evaluatePenality -= 1;
                }
                else
                {
                    target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack);
                    if (target == null) target = p.ownHero;
                }

                // 根据攻击随从是否为异虫调整伤害值
                int damage = attackingMinion.zerg ? 2 : 1;
                p.minionGetDamageOrHeal(target, damage, true);
            }
        }
    }
}
