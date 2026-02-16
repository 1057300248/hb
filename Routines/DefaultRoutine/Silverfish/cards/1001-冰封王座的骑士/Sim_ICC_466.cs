using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_ICC_466: SimTemplate //* 萨隆苦囚 Saronite Chain Gang
    {
        //[x]<b>Taunt</b><b>Battlecry:</b> Summon acopy of this minion.
        //<b>嘲讽</b><b>战吼：</b>召唤一个该随从的复制。
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ICC_466);

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (p.ownMinions.Count < 7)
            {
                p.callKid(kid, own.zonepos, own.own);
            }

            foreach (Minion m in p.ownMinions)
            {
                if (m.handcard.card.cardIDenum == CardDB.cardIDEnum.ICC_466)
                {
                    p.minionSetAngrToX(m, own.Angr);
                    p.minionSetLifetoX(m, own.Hp);
                }
            }
        }
    }
}
