using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 加兹瑞拉卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为7点，攻击力6，生命值9
    // 卡牌效果：每当该随从受到伤害，便使其攻击力翻倍。
    class Sim_GVG_049 : SimTemplate //* 加兹瑞拉 Gahz'rilla
    // Whenever this minion takes damage, double its Attack.
    // 每当该随从受到伤害，便使其攻击力翻倍。 
    {
        // 重写随从受到伤害时的触发方法，当随从受到伤害时调用
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查该随从是否受到了伤害（anzGotDmg记录受伤害次数）
            if (m.anzGotDmg > 0)
            {
                // 保存受伤害次数，然后重置计数器
                int tmp = m.anzGotDmg;
                m.anzGotDmg = 0;

                // 计算攻击力翻倍后的增加值
                // 例如：原攻击力6，受1次伤后变为12，增加值为6
                // 受2次伤后变为24，增加值为18
                int attackIncrease = m.Angr * (int)Math.Pow(2, tmp) - m.Angr;

                // 给加兹瑞拉增加相应的攻击力
                p.minionGetBuffed(m, attackIncrease, 0);
            }
        }
    }
}