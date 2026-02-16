using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 奥术冲击（Arcane Blast）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_004 : SimTemplate
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
            // 计算基础伤害并加上法术伤害增益（翻倍）
            int baseDamage = 2;
            int spellPowerBonus = ownplay ? p.spellpower : p.enemyspellpower;
            int totalDamage = baseDamage + (spellPowerBonus * 2); // 法术伤害增益翻倍

            // 根据使用者计算最终伤害值
            int dmg = ownplay ? p.getSpellDamageDamage(totalDamage) : p.getEnemySpellDamageDamage(totalDamage);

            // 对目标随从造成伤害
            p.minionGetDamageOrHeal(target, dmg);
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要指定目标
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
            };
        }
    }
}