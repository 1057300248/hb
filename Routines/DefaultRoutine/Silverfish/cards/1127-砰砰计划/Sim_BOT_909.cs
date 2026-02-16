using System;
using System.Collections.Generic;
using System.Text;


namespace HREngine.Bots
{
    class Sim_BOT_909 : SimTemplate //* 水晶学 Crystology
    {
        //[x]Draw two 1-Attackminions from your deck.
        //从你的牌库中抽两张攻击力为1的随从牌。
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (ownplay)
            {
                CardDB.Card c;
                int count = 2;
                foreach (KeyValuePair<CardDB.cardIDEnum, int> cid in p.prozis.turnDeck)
                {
                    c = CardDB.Instance.getCardDataFromID(cid.Key);
                    if (c.Attack == 1 && c.type == CardDB.cardtype.MOB)
                    {
                        p.drawACard(cid.Key, ownplay);
                        count--;
                        if (count <= 0) break;
                    }
                }
            }
        }
    }
}