using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    // 法术 猎人 费用：2
    // Evolution Chamber
    // 进化腔
    // Give your minions +1 Attack. Give your Zerg an extra +1/+1.
    // 使你的随从获得 +1 攻击力，使你的异虫额外获得 +1/+1。

    class Sim_SC_021 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;
            foreach (Minion m in temp)
            {
                p.minionGetBuffed(m, 1, 0);
                if (m.zerg) p.minionGetBuffed(m, 1, 1);
            }
        }
    }
}