
using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_TOY_330t5 : SimTemplate
    {
        public override void onAuraStarts(Playfield p, Minion own)
        {
            if (own.own)
            {
                p.奇利亚斯加攻击++;
                foreach (Minion m in p.ownMinions)
                {
                    if (own.entitiyID != m.entitiyID) p.minionGetBuffed(m, 1, 0);
                }
            }
        }

        public override void onAuraEnds(Playfield p, Minion own)
        {
            if (own.own)
            {
                p.奇利亚斯加攻击--;
                foreach (Minion m in p.ownMinions)
                {
                    if (own.entitiyID != m.entitiyID) p.minionGetBuffed(m, -1, 0);
                }
            }
        }
    }
}
