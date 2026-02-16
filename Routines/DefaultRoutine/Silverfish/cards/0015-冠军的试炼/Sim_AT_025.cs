using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 黑暗交易（Dark Bargain）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_025 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 获取敌方随从列表
            List<Minion> enemies = ownplay ? new List<Minion>(p.enemyMinions) : new List<Minion>(p.ownMinions);

            // 随机消灭两个敌方随从
            for (int i = 0; i < 2 && enemies.Count > 0; i++)
            {
                Minion randomMinion = p.searchRandomMinion(enemies, searchmode.searchLowestHP);
                if (randomMinion != null)
                {
                    p.minionGetDestroyed(randomMinion);
                    enemies.Remove(randomMinion); // 移除已消灭的随从
                }
            }

            // 随机弃两张牌
            p.discardCards(2, ownplay);
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_ENEMY_MINIONS, 1), // 至少需要一个敌方随从
            };
        }
    }
}