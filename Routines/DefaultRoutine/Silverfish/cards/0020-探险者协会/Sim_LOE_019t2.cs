using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 黄金猿（Golden Monkey）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_019t2 : SimTemplate
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
            // 替换手牌和牌库中的卡牌为传说随从（模拟逻辑）
            if (own.own)
            {
                // 清空手牌
                p.owncards.Clear();

                // 替换牌库中的卡牌为传说随从
                for (int i = 0; i < p.ownDeck.Count; i++)
                {
                    p.ownDeck[i] = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_014); // 例如：国王穆克拉
                }
            }
            else
            {
                // 敌方逻辑同理
                p.enemyAnzCards = 0;

                for (int i = 0; i < p.enemyDeck.Count; i++)
                {
                    p.enemyDeck[i] = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_014); // 例如：国王穆克拉
                }
            }
        }
    }
}