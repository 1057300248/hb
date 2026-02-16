using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 猛犸人头领（Magnataur Alpha）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_067 : SimTemplate
    {
        /// <summary>
        /// 当随从攻击后触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="attacker">攻击的随从。</param>
        /// <param name="target">被攻击的目标。</param>
        /// <param name="damage">造成的伤害值。</param>
        public override void afterMinionAttack(Playfield p, Minion attacker, Minion target, int damage)
        {
            // 检查目标是否为随从
            if (target != null && target.handcard.card.type == CardDB.cardtype.MOB)
            {
                // 获取目标所在的一侧随从列表
                List<Minion> minions = target.own ? p.ownMinions : p.enemyMinions;

                // 对目标相邻的随从造成相同伤害
                foreach (Minion adjacentMinion in minions)
                {
                    if (Math.Abs(target.zonepos - adjacentMinion.zonepos) == 1)
                    {
                        p.minionGetDamageOrHeal(adjacentMinion, damage);
                    }
                }
            }
        }
    }
}
