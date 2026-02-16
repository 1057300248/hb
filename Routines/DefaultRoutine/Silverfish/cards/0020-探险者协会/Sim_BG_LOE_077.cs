using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 布莱恩·铜须（Brann Bronzebeard）卡牌的模拟实现。
    /// </summary>
    class Sim_BG_LOE_077 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 增加战吼触发次数
            if (own.own)
            {
                p.ownBrannBronzebeard++;
            }
            else
            {
                p.enemyBrannBronzebeard++;
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 减少战吼触发次数
            if (own.own)
            {
                p.ownBrannBronzebeard--;
            }
            else
            {
                p.enemyBrannBronzebeard--;
            }
        }
    }
}
