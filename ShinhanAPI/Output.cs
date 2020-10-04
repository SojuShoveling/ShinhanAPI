using AxGIEXPERTCONTROLLib;
using System;
using System.Collections.Generic;

namespace ShinhanAPI
{
    public class Output
    {
        /// <summary>
        /// 변환 예외
        /// </summary>
        public event ConvertExceptionReceived ConvertExceptionReceived;

        /// <summary>
        /// 실시간, 변환 예외
        /// </summary>
        public event RTConvertExceptionReceived RTConvertExceptionReceived;

        /// <summary>
        /// 오류 반환
        /// </summary>
        public event ErrorReceived ErrorReceived;

        /// <summary>
        /// 계좌 리스트 조회
        /// </summary>
        public event AccountListReceived AccountListReceived;

        /// <summary>
        /// 업종 리스트 조회
        /// </summary>
        public event UpjongListReceived UpjongListReceived;

        /// <summary>
        /// 현재가 조회
        /// </summary>
        public event SCReceived SCReceived;

        /// <summary>
        /// 실시간 현재가 조회
        /// </summary>
        public event RTSCReceived RTSCReceived;

        /// <summary>
        /// 잔고 및 주문 체결 조회
        /// </summary>
        public event SABA200QBReceived SABA200QBReceived;

        /// <summary>
        /// 매수 가능 금액 조회
        /// </summary>
        public event SCDA601Q9Received SCDA601Q9Received;

        /// <summary>
        /// 상한가 하한가 종목 조회
        /// </summary>
        public event TR_1860Received TR_1860Received;

        /// <summary>
        /// 현물 마스터
        /// </summary>
        public event SBReceived SBReceived;

        private readonly AxGiExpertControl Indi;

        public Output(AxGiExpertControl indi)
        {
            Indi = indi;

            indi.ReceiveData += Indi_ReceiveData;
            indi.ReceiveRTData += Indi_ReceiveRTData;
        }

        private void Indi_ReceiveData(object sender, _DGiExpertControlEvents_ReceiveDataEvent e)
        {
            short ec = Indi.GetErrorState();
            string name = (string)Indi.GetQueryName();

            if (ec > 0)
            {
                ErrorReceived?.Invoke(ec, name, (string)Indi.GetErrorMessage());
                return;
            }

            try
            {
                ConvertReceive(name, false);
            }
            catch (Exception ex)
            {
                ConvertExceptionReceived?.Invoke(name, ex);
            }

            Indi.ClearReceiveBuffer();
        }

        private void Indi_ReceiveRTData(object sender, _DGiExpertControlEvents_ReceiveRTDataEvent e)
        {
            string name = (string)Indi.GetQueryName();

            try
            {
                ConvertReceive(name, true);
            }
            catch (Exception ex)
            {
                RTConvertExceptionReceived?.Invoke(name, ex);
            }

            Indi.ClearReceiveBuffer();
        }

        private void ConvertReceive(string name, bool IsRT)
        {
            #region :(

            //EventInfo[] eventInfos = GetType().GetEvents(BindingFlags.Public);

            //for (int i = 0; i < eventInfos.Length; i++)
            //{
            //    MethodInfo methodInfo = eventInfos[i].EventHandlerType.GetMethod("Invoke");

            //    if (methodInfo == null)
            //        continue;

            //    ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            //    if (parameterInfos.Length <= 0)
            //        continue;

            //    Type type = parameterInfos[0].ParameterType;

            //    if(type.IsGenericType && type.GetInterfaces().Length >= 1 && type.GetGenericTypeDefinition() == typeof(List<>))
            //    {
            //        methodInfo.Invoke(this, new object[] { Object.ObjAry.SetMultiValue<type>(Indi) });
            //    }
            //    if (type.Name == name)
            //        methodInfo.Invoke(this, new object[] { Object.ObjAry.SetSingleValue<type>(Indi) });
            //}

            #endregion :(

            #region :)

            for (int i = 0; i < Abjects.Length; i++)
            {
                if (Abjects[i].Name != name)
                    continue;

                Type type;

                if (string.IsNullOrWhiteSpace(Abjects[i].Class))
                    type = Object.ObjAry.ToClass(name);
                else
                    type = Object.ObjAry.ToClass(Abjects[i].Class);

                if (type == null)
                    return;

                object obj;

                if (Abjects[i].IsList)
                    obj = Object.ObjAry.SetMultiValue(type, Indi);
                else
                    obj = Object.ObjAry.SetSingleValue(type, Indi);

                if (obj == null)
                    return;

                string fname = (IsRT ? "RT" : null) + type.Name;

                InvokeEvent(fname, obj);
            }

            #endregion :)
        }

        private void InvokeEvent(string name, object obj)
        {
            switch (name)
            {
                case "AccountList":
                    AccountListReceived?.Invoke((List<Object.Account>)obj);
                    break;

                case "Upjong":
                    UpjongListReceived?.Invoke((List<Object.Upjong>)obj);
                    break;

                case "SC":
                    SCReceived?.Invoke((Object.SC)obj);
                    break;

                case "RTSC":
                    RTSCReceived?.Invoke((Object.SC)obj);
                    break;

                case "SABA200QB":
                    SABA200QBReceived?.Invoke((List<Object.SABA200QB>)obj);
                    break;

                case "SCDA601Q9":
                    SCDA601Q9Received?.Invoke((Object.SCDA601Q9)obj);
                    break;

                case "TR_1860":
                    TR_1860Received?.Invoke((List<Object.TR_1860>)obj);
                    break;

                case "SB":
                    SBReceived?.Invoke((Object.SB)obj);
                    break;
            }
        }

        #region :)

        private static readonly Abject[] Abjects = new Abject[]
        {
            new Abject()
            {
                Name = "AccountList",
                Class = "Account",
                IsList = true
            },
            new Abject()
            {
                Name = "upjong_code_mst",
                Class = "Upjong",
                IsList = true
            },
            new Abject()
            {
                Name = "SABA200QB",
                IsList = true
            },
            new Abject()
            {
                Name = "TR_1860",
                IsList = true
            },
            new Abject() { Name = "SC" },
            new Abject() { Name = "SB" },
            new Abject() { Name = "SCDA601Q9" }
        };

        private class Abject
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public bool IsList { get; set; }
        }

        #endregion :)
    }

    /// <summary>
    /// 변환 예외
    /// </summary>
    public delegate void ConvertExceptionReceived(string requestName, Exception ex);

    /// <summary>
    /// 실시간, 변환 예외
    /// </summary>
    public delegate void RTConvertExceptionReceived(string requestName, Exception ex);

    /// <summary>
    /// 오류 반환
    /// </summary>
    public delegate void ErrorReceived(int code, string requestName, string message);

    /// <summary>
    /// 계좌 리스트 조회
    /// </summary>
    public delegate void AccountListReceived(List<Object.Account> accounts);

    /// <summary>
    /// 업종 리스트 조회
    /// </summary>
    public delegate void UpjongListReceived(List<Object.Upjong> upjongs);

    /// <summary>
    /// 현재가 조회
    /// </summary>
    public delegate void SCReceived(Object.SC sc);

    /// <summary>
    /// 실시간 현재가 조회
    /// </summary>
    public delegate void RTSCReceived(Object.SC sc);

    /// <summary>
    /// 잔고 및 주문 체결 조회
    /// </summary>
    public delegate void SABA200QBReceived(List<Object.SABA200QB> sABA200QBs);

    /// <summary>
    /// 매수 가능 금액 조회
    /// </summary>
    public delegate void SCDA601Q9Received(Object.SCDA601Q9 sCDA601Q9);

    /// <summary>
    /// 상한가 하한가 종목 조회
    /// </summary>
    public delegate void TR_1860Received(List<Object.TR_1860> tR_1860);

    /// <summary>
    /// SB
    /// </summary>
    public delegate void SBReceived(Object.SB sb);
}