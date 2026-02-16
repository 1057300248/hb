using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 圣骑士 费用：3 攻击力：3 生命值：4
    //Myrmidon
    //纳迦侍从
    //After you cast a spell on this minion, draw a card.
    //在你对本随从施放一个法术后，抽一张牌。
    class Sim_TID_098 : SimTemplate
    {
        // 当一个卡牌被打出后触发
        public override void onCardWasPlayed(Playfield p, CardDB.Card c, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查是否是我方打出的法术，且这个法术的类型是SPELL
            if (wasOwnCard && c.type == CardDB.cardtype.SPELL)
            {
                // 获取最后一个动作
                Action lastAction = p.playactions.Count > 0 ? p.playactions[p.playactions.Count - 1] : null;
                
                // 检查这个动作是否是打出卡牌，以及目标是否是触发这个效果的随从
                if (lastAction != null && lastAction.actionType == actionEnum.playcard && 
                    lastAction.target != null && lastAction.target.entitiyID == triggerEffectMinion.entitiyID)
                {
                    // 抽一张牌
                    p.drawACard(CardDB.cardIDEnum.None, wasOwnCard);
                }
            }
        }
    }
}