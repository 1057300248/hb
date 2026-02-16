using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//英雄技能 恶魔猎手 费用：1
	//Demon Claws
	//恶魔之爪
	//[x]<b>Hero Power</b>+$a1 Attack this turn.
	//<b>英雄技能</b>在本回合中+$a1攻击力。
	class Sim_HERO_10ashp : SimTemplate
	{
		
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            //[x]<b>Hero Power</b>+2 Attack this turn.
            //<b>英雄技能</b>在本回合中获得+1攻击力。
            var hero = ownplay ? p.ownHero : p.enemyHero;
            p.minionGetTempBuff(hero, 1, 0);
            hero.updateReadyness();
        }
	}
}
