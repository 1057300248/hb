using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 加拉克苏斯之拳（Fist of Jaraxxus）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_022 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算伤害值
            int dmg = ownplay ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);

            // 确定目标
            if (ownplay)
            {
                target = p.getEnemyCharTargetForRandomSingleDamage(dmg);
            }
            else
            {
                target = p.searchRandomMinion(p.ownMinions, searchmode.searchLowestHP);
                if (target == null) target = p.ownHero;
            }

            // 对目标造成伤害
            p.minionGetDamageOrHeal(target, dmg);
        }

        /// <summary>
        /// 当弃牌效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="hc">被弃掉的手牌。</param>
        /// <param name="own">触发效果的随从。</param>
        /// <param name="num">弃牌数量。</param>
        /// <param name="checkBonus">是否检查奖励。</param>
        /// <returns>是否触发效果。</returns>
        public override bool onCardDicscard(Playfield p, Handmanager.Handcard hc, Minion own, int num, bool checkBonus)
        {
            // 如果是检查奖励阶段，则不触发效果
            if (checkBonus) return true;

            // 确定使用者
            bool ownplay = true;
            if (own != null) ownplay = own.own;

            // 计算伤害值
            int dmg = ownplay ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);

            // 确定目标
            Minion target = null;
            if (ownplay)
            {
                target = p.getEnemyCharTargetForRandomSingleDamage(dmg);
            }
            else
            {
                target = p.searchRandomMinion(p.ownMinions, searchmode.searchLowestHP);
                if (target == null) target = p.ownHero;
            }

            // 对目标造成伤害
            p.minionGetDamageOrHeal(target, dmg);
            return true;
        }
    }
}