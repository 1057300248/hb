using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 地精自动理发装置卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力3，生命值2
    // 卡牌效果：<b>战吼：</b>使你的武器获得+1攻击力。
    class Sim_GVG_023 : SimTemplate //* 地精自动理发装置 Goblin Auto-Barber
    //<b>Battlecry:</b> Give your weapon +1 Attack.
    //<b>战吼：</b>使你的武器获得+1攻击力。 
    {
        // 重写战吼效果方法，这是地精自动理发装置卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查地精自动理发装置是否属于己方
            if (own.own)
            {
                // 检查己方是否有武器且耐久度大于等于1
                if (p.ownWeapon.Durability >= 1)
                {
                    // 增加己方武器攻击力+1
                    p.ownWeapon.Angr += 1;

                    // 同时给己方英雄增加+1攻击力（因为武器攻击力会反映在英雄攻击力上）
                    p.minionGetBuffed(p.ownHero, 1, 0);
                }
            }
            else // 处理敌方地精自动理发装置的情况
            {
                // 检查敌方是否有武器且耐久度大于等于1
                if (p.enemyWeapon.Durability >= 1)
                {
                    // 增加敌方武器攻击力+1
                    p.enemyWeapon.Angr += 1;

                    // 同时给敌方英雄增加+1攻击力
                    p.minionGetBuffed(p.enemyHero, 1, 0);
                }
            }
        }
    }
}