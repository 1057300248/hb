using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 雷诺·杰克逊（Reno Jackson）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_011 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查是否为己方随从且牌库中没有重复卡牌
            if (m.own && p.prozis.noDuplicates)
            {
                // 为己方英雄恢复所有生命值
                p.minionGetDamageOrHeal(p.ownHero, p.ownHero.Hp - p.ownHero.maxHp);
            }
        }
    }
}