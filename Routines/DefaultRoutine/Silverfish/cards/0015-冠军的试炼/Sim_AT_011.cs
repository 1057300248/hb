using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 神圣勇士（Holy Champion）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_011 : SimTemplate
    {
        /// <summary>
        /// 当一个角色获得治疗时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="triggerEffectMinion">触发效果的随从（神圣勇士）。</param>
        /// <param name="charsGotHealed">获得治疗的角色数量。</param>
        public override void onACharGotHealed(Playfield p, Minion triggerEffectMinion, int charsGotHealed)
        {
            // 每当一个角色获得治疗，神圣勇士获得+2攻击力
            p.minionGetBuffed(triggerEffectMinion, 2 * charsGotHealed, 0);
        }
    }
}