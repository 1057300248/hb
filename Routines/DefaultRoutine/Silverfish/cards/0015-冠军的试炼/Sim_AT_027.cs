using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 威尔弗雷德·菲兹班（Wilfred Fizzlebang）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_027 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 为当前玩家启用英雄技能抽牌减费效果
            if (own.own)
            {
                p.nextSpellThisTurnCost0 = true;
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 取消英雄技能抽牌减费效果
            if (own.own)
            {
                p.nextSpellThisTurnCost0 = false;
            }
        }
    }
}