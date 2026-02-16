using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 战士 费用：3
	//Reserved Spot
	//预留泊位
	//[x]Give a random minionin your hand +4/+4.If it's the only one there,reduce its Cost by (2).
	//使你手牌中一张随机随从牌获得+4/+4。如果其为手牌中唯一的随从牌，其法力值消耗减少（2）点。
	class Sim_WORK_021 : SimTemplate
	{

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            List<Handmanager.Handcard> handMinions = new List<Handmanager.Handcard>();
            // 遍历手牌，找出所有随从牌
            foreach (Handmanager.Handcard hc in (ownplay ? p.owncards : p.enemyHand))
            {
                if (hc.card.type == CardDB.cardtype.MOB) // 仅检查随从牌
                {
                    handMinions.Add(hc);
                }
            }

            if (handMinions.Count == 0) return; // 如果手牌中没有随从牌，则什么都不做

            // 随机选择一张随从牌
            Handmanager.Handcard chosenMinion = handMinions[p.getRandomNumber(0, handMinions.Count - 1)];

            // 为选中的随从牌增加+4/+4
            chosenMinion.addattack += 4;
            chosenMinion.addHp += 4;

            // 如果手牌中只有一张随从牌，减少其法力消耗2点
            if (handMinions.Count == 1)
            {
                chosenMinion.manacost = Math.Max(0, chosenMinion.manacost - 2); // 确保法力值不会低于0
            }
        }
    }
}
