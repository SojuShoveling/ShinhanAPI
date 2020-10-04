namespace ShinhanAPI.Object
{
    /// <summary>
    /// 잔고 종목
    /// </summary>
    public class SABA200QB
    {
        public string 종목코드 { get; set; }
        public string 종목명 { get; set; }
        public string 결제일잔고수량 { get; set; }
        public string 매도미체결수량 { get; set; }
        public string 매수미체결수량 { get; set; }
        public string 현재가 { get; set; }
        public string 평균단가 { get; set; }
        public string 신용잔고수량 { get; set; }
        public string 코스피대용지정수량 { get; set; }
    }
}