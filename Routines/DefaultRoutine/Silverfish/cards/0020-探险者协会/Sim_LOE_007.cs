using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 拉法姆的诅咒（Curse of Rafaam）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_007 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从或英雄（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 对敌方英雄造成2点伤害
            p.minionGetDamageOrHeal(p.enemyHero, 2);
        }
    }
}
