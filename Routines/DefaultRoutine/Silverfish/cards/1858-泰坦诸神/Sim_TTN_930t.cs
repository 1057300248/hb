using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 德鲁伊 费用：3
	//Frost Lotus Blossom
	//雪莲成株
	//Draw 2 cards.Gain 10 Armor.
	//抽两张牌。获得10点护甲值。
	class Sim_TTN_930t : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			p.drawACard(CardDB.cardIDEnum.None, ownplay); // 抽第一张牌
			p.drawACard(CardDB.cardIDEnum.None, ownplay); // 抽第二张牌
             p.minionGetArmor(ownplay ? p.ownHero : p.enemyHero, 10); // 获得10点护甲值
		}
		
	}
}
