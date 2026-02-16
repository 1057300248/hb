using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 唤雾者伊戈瓦尔（The Mistcaller）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_054 : SimTemplate
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
            // 检查是否为当前玩家的随从
            if (own.own)
            {
                // 为手牌中的所有随从牌增加+1/+1
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.type == CardDB.cardtype.MOB)
                    {
                        hc.addattack++;
                        hc.addHp++;
                    }
                }

                // 为牌库中的所有随从牌增加+1/+1（模拟逻辑）
                foreach (CardDB.Card card in p.ownDeck)
                {
                    if (card.type == CardDB.cardtype.MOB)
                    {
                        // 假设牌库中的卡牌也有类似的属性字段
                        // card.addattack++;
                        // card.addHp++;
                    }
                }
            }
        }
    }
}