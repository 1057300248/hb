using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 鬼灵骑士卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力4，生命值6
    // 卡牌效果：无法成为法术或英雄技能的目标。
    class Sim_FP1_008 : SimTemplate //* 鬼灵骑士 Spectral Knight
    // Can't be targeted by spells or Hero Powers.
    // 无法成为法术或英雄技能的目标。 
    {
        // 重写战吼效果方法，这是鬼灵骑士卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 设置鬼灵骑士无法成为法术或英雄技能的目标
            own.cantBeTargetedBySpellsOrHeroPowers = true;
        }
    }
}