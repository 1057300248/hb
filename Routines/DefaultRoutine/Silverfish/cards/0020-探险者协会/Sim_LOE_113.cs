using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 鱼人恩典（Everyfin is Awesome）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_113 : SimTemplate
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
            // 使你的所有随从获得+2/+2
            p.allMinionOfASideGetBuffed(ownplay, 2, 2);
        }
         //减费功能ai已经完善，不需要再写，只写buff函数
    }
}