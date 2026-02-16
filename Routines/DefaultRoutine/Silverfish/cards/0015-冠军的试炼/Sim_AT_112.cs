using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 大师级枪骑士（Master Jouster）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_112 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
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

            // 如果己方随从法力值消耗更大，则获得嘲讽和圣盾
            if (ownMinion != null && enemyMinion != null && ownMinion.cost > enemyMinion.cost)
            {
                // 获得圣盾
                own.divineshild = true;

                // 获得嘲讽
                if (!own.taunt)
                {
                    own.taunt = true;
                    if (own.own)
                        p.anzOwnTaunt++;
                    else
                        p.anzEnemyTaunt++;
                }
            }
        }
    }
}