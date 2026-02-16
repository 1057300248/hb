using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 要塞指挥官（Garrison Commander）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_080 : SimTemplate
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
                // 检查是否已有另一个要塞指挥官
                bool another = false;
                foreach (Minion m in p.ownMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID)
                    {
                        another = true;
                        break;
                    }
                }

                // 如果没有另一个要塞指挥官，则增加英雄技能使用次数
                if (!another)
                {
                    p.ownHeroPowerAllowedQuantity++;
                }
            }
            else
            {
                // 敌方逻辑同理
                bool another = false;
                foreach (Minion m in p.enemyMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID)
                    {
                        another = true;
                        break;
                    }
                }

                if (!another)
                {
                    p.enemyHeroPowerAllowedQuantity++;
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
                // 检查是否已有另一个要塞指挥官
                bool another = false;
                foreach (Minion m in p.ownMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID)
                    {
                        another = true;
                        break;
                    }
                }

                // 如果没有另一个要塞指挥官，则减少英雄技能使用次数
                if (!another)
                {
                    p.ownHeroPowerAllowedQuantity--;

                    // 如果已使用次数超过剩余允许次数，则禁用英雄技能
                    if (p.anzUsedOwnHeroPower >= p.ownHeroPowerAllowedQuantity)
                    {
                        p.ownAbilityReady = false;
                    }
                }
            }
            else
            {
                // 敌方逻辑同理
                bool another = false;
                foreach (Minion m in p.enemyMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID)
                    {
                        another = true;
                        break;
                    }
                }

                if (!another)
                {
                    p.enemyHeroPowerAllowedQuantity--;

                    // 如果已使用次数超过剩余允许次数，则禁用英雄技能
                    if (p.anzUsedEnemyHeroPower >= p.enemyHeroPowerAllowedQuantity)
                    {
                        p.enemyAbilityReady = false;
                    }
                }
            }
        }
    }
}