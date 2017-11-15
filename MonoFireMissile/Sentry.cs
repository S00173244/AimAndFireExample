using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AnimatedSprite
{
    class Sentry:RotatingSprite
    {
        private int fireRate = 2000;
        private int remainingReloadTime = 0;
        private float shootingArea = 350;
        private PlayerWithWeapon player;
        private GameTime gameTime;

        private enum FireState { Ready, NotReady}

        private FireState fireState = FireState.Ready;
        public Sentry(Game g,Texture2D tx,Vector2 StartPosition, int NoOfFrames, PlayerWithWeapon p) : base(g, tx, StartPosition, NoOfFrames)
        {
            player = p;
        }


        public override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            FireAtPlayer(gameTime);
           
            base.Update(gameTime);
        }
        public float CheckPlayerDistance(PlayerWithWeapon p)
        {
            float distance = Math.Abs(Vector2.Distance(this.WorldOrigin, p.CentrePos));

            return distance;

        }
        public void FireAtPlayer(GameTime gameTime)
        {
            if (CheckPlayerDistance(player) < shootingArea && fireState == FireState.Ready)
            {
                FireArrow(player);
            }
            
            else ReloadArrow(gameTime);
            
        }

        public void ReloadArrow(GameTime gametime)
        {

            if (remainingReloadTime == 0)
            {
                remainingReloadTime = fireRate;
                UpdateFireState(true);
            }

            if (remainingReloadTime != 0)
            {
                remainingReloadTime -= gametime.ElapsedGameTime.Milliseconds;
                UpdateFireState(false);
            }


        }

        public void UpdateFireState(bool status)
        {
            if (status) fireState = FireState.Ready;
            else fireState = FireState.NotReady;
        }

        public void FireArrow(PlayerWithWeapon p)
        {
            Projectile arrow = new Projectile(this.game, game.Content.Load<Texture2D>(@"Textures/Arrow"), new Sprite(game, game.Content.Load<Texture2D>(@"Textures/explosion_strip8"), p.position, 8), this.position, 8);

            arrow.Update(game.Services.GetService<GameTime>());
        }


    }
}
