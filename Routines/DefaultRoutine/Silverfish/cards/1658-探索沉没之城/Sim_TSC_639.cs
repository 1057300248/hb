using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 萨满祭司 费用：7 攻击力：3 生命值：5
    //Glugg the Gulper
    //暴食巨鳗格拉格
    //[x]<b>Colossal +3</b> After a friendly minion dies,gain its original stats.
    //<b>巨型+3</b>在一个友方随从死亡后，获得其原始属性值。
    class Sim_TSC_639 : SimTemplate
    {
        CardDB.Card kid1 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TSC_639t);
        CardDB.Card kid2 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TSC_639t2);
        CardDB.Card kid3 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TSC_639t3);
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.callKid(kid1, own.zonepos, own.own);
            p.callKid(kid2, own.zonepos + 1, own.own);
            p.callKid(kid3, own.zonepos + 2, own.own);
        }


        public override void onMinionDiedTrigger(Playfield p, Minion checkdm, Minion diedMinion)
        {
            int diedMinions = (diedMinion.own) ? p.tempTrigger.ownMinionsDied : p.tempTrigger.enemyMinionsDied;
            if (diedMinions == 0) return;
            int residual = (p.pID == diedMinion.pID) ? diedMinions - diedMinion.extraParam2 : diedMinions;
            diedMinion.pID = p.pID;
            diedMinion.extraParam2 = diedMinions;
            for (int i = 0; i < residual; i++)
            {
                p.minionGetBuffed(checkdm, diedMinion.handcard.card.Attack, diedMinion.handcard.card.Health);
            }
        }
    }
}
