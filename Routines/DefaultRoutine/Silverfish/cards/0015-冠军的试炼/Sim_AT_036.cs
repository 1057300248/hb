using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 阿努巴拉克（Anub'arak）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_036 : SimTemplate
    {
        // 获取蛛魔卡牌数据
        private readonly CardDB.Card nerubian = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_007t);

        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 将阿努巴拉克移回手牌
            p.minionReturnToHand(m, m.own, 0);

            // 在死亡位置召唤一个4/4的蛛魔
            p.callKid(nerubian, m.zonepos - 1, m.own);
        }
    }
}