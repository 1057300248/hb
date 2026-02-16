using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 牧师 费用：3 攻击力：3 生命值：3
    //Talgath
    //塔尔加斯
    //[x]Undamaged enemy minionstake double damage.<b>Combo:</b> Get a Backstab.
    //未受伤的敌方随从受到的伤害翻倍。<b>连击：</b>获取一张背刺。
    class Sim_GDB_472 : SimTemplate
    {
        public override void onAuraStarts(Playfield p, Minion m)
        {
            if (m.own)
            {
                p.我方背刺王 = true;
            }
            else
            {
                p.敌方背刺王 = true;
            }
        }

        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own)
            {
                p.我方背刺王 = false;
            }
            else
            {
                p.敌方背刺王 = false;
            }
        }

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (p.cardsPlayedThisTurn > 0)
            {
                p.drawACard(CardDB.cardNameEN.backstab, own.own);
            }
        }

    }
}
