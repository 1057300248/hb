using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 齿轮光锤卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业武器卡牌，费用为3点，攻击力2，耐久度3
    // 卡牌效果：<b>战吼：</b>随机使一个友方随从获得<b>圣盾</b>和<b>嘲讽</b>。
    internal class Sim_GVG_059 : SimTemplate //* 齿轮光锤 Coghammer
    // <b>Battlecry:</b> Give a random friendly minion <b>Divine Shield</b> and <b>Taunt</b>.
    // <b>战吼：</b>随机使一个友方随从获得<b>圣盾</b>和<b>嘲讽</b>。 
    {
        // 在类级别定义齿轮光锤卡牌对象，用于装备武器
        private CardDB.Card w = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_059);

        // 重写卡牌打出时的效果方法，这是齿轮光锤卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备齿轮光锤武器
            p.equipWeapon(w, ownplay);

            // 根据是否是己方打出，确定要检查的随从列表
            List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;

            // 如果没有随从，则直接返回
            if (temp.Count <= 0) return;

            // 创建随机数生成器
            Random rand = new Random();

            // 随机选择一个友方随从
            Minion randomMinion = temp[rand.Next(temp.Count)];

            // 如果选中了有效的随从，则给予圣盾和嘲讽效果
            if (randomMinion != null)
            {
                // 给选中的随从添加圣盾效果
                randomMinion.divineshild = true;

                // 如果选中的随从原本没有嘲讽，则添加嘲讽效果
                if (!randomMinion.taunt)
                {
                    // 设置嘲讽标志
                    randomMinion.taunt = true;

                    // 更新嘲讽随从计数器
                    if (randomMinion.own) p.anzOwnTaunt++;
                    else p.anzEnemyTaunt++;
                }
            }
        }
    }
}