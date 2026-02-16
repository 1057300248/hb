using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 奥格瑞玛狼骑士（Orgrimmar Aspirant）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_066 : SimTemplate
    {
        /// <summary>
        /// 当激励效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发激励的随从。</param>
        /// <param name="own">是否为当前玩家的随从。</param>
        public override void onInspire(Playfield p, Minion m, bool own)
        {
            // 检查是否为当前玩家的随从
            if (m.own == own)
            {
                // 使武器获得+1攻击力
                if (own)
                {
                    if (p.ownWeapon.Durability > 0)
                        p.ownWeapon.Angr++;
                }
                else
                {
                    if (p.enemyWeapon.Durability > 0)
                        p.enemyWeapon.Angr++;
                }
            }
        }
    }
}