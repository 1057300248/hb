using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：5 攻击力：4 生命值：6
	//Carnivorous Cubicle
	//食肉格块
	//[x]<b>Battlecry:</b> Destroy afriendly minion.Summon a copy of it at the end of your turns.
	//<b>战吼：</b>消灭一个友方随从。在你的回合结束时，召唤一个它的复制。
	class Sim_WORK_042 : SimTemplate
	{

        private Handmanager.Handcard minionToReviveCard = null; // 保存要复活随从的卡牌

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null && target.own)
            {
                // 保存要召唤的随从的卡牌对象，并消灭该随从
                minionToReviveCard = target.handcard;
                p.minionGetDestroyed(target); // 消灭目标随从
            }
        }

        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 如果己方回合结束并且有保存的卡牌，召唤随从的复制
            if (turnEndOfOwner && minionToReviveCard != null && triggerEffectMinion.handcard.card.cardIDenum == CardDB.cardIDEnum.WORK_042)
            {
                p.callKid(minionToReviveCard.card, triggerEffectMinion.zonepos, true); // 召唤随从的复制
            }
        }
    }
}
