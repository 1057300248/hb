using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 达纳苏斯豹骑士（Darnassus Aspirant）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_038 : SimTemplate
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
            // 获得一个空的法力水晶（最多不超过10个）
            if (own.own)
                p.ownMaxMana = Math.Min(10, p.ownMaxMana + 1);
            else
                p.enemyMaxMana = Math.Min(10, p.enemyMaxMana + 1);
        }

        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 失去一个法力水晶
            if (m.own)
                p.ownMaxMana = Math.Max(0, p.ownMaxMana - 1);
            else
                p.enemyMaxMana = Math.Max(0, p.enemyMaxMana - 1);
        }
    }
}