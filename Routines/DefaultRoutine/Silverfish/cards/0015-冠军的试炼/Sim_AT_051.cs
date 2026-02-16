using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 元素毁灭（Elemental Destruction）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_051 : SimTemplate
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
            // 计算伤害值（4到5点之间随机）
            int baseDamage = 4;
            int randomDamage = new Random().Next(0, 2); // 随机增加0或1点伤害
            int totalDamage = baseDamage + randomDamage;

            // 对所有随从造成伤害
            int dmg = ownplay ? p.getSpellDamageDamage(totalDamage) : p.getEnemySpellDamageDamage(totalDamage);
            p.allMinionsGetDamage(dmg);

            // 过载：增加2点过载值
            if (ownplay)
            {
                p.ueberladung += 2;
            }
        }
    }
}