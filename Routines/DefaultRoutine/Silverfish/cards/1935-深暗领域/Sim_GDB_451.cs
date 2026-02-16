using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 萨满祭司 费用：2
	//Triangulate
	//三角测量
	//[x]<b>Discover</b> a differentspell from your deck.Shuffle 3 copies of itinto your deck.
	//从你的牌库中<b>发现</b>一张不同的法术牌，并将它的3张复制洗入你的牌库。
	class Sim_GDB_451 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int cnt = 3;
            foreach (KeyValuePair<CardDB.cardIDEnum, int> kvp in p.prozis.turnDeck)
            {
                // ID 转卡
                CardDB.cardIDEnum deckCard = kvp.Key;
                CardDB.Card card = CardDB.Instance.getCardDataFromID(deckCard);
                if (card.type == CardDB.cardtype.SPELL)
                {
                    p.drawACard(deckCard, true, false);
                    p.owncards[p.owncards.Count - 1].discovered = true;
                    cnt--;
                }
                if (cnt <= 0) return;
            }
        }
		
	}
}
