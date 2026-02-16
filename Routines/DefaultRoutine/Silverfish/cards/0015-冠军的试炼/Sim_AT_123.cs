using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 冰喉（Chillmaw）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_123 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
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

                // 如果手牌中有龙牌，则对所有随从造成3点伤害
                if (dragonInHand)
                {
                    p.allMinionsGetDamage(3);
                }
            }
            else
            {
                // 敌方逻辑：假设敌方手牌数量 >= 1 时视为持有龙牌
                if (p.enemyAnzCards >= 1)
                {
                    p.allMinionsGetDamage(3);
                }
            }
        }
    }
}