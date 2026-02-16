using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：4 攻击力：2 生命值：3
    //Azerite Chain Gang
    //艾泽里特苦囚
    //[x]<b>Taunt</b><b>Battlecry and Quickdraw:</b>Summon a copy of this.
    //<b>嘲讽</b>。<b>战吼，快枪：</b>召唤一个本随从的复制。
    class Sim_WW_360 : SimTemplate
    {
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.WW_360);

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (p.ownMinions.Count < 7)
            {
                p.callKid(kid, own.zonepos, own.own);
            }

            foreach (Minion m in p.ownMinions)
            {
                if (m.handcard.card.cardIDenum == CardDB.cardIDEnum.WW_360)
                {
                    p.minionSetAngrToX(m, own.Angr);
                    p.minionSetLifetoX(m, own.Hp);
                }
            }
        }
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, bool outcast)
        {
            int place = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
            p.callKid(kid, place, true);
        }
    }
}
