using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement_and_SpriteSheet_together
{
    public class Particle
    {
        public Vector2 _position;
        public Vector2 _velocity;
        public float _lifespan;
        public float _currentLife;
        public float _scale;
        public Color _color;

        public bool _isExpired => _currentLife >= _lifespan;

        public void Update(GameTime gameTime)
        {
            _currentLife += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position += _velocity;
            _scale *= 0.95f; // Shrink over time
        }
    }
}
