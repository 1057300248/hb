using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 纯洁者耶德瑞克（Eadric the Pure）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_081 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查是否为己方随从
            if (own.own)
            {
                // 将所有敌方随从的攻击力变为1
                foreach (Minion m in p.enemyMinions)
                {
                    p.minionSetAngrToX(m, 1);
                }
            }
            else
            {
                // 将所有己方随从的攻击力变为1
                foreach (Minion m in p.ownMinions)
                {
                    p.minionSetAngrToX(m, 1);
                }
            }
        }
    }
}