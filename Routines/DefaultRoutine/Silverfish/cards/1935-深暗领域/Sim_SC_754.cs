using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//英雄 无效的 费用：7
	//Artanis
	//阿塔尼斯
	//[x]<b>Battlecry:</b> Summon two3/4 Zealots with <b>Charge</b>.Your Protoss minions cost(2) less this game.
	//<b>战吼：</b>召唤两个3/4并具有<b>冲锋</b>的狂热者。在本局对战中，你的星灵随从牌的法力值消耗减少（2）点。
	class Sim_SC_754 : SimTemplate
	{
		CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.SC_751t);
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			p.setNewHeroPower(CardDB.cardIDEnum.SC_754p, ownplay); 
            if (ownplay) p.ownHero.armor += 5;
            else p.enemyHero.armor += 5;
	    }
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			p.protoss -= 2;
		}
		
	}
}
