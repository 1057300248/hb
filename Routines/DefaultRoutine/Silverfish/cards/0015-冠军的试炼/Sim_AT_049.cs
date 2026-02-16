using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 雷霆崖勇士（Thunder Bluff Valiant）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_049 : SimTemplate
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
                // 获取当前玩家的随从列表
                List<Minion> minions = own ? p.ownMinions : p.enemyMinions;

                // 为所有图腾随从增加+2攻击力
                foreach (Minion minion in minions)
                {
                    if (minion.handcard.card.race == CardDB.Race.TOTEM)
                    {
                        p.minionGetBuffed(minion, 2, 0);
                    }
                }
            }
        }
    }
}