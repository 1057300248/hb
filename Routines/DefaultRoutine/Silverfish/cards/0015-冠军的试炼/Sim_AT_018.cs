using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 银色神官帕尔崔丝（Confessor Paletress）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_018 : SimTemplate
    {
        // 预定义的传说随从卡牌（示例：国王穆克拉）
        private readonly CardDB.Card legendaryMinion = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_014);

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
                // 在触发随从的位置召唤一个传说随从
                p.callKid(legendaryMinion, m.zonepos, m.own);
            }
        }
    }
}