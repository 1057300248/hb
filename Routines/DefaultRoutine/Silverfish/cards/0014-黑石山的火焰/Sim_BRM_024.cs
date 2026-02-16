using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 龙人打击者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张战士职业随从卡牌，费用为6点，攻击力6，生命值6
    // 卡牌效果：<b>战吼：</b>如果你对手的生命值小于或等于15点，便获得+3/+3。
    class Sim_BRM_024 : SimTemplate //* 龙人打击者 Drakonid Crusher
    // <b>Battlecry:</b> If your opponent has 15 or less Health, gain +3/+3.
    // <b>战吼：</b>如果你对手的生命值小于或等于15点，便获得+3/+3。 
    {
        // 重写战吼效果方法，这是龙人打击者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 获取对手的生命值
            int opponentHealth = m.own ? p.enemyHero.Hp : p.ownHero.Hp;

            // 如果对手生命值小于或等于15，则龙人打击者获得+3/+3
            if (opponentHealth <= 15)
            {
                p.minionGetBuffed(m, 3, 3); // 参数：- 攻击力增加3，- 生命值增加3
            }
        }
    }
}