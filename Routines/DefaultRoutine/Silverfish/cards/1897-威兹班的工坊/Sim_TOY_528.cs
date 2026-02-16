
using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_TOY_528 : SimTemplate // 避免报错
    {
        //你的英雄技能会触发两次。
        public override void onAuraStarts(Playfield p, Minion own)
        {
            if (own.own) p.友方技能额外触发次数++;
            else p.enemyBrannBronzebeard++;
        }

        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own) p.友方技能额外触发次数--;
            else p.enemyBrannBronzebeard--;
        }
    }
}
