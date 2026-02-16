using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 布莱恩·铜须（Brann Bronzebeard）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_077 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            if (own.own) p.ownBrannBronzebeard++;
            else p.enemyBrannBronzebeard++;
        }

        /// <summary>
        /// 当光环效果结束时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">结束光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own) p.ownBrannBronzebeard--;
            else p.enemyBrannBronzebeard--;
        }
    }
}