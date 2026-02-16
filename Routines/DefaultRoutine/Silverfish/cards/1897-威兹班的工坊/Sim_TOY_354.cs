using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 猎人 费用：4
	//R.C. Rampage
	//遥控狂潮
	//Summon six 1/1 Hounds. Any that can't fit give the others +1/+1.
	//召唤六只1/1的猎犬。每有一只放不下的猎犬，使其余猎犬获得+1/+1。
	class Sim_TOY_354 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int houndsToSummon = 6;
            List<Minion> targetZone = ownplay ? p.ownMinions : p.enemyMinions;
            int availableSpace = 7 - targetZone.Count;
            availableSpace = Math.Max(0, availableSpace); // 确保可用空间不为负数
            int houndsSummoned = Math.Min(houndsToSummon, availableSpace);
            int buffAmount = houndsToSummon - houndsSummoned;

            int beforeCount = targetZone.Count; // 记录召唤前的随从数量

            for (int i = 0; i < houndsSummoned; i++)
            {
                p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TOY_358t), targetZone.Count, ownplay);
            }

            if (buffAmount > 0)
            {
                foreach (Minion m in (ownplay ? p.ownMinions : p.enemyMinions))
                {
                    if (!m.Ready && m.nameCN == CardDB.cardNameCN.遥控猎犬)  // 确保只给猎犬加buff
                    {
                        p.minionGetBuffed(m, buffAmount, buffAmount);
                    }
                }
            }
        }
    }
}
