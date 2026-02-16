using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 影踪骁骑兵（Shado-Pan Rider）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_028 : SimTemplate
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
            // 检查是否满足连击条件（本回合已使用过卡牌）
            if (p.cardsPlayedThisTurn > 0)
            {
                // 获得+3攻击力
                p.minionGetBuffed(own, 3, 0);
            }
        }
    }
}