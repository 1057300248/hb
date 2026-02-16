using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 暴躁的牧羊人（Ram Wrangler）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_010 : SimTemplate
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
            // 获取当前玩家的随从列表
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;

            // 检查是否有野兽随从
            foreach (Minion m in minions)
            {
                if (m.handcard.card.race == CardDB.Race.PET)
                {
                    // 随机召唤一个野兽（此处以羊为例）
                    p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_120), minions.Count, own.own);
                    break; // 只需召唤一次
                }
            }
        }
    }
}