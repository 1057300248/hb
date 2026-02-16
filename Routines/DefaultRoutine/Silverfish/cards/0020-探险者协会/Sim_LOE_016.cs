using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 顽石元素（Rumbling Elemental）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_016 : SimTemplate
    {
        /// <summary>
        /// 当随从被召唤时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发效果的随从。</param>
        /// <param name="summonedMinion">被召唤的随从。</param>
        public override void onMinionWasSummoned(Playfield p, Minion m, Minion summonedMinion)
        {
            // 检查被召唤的随从是否具有战吼且由当前玩家召唤
            if (summonedMinion.handcard.card.battlecry && summonedMinion.playedFromHand && summonedMinion.own == m.own && summonedMinion.entitiyID != m.entitiyID)
            {
                Minion target = null;

                // 确定目标
                if (m.own)
                {
                    target = p.getEnemyCharTargetForRandomSingleDamage(2);
                }
                else
                {
                    target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack);
                    if (target == null)
                        target = p.ownHero;
                }

                // 对目标造成2点伤害
                p.minionGetDamageOrHeal(target, 2, true);
            }
        }
    }
}