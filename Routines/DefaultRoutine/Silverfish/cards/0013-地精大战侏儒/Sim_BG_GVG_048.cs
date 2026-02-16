using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 猎人 费用：3 攻击力：3 生命值：3
    //Metaltooth Leaper
    //金刚刃牙兽
    //<b>Battlecry:</b> Give your other Mechs +2 Attack.
    //<b>战吼：</b>使你的其他机械获得+2攻击力。
    class Sim_BG_GVG_048 : SimTemplate
    {
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 使己方场上的其他机械单位获得+2攻击力
            foreach (Minion minion in p.ownMinions)
            {
                // 排除自身，只对其他机械单位生效
                if (minion.entitiyID != own.entitiyID && minion.handcard.card.race == CardDB.Race.MECHANICAL)
                {
                    p.minionGetBuffed(minion, 2, 0); // +2攻击力，+0生命值
                }
            }
        }
    }
}