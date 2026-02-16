using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 零食商贩（Refreshment Vendor）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_111 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 计算恢复的生命值
            int heal = own.own ? p.getMinionHeal(4) : p.getEnemyMinionHeal(4);

            // 为双方英雄恢复生命值
            p.minionGetDamageOrHeal(p.ownHero, -heal);
            p.minionGetDamageOrHeal(p.enemyHero, -heal);
        }
    }
}