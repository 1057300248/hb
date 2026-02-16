using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 热砂港狙击手表的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力2，生命值3
    // 卡牌效果：你的英雄技能能够以随从为目标。
    class Sim_GVG_087 : SimTemplate //* 热砂港狙击手 Steamwheedle Sniper
    // Your Hero Power can target minions.
    // 你的英雄技能能够以随从为目标。 
    {
        // 重写光环效果开始时的触发方法，当热砂港狙击手进入战场时调用
        public override void onAuraStarts(Playfield p, Minion m)
        {
            // 检查热砂港狙击手是否属于己方
            if (m.own)
            {
                // 设置己方拥有热砂港狙击手的标志
                p.我们有热砂港狙击手 = true;
            }
            else
            {
                // 设置敌方拥有热砂港狙击手的标志
                p.敌方有热砂港狙击手 = true;
            }
        }

        // 重写光环效果结束时的触发方法，当热砂港狙击手离开战场时调用
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 检查热砂港狙击手是否属于己方
            if (m.own)
            {
                // 检查是否还有其他未被沉默的热砂港狙击手
                bool hasss = false;
                foreach (Minion mnn in p.ownMinions)
                {
                    // 检查随从卡牌ID是否为热砂港狙击手且未被沉默
                    if (mnn.handcard.card.cardIDenum == CardDB.cardIDEnum.GVG_087 && !mnn.silenced)
                    {
                        hasss = true;
                    }
                }
                // 更新己方热砂港狙击手标志
                p.我们有热砂港狙击手 = hasss;
            }
            else
            {
                // 检查是否还有其他未被沉默的热砂港狙击手
                bool hasss = false;
                foreach (Minion mnn in p.enemyMinions)
                {
                    // 检查随从卡牌ID是否为热砂港狙击手且未被沉默
                    if (mnn.handcard.card.cardIDenum == CardDB.cardIDEnum.GVG_087 && !mnn.silenced)
                    {
                        hasss = true;
                    }
                }
                // 更新敌方热砂港狙击手标志
                p.敌方有热砂港狙击手 = hasss;
            }
        }
    }
}