using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 盗墓匪贼（Tomb Pillager）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_012 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 将一个幸运币置入当前玩家的手牌
            p.drawACard(CardDB.cardNameEN.thecoin, m.own);
        }
    }
}