namespace ShinhanAPI.Object
{
    /// <summary>
    /// 상한가 하한가 종목 조회
    /// </summary>
    public class TR_1860
    {
        public string 단축코드 { get; set; }
        public string 한글종목명 { get; set; }
        public string 해당일종가 { get; set; }
        public string 현재가 { get; set; }
        public string 전일대비구분 { get; set; }
        public string 전일대비 { get; set; }
        public string 전일대비율 { get; set; }
        public string 종가_전일대비구분 { get; set; }
        public string 종가_전일대비 { get; set; }
        public string 종가_전일대비율 { get; set; }
        public string 연속일 { get; set; }
        public string 거래강도 { get; set; }
        public string 누적거래량 { get; set; }
        public string 업종구분 { get; set; }
        public string 매도총호가수량 { get; set; }
        public string 매수총호가수량 { get; set; }
        public string 매도1호가 { get; set; }
        public string 매수1호가 { get; set; }
        public string 매도1호가수량 { get; set; }
        public string 매수1호가수량 { get; set; }
        public string 시가총액 { get; set; }
        public string 체결강도 { get; set; }
    }
}