using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 强风射击（Powershot）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_056 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算伤害值
            int dmg = ownplay ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);

            // 对目标随从造成伤害
            p.minionGetDamageOrHeal(target, dmg);

            // 获取目标随从所在的一侧随从列表
            List<Minion> minions = target.own ? p.ownMinions : p.enemyMinions;

            // 对目标随从相邻的随从造成伤害
            foreach (Minion m in minions)
            {
                if (Math.Abs(target.zonepos - m.zonepos) == 1)
                {
                    p.minionGetDamageOrHeal(m, dmg);
                }
            }
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),   // 需要指定目标
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),    // 目标必须是随从
            };
        }
    }
}