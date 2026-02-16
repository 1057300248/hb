using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：2 攻击力：1 生命值：4
    //Coconut Cannoneer
    //椰子火炮手
    //After an adjacent minion attacks, deal 1 damage to a random enemy.
    //在相邻的随从攻击后，随机对一个敌人造成1点伤害。
    class Sim_VAC_532 : SimTemplate
    {
        public override void onMinionAttacks(Playfield p, Minion triggerEffectMinion, Minion attackingMinion)
        {
            // 检查是否为同一方随从且位置相邻
            if (triggerEffectMinion.own == attackingMinion.own &&
            Math.Abs(triggerEffectMinion.zonepos - attackingMinion.zonepos) == 1)
            {
                Minion target = null;
                if (triggerEffectMinion.own)
                {
                    // 友方触发，选择敌方随机目标
                    target = p.getEnemyCharTargetForRandomSingleDamage(1);
                    p.evaluatePenality -= 1; // 策略评估调整
                }
                else
                {
                    // 敌方触发，选择友方高攻随从或英雄
                    target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack);
                    if (target == null) target = p.ownHero;
                }
                 // 造成1点伤害
                 p.minionGetDamageOrHeal(target, 1, true);
            }
        }
    }
}
