using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 窃贼（Cutpurse）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_031 : SimTemplate
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
            // 检查目标是否为英雄
            if (target != null && target.handcard.card.type == CardDB.cardtype.HERO)
            {
                // 将幸运币置入当前玩家的手牌
                p.drawACard(CardDB.cardNameEN.thecoin, attacker.own, true);
            }
        }
    }
}