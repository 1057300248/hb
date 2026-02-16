using System.Collections.Generic;
using System;
using System.Linq;
using log4net;
using Logger = Triton.Common.LogUtilities.Logger;

namespace HREngine.Bots
{
    public partial class Behavior丨狂野丨任务战 : Behavior
    {
        private static readonly ILog ilog_0 = Logger.GetLoggerInstanceForType();

        private int bonus_enemy = 4;
        private int bonus_mine = 4;
        // 危险血线
        private int hpboarder = 25;
        // 抢脸血线
        private int aggroboarder = 5;

        private static readonly ILog Log = Logger.GetLoggerInstanceForType();
        

        public override string BehaviorName() { return "丨狂野丨任务战"; }
        PenalityManager penman = PenalityManager.Instance;

        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            // 无法选中
            if (target != null && target.untouchable)
            {
                return 100000;
            }

            // 初始惩罚值
            int pen = 0;
            int deckCount = p.ownDeckSize; 
            
            
            switch (card.nameCN)		
            {
                case CardDB.cardNameCN.硕铠鼠:
                {
                    // 统计手牌中的嘲讽随从
                    int tauntCount = 0;
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.type == CardDB.cardtype.MOB && hc.card.tank)
                        {
                            tauntCount++;
                        }
                    }
                    
                    // 根据嘲讽随从数量调整优先级
                    if (tauntCount >= 2)
                    {
                        pen -= 70; // 有多个嘲讽随从时，提高优先级
                    }
                    else if (tauntCount == 1)
                    {
                        pen -= 5; // 只有一个嘲讽随从时，小幅提高优先级
                    }
                    else
                    {
                        pen += 10; // 没有嘲讽随从时，给予较大惩罚
                    }
                }
                break;
                case CardDB.cardNameCN.全副武装:
                {
                // 快攻战：优先出随从，除非没有其他选择
                bool hasOtherPlay = false;
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                if (hc.card.nameCN != CardDB.cardNameCN.全副武装 && hc.manacost <= p.mana)
                {
                hasOtherPlay = true;
                break;
                }
                }

                if (!hasOtherPlay)
                {
                // 没有其他牌可出，使用英雄技能
                pen -= 100;
                }
                else
                {
                // 有其他牌可出，尽量不出英雄技能
                pen += 7;
                }

               // 如果血量很低，也需要叠甲保命
               if (p.ownHero.Hp <= 10)
               {
                pen -= 50;
               }

               break;
               }
                case CardDB.cardNameCN.攀上新高:
                {
                if (p.mana < 7)
                {
               // 法力值小于7，增加优先级
                pen -= 100;  // 降低pen值，增加优先级
                }
               else
                 {
                // 法力值>=7，默认优先级20
                pen -= 20;  // 增加pen值，降低优先级
               }
    
                  break;
                }
                case CardDB.cardNameCN.海中向导芬利爵士:
                 {
                        int unplayableCards = 0;
                           bool hasWujieKongyu = false;  // 无界空宇
                         bool hasOtherKeyCard = false;  // 其他三张关键卡
    
                             foreach (Handmanager.Handcard hc in p.owncards)
                           {
                                   if (hc.manacost > p.mana) unplayableCards++;
        
                          if (hc.card.nameCN == CardDB.cardNameCN.无界空宇)
                          {
                                           hasWujieKongyu = true;
                        }
        
                                if (hc.card.nameCN == CardDB.cardNameCN.黑石摇滚 ||
                                 hc.card.nameCN == CardDB.cardNameCN.洛瑟玛塞隆 ||
                                 hc.card.nameCN == CardDB.cardNameCN.火成熔岩吞食者)
                            {
                                  hasOtherKeyCard = true;
                              }
                                                     }
    
                              int handCount = p.owncards.Count;
    
                      // 情况1：卡手严重，急需换牌
                   if (unplayableCards >= 3 && handCount <= 7)
                        {
                           pen 
                           += 50;  // 强烈想打出换牌
        
                        // 即使有关键卡，卡手时也优先换牌
                        if (hasOtherKeyCard)
                            {
                        pen += 100;  // 少量抵消，但仍想换
                        }
                       }
                       // 情况2：有无界空宇，增加打出优先级
                     else if (hasWujieKongyu && !hasOtherKeyCard)
                     {
                     // 只有无界空宇，没有其他关键卡，优先打出芬利
                        pen -= 100;  // 降低pen值，增加优先级
                    }
                        // 情况3：有其他关键卡，降低优先级
                       else if (hasOtherKeyCard)
                      {
                          // 有黑石摇滚等关键卡，先不打芬利
                           pen += 300;  // 显著增加pen值，降低优先级
                     }
                       // 情况4：手牌太少，风险大
                        else if (handCount <= 2)
                            {
                              pen += 100;  // 不太想打出
                     }
                           // 情况5：默认情况
                      else
                               {
                            pen += 30;  // 稍微倾向于打出
                     }
                                               break;
                     }
                case CardDB.cardNameCN.余震:
                
                // 1. 基础优先级调整
                int baseAdjustment = 0;
    
                // 2. 获取关键数据
                int currentMana = p.mana;
                int enemyMinionCount = p.enemyMinions.Count;
                int ownMinionCount = p.ownMinions.Count;
                if (currentMana < 5 && enemyMinionCount > 4)
                 {
                // 低费时敌方场面很满，急需处理
                 baseAdjustment = -200;  // 大幅提高优先级
                 }
                 else if (currentMana > 7 && enemyMinionCount > 4)
                 {
                 // 高费时敌方场面很满，应该处理
                 baseAdjustment = -40;   // 适度提高优先级
                 }
                 else
                 {
                  // 其他情况
                 baseAdjustment = -20;   // 默认优先级
                 }
                 if (ownMinionCount >= 3 && enemyMinionCount <= 2)
                 {
                 // 我方场面优势，不太需要AOE
                  baseAdjustment += 100;  // 增加pen值，降低优先级
                 } 
                break;
                case CardDB.cardNameCN.活体烈焰:
                {
                // 基础优先级调整
                pen -= 0;  // 假设这是原有的基础调整
    
                // 检查手牌中是否有黑石摇滚
                bool hasBlackRock = false;
                foreach (Handmanager.Handcard hc in p.owncards)  // 添加foreach循环
                {
                if (hc.card.nameCN == CardDB.cardNameCN.黑石摇滚)
                {
                hasBlackRock = true;
                break;
                }
                }
    
                // 根据是否有黑石摇滚调整优先级
                if (hasBlackRock)
               {
               pen += 100;  // 有黑石摇滚，增加pen值（降低优先级）
               }
               else
               {
               pen -= 100;  // 没有黑石摇滚，降低pen值（提高优先级）
               }
    
               break;  // 添加break语句
               }     
               case CardDB.cardNameCN.萨隆苦囚:
               foreach (Handmanager.Handcard hhc in p.owncards)
                {
                   if (hhc.card.nameCN == CardDB.cardNameCN.维伦流亡者领袖)
                   {
                        pen -= 1000;
                   }
                }
                    break;
                 // 假设时空之门的卡牌名是 CardDB.cardNameCN.时空之门
                case CardDB.cardNameCN.时空扭曲: // 请替换成实际的卡牌名
                {
                 int totalAttack = 0;
                 foreach (Minion minion in p.ownMinions)
                 {
                   totalAttack += minion.Angr;
                 }
        
                 int potentialDamage = totalAttack * 1;
                 int requiredDamage = p.enemyHero.Hp;
    
                    if (potentialDamage >= requiredDamage)
                    {
                    pen -= 1000; // 满足条件，提高优先级
                    }
                 else
                    {
                    pen += 40; // 不满足条件，降低优先级
                    }

                }
                 break;
                
                    break;
                case CardDB.cardNameCN.走进失落之城:
                {
                    pen -= 800;
                }
                    break; 
                case CardDB.cardNameCN.火羽之心:
                {
                    pen -= 80;
                }
                    break;    
                case CardDB.cardNameCN.盾牌格挡:
                {
                    pen -= 20;
                }
                    break;      
                case CardDB.cardNameCN.困倦的岛民:
                {
                
                int enemyAttack = 0;
                int enemyCount = p.enemyMinions.Count;
    
                // 计算敌方场攻
                foreach (Minion m in p.enemyMinions) 
                {
                enemyAttack += m.Angr;
                }
    
                // 基础优先级 = 对面场攻
                int basePriority = enemyAttack;
    
                // 额外因素：敌方随从数量
               if (enemyCount >= 5)
               {
               basePriority += 20;  // 敌方随从多，额外提高优先级
               }
    
               // 应用调整
               pen -= basePriority;
    
                break;
                }        
                case CardDB.cardNameCN.萨弗拉斯:
                {
                    pen -= 80;
                }
                    break;      
                case CardDB.cardNameCN.践踏者班纳布斯:
                {
                    pen -= 80;
                }
                    break;    
                case CardDB.cardNameCN.拉特维厄斯城市之眼:
                {  
                    int handCount = p.owncards.Count;
                    if (handCount < 9)
                {
                pen -= 300; // 降低pen值，增加优先级，具体数值可以根据需要调整
                }

                break;  
                } 
                case CardDB.cardNameCN.卑劣的脏鼠:
                if (p.enemyMinions.Count <= 2 && p.ownMinions.Count >= 4)
                {
                    pen -= 50;
                }
                else if (p.enemyMinions.Count > 3 && p.ownMinions.Count < 3)
                {
                    pen += 300;
                }
                 // 新增：如果当前最大法力值低于7，增加惩罚
                if (p.ownMaxMana < 7)
                {
                pen += 200; // 这里惩罚值设为200，用户可以根据需要调整
                }
                    break;
                case CardDB.cardNameCN.希望守护者阿玛拉:
                if (p.ownHero.Hp <= 15)
                {
                    pen -= 1000;
                }
                  
                else if (p.ownHero.Hp > 25)
                {
                    pen += 100;
                }
                    break;
                case CardDB.cardNameCN.黑石摇滚:
                {
                 int enemyAttack = 0;
                 foreach (Minion m in p.enemyMinions) enemyAttack += m.Angr;
    
                int adjustment = 0;
    
                // 场攻小于15，提高优先级
                if (enemyAttack < 15)
                {
                adjustment -= 80;  // 提高优先级
                }
    
               // 费用小于7，提高优先级
               if (p.mana < 7)
               {
               adjustment -= 50;  // 提高优先级
               }
    
               // 如果两个条件都满足，优先级更高
               if (enemyAttack < 15 && p.mana < 7)
               {
                adjustment -= 20;  // 额外奖励
               }
    
               pen += adjustment;
                break;
                }
                case CardDB.cardNameCN.蒸汽守卫:
                {
                    pen -= 80;
                }
                 break;
                case CardDB.cardNameCN.卡纳莎女王:
                 {
                 // 如果当前法力值小于20，大幅度增加惩罚，让AI几乎不打
                 if (p.mana < 20)
                 {
                 pen += 150;  // 非常大的惩罚值，让AI几乎不考虑打出
                 }
                // 法力值>=20时不调整或少量调整
                else
                {
                pen += 0;  // 或不调整
                }
    
                break;
                }
                 case CardDB.cardNameCN.老鲨嘴:
                {
                 // 如果当前法力值小于20，大幅度增加惩罚，让AI几乎不打
                 if (p.mana < 20)
                 {
                 pen += 150;  // 非常大的惩罚值，让AI几乎不考虑打出
                 }
                // 法力值>=20时不调整或少量调整
                else
                {
                pen += 0;  // 或不调整
                }
    
                break;
                } 
                case CardDB.cardNameCN.水晶核心:
                {
                 // 如果当前法力值小于20，大幅度增加惩罚，让AI几乎不打
                 if (p.mana < 20)
                 {
                 pen += 150;  // 非常大的惩罚值，让AI几乎不考虑打出
                 }
                // 法力值>=20时不调整或少量调整
                else
                {
                pen += 0;  // 或不调整
                }
    
                break;
                }
                case CardDB.cardNameCN.黏团焦油:
                {
                    pen -= 60;
                }
                    break;
                case CardDB.cardNameCN.洛瑟玛塞隆:
                {
                 int enemyAttack = 0;
                 foreach (Minion m in p.enemyMinions) enemyAttack += m.Angr;
    
                int adjustment = 0;
    
                // 场攻小于15，提高优先级
                if (enemyAttack < 15)
                {
                adjustment -= 80;  // 提高优先级
                }
    
               // 费用小于7，提高优先级
               if (p.mana < 10)
               {
               adjustment -= 50;  // 提高优先级
               }
    
               // 如果两个条件都满足，优先级更高
               if (enemyAttack < 15 && p.mana < 10)
               {
                adjustment -= 20;  // 额外奖励
               }
    
               pen += adjustment;
                break;
                }
                case CardDB.cardNameCN.乐队经理精英牛头人酋长:
                {
                    pen -= 30;
                }
                
                    break;
                case CardDB.cardNameCN.倒霉的炸药师:
                {
                    pen -= 80;
                }
                break;
                case CardDB.cardNameCN.火成熔岩吞食者:
                {
                    pen -= 60;
                }
                    break;
                case CardDB.cardNameCN.补水区:
                if(p.getCorpseCount() < 6 && p.ownMinions.Count > 4)
                {
                    pen += 1000;
                }
                else if(p.getCorpseCount() > 4 && p.ownMinions.Count < 4)
                {
                    pen -= 100;
                }
                break;    
                case CardDB.cardNameCN.剑刃风暴:
                if (p.ownMinions.Count < 1 && p.enemyMinions.Count >= 1)
                {
                    pen -= 50;
                }
                else if(p.ownMinions.Count >= 1 && p.enemyMinions.Count < 1)
                {
                    pen += 100;
                }
                break;
                case CardDB.cardNameCN.绝命乱斗:
                { 
                int enemyAttack = 0;
                int enemyCount = p.enemyMinions.Count;
                int ownCount = p.ownMinions.Count;
    
                // 计算敌方场攻
                foreach (Minion m in p.enemyMinions) 
                { 
                enemyAttack += m.Angr;
                }
    
                int adjustment = 0;
    
                // 1. 对面场攻>15，增加50优先级
                if (enemyAttack > 12)
                 {
                adjustment -= 50;
                 }
    
               // 2. 对面随从多于5个，再增加50优先级
               if (enemyCount > 5)
               {
               adjustment -= 50;
               }
    
               // 3. 我方随从多于3个，增加100惩罚值
               if (ownCount > 3)
               {
               adjustment += 100;
               }
    
              // 4. 基本逻辑：敌方有怪才能用
             if (enemyCount >= 2)
            {
            adjustment += 200;  // 没怪绝对不打
            }
    
             pen += adjustment;
    
                  break;
               }
                case CardDB.cardNameCN.荒芜之地乱斗打手:
                if(p.ownMinions.Count <= 2 && p.enemyMinions.Count >= 5)
                {
                    pen -= 100;
                }
                else if(p.ownMinions.Count >= 3 && p.enemyMinions.Count <= 2)
                {
                    pen += 1000;
                }
                    break;
                case CardDB.cardNameCN.愤怒卫士:
                {
                    pen += 10000;
                }
                    break;
                case CardDB.cardNameCN.凶魔城堡:
                {
                    pen += 10000;
                }
                    break;
                case CardDB.cardNameCN.幸运币:
                {
                    pen += 10;
                }
                    break;
                 case CardDB.cardNameCN.爆破龟:
                 if(p.enemyMinions.Count >= 4)
                 {
                     pen -= 100;
                 }
                    break;
                 case CardDB.cardNameCN.爆破工头索格伦:
                {
                 int enemyAttack = 0;
                 foreach (Minion m in p.enemyMinions) enemyAttack += m.Angr;
    
                int adjustment = 0;
    
                // 场攻小于15，提高优先级
                if (enemyAttack < 15)
                {
                adjustment -= 50;  // 提高优先级
                }
    
               // 费用大于15，提高优先级
               if (p.mana > 15)
               {
               adjustment -= 50;  // 提高优先级
               }
    
               // 如果两个条件都满足，优先级更高
               if (enemyAttack < 15 && p.mana < 7)
               {
                adjustment -= 20;  // 额外奖励
               }
    
               pen += adjustment;
                break;
                }
                case CardDB.cardNameCN.基尔加丹:
                    pen -= 5;
                    if (p.ownMinions.Count >= 2 && p.ownHero.Hp >= 17) pen -= 27;
                    if (p.ownMinions.Count >= 3 && p.ownHero.Hp >= 15) pen -= 88;
                    break;
                case CardDB.cardNameCN.商品卖家:
                if (p.enemyDeckSize < 15 || p.ownMinions.Count > 2)
                {
                    pen -= 200; // 当对方牌库少于15张或我方场上有3个以上随从时提高优先级
                }
                else
                {
                    pen += 10; // 不满足条件时给予小幅度惩罚
                }
                    break;
                case CardDB.cardNameCN.铸甲师:
                if (p.ownMinions.Count > 2)
                {
                    pen -= 50;
                }
                else
                {
                    pen += 20;
                }
                    break;
                case CardDB.cardNameCN.奇利亚斯:
                int myCurrentHP = p.ownHero.Hp;
    
                // 简单明了的写法
                if (myCurrentHP <= 15)
                {
                // 血量很低，高优先级
                pen -= 300;
                }
                else if (myCurrentHP <= 25)
                {
                // 血量中等，中等优先级
                pen -= 100;
                }
                else
                {
                // 血量健康，低优先级
                pen -=50;  // 增加pen值，降低优先级
                }
    
                break;
                case CardDB.cardNameCN.无界空宇:
                if (p.enemyMinions.Count < 5 && p.ownMinions.Count >= 3)
                {
                    pen += 2000;
                }
                else if (p.enemyMinions.Count > 5 && p.ownMinions.Count < 3)
                {
                    pen -= 300;
                }
                break;
           
                case CardDB.cardNameCN.托尔托拉:
                {
                  
                
                int enemyAttack = 0;
                int enemyCount = p.enemyMinions.Count;
    
                // 计算敌方场攻
                foreach (Minion m in p.enemyMinions) 
                {
                enemyAttack += m.Angr;
                }
    
                // 基础优先级 = 对面场攻
                int basePriority = enemyAttack;
    
                // 额外因素：敌方随从数量
               if (enemyCount >= 5)
               {
               basePriority += -20;  // 敌方随从多，额外提高优先级
               }
    
               // 应用调整
               pen -= basePriority +20;
    
                break;
                }
               
                // 根据手牌数量调整昔时古树的优先级
                case CardDB.cardNameCN.昔时古树:
               {
                     int handCount = p.owncards.Count;
    
               // 计算敌方场攻
                     int enemyAttack = 0;
                     foreach (Minion m in p.enemyMinions) enemyAttack += m.Angr;
    
                     int adjustment = 0;
    
                     // 1. 手牌数量基础判断
                     if (handCount < 3)
                        {
                       adjustment -= 100;
                    }
                    else if (handCount > 6)
                   {
                       adjustment -= 20;
                 }
                    else 
                    {
                     adjustment += 40;
                    }
                 
                     // 2. 场攻因素
                   if (enemyAttack >= 15)
                    {
                  adjustment += 100;  // 高场攻，提高优先级
                    }
                    else if (enemyAttack >= 8)
                    {
                    adjustment -= 40;   // 中场攻，适度提高
                    }
    
                 // 3. 特殊情况处理
                     if (enemyAttack == 0 )
                    {
                           adjustment -= 20;  // 没场攻且手牌多，降低优先级
                    }
    
                 pen -= adjustment;
                       break;
                      }
            // 根据手牌数量调整自助大餐的优先级
                case CardDB.cardNameCN.自助大餐:
                {
                    if (p.owncards.Count < 3)
                    {
                        pen -= 30; // 手牌少时提高优先级
                    }
                    else if (p.owncards.Count > 5)
                    {
                        pen += 20; // 手牌多时降低优先级
                    }
                    else
                    {
                        pen -= 20; // 手牌数量适中时给予小幅度提升
                    }
                }
                break;    
            }

            return pen;
        }

        // 核心，场面值
        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -200000) return p.value;
            float retval = 0;
            retval += getGeneralVal(p);            
            retval += getHpValue(p, hpboarder, aggroboarder);
            // 出牌序列数量
            int count = p.playactions.Count;
            int ownActCount = 0;
            bool useAb = false;
            // 排序问题！！！！
            for (int i = 0; i < count; i++)
            {
                Action a = p.playactions[i];
                ownActCount++;
                switch (a.actionType)
                {
                    case actionEnum.trade://交易
                        retval += 20;
                        continue;
                    case actionEnum.useLocation://地标
                        retval += 20;
                        continue;
                    case actionEnum.useTitanAbility://泰坦
                        retval += 40;
                        continue;
                    case actionEnum.forge://锻造
                        retval += 20;
                        continue;
                        // 随从攻击
                    case actionEnum.attackWithMinion:
                        continue;
                    // 英雄攻击
                    case actionEnum.attackWithHero:
                        continue;
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
                        continue;
                    case actionEnum.playcard:
                        break;
                    default:
                        continue;
                }
                switch (a.card.card.nameCN)
                {
                    case CardDB.cardNameCN.幸运币:
                        retval -= i;
                        break;
                    case CardDB.cardNameCN.绝命乱斗:
                        retval -= i;
                        break;  
                    case CardDB.cardNameCN.卑劣的脏鼠:
                        retval -= i;
                        break;     
                }
            }
            // 对手基本随从交换模拟
            retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p); // 奥秘的影响
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;
            return retval;
        }


        // 敌方随从价值 主要等于 （HP + Angr） * 4  
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
            foreach (CardDB.cardIDEnum s in p.ownSecretsIDList)
            {
                if (s == CardDB.cardIDEnum.EX1_610 || s == CardDB.cardIDEnum.VAN_EX1_610)
                {
                    if (m.Hp <= 2)
                    {
                        dieNextTurn = true;
                        break;
                    }
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 4;
            if (m.Angr > 0 || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                retval += m.Hp * bonus_enemy;
            retval += m.spellpower * bonus_enemy * 3 / 2;
            if (!m.frozen && !m.cantAttack)
            {
                retval += m.Angr * bonus_enemy;
                if (m.windfury) retval += m.Angr * bonus_enemy / 2;
            }
            if (m.silenced) return retval;

            if (m.taunt) retval += 2;
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;

            if (m.lifesteal) retval += m.Angr * bonus_enemy;

            if (m.poisonous)
            {
                retval += 4;
                if (p.ownMinions.Count < p.enemyMinions.Count) retval += 10;
            }
            // 异能价值
            switch (m.handcard.card.nameCN)
            {
                // 解不掉游戏结束
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.全息技师:
                case CardDB.cardNameCN.神话观测者:
                case CardDB.cardNameCN.亡者卡特琳娜:
                case CardDB.cardNameCN.憎恶军官:
                case CardDB.cardNameCN.克尔苏加德:
                case CardDB.cardNameCN.阳光汲取者莱妮莎:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                case CardDB.cardNameCN.黑眼:
                case CardDB.cardNameCN.聒噪怪:
                case CardDB.cardNameCN.诺甘农:
                case CardDB.cardNameCN.雷霆之神高戈奈斯:
                case CardDB.cardNameCN.阿曼苏尔:
                case CardDB.cardNameCN.翠绿之星阿古斯:
                case CardDB.cardNameCN.兵主:
                case CardDB.cardNameCN.灭世泰坦萨格拉斯:
                case CardDB.cardNameCN.脱困古神尤格萨隆:
                case CardDB.cardNameCN.生命的缚誓者艾欧娜尔:
                case CardDB.cardNameCN.伴唱机:
                case CardDB.cardNameCN.鲨鱼之灵:
                case CardDB.cardNameCN.农夫:
                case CardDB.cardNameCN.尼鲁巴蛛网领主:
                case CardDB.cardNameCN.前沿哨所:
                case CardDB.cardNameCN.洛萨:
                case CardDB.cardNameCN.考内留斯罗姆:
                case CardDB.cardNameCN.战场军官:
                case CardDB.cardNameCN.大领主弗塔根:
                case CardDB.cardNameCN.圣殿蜡烛商:
                case CardDB.cardNameCN.伯尔纳锤喙:
                case CardDB.cardNameCN.魅影歹徒:
                case CardDB.cardNameCN.灵魂窃贼:
                case CardDB.cardNameCN.甜水鱼人斥候:
                case CardDB.cardNameCN.原野联络人:
                case CardDB.cardNameCN.狂欢报幕员:
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.塔姆辛罗姆:
                case CardDB.cardNameCN.导师火心:
                case CardDB.cardNameCN.伊纳拉碎雷:
                case CardDB.cardNameCN.暗影珠宝师汉纳尔:
                case CardDB.cardNameCN.伦萨克大王:
                case CardDB.cardNameCN.洛卡拉:
                case CardDB.cardNameCN.布莱恩铜须:
                case CardDB.cardNameCN.观星者露娜:
                case CardDB.cardNameCN.大法师瓦格斯:
                case CardDB.cardNameCN.火妖:
                case CardDB.cardNameCN.下水道渔人:
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                case CardDB.cardNameCN.团伙核心:
                case CardDB.cardNameCN.巡游领队:
                case CardDB.cardNameCN.科卡尔驯犬者:
                case CardDB.cardNameCN.火舌图腾:
                    retval += bonus_enemy * 8;
                    break;
                // 不解巨大劣势
                case CardDB.cardNameCN.笨拙的杂役:
                case CardDB.cardNameCN.小鬼骑士:
                case CardDB.cardNameCN.召唤师达克玛洛:
                case CardDB.cardNameCN.时空领主戴欧斯:
                case CardDB.cardNameCN.巨像:
                case CardDB.cardNameCN.灵魂歌者安布拉:
                case CardDB.cardNameCN.决斗大师莫扎奇:
                case CardDB.cardNameCN.玛里苟斯:
                case CardDB.cardNameCN.沙德沃克:
                case CardDB.cardNameCN.伊谢尔风歌:
                case CardDB.cardNameCN.大法师罗曼斯:
                case CardDB.cardNameCN.克罗格环形山之王:
                case CardDB.cardNameCN.艾维娜:
                case CardDB.cardNameCN.索瑞森大帝:
                case CardDB.cardNameCN.极限追逐者阿兰娜:
                case CardDB.cardNameCN.拍卖师亚克森:
                case CardDB.cardNameCN.法师猎手:
                case CardDB.cardNameCN.饥饿食客哈姆:
                case CardDB.cardNameCN.萨特监工:
                case CardDB.cardNameCN.甩笔侏儒:
                case CardDB.cardNameCN.精英牛头人酋长金属之神:
                case CardDB.cardNameCN.莫尔杉哨所:
                case CardDB.cardNameCN.凯瑞尔罗姆:
                case CardDB.cardNameCN.鱼人领军:
                case CardDB.cardNameCN.南海船长:
                case CardDB.cardNameCN.坎雷萨德埃伯洛克:
                case CardDB.cardNameCN.人偶大师多里安:
                case CardDB.cardNameCN.暗鳞先知:
                case CardDB.cardNameCN.灭龙弩炮:
                case CardDB.cardNameCN.神秘女猎手:
                case CardDB.cardNameCN.鲨鳍后援:
                case CardDB.cardNameCN.怪盗图腾:
                case CardDB.cardNameCN.矮人神射手:
                case CardDB.cardNameCN.任务达人:
                case CardDB.cardNameCN.贪婪的书虫:
                case CardDB.cardNameCN.战马训练师:
                case CardDB.cardNameCN.相位追猎者:
                case CardDB.cardNameCN.鱼人宝宝车队:
                case CardDB.cardNameCN.科多兽骑手:
                case CardDB.cardNameCN.奥秘守护者:
                case CardDB.cardNameCN.获救的流民:
                case CardDB.cardNameCN.白银之手新兵:
                case CardDB.cardNameCN.低阶侍从:
                    retval += bonus_enemy * 3;
                    break;
                // 算有点用
                case CardDB.cardNameCN.幽灵狼前锋:
                case CardDB.cardNameCN.战斗邪犬:
                case CardDB.cardNameCN.饥饿的秃鹫:
                case CardDB.cardNameCN.法力浮龙:
                case CardDB.cardNameCN.加基森拍卖师:
                case CardDB.cardNameCN.飞刀杂耍者:
                case CardDB.cardNameCN.锈水海盗:
                case CardDB.cardNameCN.大法师安东尼达斯:
                    retval += bonus_enemy * 2;
                    break;
            }
            // 血量越低，解怪优先度越高
            if (p.ownHero.Hp <= 15)
            {
                retval += (16 - p.ownHero.Hp) * 3;
                if (p.ownHero.Hp <= 6) retval *= 2;
            }
            return retval;
        }
        
        /// <summary>
        /// 我方随从价值
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
            int retval = 5;
            if (m.Hp <= 0) return 0;
            retval += m.Hp * bonus_mine;
            retval += m.Angr * bonus_mine;
            if (m.Hp <= 1 && !m.divineshild) retval -= (m.Angr - 1) * (bonus_mine - 1);
            // 高攻低血是垃圾
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * (bonus_mine - 1);
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
            // 嘲讽
            if (m.taunt) retval += 3;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.黑眼:
                    break;
            }
            if (m.dormant > 0)
            {
                retval -= bonus_mine * m.dormant;
            }
            return retval;
        }

        public override int getSirFinleyPriority(List<Handmanager.Handcard> discoverCards)
        {

            return -1; //comment out or remove this to set manual priority
            int sirFinleyChoice = -1;
            int tmp = int.MinValue;
            for (int i = 0; i < discoverCards.Count; i++)
            {
                CardDB.cardNameEN name = discoverCards[i].card.nameEN;
                if (SirFinleyPriorityList.ContainsKey(name) && SirFinleyPriorityList[name] > tmp)
                {
                    tmp = SirFinleyPriorityList[name];
                    sirFinleyChoice = i;
                }
            }
            return sirFinleyChoice;
        }
        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
        }

        private Dictionary<CardDB.cardNameEN, int> SirFinleyPriorityList = new Dictionary<CardDB.cardNameEN, int>
        {
            //{HeroPowerName, Priority}, where 0-9 = manual priority
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

        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;

            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - hpboarder);
            }
            // 快死了
            else
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 5 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
            }
            if (p.ownHero.Hp + p.ownHero.armor < 10 && p.ownHero.Hp + p.ownHero.armor > 0)
            {
                retval -= 200 / (p.ownHero.Hp + p.ownHero.armor);
            }
            // 对手血线安全
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
            {
                retval += 2 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 开始打脸
            else
            {
                retval += 4 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 场攻+直伤大于对方生命，预计完成斩杀
            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 2000;
            }
            return retval;
        }

        /// <summary>
        /// 获取使用地标的惩罚值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getUseLocationPenality(Minion m, Minion target, Playfield p)
        {
            int penalty = 0; // 初始惩罚值为 0

            switch (m.handcard.card.nameCN.ToString())
            {
                case "树篱迷宫":
                    penalty = -47; // 优先级数值转为负值作为惩罚值
                    break;
                case "远足步道":
                    penalty = -49;
                    break;
                case "尤格萨隆的监狱":
                    penalty = -52;
                    break;
                case "惊险悬崖":
                    penalty = -68;
                    break;
                case "鹦鹉乐园":
                    penalty = -59;
                    break;
                case "大地之末号":
                    penalty = -59;
                    break;
                case "潮汐之地":
                    penalty = -50;
                    break;
                case "永时坚垒":
                    penalty = -48;
                    break;
                case "过去的银月城":
                    penalty = -48;
                    break;   
                case "过去的诺莫瑞根":
                    penalty = -48;
                    break;  
                case "过去的时光流汇":
                    penalty = -48;
                    break;       
                case "未来的诺莫瑞根":
                    penalty = -48;
                    break;           
                    break;       
                case "未圣地卡拉赞":
                    penalty = -48;
                    break;           
                    break;       
                case "永恒之井":
                    penalty = -48;
                    break;           
                    break;       
                case "辛艾萨莉":
                    penalty = -48;
                    break;           
                    break;       
                case "现在的银月城":
                    penalty = -48;
                    break;                               
                case "小玩物小屋":
                    penalty = -48;
                    break;
                    
                default:
                    penalty = 0; // 如果卡牌名称不匹配，使用初始惩罚值
                    break;
            }

            return (int)penalty;
        }

        

        /// <summary>
        /// 获取使用泰坦技能的惩罚值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getUseTitanAbilityPenality(Minion m, Minion target, Playfield p)
        {
            
                return 0;
        }

        /// <summary>
        /// 发现卡的价值
        private static Dictionary<CardDB.cardNameCN, int> discoverCache = new Dictionary<CardDB.cardNameCN, int>();
        // 发现卡的价值
        public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
            int fixedPriorityValue = 0;

            switch (card.nameCN)
            {   
        case CardDB.cardNameCN.无界空宇:
        fixedPriorityValue = 1000;
        break;
        case CardDB.cardNameCN.黑石摇滚:
        fixedPriorityValue = 900;
        break;
        case CardDB.cardNameCN.洛瑟玛塞隆:
        fixedPriorityValue = 800;
        break;
        
        }
        // 如果fixedPriorityValue大于0，说明是特殊卡牌，直接返回，不再进行Hsreplay查询
        if (fixedPriorityValue > 0)
        {
        return fixedPriorityValue;
        }
             
        // 如果不是特殊卡牌，则进行原来的Hsreplay查询     
                

           try
              {
                //初始化Hsreplay数据
                Hsreplay hs = Hsreplay.Instance;

        // 1. 检查数据加载状态
            if (Hsreplay.AllCardStats == null || Hsreplay.AllCardStats.Count == 0)
            {
                Helpfunctions.Instance.logg("Hsreplay 数据未加载或为空");
                return 0;
            }
            else
            {
                Helpfunctions.Instance.logg("已加载 " + Hsreplay.AllCardStats.Count.ToString() + " 条卡牌数据");
            }

                // 2. 打印调试信息
                Helpfunctions.Instance.logg(string.Format("查询卡牌：{0} (DBF ID: {1})", card.nameCN, card.dbfId));
        
            // 3. 添加类型转换确保一致
            var cardStats = Hsreplay.AllCardStats.FirstOrDefault(c => c.DbfId == card.dbfId);

            if (cardStats != null)
            {
               Helpfunctions.Instance.logg("匹配成功 - 胜率: " + cardStats.WinrateWhenDrawn.ToString());
               Helpfunctions.Instance.logg("getDiscoverVal - 使用Hsreplay数据比对" + card.nameCN.ToString() + " => " + cardStats.WinrateWhenDrawn);
               ilog_0.Info("getDiscoverVal - 使用Hsreplay数据比对" + card.nameCN.ToString() + " => " + cardStats.WinrateWhenDrawn);
               // 返回 WinrateWhenDrawn 的整数部分
               return (int)cardStats.WinrateWhenDrawn;
            }

               // 4. 输出样例数据辅助调试
               var sampleIds = Hsreplay.AllCardStats.Take(10).Select(c => c.DbfId).ToList();
               Helpfunctions.Instance.logg("未找到匹配数据，样例 DbfIds: " + string.Join(", ", sampleIds));
               return 0;
            }
            catch (Exception ex)
            {
               Helpfunctions.Instance.logg("异常发生: " + ex.Message);
               //ilog_0.Error(ex, "getDiscoverVal 执行错误");
               return 0;
            }
        }

        
    }
}