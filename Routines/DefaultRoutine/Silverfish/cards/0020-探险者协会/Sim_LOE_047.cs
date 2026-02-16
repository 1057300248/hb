using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 墓穴蜘蛛（Tomb Spider）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_047 : SimTemplate
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
            // 随机将一张野兽牌置入当前玩家的手牌（模拟发现效果）
            p.drawACard(CardDB.Race.PET, own.own, true);
        }
    }
}