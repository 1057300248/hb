using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 德鲁伊 费用：3
    //New Heights
    //攀上新高
    //[x]Increase your maximumMana by 3 and gain anempty Mana Crystal.
    //将你的法力值上限提高3点，获得一个空的法力水晶。
    /// <summary>
    /// 攀上新高（New Heights）卡牌的模拟实现。
    /// </summary>
    class Sim_VAC_949 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 将你的法力值上限提高3点，并获得1个空的法力水晶
            if (ownplay)
            {
                p.ownMaxMana = Math.Min(10, p.ownMaxMana + 3); // 法力值上限+3
                p.mana = Math.Min(p.mana + 1, p.ownMaxMana);   // 获得1个空的法力水晶（当前法力值+1，但不超过新的上限）
            }
            else
            {
                p.enemyMaxMana = Math.Min(10, p.enemyMaxMana + 3); // 敌方法力值上限+3
                                                                   // 敌方当前法力值通常由游戏逻辑处理，此处不直接修改
            }
        }
    }
}
