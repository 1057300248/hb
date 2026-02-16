using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_AV_661 : SimTemplate //* 征战平原 fieldofstrife
    {
        //你的随从获得+1攻击力。持续3回合。
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.allMinionOfASideGetBuffed(ownplay, 1, 0);
        }
    }
}
