using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 恶魔猎手 费用：0
	//Dispose of Evidence
	//处理证据
	//Give your hero +3 Attack this turn. Choose a card in your hand to shuffle into your deck.
	//在本回合中，使你的英雄获得+3攻击力。选择一张你的手牌洗入你的牌库。
	class Sim_REV_507 : SimTemplate
	{
		 public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 获取玩家的手牌列表
            List<Handmanager.Handcard> tempHand = ownplay ? p.owncards : p.enemyHand;
			if (tempHand.Count == 1)
            {
                // 如果只有一张手牌，则只将这张牌洗入牌库
                Handmanager.Handcard card = tempHand[0];
                p.AddToDeck(card.card);
                p.removeCard(card);
            }
            p.minionGetTempBuff(p.ownHero, 3, 0);
			p.ownHero.updateReadyness();
        }
		
	}
}
