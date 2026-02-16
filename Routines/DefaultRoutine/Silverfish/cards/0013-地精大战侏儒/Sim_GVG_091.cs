using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 施法者克星X-21卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力2，生命值5
    // 卡牌效果：<b>嘲讽，</b>无法成为法术或英雄技能的目标。
    class Sim_GVG_091 : SimTemplate //* 施法者克星X-21 Arcane Nullifier X-21
    // <b>Taunt</b>Can't be targeted by spells or Hero Powers.
    // <b>嘲讽，</b>无法成为法术或英雄技能的目标。 
    {
        // 重写战吼效果方法，这是施法者克星X-21卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 空实现或可删除此方法
        }
    }
}