using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 盾甲侍女卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力5，生命值5
    // 卡牌效果：<b>战吼：</b>获得5点护甲值。
    class Sim_GVG_053 : SimTemplate //* 盾甲侍女 Shieldmaiden
    // <b>Battlecry:</b> Gain 5 Armor.
    // <b>战吼：</b>获得5点护甲值。 
    {
        // 重写战吼效果方法，这是盾甲侍女卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查盾甲侍女是否属于己方
            if (own.own)
            {
                // 为己方英雄增加5点护甲值
                p.minionGetArmor(p.ownHero, 5);
            }
            else
            {
                // 为敌方英雄增加5点护甲值
                p.minionGetArmor(p.enemyHero, 5);
            }
        }
    }
}