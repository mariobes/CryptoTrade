using CryptoTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Data
{
    public class CryptoEFRepository : ICryptoRepository
    {
        private readonly CryptoTradeContext _context;

        public CryptoEFRepository(CryptoTradeContext context)
        {
            _context = context;
        }

        public void AddCrypto(Crypto crypto)
        {
            _context.Cryptos.Add(crypto);
            SaveChanges();
        }

        public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters dto) 
        {
            var query = _context.Cryptos.AsQueryable();

            if (dto != null)
            {
                query = dto.SortBy switch
                {
                    EnumSortOptions.name => dto.Order == EnumOrderOptions.asc
                        ? query.OrderBy(c => c.Name)
                        : query.OrderByDescending(c => c.Name),

                    _ => dto.Order == EnumOrderOptions.asc
                        ? query.OrderBy(c => c.MarketCap)
                        : query.OrderByDescending(c => c.MarketCap),
                };
            }

            var result = query.ToList();
            return result;
        }

        public Crypto GetCrypto(string cryptoId)
        {
            var crypto = _context.Cryptos.FirstOrDefault(c => c.Id == cryptoId);
            return crypto;
        }

        public void UpdateCrypto(Crypto crypto)
        {
            _context.Entry(crypto).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteCrypto(string cryptoId) {
            var crypto = GetCrypto(cryptoId);
            _context.Cryptos.Remove(crypto);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
 
    }   
}