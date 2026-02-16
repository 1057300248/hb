using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HREngine.Bots
{
    //随从 圣骑士 费用：2 攻击力：2 生命值：1
    //Hi Ho Silverwing
    //威猛银翼巨龙
    //<b>Divine Shield</b><b>Deathrattle:</b> Draw aHoly spell.
    //<b>圣盾</b>。<b>亡语：</b>抽一张神圣法术牌。
    class Sim_WW_344 : SimTemplate
    {
        public override void onDeathrattle(Playfield p, Minion m)
        {
            if (m.own)
            {
                CardDB.Card c;
                int count = 1;
                foreach (KeyValuePair<CardDB.cardIDEnum, int> cid in p.prozis.turnDeck)
                {
                    c = CardDB.Instance.getCardDataFromID(cid.Key);
                    if (c.SpellSchool == CardDB.SpellSchool.HOLY)
                    {
                        for (int i = 0; i < cid.Value; i++)
                        {
                            p.drawACard(cid.Key, m.own);
                            count--;
                            if (count <= 0) break;
                        }
                    }
                }
            }
        }
    }
}
