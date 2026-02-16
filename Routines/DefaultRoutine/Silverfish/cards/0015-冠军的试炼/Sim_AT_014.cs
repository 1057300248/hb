using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 暗影魔（Shadowfiend）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_014 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 增加己方暗影魔计数器
            if (own.own)
            {
                p.anzOwnShadowfiend++;
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 减少己方暗影魔计数器
            if (own.own)
            {
                p.anzOwnShadowfiend--;
            }
        }
    }
}