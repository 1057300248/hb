using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 黑曜石毁灭者（Obsidian Destroyer）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_009 : SimTemplate
    {
        // 获取甲虫卡牌数据
        private readonly CardDB.Card scarab = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_009t);

        /// <summary>
        /// 当回合结束时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="triggerEffectMinion">触发效果的随从。</param>
        /// <param name="turnEndOfOwner">是否为当前玩家的回合结束。</param>
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查是否为当前玩家的回合结束
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                // 在黑曜石毁灭者的位置召唤一只1/1并具有嘲讽的甲虫
                int position = triggerEffectMinion.zonepos;
                p.callKid(scarab, position, triggerEffectMinion.own);
            }
        }
    }
}