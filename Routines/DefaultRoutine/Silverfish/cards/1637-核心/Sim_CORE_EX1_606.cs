using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 战士 费用：2
	//Shield Block
	//盾牌格挡
	//Gain 5 Armor.Draw a card.
	//获得5点护甲值。抽一张牌。
	class Sim_CORE_EX1_606 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            if (ownplay)
            {
                p.minionGetArmor(p.ownHero, 5);
            }
            else
            {
                p.minionGetArmor(p.enemyHero, 5);
            }
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
		}
		
	}
}
