using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 小个子法师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值1
    // 卡牌效果：<b>潜行，法术伤害+1</b>
    class Sim_GVG_109 : SimTemplate //* 小个子法师 Mini-Mage
    {
        //<b>Stealth</b><b>Spell Damage +1</b>
        //<b>潜行，法术伤害+1</b>

        // 重写光环开始方法，当小个子法师进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 根据小个子法师的归属增加相应的法术伤害
            if (own.own)
            {
                // 增加己方法术伤害
                p.spellpower++;
            }
            else
            {
                // 增加敌方法术伤害
                p.enemyspellpower++;
            }
        }

        // 重写光环结束方法，当小个子法师离开战场时调用
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 根据小个子法师的归属减少相应的法术伤害
            if (m.own)
            {
                // 减少己方法术伤害
                p.spellpower--;
            }
            else
            {
                // 减少敌方法术伤害
                p.enemyspellpower--;
            }
        }
    }
}