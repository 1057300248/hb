using Buddy.Auth.Objects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HREngine.Bots
{
    //随从 术士 费用：7 攻击力：7 生命值：7
    //Archimonde
    //阿克蒙德
    //[x]<b>Battlecry:</b> Summon everyDemon you played thisgame that didn't startin your deck.
    //<b>战吼：</b>召唤你在本局对战中使用过的你套牌之外的所有恶魔。
    class Sim_GDB_128 : SimTemplate
    {

        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            int place = (m.own) ? p.ownMinions.Count : p.enemyMinions.Count;
            var temp = (m.own) ? Probabilitymaker.Instance.ownGraveyard : Probabilitymaker.Instance.enemyGraveyard;
            if (place > 6) return;

            CardDB.Card c;
            foreach (var gi in temp)
            {
                c = CardDB.Instance.getCardDataFromID(gi.Key);
                if ((TAG_RACE)c.race == TAG_RACE.DEMON)
                {
                    p.callKid(c, place, m.own, false);
                    place++;
                    if (place > 6) break;
                    if (gi.Value > 1)
                    {
                        p.callKid(c, place, m.own, false);
                        place++;
                        if (place > 6) break;
                    }
                }
            }
        }
    }
}
