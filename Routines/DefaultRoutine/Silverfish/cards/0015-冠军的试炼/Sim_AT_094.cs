using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 火焰杂耍者（Flame Juggler）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_094 : SimTemplate
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
                // 随机对一个敌方角色造成1点伤害
                target = p.getEnemyCharTargetForRandomSingleDamage(1);
            }
            else
            {
                // 敌方逻辑：随机对一个己方随从或英雄造成1点伤害
                target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack);
                if (target == null)
                    target = p.ownHero;
            }

            // 对目标造成1点伤害
            p.minionGetDamageOrHeal(target, 1);
        }
    }
}