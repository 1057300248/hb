using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 复制卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业奥秘法术卡牌，费用为3点
    // 卡牌效果：<b>奥秘：</b>当一个友方随从死亡时，将两张该随从的复制置入你的手牌。
    class Sim_FP1_018 : SimTemplate //* 复制 Duplicate
    // <b>Secret:</b> When a friendly minion dies, put 2 copies of it into your hand.
    // <b>奥秘：</b>当一个友方随从死亡时，将两张该随从的复制置入你的手牌。 
    {
        // 重写奥秘触发方法，这是复制卡牌效果的核心实现
        public override void onSecretPlay(Playfield p, bool ownplay, int number)
        {
            // 检查是己方还是敌方触发奥秘
            if (ownplay)
            {
                // 为己方玩家添加两张死亡随从的复制到手牌
                // 参数说明：- p.revivingOwnMinion：死亡的己方随从卡牌ID
                // - ownplay：true表示己方
                // - true：表示这是特殊效果抽卡
                p.drawACard(p.revivingOwnMinion, ownplay, true);
                p.drawACard(p.revivingOwnMinion, ownplay, true);
            }
            else
            {
                // 为敌方玩家添加两张死亡随从的复制到手牌
                // 参数说明：- p.revivingEnemyMinion：死亡的敌方随从卡牌ID
                // - ownplay：false表示敌方
                // - true：表示这是特殊效果抽卡
                p.drawACard(p.revivingEnemyMinion, ownplay, true);
                p.drawACard(p.revivingEnemyMinion, ownplay, true);
            }
        }
    }
}