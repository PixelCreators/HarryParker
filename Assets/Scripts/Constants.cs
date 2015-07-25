namespace Constants
{
    public class LayerMask
    {
        public const int Walls = 1 << Layers.Walls;
        public const int PlayerProjectiles = 1 << Layers.PlayerProjectile;
        public const int EnemyProjectiles = 1 << Layers.EnemyProjectile;
        public const int Player = 1 << Layers.Player;
        public const int Enemy = 1 << Layers.Enemy;
    }

    public class Layers
    {
        public const int Enemy = 12;
        public const int Player = 11;
        public const int PlayerProjectile = 9;
        public const int EnemyProjectile = 10;
        public const int Walls = 8;
    }

    public class Tags
    {
        public const string Player = "Player";
    }
}