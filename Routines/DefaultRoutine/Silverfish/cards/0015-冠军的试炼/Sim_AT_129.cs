using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 光明邪使菲奥拉（Fjola Lightbane）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_129 : SimTemplate
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
            // 检查是否为当前玩家使用的法术牌且目标为本随从
            if (wasOwnCard && hc.card.type == CardDB.cardtype.SPELL && hc.target != null && hc.target.entitiyID == triggerEffectMinion.entitiyID)
            {
                // 获得圣盾
                triggerEffectMinion.divineshild = true;
            }
        }
    }
}
