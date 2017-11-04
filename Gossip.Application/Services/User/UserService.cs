using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gossip.Contract.Interfaces.User;
using Gossip.Domain.Repositories;
using Gossip.Domain.Repositories.User;
using DTOUser = Gossip.Contract.DTO.User.User;
using DomainUser = Gossip.Domain.Models.User.User;
using LanguageExt;

namespace Gossip.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWorkFactory _uowFactory;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWorkFactory uowFactory)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _uowFactory = uowFactory;
        }

        public async Task<Unit> AddUser(DTOUser user)
        {
            using (var uow = await _uowFactory.CreateAsync())
            {
                var toInsert = _mapper.Map<DTOUser, DomainUser>(user);
                _userRepository.InsertUser(toInsert);
                await uow.CommitChangesAsync();
                return Unit.Default;
            }
        }

        public async Task<Lst<DTOUser>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return new Lst<DTOUser>(_mapper.Map<IEnumerable<DomainUser>, IEnumerable<DTOUser>>(users));
        }

        public async Task<Lst<DTOUser>> GetUsers(Lst<int> ids)
        {
            var users = await _userRepository.GetUsers(ids);
            return new Lst<DTOUser>(_mapper.Map<IEnumerable<DomainUser>, IEnumerable<DTOUser>>(users));
        }
    }
}
