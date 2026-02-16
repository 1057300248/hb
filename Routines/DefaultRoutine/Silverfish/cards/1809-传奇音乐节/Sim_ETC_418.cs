using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：2 攻击力：1 生命值：2
	//Instrument Tech
	//乐器技师
	//<b>Battlecry:</b> Draw a weapon.
	//<b>战吼：</b>抽一张武器牌。
	class Sim_ETC_418 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			p.drawACard(CardDB.cardIDEnum.GDB_726, true);
		}
		
	}
}
