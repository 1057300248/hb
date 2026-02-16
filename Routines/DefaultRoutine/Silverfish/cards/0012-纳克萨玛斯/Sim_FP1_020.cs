using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 复仇卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业奥秘法术卡牌，费用为1点
    // 卡牌效果：<b>奥秘：</b>当你的随从死亡时，随机使一个友方随从获得+3/+2。
    class Sim_FP1_020 : SimTemplate //* 复仇 Avenge
    // <b>Secret:</b> When one of your minions dies, give a random friendly minion +3/+2.
    // <b>奥秘：</b>当你的随从死亡时，随机使一个友方随从获得+3/+2。 
    {
        // 重写奥秘触发方法，这是复仇卡牌效果的核心实现
        public override void onSecretPlay(Playfield p, bool ownplay, int number)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 根据奥秘拥有者确定要处理的友方随从列表
            List<Minion> friendlyMinions = ownplay ? p.ownMinions : p.enemyMinions;

            // 如果有友方随从存在
            if (friendlyMinions.Count >= 1)
            {
                // 随机选择一个友方随从
                Minion target = friendlyMinions[rand.Next(friendlyMinions.Count)];

                // 给选中的随从增加+3/+2
                p.minionGetBuffed(target, 3, 2);
            }
        }
    }
}