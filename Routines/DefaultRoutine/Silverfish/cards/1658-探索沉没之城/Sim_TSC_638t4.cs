
using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_TSC_638t4 : SimTemplate //* 食人鱼集群 Piranha Swarmer
                                      //&lt;b&gt;突袭&lt;/b&gt;在你召唤一个食人鱼集群后，获得+1攻击力。
                                      //&lt;b&gt;Rush&lt;/b&gt;After you summon a PiranhaSwarmer, gain +1 Attack.
    {
        public override void onMinionWasSummoned(Playfield p, Minion m, Minion summonedMinion)
        {
            if (m.handcard.card.nameCN == CardDB.cardNameCN.食人鱼集群 && summonedMinion.own == m.own)
            {
                p.minionGetBuffed(m, 1, 0);
            }
        }
    }

}
