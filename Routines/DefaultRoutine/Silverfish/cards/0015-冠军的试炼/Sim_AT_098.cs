using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 杂耍吞法者（Sideshow Spelleater）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_098 : SimTemplate
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
            // 检查是否为己方随从
            if (m.own)
            {
                // 复制对手的英雄技能
                p.ownHeroAblility = new Handmanager.Handcard(p.enemyHeroAblility);
            }
            else
            {
                // 复制己方的英雄技能
                p.enemyHeroAblility = new Handmanager.Handcard(p.ownHeroAblility);
            }
        }
    }
}