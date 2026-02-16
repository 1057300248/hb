using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DRG_318 : SimTemplate //* 梦境吐息 Breath of Dreams
    {
        //Draw a card. If you're holding a Dragon, gain an empty Mana Crystal.
        //抽一张牌。如果你的手牌中有龙牌，便获得一个空的法力水晶。
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
            if (ownplay)
            {
                // 检查手牌中是否有龙牌
                bool hasDragonInHand = false;
                foreach (Handmanager.Handcard card in p.owncards)
                {
                    if (card.card.race == CardDB.Race.DRAGON)
                    {
                        hasDragonInHand = true;
                        break;
                    }
                }
                // 如果手牌中有龙牌，则获得一个空的法力水晶
                if (hasDragonInHand)
                {
                    p.ownMaxMana = Math.Min(10, p.ownMaxMana + 1); // 增加一个法力水晶，上限为10
                }
            }
        }
    }
}