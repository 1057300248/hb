using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0 种族：无效的 卡牌稀有度：无效的
	//Living Nightmare
	//活体梦魇
	//When you play this minion, summon a 2/2 copy of it.
	//当你使用本牌时，召唤一个它的2/2的复制。
	class Sim_EDR_100t5 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                bool source = own.own;
                own.setMinionToMinion(target);
                own.own = source;
                own.Angr = 2;
                own.Hp = 2;
                own.maxHp = 2;
            }
        }
		
	}
}
