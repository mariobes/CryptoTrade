@echo off
echo Iniciando...

curl -X GET http://localhost:4746/MarketApi/total-market-cap & echo.
curl -X GET http://localhost:4746/MarketApi/fear-greed-index & echo.
curl -X GET http://localhost:4746/MarketApi/CMC100-index & echo.
curl -X GET http://localhost:4746/MarketApi/cryptos-trending & echo.
curl -X GET http://localhost:4746/MarketApi/stocks-trending & echo.
curl -X GET http://localhost:4746/MarketApi/stocks-gainers & echo.
curl -X GET http://localhost:4746/MarketApi/stocks-losers & echo.
curl -X GET http://localhost:4746/MarketApi/stocks-most-actives & echo.
curl -X GET http://localhost:4746/CryptoApi/cryptos & echo.
curl -X GET http://localhost:4746/StockApi/stocks & echo.
curl -X POST http://localhost:4746/Transactions/update-users-balances & echo.

echo Base de datos actualizada.
pause