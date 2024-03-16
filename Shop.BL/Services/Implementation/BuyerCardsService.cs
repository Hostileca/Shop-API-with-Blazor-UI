using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.BL.Dtos.BuyerCard;
using Shop.BL.Dtos.User;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.BL.Services.Implementation
{
    public class BuyerCardsService : IBuyerCardsService
    {
        private readonly IBuyerCardsRepo _buyerCardsRepo;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public BuyerCardsService(IBuyerCardsRepo buyerCardsRepo, UserManager<User> userManager, IMapper mapper)
        {
            _buyerCardsRepo = buyerCardsRepo;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<BuyerCardReadDto> BindBuyerCard(string userName, BindCardDto bindCardDto)
        {
            var card = await _buyerCardsRepo.GetBuyerCardByPhoneNumber(bindCardDto.PhoneNumber);
            if (card == null)
            {
                throw new KeyNotFoundException($"Card not found with phone number:{bindCardDto.PhoneNumber}");
            }
            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                existingUser.BuyerCard = card;
            }
            await _buyerCardsRepo.SaveChanges();
            return _mapper.Map<BuyerCardReadDto>(card);
        }

        public async Task<BuyerCardReadDto> CreateBuyerCard(BuyerCardCreateDto buyerCardCreateDto)
        {
            var buyerCard = _mapper.Map<BuyerCard>(buyerCardCreateDto);
            buyerCard.RegistrationDate = DateTime.Now;
            await _buyerCardsRepo.AddBuyerCard(buyerCard);
            await _buyerCardsRepo.SaveChanges();
            return _mapper.Map<BuyerCardReadDto>(buyerCard); ;
        }

        public async Task<BuyerCardReadDto> DeleteBuyerCard(int id)
        {
            var buyerCardModelFromRepo = await _buyerCardsRepo.GetBuyerCardById(id);
            if (buyerCardModelFromRepo is null)
            {
                throw new KeyNotFoundException($"Buyer card not found with id:{id}");
            }
            _buyerCardsRepo.DeleteBuyerCard(buyerCardModelFromRepo);
            await _buyerCardsRepo.SaveChanges();
            return _mapper.Map<BuyerCardReadDto>(buyerCardModelFromRepo);
        }

        public async Task<IEnumerable<BuyerCardReadDto>> GetAllBuyerCards()
        {
            var buyerCards = await _buyerCardsRepo.GetAllBuyerCards();
            return _mapper.Map<IEnumerable<BuyerCardReadDto>>(buyerCards); ;
        }

        public async Task<BuyerCardReadDto> GetBuyerCardById(int id)
        {
            var buyerCard = await _buyerCardsRepo.GetBuyerCardById(id);
            if (buyerCard == null)
            {
                throw new KeyNotFoundException($"Buyer card not found with id:{id}");
            }
            return _mapper.Map<BuyerCardReadDto>(buyerCard);
        }

        public async Task<BuyerCardReadDto> GetBuyerCardByUsername(string userName)
        {
            var buyerCard = await _buyerCardsRepo.GetBuyerCardByUsername(userName);
            if (buyerCard == null)
            {
                throw new KeyNotFoundException($"Buyer card not found with username:{userName}");
            }
            return _mapper.Map<BuyerCardReadDto>(buyerCard);
        }

        public async Task<BuyerCardReadDto> UnbindCard(string userName)
        {
            var buyerCard = await _buyerCardsRepo.GetBuyerCardByUsername(userName);
            if (buyerCard == null)
            {
                throw new KeyNotFoundException($"Buyer card not found with username:{userName}");
            }
            buyerCard.User = null;
            await _buyerCardsRepo.SaveChanges();
            return _mapper.Map<BuyerCardReadDto>(buyerCard);
        }
    }
}
