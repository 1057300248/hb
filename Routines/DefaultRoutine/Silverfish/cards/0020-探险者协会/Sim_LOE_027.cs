using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 审判（Sacred Trial）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_027 : SimTemplate
    {
        /// <summary>
        /// 当奥秘被触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家触发此奥秘。</param>
        /// <param name="target">被消灭的目标随从。</param>
        /// <param name="number">附加参数（如有）。</param>
        public override void onSecretPlay(Playfield p, bool ownplay, Minion target, int number)
        {
            // 获取对手的随从列表
            List<Minion> opponentMinions = ownplay ? p.enemyMinions : p.ownMinions;

            // 如果对手控制至少三个随从，则消灭目标随从
            if (opponentMinions.Count >= 3)
            {
                p.minionGetDestroyed(target);
            }
        }
    }
}