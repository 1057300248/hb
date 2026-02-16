using System;
using System.Collections.Generic;
using System.Linq;

namespace HREngine.Bots
{
    class Sim_VAC_948 : SimTemplate // Hydration Station
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion m, int choice)
        {
            // 获取当前玩家拥有的随从数量
            int currentMinionCount = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;

            // 确定可以复活的最大数量（最多3个，同时不超过场上7个的限制）
            int maxResurrect = Math.Min(3, 7 - currentMinionCount);

            if (maxResurrect <= 0)
                return; // 如果不能复活任何随从，则直接返回

            // 获取可 resurrection 的不同嘲讽随从
            var candidates = GetResurrectableTauntIDs(p).Select(id => CardDB.Instance.getCardDataFromID(id))
                .OrderByDescending(card => card.cost)
                .Take(maxResurrect) // 取最多maxResurrect个
                .ToList();

            foreach (var card in candidates)
            {
                // 将新随从放置在当前玩家随从列表的末尾，以避免覆盖现有随从
                int position = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(card, position, ownplay);
            }
        }

        private List<CardDB.cardIDEnum> GetResurrectableTauntIDs(Playfield p)
        {
            // 获取所有在墓地中的嘲讽随从
            return Probabilitymaker.Instance.ownGraveyard.Keys.Where(
                id => IsResurrectableTaunt(id, p))
                .ToList();
        }

        private bool IsResurrectableTaunt(CardDB.cardIDEnum id, Playfield p)
        {
            var card = CardDB.Instance.getCardDataFromID(id);
            // 判断是否是随从、嘲讽
            return card.type == CardDB.cardtype.MOB &&
                   card.tank;
        }
    }
}
