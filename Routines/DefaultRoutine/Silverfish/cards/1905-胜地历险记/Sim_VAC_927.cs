using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 恶魔猎手 费用：2 攻击力：2 生命值：2
	//Adrenaline Fiend
	//狂飙邪魔
	//After a friendly Pirate attacks, give your hero+1 Attack this turn.
	//在一个友方海盗攻击后，使你的英雄在本回合中获得+1攻击力。
	class Sim_VAC_927 : SimTemplate
	{
        public override void onMinionAttacks(Playfield p, Minion triggerEffectMinion, Minion attackingMinion)
        {
            // 检查攻击者是否为友方海盗
            if (attackingMinion.own && (TAG_RACE)attackingMinion.handcard.card.race == TAG_RACE.PIRATE)
            {
                p.minionGetTempBuff(p.ownHero, 1, 0);
            }
        }
    }
}
