@echo off
echo Iniciando...

curl -X GET http://localhost:4746/MarketApi/total-market-cap
curl -X GET http://localhost:4746/MarketApi/fear-greed-index
curl -X GET http://localhost:4746/MarketApi/CMC100-index
curl -X GET http://localhost:4746/MarketApi/cryptos-trending
curl -X GET http://localhost:4746/MarketApi/stocks-trending
curl -X GET http://localhost:4746/MarketApi/stocks-gainers
curl -X GET http://localhost:4746/MarketApi/stocks-losers
curl -X GET http://localhost:4746/MarketApi/stocks-most-actives
curl -X GET http://localhost:4746/CryptoApi/cryptos
curl -X GET http://localhost:4746/StockApi/stocks

echo Base de datos actualizada.
pause