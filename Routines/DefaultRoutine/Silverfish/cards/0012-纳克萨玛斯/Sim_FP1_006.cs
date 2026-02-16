using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 死亡战马卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力2，生命值3
    // 卡牌效果：<b>冲锋，亡语：</b>对你的英雄造成3点伤害。
    class Sim_FP1_006 : SimTemplate //* 死亡战马 Deathcharger
    // <b>Charge. Deathrattle:</b> Deal 3 damage to your hero.
    // <b>冲锋，亡语：</b>对你的英雄造成3点伤害。 
    {
        // 重写亡语效果方法，这是死亡战马卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 对死亡战马拥有者的英雄造成3点伤害
            // 参数说明：- 目标英雄（如果死亡战马是己方的，则伤害己方英雄；反之亦然）
            // - 3点伤害值
            p.minionGetDamageOrHeal(m.own ? p.ownHero : p.enemyHero, 3);
        }
    }
}