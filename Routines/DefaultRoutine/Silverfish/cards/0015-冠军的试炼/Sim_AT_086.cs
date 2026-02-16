using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 破坏者（Saboteur）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_086 : SimTemplate
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
                // 下个回合敌方英雄技能费用增加5点
                p.enemyHeroPowerCostLessOnce += 5;
            }
            else
            {
                // 下个回合己方英雄技能费用增加5点
                p.ownHeroPowerCostLessOnce += 5;
            }
        }
    }
}