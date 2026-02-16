using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 穿刺者戈莫克（Gormok the Impaler）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_122 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发战吼的随从。</param>
        /// <param name="target">战吼的目标。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查是否有至少4个其他友方随从
            int friendlyMinionsCount = m.own ? p.ownMinions.Count : p.enemyMinions.Count;
            if (friendlyMinionsCount >= 4 && target != null)
            {
                // 造成4点伤害
                p.minionGetDamageOrHeal(target, 4);
            }
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS, 4), // 需要至少4个友方随从
            };
        }
    }
}