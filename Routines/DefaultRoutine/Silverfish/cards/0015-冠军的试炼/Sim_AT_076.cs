using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 鱼人骑士（Murloc Knight）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_076 : SimTemplate
    {
        // 获取随机鱼人卡牌数据（此处以蓝腮战士为例）
        private readonly CardDB.Card murloc = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_050);

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
                // 在鱼人骑士的位置召唤一个随机鱼人
                p.callKid(murloc, m.zonepos, m.own);
            }
        }
    }
}