using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 牧师 费用：3 攻击力：2 生命值：2
    //Iron Sensei
    //钢铁武道家
    //At the end of your turn, give another friendly Mech +2/+2.
    //在你的回合结束时，使另一个友方机械获得+2/+2。
    class Sim_BG_GVG_027 : SimTemplate
    {
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                // 查找除自身外的其他友方机械单位
                List<Minion> otherFriendlyMechs = new List<Minion>();
                foreach (Minion minion in p.ownMinions)
                {
                    if (minion.entitiyID != triggerEffectMinion.entitiyID && minion.handcard.card.race == CardDB.Race.MECHANICAL)
                    {
                        otherFriendlyMechs.Add(minion);
                    }
                }

                // 如果找到友方机械单位，则随机选择一个给予+2/+2
                if (otherFriendlyMechs.Count > 0)
                {
                    // 随机选择一个机械单位
                    Minion targetMech = otherFriendlyMechs[0]; // 简化处理，选择第一个

                    // 给予+2/+2增益
                    p.minionGetBuffed(targetMech, 2, 2);
                }
            }
        }
    }
}