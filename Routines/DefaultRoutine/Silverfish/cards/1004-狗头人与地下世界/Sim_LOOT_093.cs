using System;
using System.Collections.Generic;

namespace HREngine.Bots
{
    class Sim_LOOT_093 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int recruitsNeeded = 3;
            Random rnd = new Random();

            // 仅操作己方牌库
            List<CardDB.Card> deck = p.ownDeck;

            for (int i = 0; i < recruitsNeeded; i++)
            {
                // 战场位置检查（根据实际使用方判断）
                int boardSize = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;
                if (boardSize >= 7) break;

                // 构建候选列表（cost≤2的随从）
                List<CardDB.Card> candidates = deck.FindAll(c =>
                    c.type == CardDB.cardtype.MOB &&
                    c.cost <= 2
                );

                if (candidates.Count == 0) return;

                // 随机选择
                int randomIndex = rnd.Next(0, candidates.Count);
                CardDB.Card recruited = candidates[randomIndex];

                // 召唤到正确的战场侧
                p.callKid(recruited, boardSize, ownplay, false);

                // 从己方牌库移除
                deck.Remove(recruited);
            }
        }
    }
}