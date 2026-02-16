using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 暮光守护者（Twilight Guardian）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_017 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            if (m.own)
            {
                // 检查己方手牌中是否有龙牌
                bool dragonInHand = false;
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.race == CardDB.Race.DRAGON)
                    {
                        dragonInHand = true;
                        break;
                    }
                }

                // 如果手牌中有龙牌，则获得+1攻击力和嘲讽
                if (dragonInHand)
                {
                    p.minionGetBuffed(m, 1, 0);
                    m.taunt = true;
                    p.anzOwnTaunt++;
                }
            }
            else
            {
                // 敌方逻辑：假设敌方手牌数量 >= 2 时视为持有龙牌
                if (p.enemyAnzCards >= 2)
                {
                    p.minionGetBuffed(m, 1, 0);
                    m.taunt = true;
                    p.anzEnemyTaunt++;
                }
            }
        }
    }
}