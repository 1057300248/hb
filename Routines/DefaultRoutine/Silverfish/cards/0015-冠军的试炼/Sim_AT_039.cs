using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 狂野争斗者（Savage Combatant）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_039 : SimTemplate
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
                // 使英雄获得+2攻击力（仅限本回合）
                p.minionGetTempBuff(own ? p.ownHero : p.enemyHero, 2, 0);
            }
        }
    }
}