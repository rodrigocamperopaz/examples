using StarWars.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars
{
    public class StarWarsData
    {
        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Droid> _droids = new List<Droid>();

        public StarWarsData()
        {
            _humans.Add(new Human
            {
                Id = Guid.Empty.ToString(),
                Name = "Luke",
                Friends = new[] { "3", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });
            _humans.Add(new Human
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Vader",
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });

            _droids.Add(new Droid
            {
                Id = Guid.NewGuid().ToString(),
                Name = "R2-D2",
                Friends = new[] { "1", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Astromech"
            });
            _droids.Add(new Droid
            {
                Id = Guid.NewGuid().ToString(),
                Name = "C-3PO",
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Protocol"
            });
        }

        public IEnumerable<StarWarsCharacter> GetFriends(StarWarsCharacter character)
        {
            var friends = new List<StarWarsCharacter>();

            if (character == null)
            {
                return friends;
            }

            var lookup = character.Friends;
            if (lookup != null)
            {
                foreach (var h in _humans.Where(h => lookup.Contains(h.Id)))
                    friends.Add(h);
                foreach (var d in _droids.Where(d => lookup.Contains(d.Id)))
                    friends.Add(d);
            }
            return friends;
        }

        public Task<List<Human>> GetHumansAsync()
        {
            return Task.FromResult(_humans);
        }

        public Task<List<Droid>> GetDroidsAsync()
        {
            return Task.FromResult(_droids);
        }

        public Task<Human> GetHumanByIdAsync(string id)
        {
            return Task.FromResult(_humans.FirstOrDefault(h => h.Id == id));
        }

        public Task<Droid> GetDroidByIdAsync(string id)
        {
            return Task.FromResult(_droids.FirstOrDefault(h => h.Id == id));
        }

        public Human AddHuman(Human human)
        {
            human.Id = Guid.NewGuid().ToString();
            _humans.Add(human);
            return human;
        }

        public void DeleteHuman(string id)
        {
            _humans.RemoveAll(h => h.Id == id);            
        }
    }
}
