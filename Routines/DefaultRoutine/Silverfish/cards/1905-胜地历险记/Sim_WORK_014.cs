using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 恶魔猎手 费用：2
	//Demonic Deal
	//恶魔交易
	//[x]<b>Lifesteal</b>. Deal $4 damageto a minion. Put a randomDemon that costs (5) ormore on top of your deck.
	//<b>吸血</b>对一个随从造成$4点伤害。将一张法力值消耗大于或等于（5）点的随机恶魔牌置于你的牌库顶。
	class Sim_WORK_014 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = ownplay ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4); // 计算法术伤害

            // 对目标随从造成伤害
            p.minionGetDamageOrHeal(target, dmg);

            // 吸血效果：将造成的伤害转化为对己方英雄的治疗
            if (ownplay)
            {
                p.minionGetDamageOrHeal(p.ownHero, -dmg); // 治疗己方英雄，负值表示治疗
            }
            else
            {
                p.minionGetDamageOrHeal(p.enemyHero, -dmg); // 治疗敌方英雄，负值表示治疗（如果AI使用此卡）
            }
        }
		
	}
}
