using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 探险帽（Explorer's Hat）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_105 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 使目标随从获得+1/+1
            p.minionGetBuffed(target, 1, 1);

            // 设置亡语效果：将探险帽置入手牌
            target.explorershat = 1;
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
        };
        }
    }
}