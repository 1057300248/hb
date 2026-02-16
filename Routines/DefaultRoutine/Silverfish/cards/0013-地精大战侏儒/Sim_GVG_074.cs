using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 科赞秘术师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力3，生命值5
    // 卡牌效果：战吼：随机夺取一个敌方奥秘的控制权。
    class Sim_GVG_074 : SimTemplate //科赞秘术师
    {
        // 战吼：随机夺取一个敌方奥秘的控制权。

        // 重写战吼效果方法，这是科赞秘术师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查科赞秘术师是否属于己方
            if (own.own)
            {
                // 如果敌方有奥秘，则随机夺取一个
                if (p.enemySecretList.Count >= 1)
                {
                    // 创建随机数生成器
                    Random rand = new Random();

                    // 随机选择一个敌方奥秘的索引
                    int randomIndex = rand.Next(p.enemySecretList.Count);

                    // 获取随机选中的奥秘
                    SecretItem stolenSecret = p.enemySecretList[randomIndex];

                    // 将选中的奥秘添加到己方奥秘列表（使用正确的枚举类型）
                    p.ownSecretsIDList.Add((CardDB.cardIDEnum)stolenSecret.entityId);

                    // 从敌方奥秘列表中移除该奥秘
                    p.enemySecretList.RemoveAt(randomIndex);
                }
            }
            else
            {
                // 如果是敌方科赞秘术师，则从己方夺取一个奥秘
                if (p.ownSecretsIDList.Count >= 1)
                {
                    // 创建随机数生成器
                    Random rand = new Random();

                    // 随机选择一个己方奥秘的索引
                    int randomIndex = rand.Next(p.ownSecretsIDList.Count);

                    // 获取随机选中的奥秘ID
                    CardDB.cardIDEnum stolenSecretID = p.ownSecretsIDList[randomIndex];

                    // 创建新的奥秘项
                    SecretItem s = new SecretItem();
                    s.entityId = (int)stolenSecretID;

                    // 将新奥秘添加到敌方奥秘列表
                    p.enemySecretList.Add(s);

                    // 从己方奥秘列表中移除该奥秘
                    p.ownSecretsIDList.RemoveAt(randomIndex);
                }
            }
        }
    }
}