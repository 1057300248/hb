using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 暗影投弹手卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力3，生命值2
    // 卡牌效果：<b>战吼：</b>对每个英雄造成3点伤害。
    class Sim_GVG_009 : SimTemplate //* 暗影投弹手 Shadowbomber
    //<b>Battlecry:</b> Deal 3 damage to each hero.
    //<b>战吼：</b>对每个英雄造成3点伤害。 
    {
        // 重写战吼效果方法，这是暗影投弹手卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 定义固定的伤害值为3点
            int damage = 3;

            // 对己方英雄造成3点伤害
            // 调用游戏场地的伤害/治疗方法，对己方英雄(p.ownHero)造成指定伤害
            p.minionGetDamageOrHeal(p.ownHero, damage);

            // 对敌方英雄造成3点伤害
            // 调用游戏场地的伤害/治疗方法，对敌方英雄(p.enemyHero)造成指定伤害
            // 此方法内部会自动处理伤害计算、死亡判定等相关逻辑
            p.minionGetDamageOrHeal(p.enemyHero, damage);
        }
    }
}