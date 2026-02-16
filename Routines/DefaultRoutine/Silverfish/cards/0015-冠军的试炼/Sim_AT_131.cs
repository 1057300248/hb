using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 黑暗邪使艾蒂丝（Eydis Darkbane）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_131 : SimTemplate
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
                // 随机对一个敌人造成3点伤害
                Minion target = p.getEnemyCharTargetForRandomSingleDamage(3);
                if (target != null)
                {
                    p.minionGetDamageOrHeal(target, 3);
                }
            }
        }
    }
}
