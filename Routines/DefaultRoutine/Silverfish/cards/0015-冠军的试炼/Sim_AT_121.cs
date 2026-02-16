using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 人气选手（Crowd Favorite）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_121 : SimTemplate
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
            // 检查是否为当前玩家使用的卡片且卡片具有战吼效果
            if (triggerEffectMinion.own == wasOwnCard && hc.card.battlecry)
            {
                // 使随从获得+1/+1
                p.minionGetBuffed(triggerEffectMinion, 1, 1);
            }
        }
    }
}