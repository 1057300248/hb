using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 蹒跚的食尸鬼卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1，生命值3
    // 卡牌效果：<b>嘲讽，亡语：</b>对所有随从造成1点伤害。
    class Sim_FP1_024 : SimTemplate //* 蹒跚的食尸鬼 Unstable Ghoul
    // <b>Taunt</b>. <b>Deathrattle:</b> Deal 1 damage to all minions.
    // <b>嘲讽，亡语：</b>对所有随从造成1点伤害。 
    {
        // 重写亡语效果方法，这是蹒跚的食尸鬼卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 对所有随从（包括己方和敌方）造成1点伤害
            p.allMinionsGetDamage(1);
        }
    }
}