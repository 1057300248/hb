using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 轮回（Effigy）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_002 : SimTemplate
    {
        /// <summary>
        /// 当奥秘被触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家触发此奥秘。</param>
        /// <param name="target">触发奥秘的目标随从。</param>
        /// <param name="number">附加参数（如有）。</param>
        public override void onSecretPlay(Playfield p, bool ownplay, Minion target, int number)
        {
            // 获取与目标随从法力值消耗相同的随机随从卡牌
            CardDB.Card randomMinion = p.getRandomCardForManaMinion(target.handcard.card.cost);

            // 在目标随从死亡的位置召唤该随从
            p.callKid(randomMinion, target.zonepos, ownplay);
        }
    }
}
