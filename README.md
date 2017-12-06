Bittrex API Bot
======

Currently supported functionality
------
Public calls
- GetTicker() 
- GetCurrencies()
- GetMarkets()
- GetMarketSummary()

Private calls
- GetBalance()
- BuyLimit()
- SellLimit()
- OrderHistory()

TODO
------
- /account/orderhistory -> Quantity * PricePerUnit = price at which we bought the coin 
- Current worth of the coin - TxFee = price which the coin is worth right now
- Currentprice - Buyprice == $$$profit$$$
- Check coincmarketcap API for most stable and profitable coins to buy
