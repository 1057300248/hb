using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //地标 无效的 费用：1
    //Spawning Pool
    //孵化池
    //Get a 1/1 Zergling. <b>Deathrattle:</b> Your Zerg minions have <b>Rush</b> this turn.
    //获取一张1/1的跳虫。<b>亡语：</b>在本回合中，你的异虫随从拥有<b>突袭</b>。
    class Sim_SC_000 : SimTemplate
    {
        // 使用地标时的效果
        public override void useLocation(Playfield p, Minion triggerMinion, Minion target)
        {
            if (triggerMinion.handcard.card.CooldownTurn == 0)
            {
                p.drawACard(CardDB.cardIDEnum.SC_010, true);
            }
        }
        public override void onDeathrattle(Playfield p, Minion own)
        {
            p.孵化池突袭 = true;
            foreach (Minion m in p.ownMinions)
            {
                if (m.zerg)
                {
                    p.minionGetRush(m);
                }               
            }
        }
    }
}
