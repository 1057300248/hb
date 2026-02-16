using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 漂浮观察者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力4，生命值4
    // 卡牌效果：每当你的英雄在你的回合受到伤害，便获得+2/+2。
    class Sim_GVG_100 : SimTemplate //* 漂浮观察者 Floating Watcher
    // Whenever your hero takes damage on your turn, gain +2/+2.
    // 每当你的英雄在你的回合受到伤害，便获得+2/+2。 
    {
        // 重写随从受到伤害触发方法，这是漂浮观察者卡牌效果的核心实现
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查漂浮观察者是否属于己方且己方英雄受到了伤害
            if (p.ownHero.anzGotDmg > 0 && m.own)
            {
                // 给己方漂浮观察者增加+2/+2（根据英雄受伤害次数）
                p.minionGetBuffed(m, 2 * p.ownHero.anzGotDmg, 2 * p.ownHero.anzGotDmg);
            }
            // 检查漂浮观察者是否属于敌方且敌方英雄受到了伤害
            else if (p.enemyHero.anzGotDmg > 0 && !m.own)
            {
                // 给敌方漂浮观察者增加+2/+2（根据英雄受伤害次数）
                p.minionGetBuffed(m, 2 * p.enemyHero.anzGotDmg, 2 * p.enemyHero.anzGotDmg);
            }
        }
    }
}