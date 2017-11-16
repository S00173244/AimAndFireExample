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
        private Projectile arrow;

        private enum FireState { Ready, NotReady}

        private FireState fireState = FireState.Ready;
        public Sentry(Game g,Texture2D tx,Vector2 StartPosition, int NoOfFrames) : base(g, tx, StartPosition, NoOfFrames)
        {
           
        }
        public void loadProjectile(Projectile r)
        {
            arrow = r;
        }


        public override void Update(GameTime gameTime)
        {
            arrow.Update(gameTime);

          
           
            base.Update(gameTime);
        }

        public bool FaceThePlayer(PlayerWithWeapon p)
        {
            this.angleOfRotation = TurnToFace(position, p.position, angleOfRotation, 0.2f);
            return true;
        }
        public bool IsPlayerInShootingArea(PlayerWithWeapon p)
        {
            float distance = Math.Abs(Vector2.Distance(this.WorldOrigin, p.CentrePos));

            if (distance < shootingArea)
                return true;
            else return false;

        }
    
        public bool IsReadyToFire()
        {
            if (fireState == FireState.Ready) return true;
            else return false;          
            
        }

        
        public void ReloadArrow(GameTime gametime)
        {

            if (remainingReloadTime == 0)
            {
                remainingReloadTime = fireRate;
                UpdateFireState(true);
            }else if (remainingReloadTime != 0)
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
            
            arrow.position = this.position;
            
            arrow.fire(p.position);


            
                                                
            UpdateFireState(false);
                      
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(arrow!= null)
            {
                arrow.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
    }
}
