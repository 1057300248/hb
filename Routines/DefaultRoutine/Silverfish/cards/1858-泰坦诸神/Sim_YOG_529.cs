using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 德鲁伊 费用：0
	//Forbidden Fruit
	//禁忌之果
	//[x]Spend all your Mana.<b>Choose One -</b> Gain thatmuch Attack this turn; ortwice as much Armor.
	//消耗你所有的法力值。<b>抉择：</b>获得在本回合中的等量攻击力；或者获得双倍的护甲值。
	class Sim_YOG_529 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			if (p.mana > 0)
			{
				if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
				{
					p.minionGetTempBuff(ownplay ? p.ownHero : p.enemyHero, p.mana, 0);
				}
				if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
				{
					p.minionGetArmor(p.ownHero, 2*p.mana);
				}
				p.mana = 0;
			}
        }
	}
}
