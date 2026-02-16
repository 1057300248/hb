using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 无效的 费用：4
	//Chrono Boost
	//时空提速
	//Draw 2 Protoss cards. Summon a 3/4 Zealot with <b>Charge</b>.
	//抽两张星灵牌。召唤一个3/4并具有<b>冲锋</b>的狂热者。
	class Sim_SC_750 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			p.drawACard(CardDB.cardIDEnum.None, ownplay);
			p.drawACard(CardDB.cardIDEnum.None, ownplay);
			CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.SC_751t);
		}
		
	}
}
