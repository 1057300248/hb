using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 刃牙德鲁伊（Druid of the Saber）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_042 : SimTemplate
    {
        // 获取变形后的卡牌数据
        private readonly CardDB.Card chargeForm = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_042t);   // 2/1 冲锋形态
        private readonly CardDB.Card stealthForm = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_042t2); // 3/2 潜行形态
        private readonly CardDB.Card tigerForm = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.OG_044c);    // 老虎形态（双效果）

        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（1：冲锋形态，2：潜行形态）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 如果范达尔·鹿盔在场，则同时获得两种效果
            if (p.ownFandralStaghelm > 0 && own.own)
            {
                p.minionTransform(own, tigerForm);
            }
            else
            {
                // 根据选择变形
                if (choice == 1)
                {
                    p.minionTransform(own, chargeForm); // 变形为2/1冲锋形态
                }
                else if (choice == 2)
                {
                    p.minionTransform(own, stealthForm); // 变形为3/2潜行形态
                }
            }
        }
    }
}