using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 恶魔猎手 费用：4
	//Going Down Swinging
	//低沉摇摆
	//[x]Give your hero +2 Attackand <b>Immune</b> this turn, then attack each enemy minion.
	//在本回合中，使你的英雄获得+2攻击力和<b>免疫</b>，然后攻击每个敌方随从。
	class Sim_ETC_413 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            if (ownplay)
            {
                p.minionGetTempBuff(p.ownHero, 2, 0);
				p.ownHero.immune = true;
				foreach(Minion m in p.enemyMinions)
				{
					p.minionGetDamageOrHeal(m, p.ownHero.Angr);
				}
            }
            else
            {
                p.minionGetTempBuff(p.enemyHero, 2, 0);
				p.enemyHero.immune = true;
				foreach(Minion m in p.ownMinions)
				{
					p.minionGetDamageOrHeal(m, p.enemyHero.Angr);
				}
            }
		}
	}
}
