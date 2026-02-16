using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 神勇弓箭手（Brave Archer）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_059 : SimTemplate
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
                // 获取当前玩家的手牌数量
                int handCount = own ? p.owncards.Count : p.enemyAnzCards;

                // 如果手牌为空，则对敌方英雄造成2点伤害
                if (handCount <= 0)
                {
                    p.minionGetDamageOrHeal(own ? p.enemyHero : p.ownHero, 2);
                }
            }
        }
    }
}