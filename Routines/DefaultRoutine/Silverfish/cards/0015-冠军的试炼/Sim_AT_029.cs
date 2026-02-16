using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 锈水海盗（Buccaneer）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_029 : SimTemplate
    {
        /// <summary>
        /// 当武器发生变化时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家的武器变化。</param>
        public override void onWeaponChanged(Playfield p, bool ownplay)
        {
            // 检查是否为当前玩家的武器变化
            if (ownplay)
            {
                // 使武器获得+1攻击力
                p.ownWeapon.Angr++;
            }
            else
            {
                // 使敌方武器获得+1攻击力
                p.enemyWeapon.Angr++;
            }
        }
    }
}