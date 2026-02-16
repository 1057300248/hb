using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 雪地狗头人（Frigid Snobold）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_093 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 增加法术伤害
            if (own.own)
                p.spellpower++;
            else
                p.enemyspellpower++;
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 减少法术伤害
            if (m.own)
                p.spellpower--;
            else
                p.enemyspellpower--;
        }
    }
}