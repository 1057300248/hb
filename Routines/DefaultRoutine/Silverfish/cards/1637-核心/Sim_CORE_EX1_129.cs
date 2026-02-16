using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 牧师 费用：2
	//Fan of Knives
	//刀扇
	//Deal $1 damage to all enemy minions. Draw_a card.
	//对所有敌方随从造成$1点伤害，抽一张牌。
	class Sim_CORE_EX1_129 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            int dmg = (ownplay) ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
            p.allMinionOfASideGetDamage(!ownplay, dmg);
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
		}
		
	}
}
