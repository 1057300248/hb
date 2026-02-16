using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：4 攻击力：3 生命值：5
    //Sleepy Resident
    //困倦的岛民
    //<b>Taunt</b><b>Deathrattle:</b> ALL other minions fall asleep.
    //<b>嘲讽</b>。<b>亡语：</b>所有其他随从陷入沉睡。
    class Sim_VAC_406 : SimTemplate
    {
        public override void onDeathrattle(Playfield p, Minion m)
        {
            foreach (Minion mi in p.ownMinions.ToArray())
            {
                if (mi.Ready)
                {
                    mi.Ready = false;
                    mi.numAttacksThisTurn = 1;
                }

            }

            foreach (Minion mi in p.enemyMinions.ToArray())
            {
                if (mi.Ready)
                {
                    mi.Ready = false;
                    mi.numAttacksThisTurn = 1;
                }
            }
        }
    }
}
