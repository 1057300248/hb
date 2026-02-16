using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 管理者埃克索图斯卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为9点，攻击力6，生命值6
    // 卡牌效果：<b>亡语：</b>用炎魔之王拉格纳罗斯替换你的英雄。
    class Sim_BRM_027 : SimTemplate //* 管理者埃克索图斯 Majordomo Executus
    // <b>Deathrattle:</b> Replace your hero with Ragnaros the Firelord.
    // <b>亡语：</b>用炎魔之王拉格纳罗斯替换你的英雄。 
    {
        // 重写亡语效果方法，这是管理者埃克索图斯卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 设置新的英雄技能
            p.setNewHeroPower(CardDB.cardIDEnum.BRM_027p, m.own);

            // 根据管理者埃克索图斯的归属替换相应的英雄
            if (m.own)
            {
                // 替换己方英雄
                p.ownHeroName = HeroEnum.ragnarosthefirelord; // 设置英雄名为炎魔之王拉格纳罗斯
                p.ownHero.Hp = 8; // 设置生命值为8
                p.ownHero.maxHp = 8; // 设置最大生命值为8
            }
            else
            {
                // 替换敌方英雄
                p.enemyHeroName = HeroEnum.ragnarosthefirelord; // 设置英雄名为炎魔之王拉格纳罗斯
                p.enemyHero.Hp = 8; // 设置生命值为8
                p.enemyHero.maxHp = 8; // 设置最大生命值为8
            }
        }
    }
}