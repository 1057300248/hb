using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 庆典司仪（Master of Ceremonies）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_117 : SimTemplate
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
            // 获取当前玩家的随从列表
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;

            // 检查是否有具有法术伤害的随从
            bool hasSpellDamage = false;
            foreach (Minion m in minions)
            {
                if (m.spellpower > 0)
                {
                    hasSpellDamage = true;
                    break;
                }
            }

            // 如果有，则获得+2/+2
            if (hasSpellDamage)
            {
                p.minionGetBuffed(own, 2, 2);
            }
        }
    }
}