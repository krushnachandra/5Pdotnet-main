using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Data;
using Microsoft.AspNetCore.Cors;
using System.Threading;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using WebSocketSharp;

namespace _5paisaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FivepaisaAPIController : ControllerBase
    {
        private readonly JsonData _JsonData;
        private readonly string _MyKey, _OpenAPIURL, _LoginRequestMobileNewbyEmail,
            _NetPositionNetWise, _Holding, _OrderStatus, _TradeInformation, _OrderBook,
            _TradeBook, _Margin, _MarketFeed, _OrderRequest, _ModifyOrderRequest, _CancelOrderRequest,
            _SMOOrderRequest, _ModifySMOOrder, _OpenAPIFeedURL, _LoginCheck, _WbSocketURl,
            _History;

        public FivepaisaAPIController(IConfiguration _iConfig)//
        {
            var folderDetails = Path.Combine(Directory.GetCurrentDirectory(), "APICredentials.json");
            var JSON = System.IO.File.ReadAllText(folderDetails);

            _JsonData = JsonConvert.DeserializeObject<JsonData>(JSON);

            _OpenAPIURL = _iConfig.GetValue<string>("APIDetails:OpenAPIURL");

            _LoginRequestMobileNewbyEmail = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:LoginRequestMobileNewbyEmail");
            _NetPositionNetWise = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:NetPositionNetWise");
            _Holding = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:Holding");
            _OrderStatus = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:OrderStatus");
            _TradeInformation = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:TradeInformation");
            _OrderBook = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:OrderBook");
            _TradeBook = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:TradeBook");
            _Margin = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:Margin");
            _MarketFeed = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:MarketFeed");
            _OrderRequest = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:OrderRequest");
            _ModifyOrderRequest = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:ModifyOrderRequest");
            _CancelOrderRequest = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:CancelOrderRequest");

            _SMOOrderRequest = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:SMOOrderRequest");
            _ModifySMOOrder = _OpenAPIURL + _iConfig.GetValue<string>("APIDetails:SMOOrderRequest");

            _OpenAPIFeedURL = _iConfig.GetValue<string>("APIDetails:OpenAPIFeedURL");
            _LoginCheck = _OpenAPIFeedURL + _iConfig.GetValue<string>("APIDetails:LoginCheck");
            _WbSocketURl = _iConfig.GetValue<string>("APIDetails:WbSocketURl");
            _History = _iConfig.GetValue<string>("APIDetails:history");

        }

        [HttpGet]
        [Route("LoginRequestMobileNewbyEmail")]
        public ResponseModel LoginRequestMobileNewbyEmail()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                CommonMethod commonMethod = new CommonMethod(_JsonData.Key);

                _JsonData.Head.requestCode = _JsonData.RequestCode.LoginRequestMobileNewbyEmail;

                LoginRequestMobileNewbyEmailRequest Request = new LoginRequestMobileNewbyEmailRequest
                {
                    head = _JsonData.Head,
                    body = _JsonData.LoginRequestMobileNewbyEmail
                };

                string response = ApiRequest.SendApiRequest(_LoginRequestMobileNewbyEmail, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<LoginRequestMobileNewbyEmailResponse>>(response);

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("NetPositionNetWise")]
        public ResponseModel NetPositionNetWise()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.NetPositionNetWise;

                CommonReuqest Request = new CommonReuqest
                {
                    head = _JsonData.Head,
                    body = _JsonData.Common
                };

                string response = ApiRequest.SendApiRequestCookies(_NetPositionNetWise, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<NetPositionDetailResponse>>(response);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return objResponseModel;
        }

        [HttpGet]
        //[EnableCors("AllowOrigin")]
        [Route("Holding")]
        public ResponseModel Holding()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.Holding;

                CommonReuqest Request = new CommonReuqest
                {
                    head = _JsonData.Head,
                    body = _JsonData.Common
                };

                string response = ApiRequest.SendApiRequestCookies(_Holding, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<HoldingResponse>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("OrderStatus")]
        public ResponseModel OrderStatus()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.OrderStatus;
                _JsonData.OrderStatus.ClientCode = _JsonData.Common.ClientCode;

                OrderStatusRequest Request = new OrderStatusRequest
                {
                    head = _JsonData.Head,
                    body = _JsonData.OrderStatus
                };

                string response = ApiRequest.SendApiRequestCookies(_OrderStatus, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrdStatusResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("TradeInformation")]// Remaining the response
        public ResponseModel TradeInformation()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.TradeInformation;
                _JsonData.TradeInformation.ClientCode = _JsonData.Common.ClientCode;

                TradeInformationRequest Request = new TradeInformationRequest
                {
                    head = _JsonData.Head,
                    body = _JsonData.TradeInformation
                };

                string response = ApiRequest.SendApiRequestCookies(_TradeInformation, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<object>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("TradeBook")]
        public ResponseModel TradeBook()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.TradeBook;

                CommonReuqest Request = new CommonReuqest
                {
                    head = _JsonData.Head,
                    body = _JsonData.Common
                };

                string response = ApiRequest.SendApiRequestCookies(_TradeBook, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<TradeBookResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        [HttpGet]
        [Route("OrderBook")]
        public ResponseModel OrderBook()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.OrderBook;

                CommonReuqest Request = new CommonReuqest
                {
                    head = _JsonData.Head,
                    body = _JsonData.Common
                };

                string response = ApiRequest.SendApiRequestCookies(_OrderBook, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrderBookResponse>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("Margin")]
        public ResponseModel Margin()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.Margin;

                CommonReuqest Request = new CommonReuqest
                {
                    head = _JsonData.Head,
                    body = _JsonData.Common
                };

                string response = ApiRequest.SendApiRequestCookies(_Margin, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<MarginResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("MarketFeed")]
        public ResponseModel MarketFeed(int tabNumber = 0)
        {
            List<MarketFeedResponse> data = new List<MarketFeedResponse>();
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.MarketFeed;
                MarketFeedRequest Request = new MarketFeedRequest();
                Request.head = _JsonData.Head;

                if (tabNumber == 0)
                {
                    Request.body = _JsonData.MarketFeed;
                }
                if (tabNumber == 1)
                {
                    Request.body = _JsonData.MarketFeedTabOne;
                };
                if (tabNumber == 2)
                {
                    Request.body = _JsonData.MarketFeedTabTwo;
                };
                if (tabNumber == 3)
                {
                    Request.body = _JsonData.MarketFeedTabThree;
                };
                if (tabNumber == 4)
                {
                    Request.body = _JsonData.MarketFeedTabFour;
                };


                string response = ApiRequest.SendApiRequestCookies(_MarketFeed, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<MarketFeedResponse>>(response);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("OrderRequest")]
        public ResponseModel OrderRequest(int scripCode, double price, double perofchg, string orderType)
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
               //var scripData = prepareMonthlyExpiryFuturesSymbol("NTPC");
                //_JsonData.OrderRequest.ScripData = scripData;
                //_JsonData.OrderRequest.ExchangeType = "D";


                _JsonData.Head.requestCode = _JsonData.RequestCode.OrderRequest;
                _JsonData.OrderRequest.ClientCode = _JsonData.Common.ClientCode;
                _JsonData.OrderRequest.OrderRequesterCode = _JsonData.Common.ClientCode;
                _JsonData.OrderRequest.ExchangeType = "C";

                _JsonData.OrderRequest.ScripCode = scripCode;
                _JsonData.OrderRequest.OrderType = orderType;

                // price 
                _JsonData.OrderRequest.Qty = 10;
                //if ((price < 300) && (price < 500)) _JsonData.OrderRequest.Qty = 1000;
                //if ((price < 500) && (price < 750)) _JsonData.OrderRequest.Qty = 750;
                //if ((price < 750) && (price < 1000)) _JsonData.OrderRequest.Qty = 500;
                //if ((price < 1000) && (price < 1250)) _JsonData.OrderRequest.Qty = 400;
                //if ((price < 1250) && (price < 1500)) _JsonData.OrderRequest.Qty = 350;
                //if ((price < 1500) && (price < 1750)) _JsonData.OrderRequest.Qty = 300;
                //if ((price < 1750) && (price < 2000)) _JsonData.OrderRequest.Qty = 250;
                //if ((price < 2000) && (price < 2250)) _JsonData.OrderRequest.Qty = 200;
                //if ((price < 2250) && (price < 2500)) _JsonData.OrderRequest.Qty = 150;
                //if ((price < 2500) && (price < 3000)) _JsonData.OrderRequest.Qty = 100;


                if (orderType == "S")
                {
                    _JsonData.OrderRequest.Price = roundToNSEPrice(price + price * 0.5 / 100);
                }
                else
                {
                    _JsonData.OrderRequest.Price = roundToNSEPrice(price - price * 0.5 / 100);
                }


                // check the price which range based on that allocate quantity max allocation 20k

                // check the percentage minimum  5% increased in 10 mins

                // dont trade in same script before it comes to 15% raise

                // Do sell with .5% higher price

                OrderRequestData Request = new OrderRequestData
                {
                    head = _JsonData.Head,
                    body = _JsonData.OrderRequest
                };

                string response = ApiRequest.SendApiRequestCookies(_OrderRequest, JsonConvert.SerializeObject(Request));

                if (response != null)
                {

                    // once response is success place once more buy order
                    _JsonData.OrderRequest.IsStopLossOrder = true;
                    _JsonData.OrderRequest.AtMarket = true;

                    if (orderType == "S")
                    {
                        _JsonData.OrderRequest.OrderType = "B";
                        _JsonData.OrderRequest.StopLossPrice = (int)roundToNSEPrice(price + price * 2 / 100);
                        //_JsonData.OrderRequest.StopLossPrice = (int)(_JsonData.OrderRequest.Price - 1);

                    }
                    else
                    {
                        _JsonData.OrderRequest.OrderType = "S";
                        _JsonData.OrderRequest.StopLossPrice = (int)roundToNSEPrice(price - price * 2 / 100);
                        // _JsonData.OrderRequest.StopLossPrice = (int)(_JsonData.OrderRequest.Price + 1);

                    }

                    string response1 = ApiRequest.SendApiRequestCookies(_OrderRequest, JsonConvert.SerializeObject(Request));
                    objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrderRequestResponse>>(response1);
                    return objResponseModel;

                }

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrderRequestResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("ModifyOrderRequest")]
        public ResponseModel ModifyOrderRequest(double price, int qty, string exchOrderID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                //_JsonData.OrderRequest.ClientCode = _JsonData.Common.ClientCode;
                //_JsonData.OrderRequest.OrderRequesterCode = _JsonData.Common.ClientCode;
                // price 
                _JsonData.ModifyOrderRequest.Stoplossprice = roundToNSEPrice(price) - 1;
                _JsonData.ModifyOrderRequest.Price = 0;

                //_JsonData.ModifyOrderRequest.Price = roundToNSEPrice(price); ;

                _JsonData.ModifyOrderRequest.Qty = qty;
                _JsonData.ModifyOrderRequest.ExchOrderID = exchOrderID;

                ModifyOrderRequestData Request = new ModifyOrderRequestData
                {
                    head = _JsonData.Head,
                    body = _JsonData.ModifyOrderRequest
                };

                string response = ApiRequest.SendApiRequestCookies(_ModifyOrderRequest, JsonConvert.SerializeObject(Request));

                if (response != null)
                {

                    objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrderRequestResponse>>(response);

                    return objResponseModel;

                }


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("CancelOrderRequest")]
        public ResponseModel CancelOrderRequest(string exchOrderID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {

                _JsonData.CancelOrderRequest.ExchOrderID = exchOrderID;

                CancelOrderRequestData Request = new CancelOrderRequestData
                {
                    head = _JsonData.Head,
                    body = _JsonData.CancelOrderRequest
                };

                string response = ApiRequest.SendApiRequestCookies(_CancelOrderRequest, JsonConvert.SerializeObject(Request));

                if (response != null)
                {

                    objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrderRequestResponse>>(response);

                    return objResponseModel;

                }


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("StopLossOrderRequest")]
        public ResponseModel StopLossOrderRequest(int scripCode, double price, string orderType)
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.OrderRequest;
                _JsonData.OrderRequest.ClientCode = _JsonData.Common.ClientCode;
                _JsonData.OrderRequest.OrderRequesterCode = _JsonData.Common.ClientCode;
                _JsonData.OrderRequest.ScripCode = scripCode;
                _JsonData.OrderRequest.Price = roundToNSEPrice(price + price * 0.5 / 100);
                var stopLossprice = roundToNSEPrice(price + price * 3 / 100);
                _JsonData.OrderRequest.StopLossPrice = (int)stopLossprice;
                _JsonData.OrderRequest.Qty = 2;
                _JsonData.OrderRequest.IsStopLossOrder = true;
                _JsonData.OrderRequest.OrderType = orderType;




                // check the price which range based on that allocate quantity max allocation 20k

                // check the percentage minimum  5% increased in 10 mins

                // dont trade in same script before it comes to 15% raise

                // Do sell with .5% higher price

                OrderRequestData Request = new OrderRequestData
                {
                    head = _JsonData.Head,
                    body = _JsonData.OrderRequest
                };

                string response = ApiRequest.SendApiRequestCookies(_OrderRequest, JsonConvert.SerializeObject(Request));


                // once response is success place once more buy order

                _JsonData.OrderRequest.Price = roundToNSEPrice(price - price * 5 / 100);
                _JsonData.OrderRequest.OrderType = "B";

                string response1 = ApiRequest.SendApiRequestCookies(_OrderRequest, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<OrderRequestResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("SMOOrderRequest")]
        public ResponseModel SMOOrderRequest()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.SMOOrderRequest;
                _JsonData.SMOOrderRequest.ClientCode = _JsonData.Common.ClientCode;
                _JsonData.SMOOrderRequest.OrderRequesterCode = _JsonData.Common.ClientCode;

                SMOOrderRequestData Request = new SMOOrderRequestData
                {
                    head = _JsonData.Head,
                    body = _JsonData.SMOOrderRequest
                };

                string response = ApiRequest.SendApiRequestCookies(_SMOOrderRequest, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<SMOOrderRequestResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("ModifySMOOrder")]
        public ResponseModel ModifySMOOrder()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.Head.requestCode = _JsonData.RequestCode.ModifySMOOrder;
                _JsonData.ModifySMOOrder.ClientCode = _JsonData.Common.ClientCode;
                _JsonData.ModifySMOOrder.OrderRequesterCode = _JsonData.Common.ClientCode;

                ModifySMOOrderRequest Request = new ModifySMOOrderRequest
                {
                    head = _JsonData.Head,
                    body = _JsonData.ModifySMOOrder
                };

                string response = ApiRequest.SendApiRequestCookies(_ModifySMOOrder, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<ModifySMOOrderResponse>>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        [HttpGet]
        [Route("historical")]
        public ResponseModel historical()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                
                string response = ApiRequest.SendApiRequestHistory(_History, "", "Get");

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<HistoryResponse>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResponseModel;
        }


        [HttpGet]
        [Route("WebsocketAPi")]
        //[HttpGet("/ws")]
        public ResponseModel WebsocketAPi()
        {
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                _JsonData.LoginCheckhead.requestCode = _JsonData.RequestCode.LoginCheck;
                _JsonData.LoginCheckhead.LoginId = _JsonData.Common.ClientCode;
                _JsonData.LoginCheckBody.RegistrationID = ApiRequest.GetCookiesByName("JwtToken");


                LoginCheckRequest Request = new LoginCheckRequest
                {
                    head = _JsonData.LoginCheckhead,
                    body = _JsonData.LoginCheckBody
                };

                string response = ApiRequest.SendApiRequest(_LoginCheck, JsonConvert.SerializeObject(Request));

                objResponseModel.ResponseData = JsonConvert.DeserializeObject<Response<LoginCheckResponse>>(response);

                //Program.logRun.LogInformation("WebSocket: reuquestBody:" + JsonConvert.SerializeObject(_JsonData.SocketRequest));
                System.Diagnostics.Debug.WriteLine("WebSocket: reuquestBody:" + JsonConvert.SerializeObject(_JsonData.SocketRequest));

                //WebSocketSharp.WebSocket ws = new WebSocketSharp.WebSocket(_WbSocketURl + ApiRequest.GetCookiesByName("JwtToken") + "|" + _JsonData.Common.ClientCode);
                //ws.SetCookie(new WebSocketSharp.Net.Cookie(".ASPXAUTH", ApiRequest.GetCookiesByName("ASPXAUTH")));

                //ws.OnMessage += (sender, e) => _hub.Clients.All.SendAsync("transferchartdata", e.Data);

                //ws.Connect();
                //ws.Send(JsonConvert.SerializeObject(_JsonData.SocketRequest));

                WebsocketServer.Connect(_WbSocketURl + ApiRequest.GetCookiesByName("JwtToken") + "|" + _JsonData.Common.ClientCode,
                    ApiRequest.GetCookiesByName("ASPXAUTH"), JsonConvert.SerializeObject(_JsonData.SocketRequest));



                //return Ok(new { Message = "Request Completed" , });

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;

        }

        public double roundToNSEPrice(double price)
        {
            var x = Math.Round(price, 2) * 20;
            var y = Math.Ceiling(x);
            return y / 20;
        }

        public string prepareMonthlyExpiryFuturesSymbol(string inputSymbol)
        {

            DateTime today = DateTime.Today;
            DateTime lastDayOfCurrentMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            //int monthExpiery = endOfMonth.Day;

            //var lastDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
           
            if (lastDayOfCurrentMonth.DayOfWeek == DayOfWeek.Monday)
                lastDayOfCurrentMonth = lastDayOfCurrentMonth.AddDays(+3);
             if (lastDayOfCurrentMonth.DayOfWeek == DayOfWeek.Tuesday)
                lastDayOfCurrentMonth = lastDayOfCurrentMonth.AddDays(+2);
             if (lastDayOfCurrentMonth.DayOfWeek == DayOfWeek.Wednesday)
                lastDayOfCurrentMonth = lastDayOfCurrentMonth.AddDays(+1);
            if (lastDayOfCurrentMonth.DayOfWeek == DayOfWeek.Sunday)
                lastDayOfCurrentMonth = lastDayOfCurrentMonth.AddDays(-3);
             if (lastDayOfCurrentMonth.DayOfWeek == DayOfWeek.Saturday)
                lastDayOfCurrentMonth = lastDayOfCurrentMonth.AddDays(-2);


            // NIFTY 30 Sep 2021_20210930
            var date = lastDayOfCurrentMonth.ToString("dd");
            var month = lastDayOfCurrentMonth.ToString("MM");
            var year = DateTime.Now.Year;

            string futureSymbol = inputSymbol+ " " +  date + " MAY " + year + "_" + year + month + date;
            return futureSymbol;

        }
        
    }
}
