using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0 种族：无效的 卡牌稀有度：无效的
	//Rude Awakening
	//粗暴唤醒
	//[x]This minion's<b>Battlecries</b> trigger twice.
	//本随从的<b>战吼</b>会触发两次。
	class Sim_EDR_100t7 : SimTemplate
	{
		public override void onAuraStarts(Playfield p, Minion own)
		{
            if (own.own) p.ownBrannBronzebeard++;
            else p.enemyBrannBronzebeard++;
		}

        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own) p.ownBrannBronzebeard--;
            else p.enemyBrannBronzebeard--;
        }
		
	}
}
