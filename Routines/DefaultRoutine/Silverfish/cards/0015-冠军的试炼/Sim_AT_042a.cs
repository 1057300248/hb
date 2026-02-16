using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 雄狮形态（Lion Form）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_042a : SimTemplate
    {
        // 获取冲锋形态的卡牌数据
        private readonly CardDB.Card chargeForm = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_042t);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将目标随从变形为冲锋形态
            p.minionTransform(target, chargeForm);
        }
    }
}