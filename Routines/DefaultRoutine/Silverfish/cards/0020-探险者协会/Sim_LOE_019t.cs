using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 黄金猿藏宝图（Map to the Golden Monkey）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_019t : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从或英雄（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将“黄金猿”洗入当前玩家的牌库
            if (ownplay)
            {
                p.ownDeck.Add(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_019t2));
                p.ownDeckSize++;
            }
            else
            {
                p.enemyDeck.Add(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_019t2));
                p.enemyDeckSize++;
            }

            // 抽一张牌
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
        }
    }
}