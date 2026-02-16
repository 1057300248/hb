using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//武器 恶魔猎手 费用：3 攻击力：3 耐久度：3
	//Infernal Stapler
	//狱火订书器
	//After your hero attacks, deal 3 damage to your hero.
	//在你的英雄攻击后，对你的英雄造成3点伤害。
	class Sim_WORK_016 : SimTemplate
	{
		public override void onHeroattack(Playfield p, Minion own, Minion target)
        {
            // 检查是否为己方英雄攻击
            if (own.isHero && own.own)
            {
                // 对己方英雄造成3点伤害
                p.minionGetDamageOrHeal(p.ownHero, 3);
            }
            else if (own.isHero && !own.own)
            {
                // 对敌方英雄造成3点伤害（AI控制）
                p.minionGetDamageOrHeal(p.enemyHero, 3);
            }
        }
		
	}
}
