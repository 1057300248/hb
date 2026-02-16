using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 西风灯神（Djinni of Zephyrs）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_053 : SimTemplate
    {
        /// <summary>
        /// 当卡牌即将被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="hc">即将使用的卡牌。</param>
        /// <param name="wasOwnCard">是否为当前玩家使用此卡牌。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查是否为法术卡牌，并且目标是友方随从
            if (hc.card.type == CardDB.cardtype.SPELL && hc.target != null && hc.target.own == wasOwnCard)
            {
                // 确保目标不是西风灯神本身
                if (hc.target.entitiyID != triggerEffectMinion.entitiyID)
                {
                    // 复制法术效果到西风灯神身上
                    hc.card.sim_card.onCardPlay(p, wasOwnCard, triggerEffectMinion, hc.extraParam2);
                }
            }
        }
    }
}