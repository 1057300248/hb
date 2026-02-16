using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 国王护卫者（King's Defender）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_065 : SimTemplate
    {
        // 获取武器卡牌数据
        private readonly CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_065);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备国王护卫者武器
            p.equipWeapon(weapon, ownplay);

            // 获取当前玩家的随从列表
            List<Minion> minions = ownplay ? p.ownMinions : p.enemyMinions;

            // 检查是否有具有嘲讽的随从
            foreach (Minion m in minions)
            {
                if (m.taunt)
                {
                    // 如果有，则增加武器耐久度
                    if (ownplay)
                        p.ownWeapon.Durability++;
                    else
                        p.enemyWeapon.Durability++;
                    break;
                }
            }
        }
    }
}