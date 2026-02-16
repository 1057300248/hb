using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 低阶侍从（Lowly Squire）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_082 : SimTemplate
    {
        /// <summary>
        /// 当激励效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发激励的随从。</param>
        /// <param name="own">是否为当前玩家的随从。</param>
        public override void onInspire(Playfield p, Minion m, bool own)
        {
            // 检查是否为当前玩家的随从
            if (m.own == own)
            {
                // 使随从获得+1攻击力
                p.minionGetBuffed(m, 1, 0);
            }
        }
    }
}