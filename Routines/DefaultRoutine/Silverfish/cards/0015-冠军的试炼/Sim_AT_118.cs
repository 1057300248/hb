using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 十字军统领（Grand Crusader）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_118 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 随机将一张圣骑士牌置入手牌
            p.drawACard(CardDB.cardIDEnum.None, m.own, true);
        }
    }
}