using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 术士 费用：4
	//Fine Print
	//合约细则
	//Deal $4 damage to all_minions.<i>(Excess damage hits your_hero.)</i>
	//对所有随从造成$4点伤害。<i>（超过其生命值的伤害会命中你的英雄。）</i>
	class Sim_WORK_008 : SimTemplate
	{
		 public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = ownplay ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4); // 计算法术伤害

            List<Minion> allMinions = new List<Minion>(p.ownMinions);
            allMinions.AddRange(p.enemyMinions);  // 获取场上所有随从

            int excessDamage = 0;  // 用于统计溢出的伤害

            // 对所有随从进行伤害计算
            foreach (Minion m in allMinions)
            {
                int actualDamage = Math.Min(m.Hp, dmg);  // 计算该随从能承受的实际伤害
                excessDamage += dmg > m.Hp ? dmg - m.Hp : 0;  // 统计超出随从生命值的伤害
                p.minionGetDamageOrHeal(m, dmg);  // 对随从造成伤害
            }

            // 如果有溢出伤害，则对自己的英雄造成该溢出伤害
            if (excessDamage > 0)
            {
                if (ownplay)
                {
                    p.minionGetDamageOrHeal(p.ownHero, excessDamage);  // 对己方英雄造成溢出伤害
                }
                else
                {
                    p.minionGetDamageOrHeal(p.enemyHero, excessDamage);  // 对敌方英雄造成溢出伤害（AI控制）
                }
            }
        }
		
	}
}
