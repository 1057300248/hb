using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 击剑教头（Fencing Coach）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_115 : SimTemplate
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
            // 检查是否为己方随从
            if (own.own)
            {
                // 下一个英雄技能费用减少2点
                p.ownHeroPowerCostLessOnce -= 2;
            }
            else
            {
                // 下一个英雄技能费用减少2点
                p.enemyHeroPowerCostLessOnce -= 2;
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
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET), // 目标必须是随从
            };
        }
    }
}