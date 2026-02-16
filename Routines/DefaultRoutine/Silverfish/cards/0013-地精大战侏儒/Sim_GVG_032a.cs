using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 水晶赠礼卡牌的模拟实现类，继承自SimTemplate基类
    // 这是林地树妖的抉择选项之一，法师职业法术卡牌，费用为0点
    // 卡牌效果：使每个玩家获得一个法力水晶。
    class Sim_GVG_032a : SimTemplate //* 水晶赠礼 Gift of Mana
    // Give each player a Mana Crystal.
    // 使每个玩家获得一个法力水晶。 
    {
        // 重写战吼效果方法，这是水晶赠礼卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 增加当前法力值（上限为10）
            p.mana = Math.Min(10, p.mana + 1);

            // 增加己方法力水晶上限（上限为10）
            p.ownMaxMana = Math.Min(10, p.ownMaxMana + 1);

            // 增加敌方法力水晶上限（上限为10）
            p.enemyMaxMana = Math.Min(10, p.enemyMaxMana + 1);
        }
    }
}