using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 活体根须（Living Roots）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_037 : SimTemplate
    {
        // 获取树苗卡牌数据
        private readonly CardDB.Card sapling = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_037t);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从。</param>
        /// <param name="choice">选择的选项（1：造成伤害，2：召唤树苗）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 选项1：造成2点伤害
            if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                if (target != null)
                {
                    int damage = ownplay ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);
                    p.minionGetDamageOrHeal(target, damage);
                }
            }

            // 选项2：召唤两个1/1的树苗
            if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                int position = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(sapling, position, ownplay);
                p.callKid(sapling, position, ownplay);
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
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY), // 需要指定目标
            };
        }
    }
}