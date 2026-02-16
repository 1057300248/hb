using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 危机四伏（Beneath the Grounds）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_035 : SimTemplate
    {
        // 获取蛛魔卡牌数据
        private readonly CardDB.Card nerubian = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_007t);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从或英雄。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将三张蛛魔卡牌置入敌方牌库顶部  懒得写对面牌库了  随便写放自己牌库
            for (int i = 0; i < 3; i++)
            {
                p.AddToDeck(nerubian);
            }
        }
    }
}