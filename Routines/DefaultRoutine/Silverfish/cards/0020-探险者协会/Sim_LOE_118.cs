using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //* 诅咒之刃 Cursed Blade
    //Double all damage dealt to your hero.
    //你的英雄受到的所有伤害效果翻倍。 
    /// <summary>
    /// 诅咒之刃（Cursed Blade）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_118 : SimTemplate
    {
        // 获取“诅咒之刃”武器卡牌数据
        private readonly CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_118);

        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备“诅咒之刃”武器
            p.equipWeapon(weapon, ownplay);
        }
    }
}