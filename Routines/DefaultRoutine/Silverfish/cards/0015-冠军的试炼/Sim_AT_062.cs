using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 天降蛛群（Ball of Spiders）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_062 : SimTemplate
    {
        // 获取结网蛛卡牌数据
        private readonly CardDB.Card webspinner = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_011);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 获取召唤位置
            int position = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;

            // 召唤三个1/1的结网蛛
            for (int i = 0; i < 3; i++)
            {
                p.callKid(webspinner, position, ownplay);
            }
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1), // 需要至少1个随从槽位
            };
        }
    }
}