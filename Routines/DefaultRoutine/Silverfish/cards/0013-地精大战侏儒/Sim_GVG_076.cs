using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 自爆绵羊卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1，生命值1
    // 卡牌效果：<b>亡语：</b>对所有随从造成2点伤害。
    class Sim_GVG_076 : SimTemplate //* 自爆绵羊 Explosive Sheep
    // <b>Deathrattle:</b> Deal 2 damage to all minions.
    // <b>亡语：</b>对所有随从造成2点伤害。 
    {
        // 重写亡语效果方法，这是自爆绵羊卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 对场上所有随从造成2点伤害
            // 这个方法会遍历所有随从（包括己方和敌方）并造成指定伤害
            p.allMinionsGetDamage(2);
        }
    }
}