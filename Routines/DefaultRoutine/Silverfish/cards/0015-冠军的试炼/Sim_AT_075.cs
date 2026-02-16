using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 战马训练师（Warhorse Trainer）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_075 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 增加战马训练师计数器
            if (own.own)
                p.anzOwnWarhorseTrainer++;
            else
                p.anzEnemyWarhorseTrainer++;

            // 为所有白银之手新兵增加+1攻击力
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;
            foreach (Minion m in minions)
            {
                if (m.name == CardDB.cardNameEN.silverhandrecruit)
                    p.minionGetBuffed(m, 1, 0);
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 减少战马训练师计数器
            if (own.own)
                p.anzOwnWarhorseTrainer--;
            else
                p.anzEnemyWarhorseTrainer--;

            // 为所有白银之手新兵减少+1攻击力
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;
            foreach (Minion m in minions)
            {
                if (m.name == CardDB.cardNameEN.silverhandrecruit)
                    p.minionGetBuffed(m, -1, 0);
            }
        }
    }
}