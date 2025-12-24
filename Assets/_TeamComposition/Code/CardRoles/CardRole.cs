namespace TeamComposition2.CardRoles
{
    /// <summary>
    /// Defines the role categories for cards.
    /// Each card can only have one role.
    /// </summary>
    public enum CardRole
    {
        None = 0,
        Tank = 1,       // Defensive/survivability cards
        Atk = 2,        // Offensive/damage cards
        Heal = 3,       // Healing/regeneration cards
        Disabled = 4,   // Card is disabled and won't appear in selection
    }
}
