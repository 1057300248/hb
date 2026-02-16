using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 湖之仙女（Maiden of the Lake）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_085 : SimTemplate
    {
        /// <summary>
        /// 当光环效果开始时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发光环的随从。</param>
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 检查是否为己方随从
            if (own.own)
            {
                // 将己方英雄技能费用减少至1点
                if (p.ownHeroAblility.manacost > 1)
                {
                    p.ownHeroAblility.manacost = 1;
                }
            }
            else
            {
                // 将敌方英雄技能费用减少至1点
                if (p.enemyHeroAblility.manacost > 1)
                {
                    p.enemyHeroAblility.manacost = 1;
                }
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 检查是否为己方随从
            if (own.own)
            {
                // 检查是否还有其他湖之仙女
                bool another = false;
                foreach (Minion m in p.ownMinions)
                {
                    if (m.name == CardDB.cardNameEN.maidenofthelake && !m.silenced && own.entitiyID != m.entitiyID)
                    {
                        another = true;
                        break;
                    }
                }

                // 如果没有其他湖之仙女，则恢复英雄技能费用
                if (!another)
                {
                    p.ownHeroAblility.manacost++;
                }
            }
            else
            {
                // 敌方逻辑同理
                bool another = false;
                foreach (Minion m in p.enemyMinions)
                {
                    if (m.name == CardDB.cardNameEN.maidenofthelake && !m.silenced && own.entitiyID != m.entitiyID)
                    {
                        another = true;
                        break;
                    }
                }

                if (!another)
                {
                    p.enemyHeroAblility.manacost++;
                }
            }
        }
    }
}