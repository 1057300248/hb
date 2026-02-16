using System.Collections.Generic;
using System;
using Logger = Triton.Common.LogUtilities.Logger;
using log4net;
using System.Linq;

namespace HREngine.Bots
{
    public partial class Behavior丨狂野丨快攻暗牧 : Behavior
    {
        private int bonus_enemy = 4;
        private int bonus_mine = 4;

        public override string BehaviorName() { return "丨狂野丨快攻暗牧"; }
        PenalityManager penman = PenalityManager.Instance;



        //改于2025.4.19 
        //修复几个下牌顺序和打法伏笔

        // 存储海盗卡牌的集合
        HashSet<CardDB.Card> pirateCards = new HashSet<CardDB.Card>()
        {
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TOY_518),
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.VAC_512),
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CFM_637),
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CORE_WON_065),
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.WON_065),
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.DRG_056),
            CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.DED_513),
        };

        //文本输出
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();
        private static readonly ILog ilog_0 = Logger.GetLoggerInstanceForType();
        /// <summary>
        /// 快攻暗牧的留牌策略
        /// </summary>
        /// <param name="cards">起手卡牌列表</param>
        public override void specialMulligan(List<Mulligan.CardIDEntity> cards, HeroEnum enemyHeroClass)
        {
            int flag1 = 0;//宝藏经销商
            int flag2 = 0;//心灵按摩师
            int flag3 = 0;//虚触侍从
            int flag4 = 0;//随船外科医师
            int flag5 = 0;//空降歹徒
            int flag6 = 0;//纸艺天使
            int flag7 = 0;//狂暴邪翼蝠
            int flag8 = 0;//针灸
            foreach (Mulligan.CardIDEntity card in cards)
            {
                CardDB.Card cardCN = CardDB.Instance.getCardDataFromID(card.id);
                if (cardCN.nameCN == CardDB.cardNameCN.宝藏经销商)
                {
                    flag1 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.心灵按摩师)
                {
                    flag2 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.虚触侍从)
                {
                    flag3 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.随船外科医师)
                {
                    flag4 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.空降歹徒)
                {
                    flag5 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.纸艺天使)
                {
                    flag6 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.狂暴邪翼蝠)
                {
                    flag7 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.针灸)
                {
                    flag8 += 1;
                }
            }

            foreach (Mulligan.CardIDEntity card in cards)
            {
                CardDB.Card cardCN = CardDB.Instance.getCardDataFromID(card.id);

                if (cardCN.nameCN == CardDB.cardNameCN.宝藏经销商)
                {
                    if (cards.Count == 3 && flag4 == 0)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先手一个没有随船外科医师留一张宝藏经销商";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡宝藏经销商";
                            }
                        }
                    }
                    else if (cards.Count > 3 && flag2 + flag3 + flag4 == 1)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "后手除了宝藏经销商只有1张能用下的1费，留一张宝藏经销商";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡宝藏经销商";
                            }
                        }
                    }
                    else if (cards.Count > 3 && flag4 >= 1 && flag2 + flag3 >= 1)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "后手有随船外科医师和1张能用的1费，不留宝藏经销商";
                    }
                    else if (cards.Count > 3 && flag4 == 0 && flag2 == 0)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "后手没随船外科医师和心灵按摩师，宝藏经销商全留";
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留";
                    }

                }

                if (cardCN.nameCN == CardDB.cardNameCN.心灵按摩师)
                {
                    if (cards.Count == 3 && flag1 + flag4 == 0)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先手没宝藏经销商和随船外科医师留1张心灵按摩师";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡心灵按摩师";
                            }
                        }
                    }
                    else if (cards.Count > 3 && flag1 == 0 && flag4 == 0)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "后手没随船外科医师和宝藏经销商，心灵按摩师全留";
                    }
                    else if (cards.Count > 3 && flag1 + flag4 == 1)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "后手有随船外科医师和宝藏经销商中间的一张，心灵按摩师留一张";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡心灵按摩师";
                            }
                        }
                    }
                    else if (cards.Count > 3 && flag1 + flag4 >= 2)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "后手有随船外科医师和宝藏经销商中间的两张，心灵按摩师不留";
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.随船外科医师)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手都留两张随船外科医师";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.空降歹徒)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手留2张空降歹徒";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.虚触侍从)
                {
                    if (cards.Count >= 3
                        && (enemyHeroClass == HeroEnum.mage //法师
                       || enemyHeroClass == HeroEnum.shaman //萨满
                       || enemyHeroClass == HeroEnum.druid //德鲁伊
                       || enemyHeroClass == HeroEnum.warrior //战士
                       || enemyHeroClass == HeroEnum.deathknight//巫妖王
                       || enemyHeroClass == HeroEnum.warlock) //术士    
                       )
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手对面职业是1费竞争力不强的职业(非牧骑瞎贼)，留一张虚触侍从";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡虚触侍从";
                            }
                        }
                    }
                    else if (cards.Count == 3 && (flag1 + flag2 + flag4 >= 1)
                        && (enemyHeroClass == HeroEnum.demonhunter //恶魔猎手
                       || enemyHeroClass == HeroEnum.thief //贼
                       || enemyHeroClass == HeroEnum.priest //牧师
                       || enemyHeroClass == HeroEnum.pala) //骑士
                       )
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先手有能用的1费海盗，对面是1费竞争力强的职业(牧骑瞎贼)留一张虚触侍从";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡虚触侍从";
                            }
                        }
                    }
                    else if (cards.Count > 3 && flag4 >= 1
                        && (enemyHeroClass == HeroEnum.demonhunter //恶魔猎手
                       || enemyHeroClass == HeroEnum.thief //贼
                       || enemyHeroClass == HeroEnum.priest //牧师
                       || enemyHeroClass == HeroEnum.pala) //骑士
                       )            
                    {
                        card.holdByRule = 2;
                        card.holdReason = "后手有随船外科医师，对面是1费竞争力强的职业(牧骑瞎贼)留一张虚触侍从";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡虚触侍从";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.纸艺天使)
                {
                    if (cards.Count == 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "先手不留纸艺天使";
                    }
                    else if (cards.Count > 3 && flag1 + flag2 + flag4 == 0 && flag5 < 2)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "后手没1张能用的1费海盗，空降歹徒低于两张，留1张纸艺天使保底";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡纸艺天使";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.针灸)
                {
                    if (cards.Count >= 3 && flag7 == 2)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手有2张狂暴邪翼蝠留1张针灸";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡针灸";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.狂暴邪翼蝠)
                {
                    if (cards.Count >= 3 && flag8 >= 1 && flag7 == 2)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手有2张狂暴邪翼蝠和1张针灸留2张狂暴邪翼蝠";
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.亡者复生)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留亡者复生";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.暗影投弹手)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留暗影投弹手";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.海盗帕奇斯)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留海盗帕奇斯";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.精神灼烧)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留精神灼烧";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.心灵震爆)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留心灵震爆";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.暮光欺诈者)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留暮光欺诈者";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.迪菲亚麻风侏儒)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留迪菲亚麻风侏儒";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.黑暗主教本尼迪塔斯)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留黑暗主教本尼迪塔斯";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.赎罪教堂)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留赎罪教堂";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.异教低阶牧师)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留异教低阶牧师";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.灰烬元素)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留灰烬元素";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.雷纳索尔王子)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留雷纳索尔王子";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.皮普强力水霸)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留皮普强力水霸";
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.亮石旋岩虫)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留亮石旋岩虫";
                    }
                }
            }
        }



        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            // 无法选中(值越大越不会打出)
            if (target != null && target.untouchable)
            {
                return 100000;
            }
            // 初始惩罚值（负为优先打出该牌，正为低优先打出该牌）
            int pen = 0;
            //一费检查手牌有没有船载火炮、幸运币、海盗，此处为没有海盗，返回值1000不打出此combo。
            if (Hrtprozis.Instance.gTurn <= 2 && card.race == CardDB.Race.PIRATE && p.enemyMinions.Count == 0)
            {
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.nameCN == CardDB.cardNameCN.船载火炮)
                    {
                        foreach (Handmanager.Handcard hhc in p.owncards)
                        {
                            if (hhc.card.nameCN == CardDB.cardNameCN.幸运币 || hhc.getManaCost(p) == 1 && hhc.card.race != CardDB.Race.PIRATE && hhc.card.type == CardDB.cardtype.MOB)
                            {
                                return 1000;
                            }
                        }
                    }
                }
            }
            //如果是海盗并且随从有船载火炮 增加基础推荐出牌。
            if (card.race == CardDB.Race.PIRATE)
            {
                foreach (Minion m in p.ownMinions)
                {
                    if (m.handcard.card.nameCN == CardDB.cardNameCN.船载火炮)
                    {
                        pen -= 100;
                    }
                }
            }

            int 一费有用随从 = 0;
            int 一费海盗 = 0;
            int 暗影法术牌 = 0;
            int 空降歹徒数量 = 0;
            int 总手牌数 = p.owncards.Count;

        //    Log.ErrorFormat("=== 开始统计手牌 ===");
        //    Log.ErrorFormat("总手牌数: " + 总手牌数);

            if (总手牌数 > 0)
            {
                // 遍历手牌
                for (int i = 0; i < 总手牌数; i++)
                {
                    Handmanager.Handcard hc = p.owncards[i];
                    string 卡牌名 = hc.card.nameCN.ToString();
                    int 费用 = hc.manacost;

                    // 统计一费有用随从
                    bool 是一费有用随从 = false;
                    if (hc.card.nameCN == CardDB.cardNameCN.宝藏经销商
                        || hc.card.nameCN == CardDB.cardNameCN.心灵按摩师
                        || hc.card.nameCN == CardDB.cardNameCN.暗影投弹手
                        || hc.card.nameCN == CardDB.cardNameCN.随船外科医师)
                    {
                        一费有用随从++;
                        是一费有用随从 = true;
                    }

                    // 统计一费海盗（从一费有用随从中筛选）
                    if (hc.card.nameCN == CardDB.cardNameCN.宝藏经销商
                        || hc.card.nameCN == CardDB.cardNameCN.心灵按摩师
                        || hc.card.nameCN == CardDB.cardNameCN.随船外科医师)
                    {
                        一费海盗++;
                    }

                    // 统计暗影法术牌
                    if (hc.card.nameCN == CardDB.cardNameCN.亡者复生
                        || hc.card.nameCN == CardDB.cardNameCN.精神灼烧
                        || hc.card.nameCN == CardDB.cardNameCN.针灸
                        || hc.card.nameCN == CardDB.cardNameCN.心灵震爆)
                    {
                        暗影法术牌++;
                    }

                    // 统计空降歹徒
                    if (hc.card.nameCN == CardDB.cardNameCN.空降歹徒)
                    {
                        空降歹徒数量++;
                    }

                    // 输出当前手牌信息
                    string 标记 = "";
                    if (是一费有用随从) 标记 += "[1费随从]";
                    if (hc.card.nameCN == CardDB.cardNameCN.空降歹徒) 标记 += "[空降歹徒]";
                    if (hc.card.nameCN == CardDB.cardNameCN.亡者复生
                        || hc.card.nameCN == CardDB.cardNameCN.精神灼烧
                        || hc.card.nameCN == CardDB.cardNameCN.针灸
                        || hc.card.nameCN == CardDB.cardNameCN.心灵震爆)
                    {
                        标记 += "[暗影法术]";
                    }

                    if (!string.IsNullOrEmpty(标记))
                    {
                    //    Log.ErrorFormat("手牌" + (i + 1) + ": " + 卡牌名 + " (费用" + 费用 + ") " + 标记);
                    }
                }

                // 输出汇总
            //    Log.ErrorFormat("=== 汇总统计 ===");
            //    Log.ErrorFormat("1. 一费有用随从: " + 一费有用随从 + " 张");
            //    Log.ErrorFormat("2. 一费海盗: " + 一费海盗 + " 张");
            //    Log.ErrorFormat("3. 暗影法术牌: " + 暗影法术牌 + " 张");
            //    Log.ErrorFormat("4. 空降歹徒: " + 空降歹徒数量 + " 张");
            //    Log.ErrorFormat("=================");
            }
            else
            {
            //    Log.ErrorFormat("没有手牌");
            }

            bool 幸运币 = false;   // 是否有幸运币
            bool 一费的狂暴邪翼蝠 = false; // 是否有1费的狂暴邪翼蝠
            bool 随船外科医师 = false;   // 是否有随船外科医师
            bool 宝藏经销商 = false;   // 是否有宝藏经销商
            bool 心灵按摩师 = false;   // 是否有心灵按摩师
            bool 海盗帕奇斯 = false;   // 是否有海盗帕奇斯
            bool 空降歹徒 = false;   // 是否有空降歹徒
            bool 纸艺天使 = false;   // 是否有纸艺天使
            bool 狂暴邪翼蝠 = false;   // 是否有狂暴邪翼蝠
            bool 暗影投弹手 = false;   // 是否有暗影投弹手
            bool 虚触侍从 = false;   // 是否有虚触侍从
            bool 亡者复生 = false;   // 是否有亡者复生  
            bool 赎罪教堂 = false;   // 是否有赎罪教堂
            bool 针灸 = false;   // 是否有针灸
            bool 口渴的流浪者 = false;   // 是否有口渴的流浪者
            bool 大于一费的口渴的流浪者 = false;   // 是否有大于一费的口渴的流浪者
            bool 黑暗主教本尼迪塔斯 = false;   // 是否有黑暗主教本尼迪塔斯
            bool 雷纳索尔王子 = false;
            bool 灰烬元素 = false;
            bool 暮光欺诈者 = false;
            // 遍历手牌
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                // 检查是否有幸运币
                if (hc.card.cardIDenum == CardDB.cardIDEnum.GAME_005
                    || hc.card.cardIDenum == CardDB.cardIDEnum.AT_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.AV_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.AV_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.BAR_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.BAR_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.BAR_COIN3
                    || hc.card.cardIDenum == CardDB.cardIDEnum.BT_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.CFM_630
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DAL_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DFT_ALEX_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DINO_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DINO_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DMF_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DMF_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.DRG_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.EDR_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.EDR_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.ETC_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.ETC_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.FP1_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.GDB_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.GDB_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.GVG_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.LOE_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.MUDAN_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.REV_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.REV_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.RLK_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.RLK_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.SW_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.SW_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TIME_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TIME_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TIME_COIN3
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TIME_EVENT_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TLC_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TLC_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TOY_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TOY_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TOY_COIN3
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TSC_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TSC_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TTN_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.TTN_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.ULD_COIN
                    || hc.card.cardIDenum == CardDB.cardIDEnum.VAC_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.VAC_COIN2
                    || hc.card.cardIDenum == CardDB.cardIDEnum.WW_COIN1
                    || hc.card.cardIDenum == CardDB.cardIDEnum.WW_COIN2

                    )
                    {
                    幸运币 = true;
                }

                // 检查是否有1费的狂暴邪翼蝠
                if (hc.card.cardIDenum == CardDB.cardIDEnum.YOD_032 && hc.manacost == 1)
                {
                    一费的狂暴邪翼蝠 = true;
                }

                if ((hc.card.cardIDenum == CardDB.cardIDEnum.WON_065) || (hc.card.cardIDenum == CardDB.cardIDEnum.CORE_WON_065))
                {
                    随船外科医师 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.YOD_032)
                {
                    狂暴邪翼蝠 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.TOY_518)
                {
                    宝藏经销商 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.VAC_512)
                {
                    心灵按摩师 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.CFM_637)
                {
                    海盗帕奇斯 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.DRG_056)
                {
                    空降歹徒 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.TOY_381)
                {
                    纸艺天使 = true;
                }

                if ((hc.card.cardIDenum == CardDB.cardIDEnum.WON_062) || (hc.card.cardIDenum == CardDB.cardIDEnum.GVG_009))
                {
                    暗影投弹手 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.SW_446)
                {
                    虚触侍从 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.SCH_514)
                {
                    亡者复生 = true;
                }

                if ((hc.card.cardIDenum == CardDB.cardIDEnum.CORE_REV_290) || (hc.card.cardIDenum == CardDB.cardIDEnum.REV_290))
                {
                    赎罪教堂 = true;
                }
                if (hc.card.cardIDenum == CardDB.cardIDEnum.VAC_419)
                {
                    针灸 = true;
                }
                if ((hc.card.cardIDenum == CardDB.cardIDEnum.CORE_SW_448) || (hc.card.cardIDenum == CardDB.cardIDEnum.SW_448))
                {
                    黑暗主教本尼迪塔斯 = true;
                }
                if (hc.card.cardIDenum == CardDB.cardIDEnum.WW_387)
                {
                    口渴的流浪者 = true;
                }
                if (hc.card.cardIDenum == CardDB.cardIDEnum.WW_387 && hc.manacost >= 1)
                {
                    大于一费的口渴的流浪者 = true;
                }

                if ((hc.card.cardIDenum == CardDB.cardIDEnum.CORE_REV_018) || (hc.card.cardIDenum == CardDB.cardIDEnum.REV_018))
                {
                    雷纳索尔王子 = true;
                }

                if ((hc.card.cardIDenum == CardDB.cardIDEnum.CORE_REV_960) || (hc.card.cardIDenum == CardDB.cardIDEnum.REV_960))
                {
                    灰烬元素 = true;
                }

                if (hc.card.cardIDenum == CardDB.cardIDEnum.SW_444)
                {
                    暮光欺诈者 = true;
                }
                

            }



            // 判断是否为英雄技能
            if (card.type == CardDB.cardtype.HEROPWR)
            {
                // 判断目标是否为海盗帕奇斯
                if (target != null && target.handcard.card.nameCN == CardDB.cardNameCN.海盗帕奇斯)
                {
                    // 判断玩家是否试图使用自己的英雄技能对海盗帕奇斯造成伤害
                    if (target.own) // target.own 判断目标是否是玩家自己控制的随从
                    {
                        // 如果手牌有亡者复生且坟墓里没有随从
                        if (p.getCorpseCount() == 0 && 亡者复生)
                        {
                            pen = 500; // 设置一个较大的惩罚值，禁止使用  修复墓地没怪发癫杀自己怪用亡者复生的伏笔
                        }
                    }
                }
            }



            //此处为单卡描述
            switch (card.cardIDenum)
            {
                case CardDB.cardIDEnum.GAME_005://幸运币
                 //   if (p.ownMaxMana == 1
                 //      && 一费有用随从 >= 2) 
                 //   {
                 //       pen -= 50;
                //    }
                //    if (p.ownMaxMana >= 2)
                //    {
                //        pen -= 30;
                //    }
                    break;
                case CardDB.cardIDEnum.SCH_514://亡者复生
                    if (p.getCorpseCount() < 2) // 如果墓地里的尸体小于2 建议不出牌。 
                    {
                        pen += 500;
                    }
                    if (p.owncards.Count <= 3 && p.getCorpseCount() >= 1)   // 手牌数量太少了可以推荐出牌
                    {
                        pen -= 10;
                    }
                    break;
                case CardDB.cardIDEnum.GVG_009://暗影投弹手
                    break;
                case CardDB.cardIDEnum.WON_062://暗影投弹手
                    break;
                case CardDB.cardIDEnum.TOY_518://宝藏经销商
                    pen -= 5;
                    break;
                case CardDB.cardIDEnum.TOY_381://纸艺天使
                    pen -= 10;
                    break;
                case CardDB.cardIDEnum.VAC_512://心灵按摩师
                    break;
                case CardDB.cardIDEnum.CFM_637://海盗帕奇斯
                    pen += 1;
                    break;
                case CardDB.cardIDEnum.NX2_019://精神灼烧
                    if (target != null && target.Hp <= 2 || !target.own)       //对方随从生命值小于 2
                    {
                        pen -= 20;
                    }
                    break;
                case CardDB.cardIDEnum.WON_065://随船外科医师
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.race == CardDB.Race.PIRATE || hc.card.nameCN == CardDB.cardNameCN.错误产物 || hc.card.nameCN == CardDB.cardNameCN.口渴的流浪者 || hc.card.nameCN == CardDB.cardNameCN.虚触侍从)
                        {

                            pen -= 35;

                        }
                    }
                    pen -= 3;
                    break;
                case CardDB.cardIDEnum.CORE_WON_065://随船外科医师
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.race == CardDB.Race.PIRATE || hc.card.nameCN == CardDB.cardNameCN.错误产物 || hc.card.nameCN == CardDB.cardNameCN.口渴的流浪者 || hc.card.nameCN == CardDB.cardNameCN.虚触侍从)
                        {

                            pen -= 35;

                        }
                    }
                    pen -= 3;
                    break;
                case CardDB.cardIDEnum.TOY_528://伴唱机
                    if (p.mana < 4)
                    {
                        pen += 10;
                    }
                    if (p.anzOwnAuchenaiSoulpriest > 0 && p.mana > 4)
                    {
                        pen -= 20;
                    }
                    break;
                case CardDB.cardIDEnum.TOY_330://奇利亚斯豪华版3000型
                    if (p.ownMinions.Count >= 3) pen -= 30;
                    if (nowHandcard.getManaCost(p) <= 0) pen -= 60;
                    if (nowHandcard.getManaCost(p) <= 1) pen -= 40;
                    if (nowHandcard.getManaCost(p) <= 3) pen -= 10;
                    if (nowHandcard.getManaCost(p) > 3) pen += 10;
                    break;
                case CardDB.cardIDEnum.YOD_032://狂暴邪翼蝠
                    if (nowHandcard.getManaCost(p) <= 0) pen -= 10;
                    if (nowHandcard.getManaCost(p) >= 2) pen += 10;
                    break;
                case CardDB.cardIDEnum.WW_387://口渴的流浪者
                    if (nowHandcard.getManaCost(p) <= 0) pen -= 60;
                    if (nowHandcard.getManaCost(p) <= 1) pen -= 40;
                    if (nowHandcard.getManaCost(p) <= 3) pen -= 10;
                    if (nowHandcard.getManaCost(p) > 3) pen += 10;
                    break;
                case CardDB.cardIDEnum.SW_448://黑暗主教本尼迪塔斯
                case CardDB.cardIDEnum.CORE_SW_448://黑暗主教本尼迪塔斯
                case CardDB.cardIDEnum.EX1_625t://心灵尖刺
                    if (target != null)
                    {
                        if (target.own)
                        {
                            return 1000; // 规定不以己方为目标
                        }
                        else
                        {
                            pen -= 3;
                        }
                    }
                    break;
                case CardDB.cardIDEnum.BOM_05_Xyrella_006p2://心灵尖刺
                    if (target != null)
                    {
                        if (target.own)
                        {
                            return 1000; // 规定不以己方为目标
                        }
                        else
                        {
                            pen -= 3;
                        }
                    }
                    break;
                case CardDB.cardIDEnum.VAN_DS1_233://心灵震爆
                    if (p.enemyHero.immune) return 1000;    //对面免疫时不打。
                                                            //对面使用脱罪力证不打。(不成功)
                                                            //如果对手没有嘲讽随从，然后计算你的总攻击力加上你可以对敌方英雄造成的伤害，看是否足够来击败对手的英雄。
                    if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
                    {
                        return -20;
                    }
                    if (p.owncards.FindAll(x => x.card.nameCN == CardDB.cardNameCN.心灵震爆).Count >= 3 && p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
                    {
                        return -20;
                    }
                    if (p.ownWeapon.Durability == 0) //首先检查己方武器的耐久度是否为0
                    {
                        if (p.enemySecretCount == 0) //进一步检查对手是否没有奥秘
                            foreach (Handmanager.Handcard hc in p.owncards)
                            {
                                if (hc.card.nameCN == CardDB.cardNameCN.暮光欺诈者) return 0;
                            }
                        pen += 50;
                        if (p.ownAbilityReady) return 200;//少生孩子多射箭
                        // 手里有别牌就藏着
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.getManaCost(p) <= nowHandcard.getManaCost(p) && hc.card.nameCN != CardDB.cardNameCN.心灵震爆) return 200;
                        }
                    }
                    else
                        pen += 10;
                    break;
                case CardDB.cardIDEnum.DS1_233://心灵震爆
                    if (p.enemyHero.immune) return 1000;    //对面免疫时不打。
                                                            //对面使用脱罪力证不打。(不成功)
                                                            //如果对手没有嘲讽随从，然后计算你的总攻击力加上你可以对敌方英雄造成的伤害，看是否足够来击败对手的英雄。
                    if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
                    {
                        return -20;
                    }
                    if ((p.owncards.FindAll(x => x.card.cardIDenum == CardDB.cardIDEnum.DS1_233).Count >= 3 || p.owncards.FindAll(x => x.card.cardIDenum == CardDB.cardIDEnum.VAN_DS1_233).Count >= 3) && p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
                    {
                        return -20;
                    }
                    if (p.ownWeapon.Durability == 0) //首先检查己方武器的耐久度是否为0
                    {
                        if (p.enemySecretCount == 0) //进一步检查对手是否没有奥秘
                            foreach (Handmanager.Handcard hc in p.owncards)
                            {
                                if (hc.card.cardIDenum == CardDB.cardIDEnum.SW_444) return 0;
                            }
                        pen += 50;
                        if (p.ownAbilityReady) return 200;//少生孩子多射箭
                        // 手里有别牌就藏着
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.getManaCost(p) <= nowHandcard.getManaCost(p) && hc.card.nameCN != CardDB.cardNameCN.心灵震爆) return 200;
                        }
                    }
                    else
                        pen += 10;
                    break;
                case CardDB.cardIDEnum.DED_513://迪菲亚麻风侏儒
                    if (暗影法术牌 <= 0) pen += 20;
                    else pen -= 5;
                    break;
                case CardDB.cardIDEnum.SW_446://虚触侍从
                    int ownAtk = 0;
                    int enemyAtk = 0;
                    foreach (var item in p.ownMinions)
                    {
                        ownAtk += item.Angr;
                    }
                    foreach (var item in p.enemyMinions)
                    {
                        enemyAtk += item.Angr;
                    }

                    if (ownAtk >= enemyAtk)
                    {
                        pen -= 5;
                    }
                    else
                    {
                        pen += 3;
                    }

                    // 如果手牌有纸艺天使，费用小于等于2，场上友方随从是3个以下，以打满费用为主 TOY_381=纸艺天使
                    if (p.owncards.Any(hand => hand.card.cardIDenum == CardDB.cardIDEnum.TOY_381) &&
                        p.mana == 2 && p.ownMinions.Count < 3)
                    {
                        pen += 10;
                    }
                    break;
                case CardDB.cardIDEnum.DRG_056://空降歹徒
                   // foreach (Handmanager.Handcard hc in p.owncards)
                  //  {
                   //     if ((hc.card.race == CardDB.Race.PIRATE || pirateCards.Contains(card)) && hc.card.nameCN != CardDB.cardNameCN.空降歹徒)
                   //     {
                   //         return 1000; // 如果手牌中有其他海盗，禁止使用空降歹徒
                   //     }
                  //  }  这段有bug 无论什么情况都不会用空降歹徒
                    pen += 5;
                    break;
                case CardDB.cardIDEnum.REV_290://赎罪教堂
                    if (p.ownMinions.Select(temp => temp.handcard.card.type != CardDB.cardtype.LOCATION).ToList().Count > 0 &&
                        p.ownMaxMana >= 3 && p.owncards.Count <= 3)
                    {
                        pen -= 100;
                    }
                    pen -= 30;
                    break;
                case CardDB.cardIDEnum.CORE_REV_290://赎罪教堂
                    if (p.ownMinions.Select(temp => temp.handcard.card.type != CardDB.cardtype.LOCATION).ToList().Count > 0 &&
                        p.ownMaxMana >= 3 && p.owncards.Count <= 3)
                    {
                        pen -= 100;
                    }
                    pen -= 30;
                    break;
            }
            if (雷纳索尔王子
            && 灰烬元素)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.雷纳索尔王子:
                        pen += 20;
                        break;
                    case CardDB.cardNameCN.灰烬元素:
                        pen -= 5;
                        break;
                }
              //  Log.ErrorFormat("条件：手牌有雷纳索尔王子＋灰烬元素");
              //  Log.ErrorFormat("操作：使用灰烬元素");
            }

            if (p.ownMinions.Count >= 1
            && 赎罪教堂
            && (灰烬元素 || 雷纳索尔王子)
            )
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.赎罪教堂:
                        pen -= 20;
                        break;
                }
              //  Log.ErrorFormat("条件：场上随从大于等于1，手牌有赎罪教堂＋雷纳索尔王子和灰烬元素中的任意一个");
              //  Log.ErrorFormat("操作：使用硬币＋赎罪教堂");
            }


            if (p.ownMaxMana == 1
                 && 幸运币
                 && 宝藏经销商
                 && 心灵按摩师
                 && !随船外科医师)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.宝藏经销商:
                        pen -= 30;
                        break;
                    case CardDB.cardNameCN.幸运币:
                        pen -= 5;
                        break;
                }
              //  Log.ErrorFormat("条件：后手1费，手牌有幸运币＋宝藏经销商＋心灵按摩师，没有随船外科医师");
              //  Log.ErrorFormat("操作：使用硬币＋宝藏经销商＋心灵按摩师");
            }

            if (p.ownMaxMana == 1
                && 幸运币
                && 暗影投弹手
                && 狂暴邪翼蝠
                && 心灵按摩师
                && 随船外科医师)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.随船外科医师:
                        pen -= 200;
                        break;
                    case CardDB.cardNameCN.心灵按摩师:
                        pen -= 200;
                        break;
                    case CardDB.cardNameCN.幸运币:
                        pen -= 40;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有暗影投弹手＋狂暴邪翼蝠＋心灵按摩师＋随船外科医师＋幸运币");
              //  Log.ErrorFormat("操作：使用硬币＋随船外科医师＋心灵按摩师");
            }


            if (p.ownMaxMana == 1
                && 幸运币
                && 宝藏经销商
                && 暗影投弹手
                && !狂暴邪翼蝠
                && !心灵按摩师
                && !随船外科医师)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.宝藏经销商:
                        pen -= 25;
                        break;
                    case CardDB.cardNameCN.幸运币:
                        pen -= 40;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有幸运币＋宝藏经销商＋暗影投弹手，没有狂暴邪翼蝠＋心灵按摩师＋随船外科医师");
              //  Log.ErrorFormat("操作：使用宝藏经销商＋暗影投弹手");
            }

            if (p.ownMaxMana == 1
                 && 宝藏经销商
                 && 暗影投弹手
                 && !心灵按摩师
                 && !海盗帕奇斯
                 && !随船外科医师)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.宝藏经销商:
                        pen -= 25;
                        break;
                }
               // Log.ErrorFormat("条件：先手1费，手牌有宝藏经销商＋暗影投弹手，没有心灵按摩师＋海盗帕奇斯＋随船外科医师");
               // Log.ErrorFormat("操作：使用宝藏经销商");
            }

            if (p.ownMaxMana == 1
                && 幸运币
                && 一费有用随从 == 0
                && 海盗帕奇斯
                && 纸艺天使
                && !空降歹徒)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.纸艺天使:
                        pen -= 10;
                        break;
                    case CardDB.cardNameCN.幸运币:
                        pen -= 5;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有海盗帕奇斯＋纸艺天使，一费有用随从=0加没有空降歹徒");
               // Log.ErrorFormat("操作：使用硬币＋纸艺天使");
            }

            if (p.ownMaxMana == 1
                 && 幸运币
                 && 一费有用随从 >= 2)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.幸运币:
                        pen -= 50;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有幸运币＋一费有用随从大于等于2");
               // Log.ErrorFormat("操作：使用硬币＋一费随从X2");
            }

            if (p.ownMaxMana == 1
                && 幸运币
                && 一费有用随从 == 1
                && 一费的狂暴邪翼蝠
                 )
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.幸运币:
                        pen -= 5;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有幸运币＋暗影投弹手＋狂暴邪翼蝠，没有其他的一费有用随从");
               // Log.ErrorFormat("操作：使用硬币＋暗影投弹手＋狂暴邪翼蝠");
            }

            if (p.ownMaxMana == 1
                && 幸运币
                && 一费有用随从 == 0
                && p.enemyHero.Hp <= (p.enemyHero.maxHp - 3)
                && 一费的狂暴邪翼蝠)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.幸运币:
                        pen -= 5;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有幸运币＋对面场上有可以让精神灼伤解掉的2血以下随从＋狂暴邪翼蝠，一费有用随从=0");
               // Log.ErrorFormat("操作：使用硬币＋精神灼伤＋一费的狂暴邪翼蝠");
            }

            if (p.ownMaxMana == 1
                && 幸运币
                && 一费有用随从 == 1
                && !纸艺天使
                && 暮光欺诈者)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.心灵按摩师:
                        pen -= 20;
                        break;
                    case CardDB.cardNameCN.宝藏经销商:
                        pen -= 20;
                        break;
                    case CardDB.cardNameCN.暗影投弹手:
                        pen -= 20;
                        break;
                    case CardDB.cardNameCN.随船外科医师:
                        pen -= 20;
                        break;
                }
               // Log.ErrorFormat("条件：后手1费，手牌有幸运币＋暮光欺诈者＋一费有用随从=1，没有纸艺天使");
               // Log.ErrorFormat("操作：一费有用随从，不使用硬币");
            }

            if (p.ownMaxMana == 2
                && 幸运币
                && 一费有用随从 <= 1
                && 赎罪教堂
                && p.ownMinions.Count >= 1)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.赎罪教堂:
                        pen -= 50;
                        break;
                }
               // Log.ErrorFormat("条件：2费，手牌有幸运币＋赎罪教堂＋一费有用随从小于等于1");
               // Log.ErrorFormat("操作：使用硬币＋赎罪教堂");
            }

            if (p.ownMaxMana == 2
                && !幸运币
                && !口渴的流浪者
                && !狂暴邪翼蝠
                && 一费有用随从 <= 1
                && 宝藏经销商
                && 针灸
                && 纸艺天使)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.纸艺天使:
                        pen -= 10;
                        break;
                }
               // Log.ErrorFormat("条件：2费，手牌一费有用随从小于等于1＋宝藏经销商＋针灸＋纸艺天使，没有幸运币＋口渴的流浪者＋狂暴邪翼蝠");
               // Log.ErrorFormat("操作：使用纸艺天使");
            }

            if (p.ownMaxMana == 5 //最大水晶==5
                && p.owncards.Count == 2 //手牌数量
                && p.enemyMinions.Count == 0//敌方随从数量
                && !幸运币
                && 大于一费的口渴的流浪者
                && 黑暗主教本尼迪塔斯)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.黑暗主教本尼迪塔斯:
                        pen -= 150;
                        break;
                }
               // Log.ErrorFormat("条件：5费，手牌数量等于2＋敌方随从数量等于0,没有幸运币＋大于一费的口渴的流浪者＋黑暗主教本尼迪塔斯");
               // Log.ErrorFormat("操作：使用黑暗主教本尼迪塔斯");
            }

            if (p.ownMaxMana == 1
                //&& 幸运币
                && 一费有用随从 == 0
                && 纸艺天使
                && 暮光欺诈者)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.纸艺天使:
                        pen -= 200;
                        break;
                }
               // Log.ErrorFormat("条件：1费，手牌一费有用随从等于0＋暮光欺诈者＋纸艺天使");
               // Log.ErrorFormat("操作：使用硬币＋纸艺天使");
            }
            //Hrtprozis.Instance.gTurn == 2



            return pen;
        }

        /// <summary>
        /// 核心，场面值
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override float getPlayfieldValue(Playfield p)
        {
            // 如果场上的评分值大于-200000，则返回该值
            if (p.value > -200000) return p.value;

            float retval = 0; // 初始化返回值

            // 加上一般的场面价值
            retval += getGeneralVal(p);

            // 自己的抽牌数量，每张牌价值5分
            retval += p.owncarddraw * 5;

            // 危险血量线
            int hpboarder = 3;

            // 不考虑法术伤害加成
            if (p.enemyHeroName == HeroEnum.mage) retval += 2 * p.enemyspellpower;

            // 攻击血量线
            int aggroboarder = 20;

            // 加上血量值
            retval += getHpValue(p, hpboarder, aggroboarder);

            // 出牌的动作数量
            int count = p.playactions.Count;
            int ownActCount = 0; // 自己的动作计数
            bool useAb = false; // 是否使用了英雄技能
            bool attacted = false; // 是否已进行攻击
                                   // 遍历所有的动作

            for (int i = 0; i < count; i++)
            {
                Action a = p.playactions[i]; // 当前动作
                ownActCount++; // 计数自己的动作数量

                // 根据不同动作类型调整评分
                switch (a.actionType)
                {
                    case actionEnum.useLocation://地标
                        retval -= i * 10;
                        continue;
                    case actionEnum.useTitanAbility://泰坦
                        retval += 20;
                        continue;
     /*case actionEnum.forge://锻造  问题有点多  先废弃
    // 空值检查修正（移除空传播运算符）
    if (a.card == null || a.card.card == null) 
    {
        retval += 10;
        continue;
    }

    // 字段访问修正（使用小写字段名）
    bool isHighCostQuickdraw = a.card.getManaCost(p) > 4 
                            && a.card.isQuickdrawActive // 注意字段访问顺序
                            && a.card.drawnTurn == p.prozis.gTurn;

    // 卡牌ID检测修正（直接访问card属性）
    if (isHighCostQuickdraw && a.card.card.cardIDenum == CardDB.cardIDEnum.DEEP_024)
    {
        retval += 10;
        Log.ErrorFormat("[惩罚]");
    }
    else
    {
        retval += 20;
    }*/
                    case actionEnum.forge://锻造
                         retval += 25;
                    continue;

                    // 英雄或随从攻击
                    case actionEnum.attackWithMinion:
                        retval -= 10;
                        continue;
                    case actionEnum.attackWithHero:
                        if (a.target != null && a.target.isHero)
                        {
                            attacted = true; // 如果攻击了英雄，标记为已攻击
                        }
                        if (a.actionType == actionEnum.attackWithMinion)
                        {
                            int atk = a.own.Angr > 0 ? a.own.Angr + p.anzOldWoman : a.own.Angr;
                            retval += atk * 10;
                        }
                        continue;

                    // 使用英雄技能
                    case actionEnum.useHeroPower:
                        useAb = true;
                        if (p.ownHeroName == HeroEnum.deathknight && p.ownMinions.Count == 7) //DK
                        {
                            retval -= 10000;
                        }
                        if (p.ownHeroName == HeroEnum.shaman && p.ownMinions.Count == 7) //萨满
                        {
                            retval -= 10000;
                        }
                        //if (p.ownHeroName == HeroEnum.priest) 
                       //{
                       //     retval -= 20;
                       // }
                        continue;

                    //在这里加出牌顺序
                    case actionEnum.playcard:

                        // 判断具体的卡牌，并根据出牌顺序调整评分  减分早下  加分晚下 分数别太极端 会出毛病
                        switch (a.card.card.nameCN)
                        {
                            case CardDB.cardNameCN.幸运币:
                                retval -= i * 20;
                                break;
                            case CardDB.cardNameCN.随船外科医师:
                                retval -= i * 9;
                                break;
                            case CardDB.cardNameCN.宝藏经销商:
                                retval -= i * 8;
                                break;
                            case CardDB.cardNameCN.心灵按摩师:
                                retval += i * 20;
                                break;
                            case CardDB.cardNameCN.暮光欺诈者:
                                retval += i * 30;
                                break;
                            case CardDB.cardNameCN.空降歹徒:
                                retval += i * 15;
                                break;
                            case CardDB.cardNameCN.赎罪教堂:
                                retval -= (i * 11 + 5);
                                break;
                            case CardDB.cardNameCN.精神灼烧:
                                retval += i * 40;
                                break;                    
                        }
                        break;

                    default:
                        continue;
                }

                // 如果出牌是海盗或“虚触侍从”
                if (a.card.card.race == CardDB.Race.PIRATE || a.card.card.nameCN == CardDB.cardNameCN.虚触侍从)
                {
                    // 检查己方随从是否有“船载火炮”
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.handcard.card.nameCN == CardDB.cardNameCN.船载火炮)
                        {
                            retval += 10 - i * 3; // 根据出牌顺序加分
                            break;
                        }
                    }
                }
            }

            // 对手基本随从交换模拟
            retval -= p.lostDamage;
            retval += getSecretPenality(p); // 奥秘的影响
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3; // 对方武器影响

            // 留着技能下回合使用的情况
            if (p.ownMaxMana < 2 && p.ownHeroPowerCostLessOnce <= -99)
            {
                if (!useAb && p.enemyMinions.Count == 0)
                {
                    retval += 20;
                }
            }

            // 针对术士职业的特殊防“亵渎”
            if (retval > 50 && p.enemyHeroStartClass == TAG_CLASS.WARLOCK && p.enemyMinions.Count == 0 && p.ownMinions.Count > 2)
            {
                bool found = false;

                // 防止“亵渎”，从2血开始计算
                for (int i = 1; i <= 10; i++)
                {
                    found = false;
                    foreach (Minion m in p.ownMinions)
                    {
                        if (m.Hp == i)
                        {
                            retval -= 2 * (i - 1);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        if (i == 1) retval += 10;
                        if (i == 2) retval += 10;
                        break;
                    }
                }
            }

            // “心灵震爆”闲置时优先出
            if (p.owncards.Count <= 4)
            {
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.nameCN == CardDB.cardNameCN.心灵震爆 && hc.getManaCost(p) <= p.mana)
                    {
                        retval -= 50;
                    }
                }
            }

            // 如果不攻击就能击杀敌方英雄，额外加分
            if (!attacted && p.enemyHero.Hp <= 0) retval += 10000;

            // 返回计算后的场面价值
            return retval;
        }

        /// <summary>
        /// 发现卡的价值
        /// </summary>
        /// <param name="card"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
            switch (card.nameCN)
            {
                //法术（if条件成功了！）
                case CardDB.cardNameCN.心灵震爆:
                    if (p.enemyHero.Hp <= 5 + p.calTotalAngr()) return 100;
                    return 15;
                case CardDB.cardNameCN.精神灼烧:
                    if (p.enemyHero.Hp >= 3 + p.calTotalAngr() && p.ownMinions.Count <= p.enemyMinions.Count) return 20;
                    return 10;
                case CardDB.cardNameCN.亡者复生:
                    return 5;

                //随从（龙）
                case CardDB.cardNameCN.礼盒雏龙:
                    return 30;
                case CardDB.cardNameCN.暮光雏龙:
                case CardDB.cardNameCN.随船外科医师:
                case CardDB.cardNameCN.错误产物:
                case CardDB.cardNameCN.精灵龙:
                    return 20;
                case CardDB.cardNameCN.光明之翼:
                case CardDB.cardNameCN.星光雏龙:
                case CardDB.cardNameCN.深蓝系咒师:
                case CardDB.cardNameCN.碧蓝幼龙:
                    return 15;
                case CardDB.cardNameCN.黏土巢母:
                case CardDB.cardNameCN.骸骨巨龙:
                    return 10;

            }
            if (card.race == CardDB.Race.DRAGON)
            {
                return 3;
            }
            return 0;

        }

        /// <summary>
        /// 敌方随从价值 主要等于（HP + Angr） * 4
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getEnemyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 0;
            if (m.Angr > 0 || m.taunt || m.handcard.card.race == CardDB.Race.TOTEM || p.enemyHeroStartClass == TAG_CLASS.PALADIN || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                retval += m.Hp * 4;
            retval += m.spellpower * 2;
            retval += m.Hp * m.Angr / 2;
            if (!m.frozen && (!m.cantAttack || m.handcard.card.nameCN == CardDB.cardNameCN.邪刃豹))
            {
                retval += m.Angr * 4;
                if (m.Angr > 5) retval += 10;
                if (m.windfury) retval += m.Angr * 2;
            }
            if (m.silenced) return retval;

            if (m.taunt) retval += 2;
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;

            // 鱼人
            if (m.handcard.card.race == CardDB.Race.MURLOC) retval += bonus_enemy * 4;

            // 剧毒价值两点属性
            if (m.poisonous)
            {
                retval += 8;
            }
            if (m.lifesteal) retval += m.Angr * bonus_enemy * 4;

            //int bonus = 4;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.肢体商贩:
                case CardDB.cardNameCN.巨型图腾埃索尔:
                case CardDB.cardNameCN.驻锚图腾:
                case CardDB.cardNameCN.刺豚拳手:
                case CardDB.cardNameCN.空中飞爪:
                case CardDB.cardNameCN.金翼巨龙:
                case CardDB.cardNameCN.安保自动机:
                case CardDB.cardNameCN.机械跃迁者:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.前沿哨所:
                case CardDB.cardNameCN.战场军官:
                case CardDB.cardNameCN.伯尔纳锤喙:
                case CardDB.cardNameCN.甜水鱼人斥候:
                case CardDB.cardNameCN.塔姆辛罗姆:
                case CardDB.cardNameCN.暗影珠宝师汉纳尔:
                case CardDB.cardNameCN.伦萨克大王:
                case CardDB.cardNameCN.布莱恩铜须:
                case CardDB.cardNameCN.观星者露娜:
                case CardDB.cardNameCN.大法师瓦格斯:
                case CardDB.cardNameCN.火妖:
                case CardDB.cardNameCN.下水道渔人:
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                case CardDB.cardNameCN.火舌图腾:
                case CardDB.cardNameCN.末日预言者:
                case CardDB.cardNameCN.莫尔杉哨所:
                case CardDB.cardNameCN.鱼人领军:
                case CardDB.cardNameCN.南海船长:
                case CardDB.cardNameCN.灭龙弩炮:
                case CardDB.cardNameCN.战马训练师:
                case CardDB.cardNameCN.加基森拍卖师:
                case CardDB.cardNameCN.健谈的调酒师:
                case CardDB.cardNameCN.豪宅管家俄里翁:
                case CardDB.cardNameCN.小鬼骑士:
                case CardDB.cardNameCN.针岩图腾:
                case CardDB.cardNameCN.伴唱机:
                case CardDB.cardNameCN.空气之怒图腾:
                case CardDB.cardNameCN.战场通灵师:
                case CardDB.cardNameCN.纸艺天使:
                case CardDB.cardNameCN.纳亚克海克森:
                case CardDB.cardNameCN.粗暴的猢狲:
                case CardDB.cardNameCN.伊谢尔风歌:
                case CardDB.cardNameCN.饱胀水蛭:                   
                    retval += 150;
                    break;

                // 不解巨大劣势
                case CardDB.cardNameCN.安娜科德拉:
                case CardDB.cardNameCN.农夫:
                case CardDB.cardNameCN.旗标骷髅:
                case CardDB.cardNameCN.尼鲁巴蛛网领主:
                case CardDB.cardNameCN.凯瑞尔罗姆:
                case CardDB.cardNameCN.暗鳞先知:
                case CardDB.cardNameCN.鲨鳍后援:
                case CardDB.cardNameCN.相位追猎者:
                case CardDB.cardNameCN.鱼人宝宝车队:
                case CardDB.cardNameCN.饥饿的秃鹫:
                case CardDB.cardNameCN.锈水海盗:
                case CardDB.cardNameCN.盛装歌手:
                case CardDB.cardNameCN.玛克扎尔的小鬼:
                case CardDB.cardNameCN.发明机器人:
                case CardDB.cardNameCN.宝藏经销商:
                case CardDB.cardNameCN.随船外科医师:
                case CardDB.cardNameCN.玩具船:
                    retval += 50;
                    break;
                // 算有点用
                case CardDB.cardNameCN.贪婪的书虫:
                case CardDB.cardNameCN.治疗图腾:
                case CardDB.cardNameCN.力量图腾:
                case CardDB.cardNameCN.神秘女猎手:
                case CardDB.cardNameCN.矮人神射手:
                case CardDB.cardNameCN.低阶侍从:
                case CardDB.cardNameCN.战斗邪犬:
                case CardDB.cardNameCN.法力浮龙:
                case CardDB.cardNameCN.飞刀杂耍者:
                    retval += 15;
                    break;
            }
            return retval;
        }

        /// <summary>
        /// 我方随从价值，大致等于主要等于 （HP + Angr） * 4 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getMyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 5;
            if (m.Hp <= 0) return 0;
            retval += m.Hp * 4;
            retval += m.Angr * 4;
            retval += m.Hp * m.Angr / 2;
            // 高攻低血是垃圾
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * 3;

            // 风怒价值
            if ((!m.playedThisTurn || m.rush == 1 || m.charge == 1) && m.windfury) retval += m.Angr;
            // 圣盾价值
            if (m.divineshild) retval += m.Angr * 3;
            // 潜行价值
            if (m.stealth) retval += m.Angr / 2 + 1;
            // 吸血
            if (m.lifesteal) retval += m.Angr / 2 + 1;
            // 圣盾嘲讽
            if (m.divineshild && m.taunt) retval += 4;

            //int bonus = 4;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.虚触侍从:
                    retval += 2 * bonus_mine;
                    break;
                case CardDB.cardNameCN.船载火炮:
                    retval += 3 * bonus_mine;
                    break;
                case CardDB.cardNameCN.随船外科医师:
                    retval += 1 * bonus_mine;
                    break;
            }
            return retval;
        }

        public override int getSirFinleyPriority(List<Handmanager.Handcard> discoverCards)
        {
            return -1; //comment out or remove this to set manual priority
        }

        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
        }

        private Dictionary<CardDB.cardNameEN, int> SirFinleyPriorityList = new Dictionary<CardDB.cardNameEN, int>
        {
            { CardDB.cardNameEN.lesserheal, 0 },
            { CardDB.cardNameEN.shapeshift, 6 },
            { CardDB.cardNameEN.fireblast, 7 },
            { CardDB.cardNameEN.totemiccall, 1 },
            { CardDB.cardNameEN.lifetap, 9 },
            { CardDB.cardNameEN.daggermastery, 5 },
            { CardDB.cardNameEN.reinforce, 4 },
            { CardDB.cardNameEN.armorup, 2 },
            { CardDB.cardNameEN.steadyshot, 8 }
        };

        /// <summary>
        /// 计算当前局面下英雄血量的价值评估
        /// </summary>
        /// <param name="p">游戏场地对象，包含双方英雄和场上信息</param>
        /// <param name="hpboarder">己方英雄的安全血量边界值</param>
        /// <param name="aggroboarder">敌方英雄的攻击边界值</param>
        /// <returns>血量价值评估结果，数值越高表示局面越有利</returns>
        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;
            int offset_mine = p.calEnemyTotalAngr() + Hrtprozis.Instance.enemyDirectDmg;
            int retval = 0;

            // 己方血量安全情况下的价值计算
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - offset_mine - hpboarder) * 3 / 2;
            }
            // 己方血量危险但未死亡的情况
            else if (p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                retval -= 4 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine);
            }
            // 己方血量已经死亡的情况
            else
            {
                retval -= 3 * (hpboarder + 1) * (hpboarder + 1) + 100;
            }

            // 当己方血量在1-5之间时的额外惩罚
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine < 6 && p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                retval -= 80 / (p.ownHero.Hp + p.ownHero.armor - offset_mine);
            }

            // 敌方血量安全情况下的价值计算
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
            {
                retval += 3 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 敌方血量危险，开始攻击脸面的情况
            else
            {
                retval += 4 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }

            // 判断是否能够完成斩杀的价值奖励
            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 2000;
            }

            // 判断下回合能否斩杀本回合打脸的价值奖励
            if (p.calDirectDmg(p.ownMaxMana + 1, false, true) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 100;
            }
            return retval;
        }

        /// <summary>
        /// 攻击触发的奥秘惩罚
        /// </summary>
        /// <param name="si"></param>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns></returns>
        public override int getSecretPen_CharIsAttacked(Playfield p, SecretItem si, Minion attacker, Minion defender)
        {
            if (attacker.isHero) return 0;
            int pen = 0;
            // 攻击的基本惩罚
            if (si.canBe_explosive && !defender.isHero)
            {
                pen -= 20;
                foreach (SecretItem sii in p.enemySecretList)
                {
                    sii.canBe_explosive = false;
                }
            }
            return pen;
        }
    }


}