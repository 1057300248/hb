using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 恐惧战马（Dreadsteed）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_019 : SimTemplate
    {
        // 获取恐惧战马卡牌数据
        private readonly CardDB.Card dreadsteed = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_019);

        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 在死亡位置召唤一个新的恐惧战马
            p.callKid(dreadsteed, m.zonepos - 1, m.own);
        }
    }
}