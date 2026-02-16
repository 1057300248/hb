using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 毒镖陷阱（Dart Trap）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_021 : SimTemplate
    {
        /// <summary>
        /// 当奥秘被触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家触发此奥秘。</param>
        /// <param name="number">附加参数（如有）。</param>
        public override void onSecretPlay(Playfield p, bool ownplay, int number)
        {
            // 获取敌方随从列表
            List<Minion> enemies = ownplay ? p.enemyMinions : p.ownMinions;
            Minion target = null;

            // 优先选择高攻击力低生命值的随从
            if (enemies.Count > 0)
            {
                target = p.searchRandomMinion(enemies, searchmode.searchHighAttackLowHP);
            }

            // 如果没有合适的随从或敌方英雄生命值较低，则攻击敌方英雄
            if (target == null || (ownplay ? p.enemyHero.Hp : p.ownHero.Hp) < 6)
            {
                target = ownplay ? p.enemyHero : p.ownHero;
            }

            // 对目标造成5点伤害
            p.minionGetDamageOrHeal(target, 5);
        }
    }
}