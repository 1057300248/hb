using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 巫妖王 费用：4 攻击力：2 生命值：4
	//Malignant Horror
	//恶毒恐魔
	//[x]<b>Reborn</b>At the end of your turn,spend 4 <b>Corpses</b> to summona copy of this minion.
	//<b>复生</b>。在你的回合结束时，消耗4份<b>残骸</b>，召唤一个本随从的复制。
	class Sim_CORE_RLK_745 : SimTemplate
	{
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 获取当前可用的尸体数量
            int availableCorpses = p.getCorpseCount();

            if (triggerEffectMinion.own == turnEndOfOwner && availableCorpses >= 4)
            {
                p.本局消耗尸体数 += 4;
                p.callKid(triggerEffectMinion.handcard.card, triggerEffectMinion.zonepos, turnEndOfOwner);
                List<Minion> temp = (turnEndOfOwner) ? p.ownMinions : p.enemyMinions;
                foreach (Minion mnn in temp)
                {
                    if (mnn.name == CardDB.cardNameEN.malignanthorror && triggerEffectMinion.entitiyID != mnn.entitiyID)
                    {
                        mnn.setMinionToMinion(triggerEffectMinion);
                        break;
                    }
                }
            }
        }

    }
}
