using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Consts
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月23日
    /// Description:常量类
    /// </summary>
    public static class Const
    {
        /// <summary>
        /// 命令字段的分割符
        /// </summary>
        public const string Command_Field_Separater_ReturnLine = "\r\n";
        /// <summary>
        /// 线程每次Sleep的时间
        /// </summary>
        public const int Thread_Per_Sleep_Time = 100;
        /// <summary>
        /// 权限键的拼接字符
        /// </summary>
        public const Char KEY_SPLIT = '/';

        /// <summary>
        /// 微软消息服务路径
        /// </summary>
        public const string MessageQueue_Send_Name = ".\\private$\\vcreditSend";

        /// <summary>
        /// 微软消息服务路径
        /// </summary>
        public const string MessageQueue_Receive_Name = ".\\private$\\vcreditReceive";

        /// <summary>
        /// 逗号
        /// </summary>
        public const Char CommaChar = ',';

        /// <summary>
        /// 空格
        /// </summary>
        public const Char Space = ' ';

        /// <summary>
        /// 顺序排序大写
        /// </summary>
        public const string SortAscUpper = "ASC";

        /// <summary>
        /// 倒序排序大写
        /// </summary>
        public const string SortDescUpper = "DESC";

        /// <summary>
        /// 行政区划
        /// </summary>
        public const String REGIONALISM = "REGIONALISM";
        /// <summary>
        /// 子公司，区域
        /// </summary>
        public const String SUBSIDIARY = "SUBSIDIARY";
        /// <summary>
        /// 部门
        /// </summary>
        public const String DEPART = "DEPARTMENT";

        #region 贷款产品种类枚举
        /// <summary>
        /// 容易贷
        /// </summary>
        public const String EnumRONGYIDAI = "LOANKIND/RONGYIDAI";
        /// <summary>
        /// 法人贷II
        /// </summary>
        public const String EnumFARENDAIER = "LOANKIND/FARENDAIER";
        /// <summary>
        /// 容易贷II
        /// </summary>
        public const String EnumRONGYIDAIER = "LOANKIND/RONGYIDAIER";
        /// <summary>
        /// 精英贷
        /// </summary>
        public const String EnumJINGYINGDAI = "LOANKIND/JINGYINGDAI";
        /// <summary>
        /// 非沪籍薪易贷
        /// </summary>
        public const String EnumFEIHUJIXINYIDAI = "LOANKIND/FEIHUJIXINYIDAI";
        /// <summary>
        /// 非沪籍容易贷
        /// </summary>
        public const String EnumFEIHUJIRONGYIDAI = "LOANKIND/FEIHUJIRONGYIDAI";
        /// <summary>
        /// 加贷
        /// </summary>
        public const String EnumJIADAI = "LOANKIND/JIADAI";
        /// <summary>
        /// 再贷
        /// </summary>
        public const String EnumZAIDAI = "LOANKIND/ZAIDAI";
        /// <summary>
        /// 企业主房易贷 
        /// </summary>
        public const String EnumQIYEZHUFANGYIDAI = "LOANKIND/QIYEZHUFANGYIDAI";
        /// <summary>
        /// 非杭籍薪易贷
        /// </summary>
        public const String EnumFEIHANGJIXINYIDAI = "LOANKIND/FEIHANGJIXINYIDAI";
        /// <summary>
        /// 非杭籍容易贷 
        /// </summary>
        public const String EnumFEIHANGJIRONGYIDAI = "LOANKIND/FEIHANGJIRONGYIDAI";
        /// <summary>
        /// 法人贷
        /// </summary>
        public const String EnumFARENDAI = "LOANKIND/FARENDAI";
        /// <summary>
        /// 非苏籍薪易贷
        /// </summary>
        public const String EnumFEISUJIXINYIDAI = "LOANKIND/FEISUJIXINYIDAI";
        /// <summary>
        /// 非成都籍薪易贷
        /// </summary>
        public const String EnumFEICHENDUJIXINYIDAI = "LOANKIND/FEICHENDUJIXINYIDAI";
        /// <summary>
        /// 薪易贷 
        /// </summary>
        public const String EnumXINYIDAI = "LOANKIND/XINYIDAI";
        /// <summary>
        /// 房易贷
        /// </summary>
        public const String EnumFANGYIDAI = "LOANKIND/FANGYIDAI";
        /// <summary>
        /// 房易贷II
        /// </summary>
        public const String EnumFANGYIDAIER = "LOANKIND/FANGYIDAIER";
        /// <summary>
        /// 装修贷
        /// </summary>
        public const String EnumZHUANGXIUDAI = "LOANKIND/ZHUANGXIUDAI";
        /// <summary>
        /// 消费贷
        /// </summary>
        public const String EnumXIAOFEIDAI = "LOANKIND/XIAOFEIDAI";
        /// <summary>
        /// 购车融资
        /// </summary>
        public const String EnumGOUCHERONGZI = "LOANKIND/GOUCHERONGZI"; //"PLEDGEENUM/VLEASEENUM/LOANKIND/GOUCHERONGZI";
        /// <summary>
        /// 购车融资(自有)
        /// </summary>
        public const String EnumGOUCHEDAIKUANZIYOU = "LOANKIND/GOUCHERONGZIZIYOU";
        /// <summary>
        /// 售后回租
        /// </summary>
        public const String EnumSHOUHOUHUIZU = "LOANKIND/SHOUHOUHUIZU";

        /// <summary>
        /// 原车融资
        /// </summary>
        public const String EnumYUANCHERONGZI = "LOANKIND/YUANCHERONGZI";

        /// <summary>
        /// 购车贷款
        /// </summary>
        public const String EnumGOUCHEDAIKUAN = "LOANKIND/GOUCHEDAIKUAN";


        /// <summary>
        /// 高薪贷
        /// </summary>
        public const String EnumGAOXINDAI = "LOANKIND/GAOXINDAI";


        /// <summary>
        /// 楼一贷
        /// </summary>
        public const String EnumLOUYIDAI = "LOANKIND/LOUYIDAI";

        /// <summary>
        /// 楼二贷
        /// </summary>
        public const String EnumLOUERDAI = "LOANKIND/LOUERDAI";

        /// <summary>
        /// REQ021 新楼一贷
        /// </summary>
        public const String EnumNEWLOUYIDAI = "LOANKIND/NEWLOUYIDAI";
        /// <summary>
        ///  沪牌贷
        /// </summary>
        public const String EnumHUPAIDAI = "LOANKIND/HUPAIDAI";

        /// <summary>
        /// 卡易贷1
        /// </summary>
        public const String EnumKAYIDAIYI = "LOANKIND/KAYIDAIYI";

        /// <summary>
        /// 卡易贷2
        /// </summary>
        public const String EnumKAYIDAIER = "LOANKIND/KAYIDAIER";

        /// <summary>
        /// 丽人贷
        /// </summary>
        public const String EnumLIRENDAI = "LOANKIND/LIRENDAI";

        /// <summary>
        /// 卡易贷
        /// </summary>
        public const String EnumKAYIDAI = "LOANKIND/KAYIDAI";
        #endregion

        #region 贷款产品种类权限
        /// <summary>
        /// 容易贷
        /// </summary>
        public const String PermRONGYIDAI = "LoanKindPer/RONGYIDAI";
        /// <summary>
        /// 法人贷II
        /// </summary>
        public const String PermFARENDAIER = "LoanKindPer/FARENDAIER";
        /// <summary>
        /// 容易贷II
        /// </summary>
        public const String PermRONGYIDAIER = "LoanKindPer/RONGYIDAIER";
        /// <summary>
        /// 精英贷
        /// </summary>
        public const String PermJINGYINGDAI = "LoanKindPer/JINGYINGDAI";
        /// <summary>
        /// 非沪籍薪易贷
        /// </summary>
        public const String PermFEIHUJIXINYIDAI = "LoanKindPer/FEIHUJIXINYIDAI";
        /// <summary>
        /// 非沪籍容易贷
        /// </summary>
        public const String PermFEIHUJIRONGYIDAI = "LoanKindPer/FEIHUJIRONGYIDAI";
        /// <summary>
        /// 加贷
        /// </summary>
        public const String PermJIADAI = "LoanKindPer/JIADAI";
        /// <summary>
        /// 再贷
        /// </summary>
        public const String PermZAIDAI = "LoanKindPer/ZAIDAI";
        /// <summary>
        /// 企业主房易贷 
        /// </summary>
        public const String PermQIYEZHUFANGYIDAI = "LoanKindPer/QIYEZHUFANGYIDAI";
        /// <summary>
        /// 非杭籍薪易贷
        /// </summary>
        public const String PermFEIHANGJIXINYIDAI = "LoanKindPer/FEIHANGJIXINYIDAI";
        /// <summary>
        /// 非杭籍容易贷 
        /// </summary>
        public const String PermFEIHANGJIRONGYIDAI = "LoanKindPer/FEIHANGJIRONGYIDAI";
        /// <summary>
        /// 法人贷
        /// </summary>
        public const String PermFARENDAI = "LoanKindPer/FARENDAI";
        /// <summary>
        /// 非苏籍薪易贷
        /// </summary>
        public const String PermFEISUJIXINYIDAI = "LoanKindPer/FEISUJIXINYIDAI";
        /// <summary>
        /// 非成都籍薪易贷
        /// </summary>
        public const String PermFEICHENDUJIXINYIDAI = "LoanKindPer/FEICHENDUJIXINYIDAI";
        /// <summary>
        /// 薪易贷 
        /// </summary>
        public const String PermXINYIDAI = "LoanKindPer/XINYIDAI";
        /// <summary>
        /// 房易贷
        /// </summary>
        public const String PermFANGYIDAI = "LoanKindPer/FANGYIDAI";
        /// <summary>
        /// 房易贷II
        /// </summary>
        public const String PermFANGYIDAIER = "LoanKindPer/FANGYIDAIER";
        /// <summary>
        /// 装修贷
        /// </summary>
        public const String PermZHUANGXIUDAI = "LoanKindPer/ZHUANGXIUDAI";
        /// <summary>
        /// 消费贷
        /// </summary>
        public const String PermXIAOFEIDAI = "LoanKindPer/XIAOFEIDAI";
        /// <summary>
        /// 购车融资
        /// </summary>
        public const String PermGOUCHERONGZI = "LoanKindPer/GOUCHERONGZI";
        /// <summary>
        /// 购车融资(自有)
        /// </summary>
        public const String PermGOUCHEDAIKUANZIYOU = "LoanKindPer/GOUCHERONGZIZIYOU";
        /// <summary>
        /// 售后回租
        /// </summary>
        public const String PermSHOUHOUHUIZU = "LoanKindPer/SHOUHOUHUIZU";

        public const String PermHUPAIDAI = "LoanKindPer/HUPAIDAI";

        /// <summary>
        /// 原车融资
        /// </summary>
        public const String PermYUANCHERONGZI = "LoanKindPer/YUANCHERONGZI";

        /// <summary>
        /// 购车贷款
        /// </summary>
        public const String PermGOUCHEDAIKUAN = "LoanKindPer/GOUCHEDAIKUAN";

        /// <summary>
        /// 楼一贷
        /// </summary>
        public const String PermLOUYIDAI = "LoanKindPer/LOUYIDAI";

        /// <summary>
        /// 楼二贷
        /// </summary>
        public const String PermLOUERDAI = "LoanKindPer/LOUERDAI";

        /// <summary>
        /// REQ021 新楼一贷
        /// </summary>
        public const String PermNEWLOUYIDAI = "LoanKindPer/NEWLOUYIDAI";
        /// <summary>
        /// 高薪贷
        /// </summary>
        public const String PermGAOXINDAI = "LoanKindPer/GAOXINDAI";
        /// <summary>
        /// 丽人贷
        /// </summary>
        public const String PermLIRENDAI = "LoanKindPer/LIRENDAI";
        /// <summary>
        /// 卡易贷
        /// </summary>
        public const String PermKAYIDAI = "LoanKindPer/KAYIDAI";

        #endregion

        #region(区域枚举)
        /// <summary>
        /// 上海
        /// </summary>
        public const String EnumShangHai = "SUBSIDIARY/SHANGHAI";
        /// <summary>
        /// 杭州
        /// </summary>
        public const String EnumHangZhou = "SUBSIDIARY/HANGZHOU";
        /// <summary>
        /// 成都
        /// </summary>
        public const String EnumChengDu = "SUBSIDIARY/CHENGDU";
        /// <summary>
        /// 苏州
        /// </summary>
        public const String EnumSuZhou = "SUBSIDIARY/SUZHOU";
        /// <summary>
        /// 无锡
        /// </summary>
        public const String EnumWUXI = "SUBSIDIARY/WUXI";
        /// <summary>
        /// 宁波
        /// </summary>
        public const String EnumNINGBO = "SUBSIDIARY/NINGBO";
        /// <summary>
        /// 南京
        /// </summary>
        public const String EnumNANJING = "SUBSIDIARY/NANJING";
        /// <summary>
        /// 绍兴
        /// </summary>
        public const String EnumSHAOXING = "SUBSIDIARY/HANGZHOU/SHAOXING";
        /// <summary>
        /// 重庆
        /// </summary>
        public const String EnumChongQing = "SUBSIDIARY/CHONGQING";
        /// <summary>
        /// 武汉
        /// </summary>
        public const String EnumWuHan = "SUBSIDIARY/WUHAN";
        /// <summary>
        /// 嘉兴
        /// </summary>
        public const String EnumJiaXing = "SUBSIDIARY/JIAXING";
        /// <summary>
        /// 合肥
        /// </summary>
        public const String EnumHeFei = "SUBSIDIARY/HEFEI";
        /// <summary>
        /// 青岛
        /// </summary>
        public const String EnumQingDao = "SUBSIDIARY/QINGDAO";
        /// <summary>
        /// 南通
        /// </summary>
        public const String EnumNanTong = "SUBSIDIARY/NANTONG";
        /// <summary>
        /// 西安
        /// </summary>
        public const String EnumXiAn = "SUBSIDIARY/XIAN";
        /// <summary>
        ///沈阳
        /// </summary>
        public const String EnumShenYang = "SUBSIDIARY/SHENYANG";
        /// <summary>
        ///广州
        /// </summary>
        public const String EnumGuangZhou = "SUBSIDIARY/GUANGZHOU";
        /// <summary>
        ///石家庄
        /// </summary>
        public const String EnumShiJiaZhuang = "SUBSIDIARY/SHIJIAZHUANG";
        /// <summary>
        ///厦门
        /// </summary>
        public const String EnumXiaMen = "SUBSIDIARY/XIAMEN";

        /// <summary>
        /// 长沙
        /// </summary>
        public const String EnumChangSha = "SUBSIDIARY/CHANGSHA";

        /// <summary>
        /// 郑州
        /// </summary>
        public const String EnumZhengZhou = "SUBSIDIARY/ZHENGZHOU";

        /// <summary>
        /// 烟台
        /// </summary>
        public const String EnumYanTai = "SUBSIDIARY/YANTAI";

        /// <summary>
        /// 镇江
        /// </summary>
        public const String EnumZhenJiang = "SUBSIDIARY/ZHENJIANG";

        /// <summary>
        /// 马鞍山
        /// </summary>
        public const String EnumMaAnShan = "SUBSIDIARY/MAANSHAN";

        /// <summary>
        /// 芜湖
        /// </summary>
        public const String EnumWuHu = "SUBSIDIARY/WUHU";

        /// <summary>
        /// 保定
        /// </summary>
        public const String EnumBaoDing = "SUBSIDIARY/BAODING";

        /// <summary>
        /// 鞍山
        /// </summary>
        public const String EnumAnShan = "SUBSIDIARY/ANSHAN";

        /// <summary>
        /// 金华
        /// </summary>
        public const String EnumJinHua = "SUBSIDIARY/JINHUA";

        /// <summary>
        /// 大连
        /// </summary>
        public const String EnumDaLian = "SUBSIDIARY/DALIAN";

        /// <summary>
        /// 佛山
        /// </summary>
        public const String EnumFoShan = "SUBSIDIARY/FOSHAN";

        /// <summary>
        /// 福州
        /// </summary>
        public const String EnumFuZhou = "SUBSIDIARY/FUZHOU";

        /// <summary>
        /// 电销
        /// </summary>
        public const String EnumPhoneMarketBranch = "PHONEMARKETBRANCH";
        #region 分店Key
        /// <summary>
        /// 徐汇
        /// </summary>
        public const String EnumXuHui = "SUBSIDIARY/SHANGHAI/XUHUI";
        /// <summary>
        /// 杨浦
        /// </summary>
        public const String EnumYangPu = "SUBSIDIARY/SHANGHAI/YANGPU";
        /// <summary>
        /// 普陀
        /// </summary>
        public const String EnumPuTuo = "SUBSIDIARY/SHANGHAI/PUTUO";
        /// <summary>
        /// 浦东
        /// </summary>
        public const String EnumPuDong = "SUBSIDIARY/SHANGHAI/PUDONG";
        /// <summary>
        /// 滨江
        /// </summary>
        public const String EnumBingJiang = "SUBSIDIARY/HANGZHOU/BINGJIANG";
        /// <summary>
        /// 上城
        /// </summary>
        public const String EnumShangCheng = "SUBSIDIARY/HANGZHOU/SHANGCHENG";
        /// <summary>
        /// 西湖
        /// </summary>
        public const String EnumXiHu = "SUBSIDIARY/HANGZHOU/XIHU";
        /// <summary>
        /// 南湖
        /// </summary>
        public const String EnumNanHu = "SUBSIDIARY/JIAXING/NANHU";
        /// <summary>
        /// 黄浦
        /// </summary>
        public const String EnumHuangPu = "SUBSIDIARY/SHANGHAI/HUANGPU";
        /// <summary>
        /// 闸北
        /// </summary>
        public const String EnumZhaBei = "SUBSIDIARY/SHANGHAI/ZHABEI";
        /// <summary>
        /// 园区
        /// </summary>
        public const String EnumYuanQu = "SUBSIDIARY/SUZHOU/YUANQU";
        /// <summary>
        /// 常熟
        /// </summary>
        public const String EnumChangshu = "SUBSIDIARY/SUZHOU/CHANGSHU";
        /// <summary>
        /// 涌金
        /// </summary>
        public const String EnumYongJin = "SUBSIDIARY/HANGZHOU/YONGJIN";
        /// <summary>
        /// 锦江
        /// </summary>
        public const String EnumJingJiang = "SUBSIDIARY/CHENGDU/JINGJIANG";
        /// <summary>
        /// 徐汇一部
        /// </summary>
        public const String EnumXuHuiYiBu = "SUBSIDIARY/SHANGHAI/XUHUIYIBU";
        /// <summary>
        /// 徐汇二部
        /// </summary>
        public const String EnumXuHuiErBu = "SUBSIDIARY/SHANGHAI/XUHUIERBU";
        /// <summary>
        /// 新区
        /// </summary>
        public const String EnumXinQu = "SUBSIDIARY/SUZHOU/XINQU";
        /// <summary>
        /// 武侯
        /// </summary>
        public const String EnumWuHou = "SUBSIDIARY/CHENGDU/WUHOU";
        /// <summary>
        /// 上南路
        /// </summary>
        public const String EnumShangNanLu = "SUBSIDIARY/SHANGHAI/SHANGNANROAD";
        /// <summary>
        /// 上南路一部
        /// </summary>
        public const String EnumShangNanLuYiBu = "SUBSIDIARY/SHANGHAI/SHANGNANLUYIBU";
        /// <summary>
        /// 上南路二部
        /// </summary>
        public const String EnumShangNanLuErBu = "SUBSIDIARY/SHANGHAI/SHANGNANLUERBU";
        /// <summary>
        /// 锦江一部
        /// </summary>
        public const String EnumJingJiangYiBu = "SUBSIDIARY/CHENGDU/JINGJIANGYIBU";
        /// <summary>
        /// 锦江二部
        /// </summary>
        public const String EnumJingJiangErBu = "SUBSIDIARY/CHENGDU/JINGJIANGERBU";
        /// <summary>
        /// 武侯一部
        /// </summary>
        public const String EnumWuHouYiBu = "SUBSIDIARY/CHENGDU/WUHOUYIBU";
        /// <summary>
        /// 武侯二部
        /// </summary>
        public const String EnumWuHouErBu = "SUBSIDIARY/CHENGDU/WUHOUERBU";
        /// <summary>
        /// 成华
        /// </summary>
        public const String EnumChengHua = "SUBSIDIARY/CHENGDU/CHENGHUA";
        /// <summary>
        /// 成华一部
        /// </summary>
        public const String EnumChengHuaYIBU = "SUBSIDIARY/CHENGDU/CHENGHUAYIBU";
        /// <summary>
        /// 成华二部
        /// </summary>
        public const String EnumChengHuaERBU = "SUBSIDIARY/CHENGDU/CHENGHUAERBU";
        /// <summary>
        /// 精英
        /// </summary>
        public const String EnumJINGYING = "SUBSIDIARY/SHANGHAI/JINGYING";
        /// <summary>
        /// 拱墅
        /// </summary>
        public const String EnumGONGSHU = "SUBSIDIARY/HANGZHOU/GONGSHU";
        /// <summary>
        /// 昆山
        /// </summary>
        public const String EnumKUNSHAN = "SUBSIDIARY/SUZHOU/KUNSHAN";
        /// <summary>
        /// 吴中
        /// </summary>
        public const String EnumWuZhong = "SUBSIDIARY/SUZHOU/WUZHONG";
        /// <summary>
        /// 青岛市南
        /// </summary>
        public const String EnumShiNan = "SUBSIDIARY/QINGDAO/SHINAN";
        /// <summary>
        /// 崇川
        /// </summary>
        public const String EnumChongChuan = "SUBSIDIARY/NANTONG/CHONGCHUAN";
        /// <summary>
        /// 天河
        /// </summary>
        public const String EnumTianHe = "SUBSIDIARY/GUANGZHOU/TIANHE";
        /// <summary>
        /// 越秀
        /// </summary>
        public const String EnumYueXiu = "SUBSIDIARY/GUANGZHOU/YUEXIU";

        //电销分公司枚举
        public const string PhoneMarketBranch = "PHONEMARKETBRANCH";
        public const string PhoneShangHai = "PHONEMARKETBRANCH/DIANXIAOSHANGHAI";
        public const string PhoneHangZhou = "PHONEMARKETBRANCH/DIANXIAOHANGZHOU";
        public const string PhoneSuZhou = "PHONEMARKETBRANCH/DIANXIAOSUZHOU";
        public const string PhoneChengDu = "PHONEMARKETBRANCH/DIANXIAOCHENGDU";
        public const string PhoneTeam = "PHONEMARKETTEAM";
        /// <summary>
        /// 上海客服部（现已废弃，对应历史数据）
        /// </summary>
        public const String EnumKEFU = "SUBSIDIARY/SHANGHAI/KEFU";

        /// <summary>
        /// 静安
        /// </summary>
        public const String EnumJINGAN = "SUBSIDIARY/SHANGHAI/JINGAN";
        /// <summary>
        /// 张家港
        /// </summary>
        public const String EnumZHANGJIAGANG = "SUBSIDIARY/SUZHOU/ZHANGJIAGANG";


        /// <summary>
        /// 崇安
        /// </summary>
        public const String EnumCHONGAN = "SUBSIDIARY/WUXI/CHONGAN";

        /// <summary>
        /// 海曙
        /// </summary>
        public const String EnumHAISHU = "SUBSIDIARY/NINGBO/HAISHU";

        /// <summary>
        /// 新街口
        /// </summary>
        public const String EnumXINJIEKOU = "SUBSIDIARY/NANJING/XINJIEKOU";

        /// <summary>
        /// 文昌
        /// </summary>
        public const String EnumWENCHANG = "SUBSIDIARY/NANJING/WENCHANG";

        /// <summary>
        /// 南长
        /// </summary>
        public const String EnumNANCHANG = "SUBSIDIARY/WUXI/NANCHANG";

        /// <summary>
        /// 锡山
        /// </summary>
        public const String EnumXISHAN = "SUBSIDIARY/WUXI/XISHAN";

        /// <summary>
        /// 曹杨
        /// </summary>
        public const String EnumCAOYANG = "SUBSIDIARY/SHANGHAI/CAOYANG";

        /// <summary>
        /// 天宁
        /// </summary>
        public const String EnumTIANNING = "SUBSIDIARY/WUXI/TIANNING";

        /// <summary>
        /// 建邺
        /// </summary>
        public const String EnumJIANYE = "SUBSIDIARY/NANJING/JIANYE";
        /// <summary>
        /// 江东
        /// </summary>
        public const String EnumJIANGDONG = "SUBSIDIARY/NINGBO/JIANGDONG";
        /// <summary>
        /// 大坪(现改名为渝中)
        /// </summary>
        public const String EnumDAPING = "SUBSIDIARY/CHONGQING/DAPING";
        /// <summary>
        /// 武昌
        /// </summary>
        public const String EnumWUCHANG = "SUBSIDIARY/WUHAN/WUCHANG";
        /// <summary>
        /// 武广(现改名为江汉)
        /// </summary>
        public const String EnumWUGUANG = "SUBSIDIARY/WUHAN/WUGUANG";
        /// <summary>
        /// 庐阳
        /// </summary>
        public const String EnumLuYang = "SUBSIDIARY/HEFEI/LUYANG";

        /// <summary>
        /// 滂江街
        /// </summary>
        public const String EnumPANGJIANGJIE = "SUBSIDIARY/SHENYANG/PANGJIANGJIE";

        /// <summary>
        /// 莲坂
        /// </summary>
        public const String EnumLIANBAN = "SUBSIDIARY/XIAMEN/LIANBAN";

        /// <summary>
        /// 中山东路
        /// </summary>
        public const String EnumZHONGSHANDONGLU = "SUBSIDIARY/SHIJIAZHUANG/ZHONGSHANDONGLU";

        /// <summary>
        /// 蜀山
        /// </summary>
        public const String EnumSHUSHAN = "SUBSIDIARY/HEFEI/SHUSHAN";

        /// <summary>
        /// 新城
        /// </summary>
        public const String EnumXINCHENG = "SUBSIDIARY/XIAN/XINCHENG";

        /// <summary>
        /// 绵阳
        /// </summary>
        public const String EnumMIANYANG = "SUBSIDIARY/MIANYANG";
        /// <summary>
        /// 涪城
        /// </summary>
        public const String EnumFUCHENG = "SUBSIDIARY/MIANYANG/FUCHENG";
        /// <summary>
        /// 武进
        /// </summary>
        public const String EnumWUJIN = "SUBSIDIARY/WUXI/WUJIN";
        /// <summary>
        /// 李沧
        /// </summary>
        public const String EnumLICANG = "SUBSIDIARY/QINGDAO/LICANG";

        /// <summary>
        /// 芙蓉
        /// </summary>
        public const String EnumFURONG = "SUBSIDIARY/CHANGSHA/FURONG";

        /// <summary>
        /// 金水
        /// </summary>
        public const String EnumJINSHUI = "SUBSIDIARY/ZHENGZHOU/JINSHUI";

        /// <summary>
        /// 芝罘
        /// </summary>
        public const String EnumZHIFU = "SUBSIDIARY/YANTAI/ZHIFU";

        /// <summary>
        /// 润州
        /// </summary>
        public const String EnumRUNZHOU = "SUBSIDIARY/ZHENJIANG/RUNZHOU";

        /// <summary>
        /// 鼓楼
        /// </summary>
        public const String EnumGULOU = "SUBSIDIARY/FUZHOU/GULOU";

        /// <summary>
        /// 江北
        /// </summary>
        public const String EnumJIANGBEI = "SUBSIDIARY/CHONGQING/JIANGBEI";

        /// <summary>
        /// 镜湖
        /// </summary>
        public const String EnumJINGHU = "SUBSIDIARY/WUHU/JINGHU";

        /// <summary>
        /// 花山
        /// </summary>
        public const String EnumHUASHAN = "SUBSIDIARY/MAANSHAN/HUASHAN";

        /// <summary>
        /// 新市
        /// </summary>
        public const String EnumXINSHI = "SUBSIDIARY/BAODING/XINSHI";

        /// <summary>
        /// 铁东
        /// </summary>
        public const String EnumTIEDONG = "SUBSIDIARY/ANSHAN/TIEDONG";

        /// <summary>
        /// 中山
        /// </summary>
        public const String EnumZHONGSHAN = "SUBSIDIARY/DALIAN/ZHONGSHAN";

        /// <summary>
        /// 婺城
        /// </summary>
        public const String EnumWUCHENG = "SUBSIDIARY/JINHUA/WUCHENG";

        /// <summary>
        /// 禅城
        /// </summary>
        public const String EnumCHANCHENG = "SUBSIDIARY/FOSHAN/CHANCHENG";

        /// <summary>
        /// 万州
        /// </summary>
        public const String EnumWANZHOU = "SUBSIDIARY/CHONGQING/WANZHOU";
        #endregion
        #endregion

        #region(区域权限)
        /// <summary>
        /// 上海
        /// </summary>
        public const String PermShangHia = "Region/ShangHai";
        /// <summary>
        /// 杭州
        /// </summary>
        public const String PermHangZhou = "Region/HangZhou";
        /// <summary>
        /// 成都
        /// </summary>
        public const String PermChengDu = "Region/ChengDu";
        /// <summary>
        /// 苏州
        /// </summary>
        public const String PermSuZhou = "Region/SuZhou";
        /// <summary>
        /// 无锡
        /// </summary>
        public const String PermWUXI = "Region/WUXI";
        /// <summary>
        /// 宁波
        /// </summary>
        public const String PermNINGBO = "Region/NINGBO";
        /// <summary>
        /// 南京
        /// </summary>
        public const String PermNANJING = "Region/NANJING";
        /// <summary>
        /// 绍兴
        /// </summary>
        public const String PermSHAOXING = "Region/SHAOXING";
        /// <summary>
        /// 重庆
        /// </summary>
        public const String PermChongQing = "Region/CHONGQING";
        /// <summary>
        /// 武汉
        /// </summary>
        public const String PermWuHan = "Region/WUHAN";
        /// <summary>
        /// 合肥
        /// </summary>
        public const String PermHeFei = "Region/HEFEI";
        /// <summary>
        /// 嘉兴
        /// </summary>
        public const String PermJiaXing = "Region/JIAXING";
        /// <summary>
        /// 青岛
        /// </summary>
        public const String PermQingDao = "Region/QINGDAO";
        /// <summary>
        /// 南通
        /// </summary>
        public const String PermNanTong = "Region/NANTONG";
        /// <summary>
        /// 西安
        /// </summary>
        public const String PermXiAn = "Region/XIAN";
        /// <summary>
        /// 沈阳
        /// </summary>
        public const String PermShenYang = "Region/SHENYANG";
        /// <summary>
        /// 广州
        /// </summary>
        public const String PermGuangZhou = "Region/GUANGZHOU";
        /// <summary>
        /// 石家庄
        /// </summary>
        public const String PermShiJiaZhuang = "Region/SHIJIAZHUANG";
        /// <summary>
        /// 厦门
        /// </summary>
        public const String PermXiaMen = "Region/XIAMEN";

        /// <summary>
        /// 长沙
        /// </summary>
        public const String PermChangSha = "Region/CHANGSHA";

        /// <summary>
        /// 郑州
        /// </summary>
        public const String PermZhengZhou = "Region/ZHENGZHOU";

        /// <summary>
        /// 烟台
        /// </summary>
        public const String PermYanTai = "Region/YANTAI";

        /// <summary>
        /// 镇江
        /// </summary>
        public const String PermZhenJiang = "Region/ZHENJIANG";

        /// <summary>
        /// 马鞍山
        /// </summary>
        public const String PermMaAnShan = "Region/MAANSHAN";

        /// <summary>
        /// 芜湖
        /// </summary>
        public const String PermWuHu = "Region/WUHU";

        /// <summary>
        /// 保定
        /// </summary>
        public const String PermBaoDing = "Region/BAODING";

        /// <summary>
        /// 鞍山
        /// </summary>
        public const String PermAnShan = "Region/ANSHAN";

        /// <summary>
        /// 佛山
        /// </summary>
        public const String PermFoShan = "Region/FOSHAN";

        /// <summary>
        /// 大连
        /// </summary>
        public const String PermDaLian = "Region/DALIAN";

        /// <summary>
        /// 金华
        /// </summary>
        public const String PermJinHua = "Region/JINHUA";

        /// <summary>
        /// 滂江街
        /// </summary>
        public const String PermPANGJIANGJIE = "Region/PANGJIANGJIE";

        /// <summary>
        /// 越秀
        /// </summary>
        public const String PermYueXiu = "Region/YUEXIU";

        /// <summary>
        /// 绵阳
        /// </summary>
        public const String PermMianYang = "Region/MianYang";

        /// <summary>
        /// 福州
        /// </summary>
        public const String PermFuZhou = "Region/FUZHOU";

        /// <summary>
        /// 涪城
        /// </summary>
        public const String PermFuCheng = "Region/FuCheng";

        /// <summary>
        /// 电销
        /// </summary>
        public const String PermPhoneMarketShanghai = "Region/PhoneMarketShangHai";
        public const String PermPhoneMarketchengdu = "Region/PhoneMarketChengDu";
        public const String PermPhoneMarketSuzhou = "Region/PhoneMarketSuZhou";
        public const String PermPhoneMarketHangzhou = "Region/PhoneMarketHangZhou";
        public const String PermPhoneMarketTeam = "Region/PhoneMarketTeam";
        /// <summary>
        /// 上海客服部（现已废弃，对应历史数据）
        /// </summary>
        public const String PermKEFU = "Region/SH_CustomerService";
        #region 分店权限
        /// <summary>
        /// 徐汇
        /// </summary>
        public const String PermXuHui = "Region/XUHUI";
        /// <summary>
        /// 杨浦
        /// </summary>
        public const String PermYangPu = "Region/YANGPU";
        /// <summary>
        /// 普陀
        /// </summary>
        public const String PermPuTuo = "Region/PUTUO";
        /// <summary>
        /// 浦东
        /// </summary>
        public const String PermPuDong = "Region/PUDONG";
        /// <summary>
        /// 滨江
        /// </summary>
        public const String PermBingJiang = "Region/BINGJIANG";
        /// <summary>
        /// 上城
        /// </summary>
        public const String PermShangCheng = "Region/SHANGCHENG";
        /// <summary>
        /// 西湖
        /// </summary>
        public const String PermXiHu = "Region/XIHU";
        /// <summary>
        /// 黄浦
        /// </summary>
        public const String PermHuangPu = "Region/HUANGPU";
        /// <summary>
        /// 闸北
        /// </summary>
        public const String PermZhaBei = "Region/ZHABEI";
        /// <summary>
        /// 园区
        /// </summary>
        public const String PermYuanQu = "Region/YUANQU";
        /// <summary>
        /// 园区
        /// </summary>
        public const String PermChangshu = "Region/CHANGSHU";
        /// <summary>
        /// 涌金
        /// </summary>
        public const String PermYongJin = "Region/YONGJIN";
        /// <summary>
        /// 锦江
        /// </summary>
        public const String PermJingJiang = "Region/JINGJIANG";
        /// <summary>
        /// 徐汇一部
        /// </summary>
        public const String PermXuHuiYiBu = "Region/XUHUIYIBU";
        /// <summary>
        /// 徐汇二部
        /// </summary>
        public const String PermXuHuiErBu = "Region/XUHUIERBU";
        /// <summary>
        /// 新区
        /// </summary>
        public const String PermXinQu = "Region/XINQU";
        /// <summary>
        /// 武侯
        /// </summary>
        public const String PermWuHou = "Region/WUHOU";
        /// <summary>
        /// 上南路
        /// </summary>
        public const String PermShangNanLu = "Region/SHANGNANROAD";
        /// <summary>
        /// 上南路一部
        /// </summary>
        public const String PermShangNanLuYiBu = "Region/SHANGNANLUYIBU";
        /// <summary>
        /// 上南路二部
        /// </summary>
        public const String PermShangNanLuErBu = "Region/SHANGNANLUERBU";
        /// <summary>
        /// 锦江一部
        /// </summary>
        public const String PermJingJiangYiBu = "Region/JINGJIANGYIBU";
        /// <summary>
        /// 锦江二部
        /// </summary>
        public const String PermJingJiangErBu = "Region/JINGJIANGERBU";
        /// <summary>
        /// 武侯一部
        /// </summary>
        public const String PermWuHouYiBu = "Region/WUHOUYIBU";
        /// <summary>
        /// 武侯二部
        /// </summary>
        public const String PermWuHouErBu = "Region/WUHOUERBU";
        /// <summary>
        /// 成华
        /// </summary>
        public const String PermChengHua = "Region/CHENGHUA";
        /// <summary>
        /// 成华一部
        /// </summary>
        public const String PermChengHuaYiBu = "Region/CHENGHUAYIBU";
        /// <summary>
        /// 成华二部
        /// </summary>
        public const String PermChengHuaErBu = "Region/CHENGHUAERBU";
        /// <summary>
        /// 精英
        /// </summary>
        public const String PermJINGYING = "Region/JINGYING";
        /// <summary>
        /// 拱墅
        /// </summary>
        public const String PermGONGSHU = "Region/GONGSHU";
        /// <summary>
        /// 昆山
        /// </summary>
        public const String PermKUNSHAN = "Region/KUNSHAN";
        /// <summary>
        /// 静安
        /// </summary>
        public const String PermJINGAN = "Region/JINGAN";
        /// <summary>
        /// 张家港
        /// </summary>
        public const String PermZHANGJIAGANG = "Region/ZHANGJIAGANG";


        /// <summary>
        /// 崇安
        /// </summary>
        public const String PermCHONGAN = "Region/CHONGAN";

        /// <summary>
        /// 海曙
        /// </summary>
        public const String PermHAISHU = "Region/HAISHU";

        /// <summary>
        /// 新街口
        /// </summary>
        public const String PermXINJIEKOU = "Region/XINJIEKOU";

        /// <summary>
        /// 南长
        /// </summary>
        public const String PermNANCHANG = "Region/NANCHANG";

        /// <summary>
        /// 曹杨
        /// </summary>
        public const String PermCAOYANG = "Region/CAOYANG";

        /// <summary>
        /// 天宁
        /// </summary>
        public const String PermTIANNING = "Region/TIANNING";
        /// <summary>
        /// 建邺
        /// </summary>
        public const String PermJIANYE = "Region/JIANYE";
        /// <summary>
        /// 江东
        /// </summary>
        public const String PermJIANGDONG = "Region/JIANGDONG";
        /// <summary>
        /// 大坪(现改名为渝中)
        /// </summary>
        public const String PermDAPING = "Region/DAPING";
        /// <summary>
        /// 武昌
        /// </summary>
        public const String PermWUCHANG = "Region/WUCHANG";
        /// <summary>
        /// 庐阳
        /// </summary>
        public const String PermLuYang = "Region/LUYANG";
        /// <summary>
        /// 文昌
        /// </summary>
        public const String PermWenChang = "Region/WENCHANG";
        /// <summary>
        /// 锡山
        /// </summary>
        public const String PermXiShan = "Region/XISHAN";/// <summary>
        /// 南湖
        /// </summary>
        public const String PermNanHu = "Region/NANHU";
        /// 吴中
        /// </summary>
        public const String PermWuZhong = "Region/WUZHONG";
        /// 市南
        /// </summary>
        public const String PermShiNan = "Region/SHINAN";
        /// 崇川
        /// </summary>
        public const String PermChongChuan = "Region/CHONGCHUAN";
        /// 天河
        /// </summary>
        public const String PermTianHe = "Region/TIANHE";

        /// <summary>
        /// 莲坂
        /// </summary>
        public const String PermLianBan = "Region/LIANBAN";

        /// <summary>
        /// 中山东路
        /// </summary>
        public const String PermZhongShanDongLu = "Region/ZHONGSHANDONGLU";

        /// <summary>
        /// 蜀山
        /// </summary>
        public const String PermShuShan = "Region/SHUSHAN";

        /// <summary>
        /// 新城
        /// </summary>
        public const String PermXinCheng = "Region/XINCHENG";

        /// <summary>
        /// 武广(现改名为江汉)
        /// </summary>
        public const String PermWuGuang = "Region/WUGUANG";
        /// <summary>
        /// 武进
        /// </summary>
        public const String PermWuJin = "Region/WuJin";
        /// <summary>
        /// 李沧
        /// </summary>
        public const String PermLiCang = "Region/LiCang";

        /// <summary>
        /// 芙蓉
        /// </summary>
        public const String PermFuRong = "Region/FURONG";

        /// <summary>
        /// 金水
        /// </summary>
        public const String PermJinShui = "Region/JINSHUI";

        /// <summary>
        /// 芝罘
        /// </summary>
        public const String PermZhiFu = "Region/ZHIFU";

        /// <summary>
        /// 润州
        /// </summary>
        public const String PermRunZhou = "Region/RUNZHOU";

        /// <summary>
        /// 鼓楼
        /// </summary>
        public const String PermGuLou = "Region/GULOU";

        /// <summary>
        /// 江北
        /// </summary>
        public const String PermJiangBei = "Region/JiangBei";

        /// <summary>
        /// 镜湖
        /// </summary>
        public const String PermJingHu = "Region/JINGHU";

        /// <summary>
        /// 花山
        /// </summary>
        public const String PermHuaShan = "Region/HUASHAN";

        /// <summary>
        /// 新市
        /// </summary>
        public const String PermXinShi = "Region/XINSHI";

        /// <summary>
        /// 铁东
        /// </summary>
        public const String PermTieDong = "Region/TIEDONG";

        /// <summary>
        /// 中山
        /// </summary>
        public const String PermZhongShan = "Region/ZHONGSHAN";

        /// <summary>
        /// 婺城
        /// </summary>
        public const String PermWucheng = "Region/WUCHENG";

        /// <summary>
        /// 万州
        /// </summary>
        public const String PermWanZhou = "Region/WANZHOU";

        /// <summary>
        /// 禅城
        /// </summary>
        public const String PermChanCheng = "Region/CHANCHENG";
        #endregion
        #endregion

        #region 放贷方常量

        /// <summary>
        /// 渤海信托
        /// </summary>
        public const string COMPANY_BHXT_LENDING = "COMPANY/BHXT_LENDING";
        /// <summary>
        /// 对外经贸
        /// </summary>
        public const string COMPANY_DWJM_LENDING = "COMPANY/DWJM_LENDING";
        /// <summary>
        /// 成都维仕
        /// </summary>
        public const string COMPANY_WX_CDWS_LENDING = "COMPANY/WX_CDWS_LENDING";
        /// <summary>
        /// 苏州维信融资租赁
        /// </summary>
        public const string COMPANY_WX_SZWX_LEASE = "COMPANY/WX_SZWX_LEASE";
        /// <summary>
        /// 苏州维信融资租赁(上海)
        /// </summary>
        public const string COMPANY_WX_SZWX_LEASE_SH = "COMPANY/WX_SZWX_LEASE_SH";
        /// <summary>
        /// 工商银行
        /// </summary>
        public const string COMPANY_GONGSHANGYINHANG = "COMPANY/GONGSHANGYINHANG";
        /// <summary>
        /// 东亚银行
        /// </summary>
        public const string COMPANY_DONGYAYINHANG = "COMPANY/DONGYAYINGHANG";

        #endregion

        #region 服务方常量

        /// <summary>
        /// 维视投资咨询（上海）有限公司
        /// </summary>
        public const string COMPANY_WX_SHWS_SERVICE = "COMPANY/WX_SHWS_SERVICE";
        /// <summary>
        /// 上海维信商务咨询有限公司
        /// </summary>
        public const string COMPANY_WX_SHWX_SERVICE = "COMPANY/WX_SHWX_SERVICE";
        /// <summary>
        /// 杭州维仕金融服务有限公司
        /// </summary>
        public const string COMPANY_WX_HZWS_SERVICE = "COMPANY/WX_HZWS_SERVICE";
        /// <summary>
        /// 维信融资租赁（苏州）有限公司无锡分公司
        /// </summary>
        public const string COMPANY_WX_SZWX_LEASE_WX = "COMPANY/WX_SZWX_LEASE_WX";
        /// <summary>
        /// 维信融资租赁（苏州）有限公司南京分公司
        /// </summary>
        public const string COMPANY_WX_SZWX_LEASE_NJ = "COMPANY/WX_SZWX_LEASE_NJ";
        /// <summary>
        /// 维信融资租赁（苏州）有限公司常州分公司
        /// </summary>
        public const string COMPANY_WX_SZWX_LEASE_CZ = "COMPANY/WX_SZWX_LEASE_CZ";
        /// <summary>
        /// 杭州维仕金融服务有限公司宁波分公司
        /// </summary>
        public const string COMPANY_WX_HZWS_SERVICE_NB = "COMPANY/WX_HZWS_SERVICE_NB";
        /// <summary>
        /// 杭州维仕金融服务有限公司绍兴分公司
        /// </summary>
        public const string COMPANY_WX_HZWS_SERVICE_SX = "COMPANY/WX_HZWS_SERVICE_SX";
        /// <summary>
        /// 杭州维仕金融服务有限公司重庆分公司
        /// </summary>
        public const string COMPANY_WX_HZWS_SERVICE_CQ = "COMPANY/WX_HZWS_SERVICE_CQ";
        /// <summary>
        /// 杭州维仕金融服务有限公司武汉分公司
        /// </summary>
        public const string COMPANY_WX_HZWS_SERVICE_WH = "COMPANY/WX_HZWS_SERVICE_WH";
        /// <summary>
        /// 杭州维仕金融服务有限公司合肥分公司
        /// </summary>
        public const string COMPANY_WX_HZWS_SERVICE_HF = "COMPANY/WX_HZWS_SERVICE_HF";
        /// <summary>
        /// 未知的服务方Key.在Console项目中看到
        /// </summary>
        public const string COMPANY_WX_SZWX_SERVICE = "COMPANY/WX_SZWX_SERVICE";

        #endregion

        #region 担保方常量

        /// <summary>
        /// 维仕担保有限公司
        /// </summary>
        public const string COMPANY_WX_HZWS_GUARANTEE = "COMPANY/WX_HZWS_GUARANTEE";

        #endregion

        #region 枚举Key常量

        /// <summary>
        /// 委外状态
        /// </summary>
        public const string ENUM_EXTERNALDUNSTATUS = "EXTERNALDUNSTATUS";
        /// <summary>
        /// 委外厂商
        /// </summary>
        public const string ENUM_EXTERNALDUNCOMPANY = "EXTERNALDUNCOMPANY";
        /// <summary>
        /// 高风险状态
        /// </summary>
        public const string ENUM_HIGHRISKSTATUS = "HIGHRISKSTATUS";
        /// <summary>
        /// 委外收回原因
        /// </summary>
        public const string ENUM_EXTERNALDUNBACKREASON = "EXTERNALDUNBACKREASON";
        /// <summary>
        /// 委外收回原因-其他
        /// </summary>
        public const string ENUM_ENUM_EXTERNALDUNBACKREASON_OTHER = "EXTERNALDUNBACKREASON/OTHER";

        #endregion

        #region 存储过程名称常量

        /// <summary>
        /// 查询委外催收订单的SP
        /// 使用页面
        /// 1.ExternalDun.aspx
        /// </summary>
        public const string SP_QUERYEXTERNALDUNBUSINESS = "QueryExternalDunBusiness";
        /// <summary>
        /// 委外操作的SP
        /// 使用页面
        /// 1.ExternalDun.aspx
        /// </summary>
        public const string SP_EXTERNALDUNOPERATION = "ExternalDunOperation";
        /// <summary>
        /// 导入委外数据的SP
        /// 使用页面
        /// 1.ExternalDun.aspx
        /// </summary>
        public const string SP_IMPORTEXTERNALDUN = "ImportExternalDun";

        /// <summary>
        /// 查询待生成催收函件订单的SP
        /// 使用页面
        /// 1.DunLetterGeneration.aspx
        /// </summary>
        public const string SP_DUN_QUERYBUSINESSDUNLETTER = "proc_BillDun_QueryBusinessDunLetter";

        #endregion

        #region ProductKind常量
        /// <summary>
        /// 车贷
        /// </summary>
        public const string PRODUCTKIND_CHEDAI = "PRODUCTKIND/CHEDAI";
        /// <summary>
        /// 无抵押贷款
        /// </summary>
        public const string PRODUCTKIND_WUDIYADAIKUAN = "PRODUCTKIND/WUDIYADAIKUAN";
        /// <summary>
        /// 房贷
        /// </summary>
        public const string PRODUCTKIND_FANGDAI = "PRODUCTKIND/FANGDAI";
        #endregion

        /// <summary>
        /// 预交单申请表
        /// </summary>
        public const string YUJIAODANSHENQINGBIAO = @"DOCUMENTKIND/YUJIAODANSHENQINGBIAO";

        /// <summary>
        /// 移动端预提交标记
        /// </summary>
        public const string YIDONGDUAN = @"CUSTOMERSOURCE/YIDONGDUAN";

        /// <summary>
        /// 申请阶段申请资料
        /// </summary>
        public const string SHENQINGZILIAO = @"DOCUMENTKIND/SHENQINGZILIAO";

        /// <summary>
        /// 客户性质
        /// </summary>
        public const string CUSTOMERTYPE = "CUSTOMERTYPE";

        /// <summary>
        /// 房产地段
        /// </summary>
        public const string HOUSESECTOR = "HOUSESECTOR";

        /// <summary>
        /// 公司
        /// </summary>
        public const string COMPANY = "COMPANY";

        /// <summary>
        /// 订单类型
        /// </summary>
        public const string LOANKIND = "LOANKIND";

        /// <summary>
        /// 贷款产品类型
        /// </summary>
        public const string PRODUCTKIND = "PRODUCTKIND";

        /// <summary>
        /// 银行列表
        /// </summary>
        public const string BANKLIST = "BANKLIST";
        /// <summary>
        /// 订单状态
        /// </summary>
        public const string BUSINESSSTATUS = "BUSINESSSTATUS";
        /// <summary>
        /// 清贷状态
        /// </summary>
        public const string CLOANSTATUS = "CLOANSTATUS";
        /// <summary>
        /// 帐单状态
        /// </summary>
        public const string BILLSTATUS = "BILLSTATUS";
        /// <summary>
        /// 诉讼执行状态
        /// </summary>
        public const string LAWSUITSTATUS = "LAWSUITSTATUS";
        /// <summary>
        /// 销售模式
        /// </summary>
        public const string SALEMODE = "SALEMODE";
        /// <summary>
        /// 工商注册类型
        /// </summary>
        public const string ENTREGIST = "ENTREGIST";
        /// <summary>
        /// 18位合同号适用地区
        /// </summary>
        public const string CODE_FITAREA = "CONTRACTCODE/FITAREA";
        /// <summary>
        /// 分公司
        /// </summary>
        public const string SUBCOMPANY = "SUBCOMPANY";
    }
}
