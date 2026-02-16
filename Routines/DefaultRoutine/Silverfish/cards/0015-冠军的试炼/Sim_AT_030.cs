using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 幽暗城勇士（Undercity Valiant）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_030 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查是否满足连击条件（本回合已使用过卡牌）且目标不为空
            if (p.cardsPlayedThisTurn >= 1 && target != null)
            {
                // 对目标造成1点伤害
                p.minionGetDamageOrHeal(target, 1);
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
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_FOR_COMBO), // 连击时需要指定目标
            };
        }
    }
}