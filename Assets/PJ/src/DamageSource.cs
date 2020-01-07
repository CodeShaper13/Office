public class DamageSource {

    public readonly Player player;
    public int amount;
    public readonly float power;

    public DamageSource(int amount, Player player, float power = 1f) {
        this.player = player;
        this.amount = amount;
        this.power = power;
    }
}
