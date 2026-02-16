using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 诅咒（Cursed!）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_007t : SimTemplate
    {
        /// <summary>
        /// 当回合开始时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        /// <param name="turnStartOfOwner">是否为当前玩家的回合开始。</param>
        public override void onTurnStartTrigger(Playfield p, Minion triggerEffectMinion, bool turnStartOfOwner)
        {
            // 检查是否为当前玩家的回合开始
            if (triggerEffectMinion.own == turnStartOfOwner)
            {
                // 对当前玩家的英雄造成2点伤害
                p.minionGetDamageOrHeal(turnStartOfOwner ? p.ownHero : p.enemyHero, 2, true);
            }
        }
    }
}