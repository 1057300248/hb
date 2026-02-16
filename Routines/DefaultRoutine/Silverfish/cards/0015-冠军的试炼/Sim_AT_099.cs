using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 科多兽骑手（Kodorider）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_099 : SimTemplate
    {
        // 获取作战科多兽卡牌数据
        private readonly CardDB.Card warKodo = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_099t);

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
                // 在科多兽骑手的位置召唤一个3/5的作战科多兽
                p.callKid(warKodo, m.zonepos, own);
            }
        }
    }
}