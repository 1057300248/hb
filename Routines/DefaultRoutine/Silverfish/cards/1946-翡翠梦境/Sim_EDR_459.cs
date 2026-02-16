using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 战士 费用：4 攻击力：6 生命值：6
	//Afflicted Devastator
	//受难的毁灭者
	//[x]<b>Battlecry:</b> Deal 3 damageto all other friendly minions.<b>Deathrattle:</b> Deal 3 damageto all enemy minions.
	//<b>战吼：</b>对所有其他友方随从造成3点伤害。<b>亡语：</b>对所有敌方随从造成3点伤害。
	class Sim_EDR_459 : SimTemplate
	{
		// 战吼效果：对所有其他友方随从造成3点伤害
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			foreach (Minion m in p.ownMinions)
			{
				if (m.entitiyID != own.entitiyID) // 跳过自身
				{
					p.minionGetDamageOrHeal(m, 3);
				}
			}
		}

		// 亡语效果：对所有敌方随从造成3点伤害
		public override void onDeathrattle(Playfield p, Minion m)
		{
			foreach (Minion enemy in p.enemyMinions)
			{
				p.minionGetDamageOrHeal(enemy, 3);
			}
		}
	}
}
