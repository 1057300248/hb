using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 埋葬（Entomb）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_104 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（敌方随从）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将目标随从洗入当前玩家的牌库
            if (ownplay)
            {
                p.AddToDeck(target.handcard.card);
            }
            else
            {
                // 敌方使用埋葬，将目标随从洗入敌方牌库
                p.AddToDeck(target.handcard.card);
            }
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
        };
        }
    }
}