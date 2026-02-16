using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 摇摆的俾格米（Wobbling Runts）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_089 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 召唤三个2/2的俾格米
            CardDB.Card runt1 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_089t);
            CardDB.Card runt2 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_089t2);
            CardDB.Card runt3 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_089t3);

            p.callKid(runt1, m.zonepos - 1, m.own);
            p.callKid(runt2, m.zonepos, m.own);
            p.callKid(runt3, m.zonepos + 1, m.own);
        }
    }
}