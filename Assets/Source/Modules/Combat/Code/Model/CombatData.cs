namespace Combat
{
    public class CombatData
    {
        public Damage Damage;
        public DamageSettings DamageSettings;

        public CombatData(DamageSettings damageSettings)
        {
            DamageSettings = damageSettings;
            Damage = new Damage(damageSettings.Min, damageSettings.Max);
        }
    }
}
