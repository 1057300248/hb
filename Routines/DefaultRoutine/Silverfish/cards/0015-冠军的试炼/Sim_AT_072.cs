using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 瓦里安·乌瑞恩（Varian Wrynn）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_072 : SimTemplate
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
            if (own.own)
            {
                // 记录抽牌前的手牌数量
                int initialHandCount = p.owncards.Count;

                // 抽三张牌
                for (int i = 0; i < 3; i++)
                {
                    p.drawACard(CardDB.cardIDEnum.None, own.own);
                }

                // 检查抽到的牌是否为随从牌，若是则直接召唤
                for (int i = initialHandCount; i < p.owncards.Count; i++)
                {
                    Handmanager.Handcard hc = p.owncards[i];
                    if (hc.card.type == CardDB.cardtype.MOB)
                    {
                        // 召唤随从
                        p.callKid(hc.card, p.ownMinions.Count, own.own);

                        // 从手牌中移除该随从牌
                        p.owncards.RemoveAt(i);
                        p.owncarddraw--;
                        i--; // 调整索引
                    }
                }
            }
        }
    }
}