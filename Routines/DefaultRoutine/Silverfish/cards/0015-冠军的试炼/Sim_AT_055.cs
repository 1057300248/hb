using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 快速治疗（Flash Heal）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_055 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从或英雄。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算恢复的生命值
            int heal = ownplay ? p.getSpellHeal(5) : p.getEnemySpellHeal(5);

            // 对目标恢复生命值
            p.minionGetDamageOrHeal(target, -heal);
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY), // 需要指定目标
            };
        }
    }
}