﻿using Cards.CardElementClasses;
using Cards.Factory;
using Enemies;

namespace Cards.Debuffs
{
    public abstract class Debuff : CardElement
    {
        protected Debuff(CardElementClassAttributes attributes, Rarity rarity) : base(attributes, rarity) { }
        
        protected override void RemoveSelfFromPool(CardPool pool) => pool.Remove(Type, Rarity);

        public abstract DebuffType Type { get; }
    }
}