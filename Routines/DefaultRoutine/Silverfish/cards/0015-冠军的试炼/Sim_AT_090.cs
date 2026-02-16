using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 穆克拉的勇士（Mukla's Champion）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_090 : SimTemplate
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

                // 为其他随从增加+1/+1
                foreach (Minion minion in minions)
                {
                    // 排除自身
                    if (m.entitiyID != minion.entitiyID)
                    {
                        p.minionGetBuffed(minion, 1, 1);
                    }
                }
            }
        }
    }
}