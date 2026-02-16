using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    // 随从 中立 费用：3 攻击力：2 生命值：4
    // Hozen Roughhouser
    // 粗暴的猢狲
    // Whenever another friendly Pirate attacks, give it +1/+1.
    // 每当其他友方海盗攻击时，使其获得+1/+1。
    class Sim_VAC_938 : SimTemplate
    {
        // 当随从攻击时触发此方法
        public override void onMinionAttacks(Playfield p, Minion triggerEffectMinion, Minion attackingMinion)
        {
            // 检查攻击者是否为友方海盗
            if (attackingMinion.own && (TAG_RACE)attackingMinion.handcard.card.race == TAG_RACE.PIRATE)
            {
                p.minionGetBuffed(attackingMinion, 1, 1);
            }
        }
    }
}
