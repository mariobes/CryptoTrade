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

        public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters cryptoQueryParameters) 
        {
            var query = _context.Cryptos.AsQueryable();

            if (cryptoQueryParameters != null)
            {
                query = cryptoQueryParameters.SortBy switch
                {
                    EnumSortOptions.name => cryptoQueryParameters.Order == EnumOrderOptions.asc
                        ? query.OrderBy(c => c.Name)
                        : query.OrderByDescending(c => c.Name),

                    EnumSortOptions.marketCap => cryptoQueryParameters.Order == EnumOrderOptions.asc
                        ? query.OrderBy(c => c.MarketCap)
                        : query.OrderByDescending(c => c.MarketCap),

                    _ => cryptoQueryParameters.Order == EnumOrderOptions.asc
                        ? query.OrderBy(c => c.Value)
                        : query.OrderByDescending(c => c.Value),
                };
            }

            var result = query.ToList();
            return result;
        }

        public Crypto GetCrypto(int cryptoId)
        {
            var crypto = _context.Cryptos.FirstOrDefault(crypto => crypto.Id == cryptoId);
            return crypto;
        }

        public void UpdateCrypto(Crypto crypto)
        {
            _context.Entry(crypto).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteCrypto(int cryptoId) {
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