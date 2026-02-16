using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 石化魔暴龙（Fossilized Devilsaur）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_073 : SimTemplate
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
            // 获取己方随从列表
            List<Minion> friendlyMinions = own.own ? p.ownMinions : p.enemyMinions;

            // 检查是否控制野兽
            bool hasBeast = false;
            foreach (Minion m in friendlyMinions)
            {
                if (m.handcard.card.race == CardDB.Race.PET)
                {
                    hasBeast = true;
                    break;
                }
            }

            // 如果控制野兽，则获得嘲讽
            if (hasBeast)
            {
                own.taunt = true;
                if (own.own)
                    p.anzOwnTaunt++;
                else
                    p.anzEnemyTaunt++;
            }
        }
    }
}