using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 德鲁伊 费用：0
	//Reject Temptation
	//拒绝诱惑
	//[x]Spend all your Mana.Gain twice asmuch Armor.
	//消耗你所有的法力值，获得双倍的护甲值。
	class Sim_YOG_529b : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			if (ownplay)
			{
				if (p.mana > 0)
                {
					p.minionGetArmor(p.ownHero, 2*p.mana); //消耗你所有的法力值，获得双倍的护甲值。
					p.mana = 0;
				}
            }
        }
		
	}
}
