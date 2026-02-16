using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 石化迅猛龙（Unearthed Raptor）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_019 : SimTemplate
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
            // 检查目标是否存在且具有亡语
            if (target != null && target.handcard.card.deathrattle)
            {
                // 复制目标随从的亡语效果
                own.deathrattle2 = target.deathrattle2;
                own.ancestralspirit = target.ancestralspirit;
                own.desperatestand = target.desperatestand;
                own.souloftheforest = target.souloftheforest;
                own.stegodon = target.stegodon;
                own.livingspores = target.livingspores;
                own.explorershat = target.explorershat;
                own.returnToHand = target.returnToHand;
                own.infest = target.infest;
                own.itsnecrolit = target.itsnecrolit;
                own.sheepmask = target.sheepmask;
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
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_DEATHRATTLE),  // 目标必须具有亡语
            new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),          // 目标必须是友方
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),      // 有目标时才能使用
            };
        }
    }
}
