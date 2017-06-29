using System;

namespace TDDLab
{
    public class BirdService : IBirdService
    {
        private readonly IBirdValidator _validator;
        private readonly IBirdRepository _repository;

        public BirdService(IBirdValidator validator, IBirdRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }
        
        public void Save(Bird bird)
        {
            if (!_validator.IsValid(bird))
            {
                throw new ArgumentException("The bird is not valid");
            }

            _repository.SaveOrUpdate(bird);
        }
    }
}
