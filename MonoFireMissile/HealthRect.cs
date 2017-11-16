using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFireMissile
{
    class HealthRect
    {
        private int health;
        public Texture2D txBar; // hold the texture
        Rectangle _healthRect;   // display the Health bar size
        public Vector2 position; // Position on the screen
     
        public HealthRect(GraphicsDevice gD,Vector2 pos,int h)
        {
            position = pos;
            txBar = new Texture2D(gD,1, 1);
            txBar.SetData(new[] { Color.White });
            health = h;
           
        }

        public void UpdateHealthbarPos(Vector2 pos){
            position = pos;
            
        }

        public Rectangle HealthRectangle
        {
            get
            {

                return new Rectangle((int)position.X , (int)position.Y,Health, 10);
            }

            set
            {
                _healthRect = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                if (value >= 100) health = 100;
                else if (value <= 0) health = 0;
                else health = value;
            }
        }

        public void DrawHealth(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (Health > 60)
                spriteBatch.Draw(txBar, HealthRectangle, Color.Green);
            else if (Health > 30 && Health <= 60)
                spriteBatch.Draw(txBar, HealthRectangle, Color.Orange);
            else if (Health > 0 && Health < 30)
                spriteBatch.Draw(txBar,HealthRectangle, Color.Red);
            spriteBatch.End();
        }

    }
}
