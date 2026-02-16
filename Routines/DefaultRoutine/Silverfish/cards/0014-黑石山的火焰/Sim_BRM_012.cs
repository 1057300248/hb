using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 火焰驱逐者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张战士职业随从卡牌，费用为3点，攻击力1，生命值3
    // 卡牌效果：<b>战吼：</b>随机获得1-4点攻击力。<b>过载：</b>（1）
    class Sim_BRM_012 : SimTemplate //* 火焰驱逐者 Fireguard Destroyer
    // <b>Battlecry:</b> Gain 1-4 Attack. <b>Overload:</b> (1)
    // <b>战吼：</b>随机获得1-4点攻击力。<b>过载：</b>（1） 
    {
        // 重写战吼效果方法，这是火焰驱逐者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 随机获得1-4点攻击力
            int attackGain = rand.Next(1, 5); // Next(1, 5) 会生成 1, 2, 3, 或 4

            // 给火焰驱逐者增加攻击力
            p.minionGetBuffed(m, attackGain, 0);

            // 如果是己方随从，增加过载值（影响下一回合可用法力值）
            if (m.own)
            {
                p.ueberladung++; // 增加过载计数器，下一回合会失去1点可用法力值
            }
        }
    }
}