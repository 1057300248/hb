using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 银色警卫（Argent Watchman）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_109 : SimTemplate
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
                // 解除无法攻击状态
                m.cantAttack = false;

                // 更新就绪状态
                m.updateReadyness();
            }
        }

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
                // 如果未被沉默，则恢复无法攻击状态
                if (!triggerEffectMinion.silenced)
                {
                    triggerEffectMinion.cantAttack = true;
                }
            }
        }
    }
}