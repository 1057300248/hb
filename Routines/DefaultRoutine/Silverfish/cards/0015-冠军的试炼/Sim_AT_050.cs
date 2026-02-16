using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 灌魔之锤（Charged Hammer）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_050 : SimTemplate
    {
        // 获取武器卡牌数据
        private readonly CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_050);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备灌魔之锤武器
            p.equipWeapon(weapon, ownplay);
        }

        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 将英雄技能改为“造成2点伤害”
            p.setNewHeroPower(CardDB.cardIDEnum.AT_050t, m.own);
        }
    }
}