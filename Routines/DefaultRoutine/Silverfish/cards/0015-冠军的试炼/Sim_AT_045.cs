using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 艾维娜（Aviana）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_045 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion m)
        {
            // 增加己方艾维娜计数器
            if (m.own)
            {
                p.anzOwnAviana++;
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 减少己方艾维娜计数器
            if (m.own)
            {
                p.anzOwnAviana--;
            }
        }
    }
}