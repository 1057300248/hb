using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：9 种族：龙 卡牌稀有度：传说 攻击力：4 生命值：12
	//Ysera, Emerald Aspect
	//伊瑟拉，翡翠守护巨龙
	//[x]<b>Start of Game:</b> Increase bothplayers'maximum Mana by 5.<b>Battlecry:</b> Gain 3Mana Crystals.
	//<b>对战开始时：</b>双方玩家的法力值上限提高5点。<b>战吼：</b>获得3个法力水晶。
	class Sim_EDR_000 : SimTemplate
	{
		

		public virtual void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
			if (own.own)
			{
				if (p.ownMaxMana < 10)
				{
					p.ownMaxMana+=3;
					if(p.ownMaxMana > 10 ) p.ownMaxMana = 10;
                }
                else
                {
                    p.drawACard(CardDB.cardNameEN.excessmana, true, true);
                }

            }
            else
            {
                if (p.enemyMaxMana < 10)
                {
                    p.enemyMaxMana+=3;
					if(p.enemyMaxMana > 10 ) p.enemyMaxMana = 10;
                }
                else
                {
                    p.drawACard(CardDB.cardNameEN.excessmana, false, true);
                }
            }
		
		}
		
	}
}
