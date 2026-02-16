using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 神秘挑战者（Mysterious Challenger）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_079 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (own.own)
            {
                // 获取牌库中的所有奥秘
                List<CardDB.cardIDEnum> secrets = new List<CardDB.cardIDEnum>();
                foreach (KeyValuePair<CardDB.cardIDEnum, int> cid in p.prozis.turnDeck)
                {
                    CardDB.Card c = CardDB.Instance.getCardDataFromID(cid.Key);
                    if (c.Secret && !secrets.Contains(cid.Key))
                    {
                        secrets.Add(cid.Key);
                    }
                }

                // 将每种不同的奥秘置入战场（最多5个）
                foreach (CardDB.cardIDEnum secretId in secrets)
                {
                    if (p.ownSecretsIDList.Count < 5 && !p.ownSecretsIDList.Contains(secretId))
                    {
                        p.ownSecretsIDList.Add(secretId);
                    }
                }
            }
            else
            {
                // 敌方逻辑：随机添加奥秘（最多5个）
                while (p.enemySecretCount < 5)
                {
                    p.enemySecretCount++;
                    p.enemySecretList.Add(Probabilitymaker.Instance.getNewSecretGuessedItem(p.getNextEntity(), p.enemyHeroStartClass));
                }
            }
        }
    }
}