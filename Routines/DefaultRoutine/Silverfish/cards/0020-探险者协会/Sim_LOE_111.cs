using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 极恶之咒（Excavated Evil）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_111 : SimTemplate
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
            // 对所有随从造成3点伤害
            int damage = ownplay ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
            p.allMinionsGetDamage(damage);

            // 将该牌洗入对手的牌库
            CardDB.Card excavatedEvil = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_111);
            if (ownplay)
            {
                // 己方使用，洗入敌方牌库
                p.AddToDeck(excavatedEvil);
            }
            else
            {
                // 敌方使用，洗入己方牌库
                p.AddToDeck(excavatedEvil);
            }
        }
    }
}