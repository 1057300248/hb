using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 巫妖王 费用：0
	//Horn of Winter
	//寒冬号角
	//Refresh 2 Mana Crystals.
	//复原两个法力水晶。
	class Sim_CORE_RLK_042 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			p.mana = Math.Min(p.mana + 2, p.ownMaxMana);
		}
		
	}
}
