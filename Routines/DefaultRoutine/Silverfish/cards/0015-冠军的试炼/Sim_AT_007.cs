using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 嗜法者（Spellslinger）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_007 : SimTemplate
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
            // 为双方玩家各抽取一张随机法术牌
            p.drawACard(CardDB.cardNameEN.unknown, true, true);  // 己方玩家抽取一张随机法术牌
            p.drawACard(CardDB.cardNameEN.unknown, false, true); // 敌方玩家抽取一张随机法术牌
        }
    }
}