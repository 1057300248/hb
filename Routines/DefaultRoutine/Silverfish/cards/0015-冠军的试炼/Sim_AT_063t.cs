using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 恐鳞（Dreadscale）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_063t : SimTemplate
    {
        /// <summary>
        /// 当回合结束时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        /// <param name="turnEndOfOwner">是否为当前玩家的回合结束。</param>
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查是否为当前玩家的回合结束
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                // 对所有其他随从造成1点伤害
                p.allMinionsGetDamage(1, triggerEffectMinion.entitiyID);
            }
        }
    }
}