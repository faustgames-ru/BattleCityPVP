namespace BattleCity.Game.Damage
{
    public static class DamageLayerExtension
    {
        public static bool Intersects(this DamageLayer layer, DamageLayer value)
        {
            return (layer & value) != DamageLayer.None;
        }
    }
}