
using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_WON_065 : SimTemplate // 避免报错
    {


        public override void onMinionWasSummoned(Playfield p, Minion ownMinion, Minion summonedMinion)
        {
            if (ownMinion.own) // 判断是否是自己的随从召唤
            {
                p.minionGetBuffed(summonedMinion, 0, 1); // 给予召唤的随从+1生命值
            }
            else
            {
                p.minionGetBuffed(summonedMinion, 0, 1); // 给予召唤的随从+1生命值
            }
        }

    }
}
