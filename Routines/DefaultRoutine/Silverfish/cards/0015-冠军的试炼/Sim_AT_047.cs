using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 德莱尼图腾师（Draenei Totemcarver）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_047 : SimTemplate
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
            // 计算友方图腾数量
            int totemCount = 0;
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;

            foreach (Minion m in minions)
            {
                if (m.handcard.card.race == CardDB.Race.TOTEM)
                {
                    totemCount++;
                }
            }

            // 根据图腾数量获得+1/+1
            if (totemCount >= 1)
            {
                p.minionGetBuffed(own, totemCount, totemCount);
            }
        }
    }
}