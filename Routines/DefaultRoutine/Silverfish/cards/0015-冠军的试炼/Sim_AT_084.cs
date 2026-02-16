using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 持枪侍从（Lance Carrier）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_084 : SimTemplate
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
            // 检查目标是否存在
            if (target != null)
            {
                // 使目标随从获得+2攻击力
                p.minionGetBuffed(target, 2, 0);
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
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),  // 有目标时才能使用
            new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),      // 目标必须是友方
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),        // 目标必须是随从
            };
        }
    }
}