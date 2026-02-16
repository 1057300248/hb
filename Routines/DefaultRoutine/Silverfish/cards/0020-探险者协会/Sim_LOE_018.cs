using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 坑道穴居人（Tunnel Trogg）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_018 : SimTemplate
    {
        /// <summary>
        /// 当卡片即将被使用时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="hc">即将使用的卡片。</param>
        /// <param name="wasOwnCard">是否为当前玩家使用的卡片。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查是否为当前玩家使用的卡片且卡片有过载效果
            if (wasOwnCard == triggerEffectMinion.own && hc.card.overload > 0)
            {
                // 每一个被锁的法力水晶使其获得+1攻击力
                p.minionGetBuffed(triggerEffectMinion, hc.card.overload, 0);
            }
        }
    }
}