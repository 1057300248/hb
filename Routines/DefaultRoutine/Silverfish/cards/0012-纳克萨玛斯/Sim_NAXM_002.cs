using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 骷髅铁匠卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力3，生命值5
    // 卡牌效果：<b>亡语：</b>摧毁对手的武器。
    class Sim_NAXM_002 : SimTemplate //* 骷髅铁匠 Skeletal Smith
    // <b>Deathrattle:</b> Destroy your opponent's weapon.
    // <b>亡语：</b>摧毁对手的武器。 
    {
        // 重写亡语效果方法，这是骷髅铁匠卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 摧毁对手的武器
            // 参数说明：- 1000：传入一个足够大的数值来确保完全摧毁武器（耐久度降为0或以下）
            // - !m.own：取反操作，如果骷髅铁匠是己方的，则摧毁敌方武器；如果是敌方的，则摧毁己方武器
            p.lowerWeaponDurability(1000, !m.own);
        }
    }
}