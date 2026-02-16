using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 白银之手教官（Silver Hand Regent）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_100 : SimTemplate
    {
        // 获取白银之手新兵卡牌数据
        private readonly CardDB.Card recruit = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_101t);

        /// <summary>
        /// 当激励效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发激励的随从。</param>
        /// <param name="own">是否为当前玩家的随从。</param>
        public override void onInspire(Playfield p, Minion m, bool own)
        {
            // 检查是否为当前玩家的随从
            if (m.own == own)
            {
                // 在白银之手教官的位置召唤一个1/1的白银之手新兵
                p.callKid(recruit, m.zonepos, own);
            }
        }
    }
}