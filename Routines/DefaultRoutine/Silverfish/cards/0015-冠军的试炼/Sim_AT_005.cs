using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 变形术：野猪（Polymorph: Boar）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_005 : SimTemplate
    {
        // 获取野猪卡牌数据
        private readonly CardDB.Card boar = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_005t);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将目标随从变形为野猪
            p.minionTransform(target, boar);
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