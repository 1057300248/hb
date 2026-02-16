using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 剽窃（Burgle）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_033 : SimTemplate
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
            // 随机将两张对手职业的卡牌置入当前玩家的手牌
            for (int i = 0; i < 2; i++)
            {
                p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
            }
        }
    }
}