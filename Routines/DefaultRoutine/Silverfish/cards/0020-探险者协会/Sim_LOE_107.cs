using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 诡异的雕像（Eerie Statue）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_107 : SimTemplate
    {
        /// <summary>
        /// 当随从被召唤时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发效果的随从。</param>
        /// <param name="summonedMinion">被召唤的随从。</param>
        public override void onMinionWasSummoned(Playfield p, Minion m, Minion summonedMinion)
        {
            if (!m.silenced)
            {
                // 如果场上存在其他随从，则无法攻击
                int totalMinions = p.ownMinions.Count + p.enemyMinions.Count;
                m.cantAttack = (totalMinions > 1);
                m.updateReadyness();
            }
        }

        /// <summary>
        /// 当随从死亡时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发效果的随从。</param>
        /// <param name="diedMinion">死亡的随从。</param>
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            if (!m.silenced)
            {
                // 计算场上存活的随从数量
                int minionsOnBoard = 0;
                foreach (Minion minion in p.ownMinions)
                {
                    if (minion.Hp > 0) minionsOnBoard++;
                }
                foreach (Minion minion in p.enemyMinions)
                {
                    if (minion.Hp > 0) minionsOnBoard++;
                }

                // 如果场上存在其他随从，则无法攻击
                m.cantAttack = (minionsOnBoard > 1);
                m.updateReadyness();
            }
        }

        /// <summary>
        /// 当回合开始时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        /// <param name="turnStartOfOwner">是否为拥有者的回合开始。</param>
        public override void onTurnStartTrigger(Playfield p, Minion triggerEffectMinion, bool turnStartOfOwner)
        {
            if (triggerEffectMinion.own == turnStartOfOwner && !triggerEffectMinion.silenced)
            {
                // 如果场上存在其他随从，则无法攻击
                int totalMinions = p.ownMinions.Count + p.enemyMinions.Count;
                triggerEffectMinion.cantAttack = (totalMinions > 1);
                triggerEffectMinion.updateReadyness();
            }
        }
    }
}