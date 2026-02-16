using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 争强好胜（Competitive Spirit）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_073 : SimTemplate
    {
        /// <summary>
        /// 当奥秘被触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家触发此奥秘。</param>
        /// <param name="number">附加参数（如有）。</param>
        public override void onSecretPlay(Playfield p, bool ownplay, int number)
        {
            // 使当前玩家的所有随从获得+1/+1
            p.allMinionOfASideGetBuffed(ownplay, 1, 1);
        }
    }
}