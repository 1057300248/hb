using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //* 遗物搜寻者 Reliquary Seeker
    //<b>Battlecry:</b> If you have 6 other minions, gain +4/+4.
    //<b>战吼：</b>如果你拥有六个其他随从，便获得+4/+4。 
    /// <summary>
    /// 遗物搜寻者（Reliquary Seeker）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_116 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 计算己方场上的随从数量（不包括遗物搜寻者本身）
            int friendlyMinionsCount = (m.own) ? p.ownMinions.Count - 1 : p.enemyMinions.Count - 1;

            // 如果拥有六个其他随从，则获得+4/+4
            if (friendlyMinionsCount >= 6)
            {
                p.minionGetBuffed(m, 4, 4);
            }
        }
    }
}