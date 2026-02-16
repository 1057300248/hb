using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 骷髅骑士（The Skeleton Knight）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_128 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 揭示双方牌库里的一张随从牌（模拟逻辑）
            CardDB.Card ownMinion = null;
            CardDB.Card enemyMinion = null;

            // 查找己方牌库中的第一张随从牌
            foreach (CardDB.Card card in p.ownDeck)
            {
                if (card.type == CardDB.cardtype.MOB)
                {
                    ownMinion = card;
                    break;
                }
            }

            // 查找敌方牌库中的第一张随从牌
            foreach (CardDB.Card card in p.enemyDeck)
            {
                if (card.type == CardDB.cardtype.MOB)
                {
                    enemyMinion = card;
                    break;
                }
            }

            // 如果己方随从法力值消耗更大，则将骷髅骑士移回手牌
            if (ownMinion != null && enemyMinion != null && ownMinion.cost > enemyMinion.cost)
            {
                p.minionReturnToHand(m, m.own, 0);
            }
        }
    }
}