using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    // 随从 中立 费用：3 攻击力：2 生命值：1
    // Terrible Chef
    // 可怕的主厨
    // [x]<b>Battlecry:</b> Summon a 0/2 Nerubian Egg. <b>Deathrattle:</b> Destroy it.
    // <b>战吼：</b>召唤一个 0/2 的蛛魔之卵。<b>亡语：</b>消灭它。

    class Sim_VAC_946 : SimTemplate
    {
        private static readonly CardDB.Card egg = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_007); // 蛛魔之卵 Nerubian Egg

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.callKid(egg, own.zonepos, own.own); // 召唤蛛魔之卵
        }

        public override void onDeathrattle(Playfield p, Minion m)
        {
            List<Minion> temp = (m.own) ? p.ownMinions : p.enemyMinions; // 获取友方随从列表

            foreach (Minion minion in temp)
            {
                if (minion.handcard.card.nameCN == CardDB.cardNameCN.蛛魔之卵) // 找到蛛魔之卵
                {
                    p.minionGetDestroyed(minion); // 消灭蛛魔之卵
                    return; // 只消灭一个，找到后就跳出
                }
            }
        }
    }
}