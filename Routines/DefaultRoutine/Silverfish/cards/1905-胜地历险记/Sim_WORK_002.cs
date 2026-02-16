using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 圣骑士 费用：2 攻击力：3 生命值：2
	//Busy-Bot
	//忙碌机器人
	//<b>Battlecry:</b> Give your1-Attack minions +1/+1.
	//<b>战吼：</b>使你攻击力为1的随从获得+1/+1。
	class Sim_WORK_002 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion ownMinion, Minion target, int choice)
        {
            // 遍历己方场上的随从
            foreach (Minion m in (ownMinion.own ? p.ownMinions : p.enemyMinions))
            {
                // 如果随从的攻击力为1，给它 +1/+1
                if (m.Angr == 1)
                {
                    p.minionGetBuffed(m, 1, 1); // 增加1攻击力和1生命值
                }
            }
        }
		
	}
}
