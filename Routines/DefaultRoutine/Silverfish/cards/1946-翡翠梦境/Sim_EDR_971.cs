using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：1 攻击力：2 生命值：2
	//Critter Caretaker
	//小动物看护者
	//At the end of your turn, restore #3 Health to both_heroes.
	//在你的回合结束时，为双方英雄各恢复#3点生命值。
	class Sim_EDR_971 : SimTemplate
	{
		public override void onTurnEndsTrigger(Playfield p, Minion m, bool turnEndOfOwner)
		{
			// 只在卡牌所有者的回合结束时触发
			if (turnEndOfOwner == m.own && !m.silenced)
			{
				// 为双方英雄各恢复3点生命值
				p.minionGetDamageOrHeal(p.ownHero, -3); // 负数表示恢复生命值
				p.minionGetDamageOrHeal(p.enemyHero, -3);
			}
		}
	}
}
