using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 捕熊陷阱（Bear Trap）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_060 : SimTemplate
    {
        // 获取灰熊卡牌数据
        private readonly CardDB.Card bear = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_125);

        /// <summary>
        /// 当奥秘被触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家触发此奥秘。</param>
        /// <param name="number">附加参数（如有）。</param>
        public override void onSecretPlay(Playfield p, bool ownplay, int number)
        {
            // 获取召唤位置
            int position = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;

            // 召唤一个3/3并具有嘲讽的灰熊
            p.callKid(bear, position, ownplay);
        }
    }
}