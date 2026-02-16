using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 战士 费用：5
    //Bridge Riff
    //过渡乐句
    //[x]Summon a 3/4 Rocker with<b>Taunt</b> and a 4/3 with <b>Rush</b>.<b>Finale:</b> Play your last Riff.
    //召唤一个3/4并具有<b>嘲讽</b>的乐手和一个4/3并具有<b>突袭</b>的乐手。<b>压轴：</b>演奏你的上一个乐句。
    class Sim_ETC_365 : SimTemplate
    {
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ETC_365t);
        CardDB.Card kid2 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ETC_365t2);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
            p.callKid(kid2, pos, ownplay, false);
            p.callKid(kid, pos, ownplay, false);

            if (p.ownMinions.Count < 6 && p.mana == 0 && Probabilitymaker.Instance.ownGraveyard.ContainsKey(CardDB.cardIDEnum.ETC_365))
            {

                p.callKid(kid2, pos, ownplay, false);
                p.callKid(kid, pos, ownplay, false);
            }
            else
                p.evaluatePenality += 70;
        }
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 2),
            };
        }
    }
}
