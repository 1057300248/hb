using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 巫妖王 费用：4
	//Eternal Layover
	//无限滞留
	//[x]Give ALL minions <b>Reborn</b>,then destroy all minions.
	//使所有随从获得<b>复生</b>，然后消灭所有随从。
	class Sim_WORK_028 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 赋予所有随从 "复生" 效果
            List<Minion> allMinions = new List<Minion>(p.ownMinions);
            allMinions.AddRange(p.enemyMinions);

            foreach (Minion m in allMinions)
            {
                if (!m.silenced && !m.reborn)
                {
                    m.reborn = true; // 给随从赋予复生效果
                }
            }

            // 然后消灭所有随从
            foreach (Minion m in allMinions)
            {
                p.minionGetDestroyed(m); // 消灭每个随从
            }
        }
    }
}
