using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 海象人图腾师（Tuskarr Totemic）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_046 : SimTemplate
    {
        // 获取基础图腾卡牌数据（此处以灼热图腾为例）
        private readonly CardDB.Card basicTotem = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_050);

        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 在海象人图腾师的位置召唤一个基础图腾
            p.callKid(basicTotem, own.zonepos, own.own);
        }
    }
}