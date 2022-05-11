using UnityEngine;
public class ShootCommand: Command{

    private Player player;

    //Pass the entity to the base class
    public ShootCommand(Player entity) : base(entity){
        player = entity;
    }
    //Project the bullet in the upwards direction of the passed entity
    public override void Execute()
    {
        if (player.GetComponent<Player>().tripleShot != null)
        {
            Bullet bulletOne = Instantiate(player.bulletPrefab, player.transform.position, player.transform.rotation);
            bulletOne.Project(entity.transform.up);         

            Bullet bulletTwo = Instantiate(player.bulletPrefab, player.transform.position, player.transform.rotation);
            Vector2 leftShot = entity.transform.up;
            leftShot.x = (((entity.transform.up.x * 2)- entity.transform.right.x)/3);
            leftShot.y = (((entity.transform.up.y * 2)- entity.transform.right.y)/3);
            bulletTwo.Project(leftShot);

            Bullet bulletThree = Instantiate(player.bulletPrefab, player.transform.position, player.transform.rotation);
            Vector2 rightShot = entity.transform.up;
            rightShot.x = (((entity.transform.up.x * 2) + entity.transform.right.x)/3);
            rightShot.y = (((entity.transform.up.y * 2) + entity.transform.right.y)/3);
            bulletThree.Project(rightShot);

        } else {
            Bullet bullet = Instantiate(player.bulletPrefab, player.transform.position, player.transform.rotation);
            bullet.Project(entity.transform.up);
        }
    }
}
