using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 考达拉幼龙（Coldarra Drake）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_008 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 增加英雄技能使用次数上限
            if (own.own)
                p.ownHeroPowerAllowedQuantity += 100; // 己方英雄技能使用次数+100
            else
                p.enemyHeroPowerAllowedQuantity += 100; // 敌方英雄技能使用次数+100
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 减少英雄技能使用次数上限
            if (own.own)
            {
                p.ownHeroPowerAllowedQuantity -= 100; // 己方英雄技能使用次数-100
                                                      // 如果已使用次数超过剩余允许次数，则禁用英雄技能
                if (p.anzUsedOwnHeroPower >= p.ownHeroPowerAllowedQuantity)
                    p.ownAbilityReady = false;
            }
            else
            {
                p.enemyHeroPowerAllowedQuantity -= 100; // 敌方英雄技能使用次数-100
                                                        // 如果已使用次数超过剩余允许次数，则禁用英雄技能
                if (p.anzUsedEnemyHeroPower >= p.enemyHeroPowerAllowedQuantity)
                    p.enemyAbilityReady = false;
            }
        }
    }
}