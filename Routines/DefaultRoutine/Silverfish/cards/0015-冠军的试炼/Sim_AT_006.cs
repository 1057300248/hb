using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 达拉然铁骑士（Dalaran Aspirant）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_006 : SimTemplate
    {
        /// <summary>
        /// 当激励效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发激励的随从。</param>
        /// <param name="own">是否为当前玩家的随从。</param>
        public override void onInspire(Playfield p, Minion m, bool own)
        {
            // 检查是否为当前玩家的随从
            if (m.own == own)
            {
                // 增加随从的法术伤害
                m.spellpower++;

                // 更新全局法术伤害值
                if (m.own)
                    p.spellpower++; // 己方法术伤害+1
                else
                    p.enemyspellpower++; // 敌方法术伤害+1
            }
        }

        /// <summary>
        /// 当光环效果结束时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">失去光环的随从。</param>
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 减去随从贡献的法术伤害值
            if (m.own)
                p.spellpower -= m.spellpower; // 己方法术伤害减少
            else
                p.enemyspellpower -= m.spellpower; // 敌方法术伤害减少
        }
    }
}