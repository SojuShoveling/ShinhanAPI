using AxGIEXPERTCONTROLLib;
using System.IO;

namespace ShinhanAPI
{
    public class Input
    {
        private readonly AxGiExpertControl Indi;

        public Input(AxGiExpertControl indi)
        {
            Indi = indi;
        }

        /// <summary>
        /// Indi 자동 로그인
        /// </summary>
        public bool StartIndi(string User, string UserPassword, string UserCertPassword)
        {
            return Indi.StartIndi(User, UserPassword, UserCertPassword,
                Path.Combine(new FileInfo(Program.GetProgramPath()).DirectoryName, "giexpertstarter.exe"));
        }

        /// <summary>
        /// RT 수신을 해제합니다
        /// </summary>
        public bool UnsubscribeRT(string type, string code)
        {
            return Indi.UnRequestRTReg(type, code);
        }

        /// <summary>
        /// RT 수신을 모두 해제합니다
        /// </summary>
        public bool UnsubscribeAllRT()
        {
            return Indi.UnRequestRTRegAll();
        }

        /// <summary>
        /// 주식 현재가
        /// </summary>
        public int SC(string code)
        {
            Indi.SetQueryName("SC");
            Indi.SetSingleData(0, code);
            return Indi.RequestData();
        }

        /// <summary>
        /// 주식 현재가 (실시간)
        /// </summary>
        public bool RTSC(string code)
        {
            return Indi.RequestRTReg("SC", code);
        }

        /// <summary>
        /// 현물 마스터
        /// </summary>
        public int SB(string code)
        {
            Indi.SetQueryName("SB");
            Indi.SetSingleData(0, code);
            return Indi.RequestData();
        }

        /// <summary>
        /// 국내 잔고, 체결 조회
        /// </summary>
        public int SABA200QB(string Account, string AccountPassword)
        {
            Indi.SetQueryName("SABA200QB");
            Indi.SetSingleData(0, Account);
            Indi.SetSingleData(1, "01");
            Indi.SetSingleData(2, AccountPassword);
            return Indi.RequestData();
        }

        /// <summary>
        /// 매수 가능 금액 조회
        /// </summary>
        public int SCDA601Q9(string Account, string AccountPassword)
        {
            Indi.SetQueryName("SCDA601Q9");
            Indi.SetSingleData(0, Account);
            Indi.SetSingleData(1, AccountPassword);
            return Indi.RequestData();
        }

        /// <summary>
        /// 계좌 목록 조회
        /// </summary>
        public int AccountList()
        {
            Indi.SetQueryName("AccountList");
            return Indi.RequestData();
        }

        /// <summary>
        /// 종목 리스트 조회
        /// </summary>
        public int Upjong_code_mst()
        {
            Indi.SetQueryName("upjong_code_mst");
            Indi.SetSingleData(0, "0001");
            return Indi.RequestData();
        }

        /// <summary>
        /// 상하한가 조회
        /// </summary>
        public int TR_1860(
            string naljja,
            string geolaelyang_jogeon = "0",
            string sanghahan_gubun = "1",
            string jongmog_jogeon = "1",
            string siga_chong_aeg = "0",
            string jang_gubun = "2")
        {
            Indi.SetQueryName("TR_1860");
            Indi.SetSingleData(0, jang_gubun);
            Indi.SetSingleData(1, sanghahan_gubun);
            Indi.SetSingleData(2, naljja);
            Indi.SetSingleData(3, geolaelyang_jogeon);
            Indi.SetSingleData(4, jongmog_jogeon);
            Indi.SetSingleData(5, siga_chong_aeg);
            return Indi.RequestData();
        }

        /// <summary>
        /// 시가 대비 상승율 조회
        /// </summary>
        public int TR_1862()
        {
            Indi.SetQueryName("TR_1862");
            return Indi.RequestData();
        }

        /// <summary>
        /// 주가 등락율 상하위 종목 조회
        /// </summary>
        public int TR_1863(
            string iblyeog_ilja,
            string deunglag_yul_sijag = "0",
            string deunglag_yul_jonglyo = "30",
            string geolaelyang_isang = "1000",
            string sanghawi_gubun = "0",
            string dang_il_yeobu = "1",
            string jang_gubun = "2",
            string siga_chong_aeg = "0",
            string jongmog_jogeon = "1")
        {
            Indi.SetQueryName("TR_1863");
            Indi.SetSingleData(0, jang_gubun);
            Indi.SetSingleData(1, sanghawi_gubun);
            Indi.SetSingleData(2, deunglag_yul_sijag);
            Indi.SetSingleData(3, deunglag_yul_jonglyo);
            Indi.SetSingleData(4, geolaelyang_isang);
            Indi.SetSingleData(5, dang_il_yeobu);
            Indi.SetSingleData(6, iblyeog_ilja);
            Indi.SetSingleData(7, siga_chong_aeg);
            Indi.SetSingleData(8, jongmog_jogeon);
            return Indi.RequestData();
        }

        /// <summary>
        /// 거래량 급 등락 종목 조회
        /// </summary>
        public int TR_1864(
            string jang_gubun = "2",
            string daebi_geubdeunglag_gubun = "1",
            string daebiyul = "1",
            string geolaelyang_jogeon = "1000",
            string jongmog_jogeon = "1",
            string siga_chong_aeg_jogeon = "0")
        {
            Indi.SetQueryName("TR_1864");
            Indi.SetSingleData(0, jang_gubun);
            Indi.SetSingleData(1, daebi_geubdeunglag_gubun);
            Indi.SetSingleData(2, daebiyul);
            Indi.SetSingleData(3, geolaelyang_jogeon);
            Indi.SetSingleData(4, jongmog_jogeon);
            Indi.SetSingleData(5, siga_chong_aeg_jogeon);
            return Indi.RequestData();
        }
    }
}