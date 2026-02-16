using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 煤烟喷吐装置卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值3
    // 卡牌效果：<b>法术伤害+1</b>
    class Sim_GVG_123 : SimTemplate //* 煤烟喷吐装置 Soot Spewer
    // <b>Spell Damage +1</b>
    // <b>法术伤害+1</b> 
    {
        // 重写光环开始方法，当煤烟喷吐装置进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 根据煤烟喷吐装置的归属增加相应的法术伤害
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

        // 重写光环结束方法，当煤烟喷吐装置离开战场时调用
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 根据煤烟喷吐装置的归属减少相应的法术伤害
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