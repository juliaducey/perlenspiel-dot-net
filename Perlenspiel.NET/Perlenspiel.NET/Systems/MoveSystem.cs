using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielEngine.Systems;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Maps;

namespace PerlenspielGame.Systems
{
    public class MoveSystem : AbstractSystem
    {
        private List<Entity> _mobs;
        private List<Entity> _obstacles;

        #region ISystem Members
        public MoveSystem()
        {
            _mobs = new List<Entity>();
            _obstacles = new List<Entity>();
        }

        public override void Register(Entity entity)
        {
            if (entity.Component<Mob>() != null)
            {
                _mobs.Add(entity);
            }
            if (entity.Component<Obstacle>() != null)
            {
                _obstacles.Add(entity);
            }
        }

        public override void Deregister(Entity entity)
        {
            _mobs.Remove(entity);
            _obstacles.Remove(entity);
        }
        #endregion
        #region Public Methods

        public void MoveEntity(Entity entity, GridPoint oldPos, GridPoint newPos, bool checkPassable = true)
        {
            var canMove = true;
            if (checkPassable == true)
            {
                if (CanPassThroughSquare(entity, newPos) == false)
                    canMove = false;
            }
            if (canMove == true)
            {
                var pos = entity.Component<IPosition>();
                pos.ChangePosition(oldPos, newPos);
                GameState.DrawMap();
            }
        }

        public void DrawMobs()
        {
            foreach (var mob in _mobs)
            {
                GameState.DrawEntity(mob);
            }
        }

        #endregion
        #region Private Methods
        // Not finished yet
        private void GetMovementPath(Entity entity, GridPoint destination)
        {
            /*
            var mob = entity.GetComponent<Mob>();
            var pos = entity.GetComponent<Position>();

            var blockedEntityCoords = from blocker in _obstacles
                                      let obstacle = blocker.GetComponent<Obstacle>()
                                      where CanPass(mob, obstacle) == false
                                      select blocker.GetComponent<Position>();

            //var blockedTileCoords = Singleton<GameState>.Instance.GetBlockedTileCoords(mob.MovementTypes);

            //var blockedCoords = blockedEntityCoords.Union(blockedTileCoords);
            */
            throw new NotImplementedException();
        }

        private bool CanPassThroughSquare(Entity traveler, GridPoint pos)
        {
            var obstacles = from entity in _obstacles
                where entity.Component<IPosition>().IsAtPosition(pos.X, pos.Y) && CanPass(traveler, entity) == false
                select entity;
            return !obstacles.Any();
        }

        private bool CanPass(Entity traveler, Entity blocker)
        {
            var mob = traveler.Component<Mob>();
            var obstacle = blocker.Component<Obstacle>();

            // If the obstacle blocks everything, return false
            if (obstacle.MovementTypesBlocked.Contains("all"))
                return false;

            // Get all the flags the traveler has that the obstacle doesn't
            var flags = from flag in mob.MovementTypes
                        where obstacle.MovementTypesBlocked.Contains(flag) == false
                        select flag;

            // If the traveler has any flags (can pass) that the obstacle doesn't (can block),
            // then the traveler can pass over the obstacle
            return flags.Any();
        }
        #endregion
    }
}
