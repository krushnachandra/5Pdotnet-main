//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace _5paisaAPI
//{
//    public static class Utils
//    {

        

        
//        public static object roundOff(double price)
//        {
//            // Round off to 2 decimal places
//            return Math.Round(price, 2);
//        }

        
//        public static object roundToNSEPrice(double price)
//        {
//            var x = Math.Round(price, 2) * 20;
//            var y = Math.Ceiling(x);
//            return y / 20;
//        }

       
      

//        //public static object calculateTradePnl(object trade)
//        //{
//        //    if (trade.tradeState == TradeState.ACTIVE)
//        //    {
//        //        if (trade.cmp > 0)
//        //        {
//        //            if (trade.direction == Direction.LONG)
//        //            {
//        //                trade.pnl = Utils.roundOff(trade.filledQty * (trade.cmp - trade.entry));
//        //            }
//        //            else
//        //            {
//        //                trade.pnl = Utils.roundOff(trade.filledQty * (trade.entry - trade.cmp));
//        //            }
//        //        }
//        //    }
//        //    else if (trade.exit > 0)
//        //    {
//        //        if (trade.direction == Direction.LONG)
//        //        {
//        //            trade.pnl = Utils.roundOff(trade.filledQty * (trade.exit - trade.entry));
//        //        }
//        //        else
//        //        {
//        //            trade.pnl = Utils.roundOff(trade.filledQty * (trade.entry - trade.exit));
//        //        }
//        //    }
//        //    var tradeValue = trade.entry * trade.filledQty;
//        //    if (tradeValue > 0)
//        //    {
//        //        trade.pnlPercentage = Utils.roundOff(trade.pnl * 100 / tradeValue);
//        //    }
//        //    return trade;
//        //}

        
//        public static object prepareMonthlyExpiryFuturesSymbol(object inputSymbol)
//        {
//            var expiryDateTime = Utils.getMonthlyExpiryDayDate();
//            var expiryDateMarketEndTime = Utils.getMarketEndTime(expiryDateTime);
//            var now = DateTime.Now;;
//            if (now > expiryDateMarketEndTime)
//            {
//                // increasing today date by 20 days to get some day in next month passing to getMonthlyExpiryDayDate()
//                expiryDateTime = Utils.getMonthlyExpiryDayDate(now + timedelta(days: 20));
//            }
//            var year2Digits = expiryDateTime.year.ToString()[2];
//            var monthShort = calendar.month_name[expiryDateTime.month].upper()[0::3];
//            var futureSymbol = inputSymbol + year2Digits + monthShort + "FUT";
//            logging.info("prepareMonthlyExpiryFuturesSymbol[%s] = %s", inputSymbol, futureSymbol);
//            return futureSymbol;
//        }

        
//        public static object prepareWeeklyOptionsSymbol(object inputSymbol, object strike, object optionType, object numWeeksPlus = 0)
//        {
//            var expiryDateTime = Utils.getWeeklyExpiryDayDate();
//            var todayMarketStartTime = Utils.getMarketStartTime();
//            var expiryDayMarketEndTime = Utils.getMarketEndTime(expiryDateTime);
//            if (numWeeksPlus > 0)
//            {
//                expiryDateTime = expiryDateTime + timedelta(days: numWeeksPlus * 7);
//                expiryDateTime = Utils.getWeeklyExpiryDayDate(expiryDateTime);
//            }
//            if (todayMarketStartTime > expiryDayMarketEndTime)
//            {
//                expiryDateTime = expiryDateTime + timedelta(days: 6);
//                expiryDateTime = Utils.getWeeklyExpiryDayDate(expiryDateTime);
//            }
//            // Check if monthly and weekly expiry same
//            var expiryDateTimeMonthly = Utils.getMonthlyExpiryDayDate();
//            var weekAndMonthExpriySame = false;
//            if (expiryDateTime == expiryDateTimeMonthly)
//            {
//                weekAndMonthExpriySame = true;
//                logging.info("Weekly and Monthly expiry is same for %s", expiryDateTime);
//            }
//            var year2Digits = expiryDateTime.year.ToString()[2];
//            object optionSymbol = null;
//            if (weekAndMonthExpriySame == true)
//            {
//                var monthShort = calendar.month_name[expiryDateTime.month].upper()[0::3];
//                optionSymbol = inputSymbol + year2Digits.ToString() + monthShort + strike.ToString() + optionType.upper();
//            }
//            else
//            {
//                var m = expiryDateTime.month;
//                var d = expiryDateTime.day;
//                var mStr = m.ToString();
//                if (m == 10)
//                {
//                    mStr = "O";
//                }
//                else if (m == 11)
//                {
//                    mStr = "N";
//                }
//                else if (m == 12)
//                {
//                    mStr = "D";
//                }
//                var dStr = d < 10 ? "0" + d.ToString() : d.ToString();
//                optionSymbol = inputSymbol + year2Digits.ToString() + mStr + dStr + strike.ToString() + optionType.upper();
//            }
//            logging.info("prepareWeeklyOptionsSymbol[%s, %d, %s, %d] = %s", inputSymbol, strike, optionType, numWeeksPlus, optionSymbol);
//            return optionSymbol;
//        }

        
//        public static object getMonthlyExpiryDayDate(object datetimeObj = null)
//        {
//            if (datetimeObj == null)
//            {
//                datetimeObj = DateTime.Now;;
//            }
//            var year = datetimeObj.year;
//            var month = datetimeObj.month;
//            var lastDay = calendar.monthrange(year, month)[1];
//            var datetimeExpiryDay = datetime(year, month, lastDay);
//            while (calendar.day_name[datetimeExpiryDay.weekday()] != "Thursday")
//            {
//                datetimeExpiryDay = datetimeExpiryDay - timedelta(days: 1);
//            }
//            while (Utils.isHoliday(datetimeExpiryDay) == true)
//            {
//                datetimeExpiryDay = datetimeExpiryDay - timedelta(days: 1);
//            }
//            datetimeExpiryDay = Utils.getTimeOfDay(0, 0, 0, datetimeExpiryDay);
//            return datetimeExpiryDay;
//        }
//        private DateTime GetLastFridayOfTheMonth(DateTime date)
//        {
//            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

//            while (lastDayOfMonth.DayOfWeek != DayOfWeek.Friday)
//                lastDayOfMonth = lastDayOfMonth.AddDays(-1);

//            return lastDayOfMonth;
//        }

//        public static object getWeeklyExpiryDayDate(object dateTimeObj = null)
//        {
//            if (dateTimeObj == null)
//            {
//                dateTimeObj = DateTime.Now;;
//            }
//            var daysToAdd = 0;
//            if (dateTimeObj.weekday() >= 3)
//            {
//                daysToAdd = -1 * (dateTimeObj.weekday() - 3);
//            }
//            else
//            {
//                daysToAdd = 3 - dateTimeObj.weekday();
//            }
//            var datetimeExpiryDay = dateTimeObj + timedelta(days: daysToAdd);
//            while (Utils.isHoliday(datetimeExpiryDay) == true)
//            {
//                datetimeExpiryDay = datetimeExpiryDay - timedelta(days: 1);
//            }
//            datetimeExpiryDay = Utils.getTimeOfDay(0, 0, 0, datetimeExpiryDay);
//            return datetimeExpiryDay;
//        }

        
//        public static object isTodayWeeklyExpiryDay()
//        {
//            var expiryDate = Utils.getWeeklyExpiryDayDate();
//            var todayDate = Utils.getTimeOfToDay(0, 0, 0);
//            if (expiryDate == todayDate)
//            {
//                return true;
//            }
//            return false;
//        }

        
//        public static object isTodayOneDayBeforeWeeklyExpiryDay()
//        {
//            var expiryDate = Utils.getWeeklyExpiryDayDate();
//            var todayDate = Utils.getTimeOfToDay(0, 0, 0);
//            if (expiryDate - timedelta(days: 1) == todayDate)
//            {
//                return true;
//            }
//            return false;
//        }

        
//        public static object getNearestStrikePrice(double price, object nearestMultiple = 50)
//        {
//            var inputPrice = Convert.ToInt32(price);
//            var remainder = Convert.ToInt32(inputPrice % nearestMultiple);
//            if (remainder < Convert.ToInt32(nearestMultiple / 2))
//            {
//                return inputPrice - remainder;
//            }
//            else
//            {
//                return inputPrice + (nearestMultiple - remainder);
//            }
//        }
//    }
//}
