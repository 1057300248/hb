using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 疯狂爆破者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力4，生命值4
    // 卡牌效果：<b>战吼：</b>造成6点伤害，随机分配到所有其他角色身上。
    class Sim_GVG_090 : SimTemplate //* 疯狂爆破者 Madder Bomber
    // <b>Battlecry:</b> Deal 6 damage randomly split between all other characters.
    // <b>战吼：</b>造成6点伤害，随机分配到所有其他角色身上。 
    {
        // 重写战吼效果方法，这是疯狂爆破者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 总共造成6点伤害
            int totalDamage = 6;

            // 创建随机数生成器
            Random rand = new Random();

            // 循环分配每次1点伤害，直到总伤害分配完毕
            for (int i = 0; i < totalDamage; i++)
            {
                // 收集所有可能的目标（敌方随从、敌方英雄、己方随从、己方英雄）
                List<Minion> targets = new List<Minion>();

                // 添加敌方随从
                targets.AddRange(p.enemyMinions);
                // 添加己方随从（排除自身）
                targets.AddRange(p.ownMinions.FindAll(m => m.entitiyID != own.entitiyID));
                // 添加敌方英雄
                targets.Add(p.enemyHero);
                // 添加己方英雄（如果存活）
                if (p.ownHero.Hp > 0) targets.Add(p.ownHero);

                // 如果有目标，则随机选择一个
                if (targets.Count > 0)
                {
                    Minion selectedTarget = targets[rand.Next(targets.Count)];
                    p.minionGetDamageOrHeal(selectedTarget, 1, true);
                }
            }
        }
    }
}