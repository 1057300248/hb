using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 冰吼（Icehowl）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_125 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 使随从无法攻击英雄
            own.cantAttackHeroes = true;
        }

        /// <summary>
        /// 当回合结束时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发效果的随从。</param>
        /// <param name="turnEndOfOwner">是否为当前玩家的回合结束。</param>
        public override void onTurnEndsTrigger(Playfield p, Minion m, bool turnEndOfOwner)
        {
            // 检查是否为当前玩家的回合结束
            if (m.own == turnEndOfOwner)
            {
                // 使随从无法攻击英雄
                m.cantAttackHeroes = true;
            }
        }
    }
}