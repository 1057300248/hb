using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 虚空碾压者（Void Crusher）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_023 : SimTemplate
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
                // 随机消灭敌方一个随从
                Minion enemyMinion = p.searchRandomMinion(p.enemyMinions, searchmode.searchLowestHP);
                if (enemyMinion != null)
                {
                    p.minionGetDestroyed(enemyMinion);
                }

                // 随机消灭己方一个随从
                Minion ownMinion = p.searchRandomMinion(p.ownMinions, searchmode.searchLowestHP);
                if (ownMinion != null)
                {
                    p.minionGetDestroyed(ownMinion);
                }
            }
        }
    }
}