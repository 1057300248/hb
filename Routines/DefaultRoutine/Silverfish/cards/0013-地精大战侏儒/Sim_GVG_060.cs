using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 军需官卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力2，生命值5
    // 卡牌效果：<b>战吼：</b>使你的白银之手新兵获得+2/+2。
    class Sim_GVG_060 : SimTemplate //* 军需官 Quartermaster
    // <b>Battlecry:</b> Give your Silver Hand Recruits +2/+2.
    // <b>战吼：</b>使你的白银之手新兵获得+2/+2。 
    {
        // 重写战吼效果方法，这是军需官卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据随从的归属确定要检查的随从列表（己方或敌方）
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，为白银之手新兵增加属性
            foreach (Minion m in temp)
            {
                // 检查随从是否为白银之手新兵
                if (m.name == CardDB.cardNameEN.silverhandrecruit)
                {
                    // 给白银之手新兵增加+2攻击力和+2生命值
                    p.minionGetBuffed(m, 2, 2);
                }
            }
        }
    }
}