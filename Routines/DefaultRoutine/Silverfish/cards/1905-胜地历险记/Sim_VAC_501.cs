using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 恶魔猎手 费用：5 攻击力：5 生命值：6
	//Aranna, Thrill Seeker
	//极限追逐者阿兰娜
	//[x]<b>Priest Tourist</b>Damage your hero takeson your turn is redirectedto a random enemy.
	//<b>牧师游客</b>你的英雄在你的回合中受到的伤害会转移给一个随机敌人。
	class Sim_VAC_501 : SimTemplate
	{
		
		public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            if (p.ownHero.anzGotDmg > 0 && m.own)
            {
                p.DealDamageToRandomCharacter(true, anzOwnMinionsGotDmg);
            }
            else if (p.enemyHero.anzGotDmg > 0 && !m.own)
            {
                p.DealDamageToRandomCharacter(false, anzEnemyMinionsGotDmg);
            }
        }
	}
}
