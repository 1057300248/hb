using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 伊莉斯·逐星（Elise Starseeker）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_079 : SimTemplate
    {
        // 获取“黄金猿藏宝图”卡牌数据
        private readonly CardDB.Card goldenMonkeyMap = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_019t);

        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 将“黄金猿藏宝图”洗入牌库
            if (own.own)
            {
                p.AddToDeck(goldenMonkeyMap);
            }
            else
            {
                // 注意：敌方牌库操作可能需要特殊处理，这里假设AddToDeck可以处理
                p.AddToDeck(goldenMonkeyMap);
            }
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
            new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
        };
        }
    }
}